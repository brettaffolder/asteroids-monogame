using Asteroids.MonoGame.Sprites;

namespace Asteroids.MonoGame;

public class MainGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<Sprite> _sprites;

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this);

        Window.Title = "Asteroids";
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        var ship = Content.Load<Texture2D>("ship");

        _sprites =
        [
            new Ship(ship)
            {
                Position = new Vector2(200, 200),
                Scale = 0.1f,
                Bullet = new Bullet(Content.Load<Texture2D>("bullet"))
                {
                    Scale = 0.05f
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

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(15, 15, 15));

        _spriteBatch.Begin();

        for (var i = 0; i < _sprites.Count; i++)
        {
            _sprites[i].Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}