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
        private Score scoreLabel;
        private List<Pipe> pipes;

        private MessageBoxIcon icon;
        private MessageBoxButtons buttons;
        private string text;
        private string caption;
        private bool canShow;
        private int score;

        private int time;
        private int pipeSpawnTime;
        private int pipeMoveSpeed;

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialize(new Game(new Random()), new Timer(), new Bird(100, 164, 50, 50, 20), new Ground(), new Score(), new List<Pipe>());
        }

        private void Initialize(Game game, Timer timer, Bird bird, Ground ground, Score scoreLabel, List<Pipe> pipes)    //Start
        {
            this.timer = timer;
            timer.Interval = 25;
            timer.Start();
            timer.Tick += Timer_Tick;

            this.game = game;
            this.bird = bird;
            this.ground = ground;
            this.scoreLabel = scoreLabel;
            this.pipes = pipes;
            this.Controls.Add(bird);
            this.Controls.Add(ground);
            this.Controls.Add(scoreLabel);

            pipeMoveSpeed = 5;
            pipeSpawnTime = 1800;

            icon = MessageBoxIcon.Question;
            buttons = MessageBoxButtons.YesNo;
            text = "GAMEOVER, \n COUNTINUE?";
            caption = "GAMEOVER";
            score = 0;

            canShow = true;
        }

        private void Timer_Tick(object sender, EventArgs e)    //Update
        {
            game.DoBird(bird, timer.Interval);
            game.SetBirdVolecityY(bird, timer.Interval);

            SpawnPipe();
            EndOrResetGame();
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

            if (pipes != null)
                foreach (Pipe pipe in pipes)
                {
                    if (pipe.Location.X == bird.Location.X)
                        score++;
                    scoreLabel.Text = "Score : " + (score / 2).ToString();
                        
                    pipe.Location = new Point(pipe.Location.X - pipe.moveSpeed, pipe.Location.Y);
                    if (pipe.Location.X <= -100)
                        this.Controls.Remove(pipe);
                }
        }

        private bool CheckGameOver()
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

        private void EndOrResetGame()
        {
            if (CheckGameOver() && canShow)
            {
                timer.Stop();
                DialogResult result = MessageBox.Show(text, caption, buttons, icon);
                canShow = false;

                switch (result)
                {
                    case DialogResult.Yes:
                        this.Controls.Clear();
                        Initialize(new Game(new Random()), new Timer(), new Bird(100, 164, 50, 50, 20), new Ground(), new Score(), new List<Pipe>());
                        break;
                    case DialogResult.No:
                        this.Close();
                        break;
                }
            }
        }
    }
}
