using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Flappy_Bird
{
    class PipeDown : Pipe
    {
        public PipeDown(int height, int moveSpeed) : base(height, moveSpeed)
        {
            this.BackgroundImage = Properties.Resources.pipedown;
            this.Location = new Point(880, 0);
            this.Size = new Size(76, height);
        }
    }
}
