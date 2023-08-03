using System.Drawing;

namespace Invaders.Domain
{
    class Invader : Ship
    {
        private const int horizontalInterval = 5;
        private const int vericalInterval = 40;
        private Direction direction;
        public int Score { get; private set; }
        public ShipType InvaderType { get; private set; }
        public Direction Direction { get { return direction; } }

        public Invader(ShipType invaderType, Point location, int score, Bitmap image) : base(location, image)
        {
            this.InvaderType = invaderType;
            this.Score = score;
            this.direction = Direction.Left;
        }

        public override void Move(Direction direction)
        {
            this.direction = direction;
            int y = this.Location.Y;
            int x = this.Location.X;
            switch (this.direction)
            {
                case Direction.Up:
                    y -= vericalInterval;
                    break;
                case Direction.Down:
                    y += vericalInterval;
                    break;
                case Direction.Left:
                    x -= horizontalInterval;
                    break;
                case Direction.Right:
                    x += horizontalInterval;
                    break;
            }
            this.Location.X = x;
            this.Location.Y = y;
        }

        public override void Draw(Graphics g)
        {
            g.DrawImageUnscaled(this.Image, this.Location);
        }

        public Shot FireShot()
        {
            return new Shot(Location);
        }
    }
}
