namespace Asteroids.MonoGame.Helpers;

public static class InputHelper
{
    private static MouseState _lastMouseState;

    private static Vector2 _direction;
    private static Vector2 _directionArrows;
    private static bool _mouseClicked;

    public static Vector2 Direction => _direction;
    public static Vector2 DirectionArrows => _directionArrows;
    public static Vector2 MousePosition => Mouse.GetState().Position.ToVector2();
    public static bool MouseClicked => _mouseClicked;

    public static void Update()
    {
        var state = Keyboard.GetState();

        _direction = Vector2.Zero;

        if (state.IsKeyDown(Keys.W))
        {
            _direction.Y++;
        }

        if (state.IsKeyDown(Keys.S))
        {
            _direction.Y--;
        }

        if (state.IsKeyDown(Keys.A))
        {
            _direction.X--;
        }

        if (state.IsKeyDown(Keys.D))
        {
            _direction.X++;
        }

        _directionArrows = Vector2.Zero;

        if (state.IsKeyDown(Keys.Up))
        {
            _directionArrows.Y++;
        }

        if (state.IsKeyDown(Keys.Down))
        {
            _directionArrows.Y--;
        }

        if (state.IsKeyDown(Keys.Left))
        {
            _directionArrows.X--;
        }

        if (state.IsKeyDown(Keys.Right))
        {
            _directionArrows.X++;
        }

        _mouseClicked = (Mouse.GetState().LeftButton == ButtonState.Pressed) && (_lastMouseState.LeftButton == ButtonState.Released);
        _lastMouseState = Mouse.GetState();
    }
}