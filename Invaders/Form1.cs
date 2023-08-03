using System;
using System.Drawing;
using System.Windows.Forms;
using Invaders.Domain;

namespace Invaders
{
    public partial class Form1 : Form
    {
        Bitmap[,] images;
        private int animationCell = 0;
        private int frame = 0;
        private bool leftDown = false;
        private bool rightDown = false;
        private bool spaceDown = false;
        private Game game;
        public void Animate()
        {
            frame++;
            if (frame >= 6)
                frame = 0;
            switch (frame)
            {
                case 0:
                    animationCell = 0;
                    break;
                case 1:
                    animationCell = 1;
                    break;
                case 2:
                    animationCell = 2;
                    break;
                case 3:
                    animationCell = 3;
                    break;
                case 4:
                    animationCell = 2;
                    break;
                case 5:
                    animationCell = 1;
                    break;
                default:
                    animationCell = 0;
                    break;
            }
            Invalidate();
        }

        public Form1()
        {
            InitializeComponent();
            InitializeImage();
        }

        public void InitializeImage()
        {
            images = new Bitmap[7, 4];
            images[0, 0] = ResizeImage(Properties.Resources.bug1, 35, 35);
            images[0, 1] = ResizeImage(Properties.Resources.bug2, 35, 35);
            images[0, 2] = ResizeImage(Properties.Resources.bug3, 35, 35);
            images[0, 3] = ResizeImage(Properties.Resources.bug4, 35, 35);
            images[1, 0] = ResizeImage(Properties.Resources.flyingsaucer1, 35, 35);
            images[1, 1] = ResizeImage(Properties.Resources.flyingsaucer2, 35, 35);
            images[1, 2] = ResizeImage(Properties.Resources.flyingsaucer3, 35, 35);
            images[1, 3] = ResizeImage(Properties.Resources.flyingsaucer4, 35, 35);
            images[2, 0] = ResizeImage(Properties.Resources.satellite1, 35, 35);
            images[2, 1] = ResizeImage(Properties.Resources.satellite2, 35, 35);
            images[2, 2] = ResizeImage(Properties.Resources.satellite3, 35, 35);
            images[2, 3] = ResizeImage(Properties.Resources.satellite4, 35, 35);
            images[3, 0] = ResizeImage(Properties.Resources.spaceship1, 35, 35);
            images[3, 1] = ResizeImage(Properties.Resources.spaceship2, 35, 35);
            images[3, 2] = ResizeImage(Properties.Resources.spaceship3, 35, 35);
            images[3, 3] = ResizeImage(Properties.Resources.spaceship4, 35, 35);
            images[4, 0] = ResizeImage(Properties.Resources.star1, 35, 35);
            images[4, 1] = ResizeImage(Properties.Resources.star2, 35, 35);
            images[4, 2] = ResizeImage(Properties.Resources.star3, 35, 35);
            images[4, 3] = ResizeImage(Properties.Resources.star4, 35, 35);
            images[5, 0] = ResizeImage(Properties.Resources.watchit1, 35, 35);
            images[5, 1] = ResizeImage(Properties.Resources.watchit2, 35, 35);
            images[5, 2] = ResizeImage(Properties.Resources.watchit3, 35, 35);
            images[5, 3] = ResizeImage(Properties.Resources.watchit4, 35, 35);
            images[6, 0] = ResizeImage(Properties.Resources.player, 35, 35);
            game = new Game(images);
        }


        public Bitmap ResizeImage(Image imageToResize, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawImage(imageToResize, 0, 0, width, height);
            }
            return bitmap;
        }


        private void Refresh(object sender, EventArgs e)
        {
            this.Life.Text = $"Life : {game.Life}";
            this.Score.Text = $"Score : {game.Score}";
            Animate();
            if (!game.GameOver)
            {
                game.Go();
                if (leftDown)
                    game.MovePlayer(Direction.Left);
                else if (rightDown)
                    game.MovePlayer(Direction.Right);
                if (spaceDown)
                    game.FireShot();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            game.Draw(g, animationCell);
            game.Twinkle(g, this.Width, this.Height);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && !leftDown)
                leftDown = true;
            else if (e.KeyCode == Keys.Right && !rightDown)
                rightDown = true;
            else if (e.KeyCode == Keys.Space && !spaceDown)
                spaceDown = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                leftDown = false;
            else if (e.KeyCode == Keys.Right)
                rightDown = false;
            else if (e.KeyCode == Keys.Space)
                spaceDown = false;
            else if (e.KeyCode == Keys.R)
            {
                game = new Game(images);
                GC.Collect();
            }
        }
    }
}
