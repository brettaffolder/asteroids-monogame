namespace Asteroids.MonoGame.Sprites;

public class Sprite : ICloneable
{
    protected Texture2D _texture;
    protected float _rotation;
    protected KeyboardState _currentKey;
    protected KeyboardState _previousKey;

    public Vector2 Position { get; set; } = default;
    public Vector2 Origin { get; set; } = default;
    public Vector2 Direction { get; set; } = default;
    public float RotationVelocity { get; set; } = 3.0f;
    public float LinearVelocity { get; set; } = 4.0f;
    public Sprite Parent { get; set; } = default;
    public float LifeSpan { get; set; } = 0.0f;
    public bool IsRemoved { get; set; } = false;
    public float Scale { get; set; } = 1.0f;

    public Sprite(Texture2D texture)
    {
        _texture = texture;

        Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
    }

    public void SetRotation(float rotation)
    {
        _rotation = rotation;
    }

    public virtual void Update(GameTime gameTime, List<Sprite> sprites)
    {

    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, Scale, SpriteEffects.None, 0);
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}