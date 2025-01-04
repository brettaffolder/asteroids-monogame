using Asteroids.MonoGame.Helpers;
using Asteroids.MonoGame.Models;

namespace Asteroids.MonoGame.Services;

public class GameService
{
    private readonly Ship _ship;

    public GameService()
    {
        _ship = new Ship(Globals.Content.Load<Texture2D>("ship"), new Vector2(200, 200));
    }

    public void Update()
    {
        InputHelper.Update();
        _ship.Update();
    }

    public void Draw()
    {
        _ship.Draw();
    }
}