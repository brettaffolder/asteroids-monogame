using System;

using Asteroids.MonoGame.Helpers;

namespace Asteroids.MonoGame.Models;

public class Ship(
    Texture2D texture,
    Vector2 position) : Sprite(texture, position)
{
    private float _rotation = 0.0f;
    private readonly float _rotateSpeed = 3.0f;

    public void Update()
    {
        _rotation += InputHelper.Direction.X * _rotateSpeed * Globals.TotalSeconds;
        var direction = new Vector2((float)Math.Sin(_rotation), -(float)Math.Cos(_rotation));
        _position += InputHelper.Direction.Y * direction * _speed * Globals.TotalSeconds;
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(_texture, _position, null, Color.White, _rotation, _origin, 0.1f, SpriteEffects.None, 1);
    }
}