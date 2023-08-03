using System.Drawing;

namespace Invaders.Domain
{
    abstract class Ship
    {
        public Point Location;

        public Bitmap Image;

        public Rectangle Area
        {
            get
            {
                return new Rectangle(Location, Image.Size);
            }
        }

        public Ship(Point location)
        {
            Location = location;
        }

        public Ship(Point location, Bitmap image) 
        {
            Location = location;
            Image = image;
        }
        public abstract void Move(Direction direction);

        public abstract void Draw(Graphics g);
    }
}
