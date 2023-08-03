using System.Collections.Generic;
using System.Drawing;

namespace Invaders.Domain
{
    class Player : Ship
    {
        private const int horizontalInterval = 10;
        public bool Dead;
        public List<Shot> Shots;
        public Player(Point location, Bitmap image) : base(location, image)
        {
            Shots = new List<Shot>();
        }

        public override void Move(Direction direction)
        {
            int y = this.Location.Y;
            int x = this.Location.X;
            switch (direction)
            {
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

        public void FireShot()
        {
            if (Shots.Count < 2) 
            {
                Shots.Add(new Shot(Location));
            }
        }
    }
}
