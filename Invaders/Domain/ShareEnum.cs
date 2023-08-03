namespace Invaders.Domain
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    enum ShipType
    {
        Bug = 0,
        FlyingSaucer = 1,
        SateLlite = 2,
        SpaceShip = 3,
        Star = 4,
        Watchit = 5,
        Player = 6
    }

    enum HpShipType
    {
        Bug = 40,
        FlyingSaucer = 30,
        SateLlite = 50,
        SpaceShip = 20,
        Star = 10,
        Watchit = 60
    }
}
