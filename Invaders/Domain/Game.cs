using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Invaders.Domain
{
    public class Game
    {
        private int row = 6;
        private int col = 4;
        private int score;
        private int livesLeft;
        private int firstIndex = 0;
        private int starCount = 100;
        private const int distance = 30;
        private int invaderShotCount = 2;
        public bool GameOver;
        public bool Finish;
        private Bitmap[,] bitmaps;
        private List<Shot> invaderShots;
        private Random random;
        private Invader[] invaders;
        private int[,] invaderLocation;
        private Player player;

        public Game(Bitmap[,] bitmaps)
        {
            score = 0;
            livesLeft = 2;
            GameOver = false;
            Finish = false;
            this.bitmaps = bitmaps;
            this.CreateInvadersAndPlayer();
            invaderShots = new List<Shot>();
            random = new Random();
        }

        public int Life
        {
            get { return livesLeft; }
        }

        public int Score
        {
            get { return score; }
        }

        private void CreateInvadersAndPlayer()
        {
            invaders = new Invader[row * col];
            invaderLocation = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                HpShipType hpShipType = HpShipType.Bug;
                ShipType shipType = (ShipType)i;
                switch (shipType)
                {
                    case ShipType.Bug:
                        hpShipType = HpShipType.Bug;
                        break;
                    case ShipType.FlyingSaucer:
                        hpShipType = HpShipType.FlyingSaucer;
                        break;
                    case ShipType.SateLlite:
                        hpShipType = HpShipType.SateLlite;
                        break;
                    case ShipType.SpaceShip:
                        hpShipType = HpShipType.SpaceShip;
                        break;
                    case ShipType.Star:
                        hpShipType = HpShipType.Star;
                        break;
                    case ShipType.Watchit:
                        hpShipType = HpShipType.Watchit;
                        break;
                }
                for (int j = 0; j < col; j++)
                {
                    var index = i * col + j;
                    invaders[index] = new Invader(shipType, new Point(70 * (j + 1), 20 + (i + 1) * 50), (int)hpShipType, this.bitmaps[(int)shipType, 0]);
                    invaderLocation[i, j] = index;
                }
                this.player = new Player(new Point(500, 580), this.bitmaps[(int)ShipType.Player, 0]);
            }
        }

        private void moveInvader()
        {
            Invader invaderNearLeftBoundary = null;
            for (var i = 0; i < row; i++)
            {
                var currentIndex = invaderLocation[i, firstIndex];
                invaderNearLeftBoundary = invaders[currentIndex];
                if (invaderNearLeftBoundary != null)
                    break;
            }

            if (invaderNearLeftBoundary != null)
            {
                if (invaderNearLeftBoundary.Direction == Direction.Left)
                {
                    foreach (var invader in invaders)
                    {
                        if (invader != null)
                        {
                            invader.Move(Direction.Left);
                            if (invaderNearLeftBoundary.Location.X <= 10)
                                invader.Move(Direction.Down);
                        }

                    }
                }

                if (invaderNearLeftBoundary.Direction == Direction.Down)
                {
                    foreach (var invader in invaders)
                    {
                        if (invader != null)
                            invader.Move(Direction.Right);
                    }
                }

                if (invaderNearLeftBoundary.Direction == Direction.Right)
                {
                    foreach (var invader in invaders)
                    {
                        if (invader != null)
                        {
                            invader.Move(Direction.Right);
                            if (invaderNearLeftBoundary.Location.X >= 550)
                                invader.Move(Direction.Up);
                        }

                    }
                }

                if (invaderNearLeftBoundary.Direction == Direction.Up)
                {
                    foreach (var invader in invaders)
                    {
                        if (invader != null)
                            invader.Move(Direction.Left);
                    }
                }
            }
            else
                firstIndex++;

        }

        public void Draw(Graphics g, int animationCell)
        {
            if (!GameOver)
            {
                foreach (var invader in invaders)
                {
                    if (invader != null)
                    {
                        invader.Image = this.bitmaps[(int)invader.InvaderType, animationCell];
                        invader.Draw(g);
                    }

                }
                if (!player.Dead)
                {
                    this.player.Draw(g);
                    this.player.Shots.ForEach(shot => shot.Draw(g));
                }
                invaderShots.ForEach(invaderShot => invaderShot.Draw(g));

            }
            else
            {
                var msg = Finish == true ? "You win !" : "Game Over !";
                g.DrawString(msg, new Font("Arial", 120), new SolidBrush(Color.Red), 50f, 150f, new StringFormat());
            }
        }

        public void Twinkle(Graphics g, int width, int height)
        {
            var pens = new Pen[] { Pens.SkyBlue, Pens.White, Pens.Yellow, Pens.Green, Pens.Blue };

            for (var i = 0; i < starCount; i++)
            {
                var randomIndex = random.Next(0, pens.Length - 1);
                var pen = pens[randomIndex];
                var randomX = random.Next(0, width);
                var randomY = random.Next(0, height);
                g.DrawRectangle(pen, randomX, randomY, 1, 1);
            }

        }

        public void MovePlayer(Direction direction)
        {
            if (player.Location.X <= 30)
            {
                player.Location.X = 30;
            }
            else if (player.Location.X >= 1000)
            {
                player.Location.X = 1000;
            }
            player.Move(direction);
        }

        public void moveInvaderShots()
        {
            if (invaderShots != null)
            {
                for (var i = 0; i < invaderShots.Count; i++)
                {
                    var invaderShot = invaderShots[i];
                    invaderShot.Move(Direction.Down);
                    var distanceX = invaderShot.Location.X - player.Location.X;
                    var distanceY = invaderShot.Location.Y - player.Location.Y;
                    if (distanceY < distance && distanceY > -distance && distanceX > -distance && distanceX < distance && !player.Dead)
                    {
                        player.Dead = true;
                    }
                    if (invaderShot.Location.Y >= 700)
                    {
                        invaderShots.Remove(invaderShot);
                    }
                }
            }
        }

        private void movePlayerShots()
        {
            for (var i = 0; i < player.Shots.Count; i++)
            {
                var shot = player.Shots[i];
                shot.Move(Direction.Up);
                for (var j = 0; j < invaders.Length; j++)
                {
                    var invader = invaders[j];
                    if (invader != null)
                    {
                        var distanceY = shot.Location.Y - invader.Location.Y;
                        var distanceX = shot.Location.X - invader.Location.X;
                        if (distanceY < distance && distanceY > -distance && distanceX > -distance && distanceX < distance)
                        {
                            player.Shots.Remove(shot);
                            score += invader.Score;
                            invaders[j] = null;
                        }
                    }

                }
                if (shot.Location.Y <= 0)
                {
                    player.Shots.Remove(shot);
                }
            }
        }

        public void Go()
        {
            moveInvader();
            movePlayerShots();
            moveInvaderShots();
            checkGameIsOver();
            returnFireShot();
        }

        public void FireShot()
        {
            player.FireShot();
        }

        public void returnFireShot()
        {
            var invaderCount = invaders.Count();
            var randomIndex = random.Next(0, invaderCount - 1);
            var invader = invaders[randomIndex];
            if (invader != null)
            {
                var shot = invader.FireShot();
                if (invaderShots == null)
                    invaderShots = new List<Shot>();
                if (invaderShots.Count < invaderShotCount)
                    invaderShots.Add(shot);
            }
        }

        private void checkGameIsOver()
        {
            if (livesLeft > 0 && player.Dead == true)
            {
                livesLeft--;
                player.Dead = false;
                player.Location = new Point(500, 580);
            }
            else if (livesLeft <= 0 && player.Dead == true)
            {
                GameOver = true;
            }
            else if (invaders.Length == invaders.Where(a => a == null).Count())
            {
                GameOver = true;
                Finish = true;
            }
        }

    }
}
