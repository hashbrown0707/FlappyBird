using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Flappy_Bird
{
    class Game
    {
        private const int GRAVITY = 10;

        public Random random;

        public Game(Random random)
        {
            this.random = random;
        }

        private int score;

        private int pipeSpace;     //水管中間長

        public void DoBird(Bird bird, int timeStep)
        {
            switch (bird.BirdState)
            {
                case birdState.Jumping:
                    bird.Location = new Point(bird.Location.X, bird.Location.Y - GetBirdVolecityY(bird) * (timeStep / 25));
                    if (GetBirdVolecityY(bird) < 0)
                        bird.BirdState = birdState.Falling;
                    break;
                case birdState.Falling:
                    bird.Location = new Point(bird.Location.X, bird.Location.Y + GRAVITY * (timeStep / 25));
                    break;
            }
        }

        public void SetBirdVolecityY(Bird bird, int timeStep)
        {
            bird.Volecity -= GRAVITY * (timeStep / 25);
        }

        public void BirdJump(Bird bird, int timeStep)
        {
            bird.BirdState = birdState.Jumping;
            bird.Volecity = 0;
            bird.Volecity += GRAVITY * (timeStep / 25) + bird.HeightIncrement;
        }

        public List<Pipe> SpawnPipe(int pipeMoveSpeed)
        {
            pipeSpace = random.Next(180, 200);
            int downHeight = random.Next(0, 300);
            int upHeight = downHeight + pipeSpace;
            List<Pipe> pipes = new List<Pipe>{ new PipeUp(upHeight, pipeMoveSpeed), new PipeDown(downHeight, pipeMoveSpeed) };
            return pipes;
        }

        public void CheckScore(Bird bird, List<Pipe> pipes, Label scoreLabel)
        {
            if (pipes != null)
                foreach (Pipe pipe in pipes)
                    if (pipe.Location.X == bird.Location.X)
                        score++;
            scoreLabel.Text = "Score : " + (score / 2).ToString();
        }

        public bool CheckGameOver(Bird bird, Ground ground, List<Pipe> pipes)
        {
            Rectangle groundRect = new Rectangle(ground.Location, ground.Size);
            if (bird.Bounds.IntersectsWith(groundRect))
                return true;

            if (pipes != null)
            {
                foreach (Pipe pipe in pipes)
                {
                    Rectangle pipeRect = new Rectangle(pipe.Location, pipe.Size);
                    if (bird.Bounds.IntersectsWith(pipeRect))
                        return true;
                }
            }

            return false;
        }

        private int GetBirdVolecityY(Bird bird)
        {
            return bird.Volecity;
        }
    }
}
