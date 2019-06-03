using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Run
{
    public class Bullet
    {
        public float dx { get; set; }
        public float dy { get; set; }
        public float posx{get;set;}
        public float posy{get;set;}
        public  Point center { get{ return new Point((int)posx,(int)posy);} }
        public static int radius=4;
        public static int speed=35;
        public float moveCompensation{get;set;}
        //public int rightest { get{ return center.X} }
        public Bullet(float dx, float dy, Point center,float movecomp)
        {
            moveCompensation=Math.Abs(movecomp);
            this.dx = dx-Platform.speed;
            this.dy = dy;
            posx=center.X;
            posy=center.Y;
        }
        public void Draw(Graphics g)
        {
            Brush  b=new SolidBrush(Color.Blue);
            g.FillEllipse(b,posx-radius,posy-radius,2*radius,2*radius);
        }
        public void Move()
        {
            posx+=dx;
            //posx-=moveCompensation;
            posy+=dy;
        }
    }
}
