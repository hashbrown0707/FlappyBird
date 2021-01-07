using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Bird
{
    enum birdState
    {
        Idle,
        Jumping,
        Falling
    }

    class Bird : PictureBox
    {
        public birdState BirdState { get; set; }
        public int HeightIncrement { get; }
        public int Volecity { get; set; }

        public Bird(int x, int y, int sizeX, int sizeY, int heightIncrement)
        {
            this.Name = "birdPic";
            this.Location = new Point(x, y);
            this.BirdState = birdState.Idle;
            this.BackgroundImage = Properties.Resources.bird;
            this.BackgroundImageLayout = ImageLayout.None;
            this.BackColor = Color.Transparent;
            this.Size = new Size(sizeX, sizeY);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.HeightIncrement = heightIncrement;
        }
    }
}
