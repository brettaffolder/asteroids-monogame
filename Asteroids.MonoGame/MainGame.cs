using Asteroids.MonoGame.Sprites;

namespace Asteroids.MonoGame;

public class MainGame : Game
{
    private readonly int _windowWidth = 1280;
    private readonly int _windowHeight = 720;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<Sprite> _sprites;
    private Texture2D _background;
    private Texture2D _ship;
    private Texture2D _largeAsteroid;
    private Texture2D _mediumAsteroid;
    private Texture2D _smallAsteroid;

    private Random _random = new();
    private int _timer;

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this);

        Window.Title = "Asteroids";
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _windowWidth;
        _graphics.PreferredBackBufferHeight = _windowHeight;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _background = Content.Load<Texture2D>("background");
        _ship = Content.Load<Texture2D>("ship");
        _largeAsteroid = Content.Load<Texture2D>("asteroid_large");
        _mediumAsteroid = Content.Load<Texture2D>("asteroid_medium");
        _smallAsteroid = Content.Load<Texture2D>("asteroid_small");

        _sprites =
        [
            new Ship(_ship)
            {
                LinearVelocity = 6.0f,
                RotationVelocity = 3.0f,
                Position = new Vector2(200, 200),
                Scale = 0.1f,
                Bullet = new Bullet(Content.Load<Texture2D>("bullet"))
                {
                    LifeSpan = 3.0f,
                    Scale = 0.04f
                }
            }
        ];
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        for (var i = 0; i < _sprites.Count; i++)
        {
            _sprites[i].Update(gameTime, _sprites);
        }

        PostUpdate();

        _timer++;

        if (_timer >= 60)
        {
            var size = _random.Next(0, 3);
            var width = _random.Next(0, _windowWidth);
            var height = _random.Next(0, _windowHeight);

            switch (size)
            {
                case 1:
                    AddAsteroid(AsteroidType.Large, width, height);
                    break;
                case 2:
                    AddAsteroid(AsteroidType.Medium, width, height);
                    break;
                default:
                    AddAsteroid(AsteroidType.Small, width, height);
                    break;
            }

            _timer = 0;
        }

        base.Update(gameTime);
    }

    private void PostUpdate()
    {


        for (var i = _sprites.Count - 1; i >= 0; i--)
        {
            if (_sprites[i].IsRemoved)
            {
                _sprites.RemoveAt(i);
            }
        }
    }




    private void AddAsteroid(AsteroidType type, int x, int y)
    {
        switch (type)
        {
            case AsteroidType.Large:
                AddLargeAsteroid(x, y);
                break;
            case AsteroidType.Medium:
                AddMediumAsteroid(x, y);
                break;
            default:
                AddSmallAsteroid(x, y);
                break;
        }
    }

    private void AddLargeAsteroid(int x, int y)
    {
        _sprites.Add(new Asteroid(_largeAsteroid, AsteroidType.Large)
        {
            LifeSpan = 5.0f,
            Scale = 0.2f,
            LinearVelocity = 1.0f,
            Direction = new Vector2(0, 1),
            Position = new Vector2(x, y)
        });
    }

    private void AddMediumAsteroid(int x, int y)
    {
        _sprites.Add(new Asteroid(_mediumAsteroid, AsteroidType.Medium)
        {
            LifeSpan = 5.0f,
            Scale = 0.3f,
            LinearVelocity = 1.0f,
            Direction = new Vector2(0, 1),
            Position = new Vector2(x, y)
        });
    }

    private void AddSmallAsteroid(int x, int y)
    {
        _sprites.Add(new Asteroid(_smallAsteroid, AsteroidType.Small)
        {
            LifeSpan = 5.0f,
            Scale = 0.2f,
            LinearVelocity = 1.0f,
            Direction = new Vector2(0, 1),
            Position = new Vector2(x, y)
        });
    }








    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(15, 15, 15));

        _spriteBatch.Begin();

        _spriteBatch.Draw(_background, new Rectangle(0, 0, 1280, 720), Color.White);

        for (var i = 0; i < _sprites.Count; i++)
        {
            _sprites[i].Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}