using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Bird
{
    class Ground : PictureBox
    {
        public Ground()
        {
            this.Location = new Point(-5, 399);
            this.Size = new Size(896, 64);
            this.BackColor = Color.Transparent;
            this.BackgroundImage = Properties.Resources.ground;
            this.BackgroundImageLayout = ImageLayout.Tile;
        }

    }
}
