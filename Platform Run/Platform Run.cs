using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Run
{
    public partial class Form1 : Form
    {
        PlatformRunDoc doc;
        bool paused;
      
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered=true;
            paused=false;
            doc=new PlatformRunDoc(Width,Height-150);
            SpawnTimer.Start();
            MoveTimer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            doc.Move();
            if(doc.gameOver){
                doc=new PlatformRunDoc(Width,Height);
                }
            ScoreStatus.Text="Score: "+doc.score;
            BulletsCount.Text="Bullets: "+doc.bullets.Count;
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            doc.Draw(e.Graphics);
           /* int i=100;
            float [] pattern={3,3};
            while (i < Height)
            {
                Pen p=new Pen(Color.Turquoise);
                p.DashPattern=pattern;
                e.Graphics.DrawLine(p,new Point (0,i),new Point(Width,i));
                i+=100;
            }
            i=100;
            while (i < Width)
            {
                Pen p=new Pen(Color.Turquoise);
                p.DashPattern=pattern;
                e.Graphics.DrawLine(p,new Point (i,0),new Point(i,Height));
                i+=100;
            }*/
        }

        private void SpawnTimer_Tick(object sender, EventArgs e)
        {
            doc.addPlatform(); 
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up||e.KeyData==Keys.Space)
                doc.Jump();
            
            else if(e.KeyData==Keys.Down)
                doc.Duck();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doc=new PlatformRunDoc(Width,Height);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

            doc.Shoot(e.Location);
            
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (paused)
            {
                MoveTimer.Stop();
                SpawnTimer.Stop();
            }
            else
            {
                MoveTimer.Start();
                SpawnTimer.Start();
            }
            paused=!paused;
        }
    }
}
