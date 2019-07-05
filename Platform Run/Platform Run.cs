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
        enum Actions
        {
            jump,melee,duck
        }
        PlatformRunDoc doc;
        bool paused;
        Queue<Actions> actions;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered=true;
            paused=false;
            actions=new Queue<Actions>();
            WindowState = FormWindowState.Maximized;
            KeyPreview=true;
            doc=new PlatformRunDoc(Width,Height-200-2*Platform.height);
            MoveTimer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            doc.addPlatform();
            while (actions.Count > 0)
            {
                if(actions.Peek()==Actions.jump)
                    doc.Jump();
                else if(actions.Peek()==Actions.melee)
                    doc.Melee();
                else
                    doc.Duck();
                actions.Dequeue();
            }
            doc.Move();
            if(doc.gameOver){
                doc=new PlatformRunDoc(Width,Height);
                }
            ScoreStatus.Text="Score: "+doc.score;
            KillStatus.Text="Kills: "+doc.kills;
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.Black),0,Height-200,Width,200);
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

        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(" a");
            if (e.KeyData == Keys.Up||e.KeyCode==Keys.W)
                doc.Jump();
                //actions.Enqueue(Actions.jump);
            else if(e.KeyData==Keys.Space){
                doc.Melee(); 
                //actions.Enqueue(Actions.melee);
                //Invalidate();
                }
            else if(e.KeyData==Keys.Down||e.KeyData==Keys.S)
                //actions.Enqueue(Actions.duck);
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
            }
            else
            {
                MoveTimer.Start();
            }
            paused=!paused;
        }

        

        
    }
}
