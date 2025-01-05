using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids.OpenGL.Sprites;

public class Sprite(Texture2D texture) : ICloneable
{
    private Texture2D _texture = texture;
    private Vector2 _origin = new Vector2(texture.Width / 2, texture.Height / 2);

    protected KeyboardState _currentKey = default;
    protected KeyboardState _previousKey = default;

    public Vector2 Position { get; set; } = Vector2.Zero;
    public Vector2 Direction { get; set; } = Vector2.Zero;
    public float LinearVelocity { get; set; } = 0.00f;
    public float RotationVelocity { get; set; } = 0.00f;
    public float Rotation { get; set; } = 0.00f;
    public float Scale { get; set; } = 1.00f;
    public float LifeSpan { get; set; } = 0.00f;
    public bool IsRemoved { get; set; } = false;
    public Sprite Parent { get; set; } = default;

    public virtual void Update(GameTime gameTime, List<Sprite> sprites)
    {

    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, _origin, Scale, SpriteEffects.None, 0);
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}