using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Flappy_Bird
{
    class Pipe : PictureBox
    {
        public int moveSpeed { get; }
        public Pipe(int height, int moveSpeed)
        {
            this.moveSpeed = moveSpeed;
            this.BackColor = Color.Transparent;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}
