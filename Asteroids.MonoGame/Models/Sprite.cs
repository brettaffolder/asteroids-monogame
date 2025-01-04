namespace Asteroids.MonoGame.Models;

public class Sprite(Texture2D texture, Vector2 position)
{
    protected readonly Texture2D _texture = texture;
    protected readonly Vector2 _origin = new(texture.Width / 2, texture.Height / 2);
    protected Vector2 _position = position;
    protected int _speed = 500;
    
    public virtual void Draw()
    {
        Globals.SpriteBatch.Draw(_texture, _position, null, Color.White, 0, _origin, 1, SpriteEffects.None, 1);
    }
}