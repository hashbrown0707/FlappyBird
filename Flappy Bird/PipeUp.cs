using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Flappy_Bird
{
    class PipeUp : Pipe
    {
        public PipeUp(int height, int moveSpeed) : base(height, moveSpeed)
        {
            this.BackgroundImage = Properties.Resources.pipe;
            this.Location = new Point(880, height);
            this.Size = new Size(76, 400 - height);  
        }
    }
}
