using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.OpenGL.Sprites;

public enum AsteroidType : int
{
    Small = 0,
    Medium = 1,
    Large = 2
}

public class Asteroid(Texture2D texture) : Sprite(texture)
{
    private float _timer;

    public AsteroidType Type { get; set; }

    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_timer > LifeSpan)
        {
            IsRemoved = true;
        }

        Position += Direction * LinearVelocity;
        Rotation += MathHelper.ToRadians(RotationVelocity);
    }
}