using System;
using System.Collections.Generic;
using System.Linq;

using Asteroids.OpenGL.Sprites;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids.OpenGL;

public class MainGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _background;
    private Texture2D _ship;
    private Texture2D _bullet;
    private Texture2D _smallAsteroid;
    private Texture2D _mediumAsteroid;
    private Texture2D _largeAsteroid;
    private SpriteFont _font;
    private SoundEffect _shoot;
    private SoundEffect _gameOver;
    private SoundEffect _hit;

    private List<Sprite> _sprites = [];
    private bool _hasStarted = false;
    private int _score = 0;
    private int _timer = 0;
    private Random _random = new();
    private int _playCount = 0;

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this);

        Window.Title = Globals.WindowTitle;
        Content.RootDirectory = Globals.RootDirectory;
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = Globals.WindowWidth;
        _graphics.PreferredBackBufferHeight = Globals.WindowHeight;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _background = Content.Load<Texture2D>("background");
        _ship = Content.Load<Texture2D>("ship");
        _bullet = Content.Load<Texture2D>("bullet");
        _smallAsteroid = Content.Load<Texture2D>("asteroid_small");
        _mediumAsteroid = Content.Load<Texture2D>("asteroid_medium");
        _largeAsteroid = Content.Load<Texture2D>("asteroid_large");
        _font = Content.Load<SpriteFont>("pixel_font");
        _shoot = Content.Load<SoundEffect>("shoot");
        _gameOver = Content.Load<SoundEffect>("game_over");
        _hit = Content.Load<SoundEffect>("hit");
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Space) && !_hasStarted)
        {
            Start();
            _hasStarted = true;
        }

        if (_hasStarted)
        {
            for (var i = 0; i < _sprites.Count; i++)
            {
                _sprites[i].Update(gameTime, _sprites);
            }

            CheckCollision();

            _timer++;
            if (_timer >= 30)
            {
                SpawnAsteroid();
                _timer = 0;
            }

            Clean();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_background, new Rectangle(0, 0, Globals.WindowWidth, Globals.WindowHeight), Color.White);

        if (_hasStarted)
        {
            _spriteBatch.DrawString(_font, $"SCORE: {_score}", new Vector2(5, 5), Color.White);

            for (var i = 0; i < _sprites.Count; i++)
            {
                _sprites[i].Draw(_spriteBatch);
            }
        }
        else
        {
            var viewport = _graphics.GraphicsDevice.Viewport;

            if (_playCount > 0)
            {
                var output1 = "GAME OVER!";
                var output2 = "PRESS [SPACE] TO RESTART";

                var position1 = new Vector2(viewport.Width / 2, (viewport.Height / 2) - 16);
                var position2 = new Vector2(viewport.Width / 2, (viewport.Height / 2) + 16);

                var origin1 = _font.MeasureString(output1) / 2;
                var origin2 = _font.MeasureString(output2) / 2;

                _spriteBatch.DrawString(_font, output1, position1, Color.White, 0, origin1, 1.5f, SpriteEffects.None, 0);
                _spriteBatch.DrawString(_font, output2, position2, Color.White, 0, origin2, 1.5f, SpriteEffects.None, 0);
            }
            else
            {
                var output = "PRESS [SPACE] TO START";
                var position = new Vector2(viewport.Width / 2, viewport.Height / 2);
                var origin = _font.MeasureString(output) / 2;
                _spriteBatch.DrawString(_font, output, position, Color.White, 0, origin, 1.5f, SpriteEffects.None, 0);
            }
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void Clean()
    {
        for (var i = _sprites.Count - 1; i >= 0; i--)
        {
            if (_sprites[i].IsRemoved)
            {
                _sprites.RemoveAt(i);
            }
        }
    }

    private void Start()
    {
        _playCount++;

        _sprites.Add(new Ship(_ship, _shoot)
        {
            Position = new Vector2((Globals.WindowWidth / 2) - (_ship.Width * Globals.ShipScale / 2), (Globals.WindowHeight / 2) - (_ship.Height * Globals.ShipScale / 2)),
            LinearVelocity = Globals.ShipLinearVelocity,
            RotationVelocity = Globals.ShipRotationVelocity,
            Scale = Globals.ShipScale,
            Bullet = new Bullet(_bullet)
        });
    }

    private void Restart()
    {
        _sprites.Clear();
        _score = 0;
        _hasStarted = false;
    }

    private void SpawnAsteroid(Asteroid parent = null)
    {
        var size = _random.Next(0, 3);
        var angle = _random.NextDouble() * Math.PI;
        var direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

        var x = _random.Next(-400, Globals.WindowWidth + 400);
        var y = _random.Next(-400, Globals.WindowHeight + 400);

        if (x > 0 && x <= Globals.WindowWidth)
        {
            if (y > 0 && y <= Globals.WindowHeight)
            {
                y = -400;
            }
        }

        var position = parent is null ? 
            new Vector2(x, y) : 
            new Vector2(parent.Position.X, parent.Position.Y);

        var texture = parent is null ?
            size switch
            {
                1 => _largeAsteroid,
                2 => _mediumAsteroid,
                _ => _smallAsteroid
            } :
            parent.Type == AsteroidType.Large ? _mediumAsteroid : _smallAsteroid;

        var linearVelocity = parent is null ?
            size switch
            {
                1 => Globals.LargeAsteroidLinearVelocity,
                2 => Globals.MediumAsteroidLinearVelocity,
                _ => Globals.SmallAsteroidLinearVelocity
            } :
            parent.Type == AsteroidType.Large ? Globals.MediumAsteroidLinearVelocity : Globals.SmallAsteroidLinearVelocity;

        var rotationVelocity = parent is null ?
            size switch
            {
                1 => Globals.LargeAsteroidRotationVelocity,
                2 => Globals.MediumAsteroidRotationVelocity,
                _ => Globals.SmallAsteroidRotationVelocity
            } :
            parent.Type == AsteroidType.Large ? Globals.MediumAsteroidRotationVelocity : Globals.SmallAsteroidRotationVelocity;

        var scale = parent is null ?
            size switch
            {
                1 => Globals.LargeAsteroidScale,
                2 => Globals.MediumAsteroidScale,
                _ => Globals.SmallAsteroidScale
            } :
            parent.Type == AsteroidType.Large ? Globals.MediumAsteroidScale : Globals.SmallAsteroidScale;

        var type = parent is null ?
            size switch
            {
                1 => AsteroidType.Large,
                2 => AsteroidType.Medium,
                _ => AsteroidType.Small
            } :
            parent.Type == AsteroidType.Large ? AsteroidType.Medium : AsteroidType.Small;

        _sprites.Add(new Asteroid(texture)
        {
            Direction = direction,
            Position = position,
            LinearVelocity = linearVelocity,
            RotationVelocity = rotationVelocity,
            Scale = scale,
            LifeSpan = Globals.AsteroidLifeSpan,
            Type = type
        });
    }

    private void CheckCollision()
    {
        var ship = _sprites.Where(x => x is Ship).First();
        var bullets = _sprites.Where(x => x is Bullet).Cast<Bullet>().ToList();
        var asteroids = _sprites.Where(x => x is Asteroid).Cast<Asteroid>().ToList();

        var shipWidth = _ship.Width * Globals.ShipScale;
        var shipHeight = _ship.Height * Globals.ShipScale;
        var shipRadius = ((shipWidth / 2) + (shipHeight / 2)) / 2;

        for (var i = asteroids.Count - 1; i >= 0; i--)
        {
            var texture = asteroids[i].Type switch
            {
                AsteroidType.Large => _largeAsteroid,
                AsteroidType.Medium => _mediumAsteroid,
                _ => _smallAsteroid
            };

            var scale = asteroids[i].Type switch
            {
                AsteroidType.Large => Globals.LargeAsteroidScale,
                AsteroidType.Medium => Globals.MediumAsteroidScale,
                _ => Globals.SmallAsteroidScale
            };

            var asteroidWidth = texture.Width * scale;
            var asteroidHeight = texture.Height * scale;
            var asteroidRadius = ((asteroidWidth / 2) + (asteroidHeight / 2)) / 2;

            var shipDistance = Vector2.Distance(ship.Position, asteroids[i].Position);

            if (shipDistance <= (shipRadius + asteroidRadius))
            {
                var instance = _gameOver.CreateInstance();
                instance.Volume = 0.1f;
                instance.Play();
                Restart();
            }

            for (var j = bullets.Count - 1; j >= 0; j--)
            {
                var bulletWidth = _bullet.Width * Globals.BulletScale;
                var bulletHeight = _bullet.Height * Globals.BulletScale;
                var bulletRadius = ((bulletWidth / 2) + (bulletHeight / 2)) / 2;

                var bulletDistance = Vector2.Distance(bullets[j].Position, asteroids[i].Position);

                if (bulletDistance <= (bulletRadius + asteroidRadius))
                {
                    _ = _sprites.Remove(asteroids[i]);
                    _ = _sprites.Remove(bullets[j]);

                    _score += 5;

                    var instance = _hit.CreateInstance();
                    instance.Volume = 0.1f;
                    instance.Play();

                    if (asteroids[i].Type != AsteroidType.Small)
                    {
                        SpawnAsteroid(asteroids[i]);
                        SpawnAsteroid(asteroids[i]);
                    }
                }
            }
        }
    }
}