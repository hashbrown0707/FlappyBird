using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Bird
{
    class Score : System.Windows.Forms.Label
    {
        public Score()
        {
            this.Location = new System.Drawing.Point(811, 30);
            this.BackColor = Color.Transparent;
        }
    }
}
