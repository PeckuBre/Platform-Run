using Platform_Run.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Run
{
    public class Player
    {
        public Point top {get; set; }
        public Point end { get; set; }
        public static double GRAVITY=7;
        public static double dy{get;set;}
        public static int WIDTH=40;
        public static int HEIGHT=80;
        public static int jumpSpeed=70;
        public static int maxSpeed=75;
        //private static Image goingUP;
        private static Image running=Resources.ezgif_com_crop;
        //public Rectangle rect;
        //private static Image running2;
        //private static Image goingDOWN;
        public bool inJump{get;set; }
        public Platform standingOn{ get; set; }
        public bool weapon;
        public Player(Platform first)
        {
            weapon=true;
            standingOn=first;
            inJump=false;
            dy=0;
            end=new Point(first.start.X+WIDTH,first.start.Y);
            top=new Point(first.start.X,first.start.Y-HEIGHT);
            //rect=new Rectangle(top.X,top.Y,WIDTH,HEIGHT);
        }
        public void Draw(Graphics g)
        {
            Brush b=new SolidBrush(Color.Red);
            g.FillRectangle(b,top.X,top.Y,WIDTH,HEIGHT);
            //g.DrawImage(running,new Point(end.X-running.Width,end.Y-running.Height));
            b=new SolidBrush(Color.Blue);
            g.FillEllipse(b,end.X-5,end.Y-5,2*5,2*5);
        }

        public void Jump()
        {
            if(!inJump){
            standingOn=null;
            inJump=true;
            dy=jumpSpeed; 
            }
        }
        public void Move()
        {
            if(inJump){
            end=new Point(end.X,end.Y-(int)dy);
            top=new Point(top.X,top.Y-(int)dy);
            dy-=GRAVITY;
            dy=Math.Min(dy,jumpSpeed);
            }
            else
            {
                if(top.X>standingOn.length+standingOn.start.X)
                    Duck();
            }
            //rect=new Rectangle(top.X,top.Y,WIDTH,HEIGHT);
        }
        public bool TryJumpOnMonster(Monster pl)
        {
            return inJump&&(pl.position.Y<=end.Y-dy+GRAVITY&&pl.position.Y>=end.Y)&&((top.X>=pl.position.X&&end.X<=pl.position.X+Monster.WIDTH)
                  ||(top.X<=pl.position.X&&end.X>=pl.position.X+Monster.WIDTH)||(end.X>=pl.position.X&&end.X<=pl.position.X+WIDTH)||
                (top.X>=pl.position.X&&top.X<=pl.position.X+WIDTH));
        }

        public void TryLand(Platform pl)
        {
            if(inJump){
            int dify=this.end.Y-pl.start.Y;
            if((pl.start.X<=this.end.X&&pl.start.X+pl.length>=this.top.X)&&
                pl.start.Y<=end.Y-dy+GRAVITY&&pl.start.Y>=end.Y)
                    this.Land(pl);
            }
        }
        public void Land(Platform pl)
        {
            standingOn=pl;
            end=new Point(end.X,pl.start.Y);
            top=new Point(top.X,pl.start.Y-HEIGHT);
            inJump=false;
            dy=0;
        }
        public void Duck()
        {
            if(!inJump){
            //end=new Point(end.X,end.Y+20);
            //top=new Point(top.X,top.Y+20);
            standingOn=null;
            inJump=true;
            Move();
            }
        }

    }

}
