using System.Drawing;

namespace Invaders.Domain
{
    internal class Shot : Ship
    {
        private const int width = 8;
        private const int height = 20;
        private const int vericalInterval = 30;

        public Shot(Point location) : base(location)
        {
            location.X += 13;
            Location= location;
        }

        public new Rectangle Area
        {
            get
            {
                var size = new Size(width, height); 
                return new Rectangle(Location, size);
            }
        }
        public override void Draw(Graphics g)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
            g.FillRectangle(myBrush, Area);
        }

        public override void Move(Direction direction)
        {
            if (direction == Direction.Up)
                Location.Y -= vericalInterval;
            else if(direction == Direction.Down)
                Location.Y += vericalInterval;
        }
    }
}
