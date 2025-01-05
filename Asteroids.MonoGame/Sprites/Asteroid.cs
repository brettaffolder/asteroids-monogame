namespace Asteroids.MonoGame.Sprites;

public enum AsteroidType : int
{
    Small = 0,
    Medium = 1,
    Large = 2
}

public class Asteroid(Texture2D texture, AsteroidType type = 0) : Sprite(texture)
{
    private readonly AsteroidType type = type;

    private float _timer;

    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer > LifeSpan)
        {
            IsRemoved = true;
        }

        Position += Direction * LinearVelocity;

        _rotation += MathHelper.ToRadians(RotationVelocity * 0.25f);
    }
}