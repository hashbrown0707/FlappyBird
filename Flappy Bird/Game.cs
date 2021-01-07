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
        private static int GRAVITY = 10;

        public Random random;

        public Game(Random random)
        {
            this.random = random;
        }

        int pipeSpace;     //水管中間長

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

        private int GetBirdVolecityY(Bird bird)
        {
            return bird.Volecity;
        }
    }
}
