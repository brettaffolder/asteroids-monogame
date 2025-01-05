namespace Asteroids.MonoGame.Sprites;

public class Ship(Texture2D texture) : Sprite(texture)
{
    public Bullet Bullet { get; set; }

    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        _previousKey = _currentKey;
        _currentKey = Keyboard.GetState();

        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            _rotation -= MathHelper.ToRadians(RotationVelocity);
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            _rotation += MathHelper.ToRadians(RotationVelocity);
        }

        Direction = new Vector2((float)Math.Sin(_rotation), -(float)Math.Cos(_rotation));

        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            Position += Direction * LinearVelocity;
        }

        if (_currentKey.IsKeyDown(Keys.Space) && _previousKey.IsKeyUp(Keys.Space))
        {
            Shoot(sprites);
        }
    }

    private void Shoot(List<Sprite> sprites)
    {
        var bullet = Bullet.Clone() as Bullet;

        bullet.Direction = Direction;
        bullet.Position = Position;
        bullet.LinearVelocity = LinearVelocity * 2;
        bullet.LifeSpan = 3.0f;
        bullet.Parent = this;

        bullet.SetRotation(_rotation + MathHelper.ToRadians(-90));

        sprites.Add(bullet);
    }
}