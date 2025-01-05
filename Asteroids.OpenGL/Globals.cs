namespace Asteroids.OpenGL;

public static class Globals
{
    public const string RootDirectory = "Content";

    public const string WindowTitle  =  "Asteroids";
    public const int WindowWidth = 1280;
    public const int WindowHeight = 720;

    public const float ShipLinearVelocity = 6.00f;
    public const float ShipRotationVelocity = 3.00f;
    public const float ShipScale = 0.10f;

    public const float BulletLinearVelocity = ShipLinearVelocity * 2.00f;
    public const float BulletRotationVelocity = 0.00f;
    public const float BulletScale = 0.02f;
    public const float BulletLifeSpan = 8.00f;

    public const float AsteroidLifeSpan = 60.00f;

    public const float SmallAsteroidLinearVelocity = 2.00f;
    public const float SmallAsteroidRotationVelocity = 1.15f;
    public const float SmallAsteroidScale = 0.10f;

    public const float MediumAsteroidLinearVelocity = 1.20f;
    public const float MediumAsteroidRotationVelocity = 0.60f;
    public const float MediumAsteroidScale = 0.25f;

    public const float LargeAsteroidLinearVelocity = 0.70f;
    public const float LargeAsteroidRotationVelocity = 0.30f;
    public const float LargeAsteroidScale = 0.25f;
}