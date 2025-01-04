namespace Asteroids.MonoGame.Sprites;

public class Bullet(Texture2D texture) : Sprite(texture)
{
    private float _timer;

    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer > LifeSpan)
        {
            IsRemoved = true;
        }

        Position += Direction * LinearVelocity;
    }
}