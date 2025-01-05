namespace Asteroids.MonoGame.Sprites;

public class Sprite : ICloneable
{
    protected float _rotation;
    protected KeyboardState _currentKey;
    protected KeyboardState _previousKey;

    public Texture2D Texture { get; set; }
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
        Texture = texture;

        Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
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
        spriteBatch.Draw(Texture, Position, null, Color.White, _rotation, Origin, Scale, SpriteEffects.None, 0);
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}