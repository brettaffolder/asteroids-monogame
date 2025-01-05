using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids.OpenGL.Sprites;

public class Ship(Texture2D texture) : Sprite(texture)
{
    public Bullet Bullet { get; set; }

    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        _previousKey = _currentKey;
        _currentKey = Keyboard.GetState();

        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            Rotation -= MathHelper.ToRadians(RotationVelocity);
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            Rotation += MathHelper.ToRadians(RotationVelocity);
        }

        Direction = new Vector2((float)Math.Sin(Rotation), -(float)Math.Cos(Rotation));

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
        bullet.LinearVelocity = Globals.BulletLinearVelocity;
        bullet.RotationVelocity = Globals.BulletRotationVelocity;
        bullet.Rotation = Rotation + MathHelper.ToRadians(-90);
        bullet.Scale = Globals.BulletScale;
        bullet.LifeSpan = Globals.BulletLifeSpan;
        bullet.Parent = this;

        sprites.Add(bullet);
    }
}