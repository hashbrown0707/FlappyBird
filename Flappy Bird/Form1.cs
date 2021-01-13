using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Bird
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Timer timer;
        private Game game;
        private Bird bird;
        private Ground ground;
        private ScoreLabel scoreLabel;
        private List<Pipe> pipes;

        private MessageBoxIcon icon;
        private MessageBoxButtons buttons;
        private string text;
        private string caption;
        private bool canShow;

        private int time;
        private int pipeSpawnTime;
        private int pipeMoveSpeed;

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialize(new Game(new Random()), new Timer(), new Bird(100, 164, 50, 50, 20), new Ground(), new ScoreLabel(), new List<Pipe>());
        }

        private void Initialize(Game game, Timer timer, Bird bird, Ground ground, ScoreLabel scoreLabel, List<Pipe> pipes)    //Start
        {
            this.timer = timer;
            timer.Interval = 25;
            timer.Start();
            timer.Tick += Timer_Tick;

            this.game = game;
            this.bird = bird;
            this.ground = ground;
            this.pipes = pipes;
            this.scoreLabel = scoreLabel;
            this.Controls.Add(bird);
            this.Controls.Add(ground);
            this.Controls.Add(scoreLabel);

            pipeMoveSpeed = 5;
            pipeSpawnTime = 1800;

            icon = MessageBoxIcon.Question;
            buttons = MessageBoxButtons.YesNo;
            text = "GAMEOVER, \n COUNTINUE?";
            caption = "GAMEOVER";

            canShow = true;
        }

        private void Timer_Tick(object sender, EventArgs e)    //Update
        {
            game.DoBird(bird, timer.Interval);
            game.SetBirdVolecityY(bird, timer.Interval);

            SpawnPipe();
            PipeRecycle();

            game.CheckScore(bird, pipes, scoreLabel);
            EndOrResetGame();
            //Console.WriteLine(GC.GetTotalMemory(false));
            //TransparetBackground(scoreLabel);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            game.BirdJump(bird, timer.Interval);
        }

        private void SpawnPipe()
        {
            time += timer.Interval;
            if (time >= pipeSpawnTime)
            {
                time = 0;

                pipes.AddRange(game.SpawnPipe(pipeMoveSpeed));
                if (pipes != null)
                    foreach (Pipe pipe in pipes)
                        this.Controls.Add(pipe);
            }
        }

        private void PipeRecycle()
        {
            if (pipes != null)
                foreach (Pipe pipe in pipes)
                {
                    pipe.Location = new Point(pipe.Location.X - pipe.moveSpeed, pipe.Location.Y);
                    if (pipe.Location.X <= -100)
                        this.Controls.Remove(pipe);
                }
        }

        private void EndOrResetGame()
        {
            if (game.CheckGameOver(bird, ground, pipes) && canShow)
            {
                timer.Stop();
                DialogResult result = MessageBox.Show(text, caption, buttons, icon);
                canShow = false;

                switch (result)
                {
                    case DialogResult.Yes:
                        this.Controls.Clear();
                        Initialize(new Game(new Random()), new Timer(), new Bird(100, 164, 50, 50, 20), new Ground(), new ScoreLabel(), new List<Pipe>());
                        break;
                    case DialogResult.No:
                        this.Close();
                        break;
                }
            }
        }

        //private void TransparetBackground(Control C)
        //{
        //    C.Visible = false;

        //    C.Refresh();
        //    Application.DoEvents();

        //    Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
        //    int titleHeight = screenRectangle.Top - this.Top;
        //    int Right = screenRectangle.Left - this.Left;

        //    Bitmap bmp = new Bitmap(this.Width, this.Height);
        //    this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));
        //    Bitmap bmpImage = new Bitmap(bmp);
        //    bmp = bmpImage.Clone(new Rectangle(C.Location.X + Right, C.Location.Y + titleHeight, C.Width, C.Height), bmpImage.PixelFormat);
        //    C.BackgroundImage = bmp;

        //    C.Visible = true;
        //}
    }
}
