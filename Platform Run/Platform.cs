using Platform_Run.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Run
{
    public class Platform
    {
        public Point start { get; set; }
       // private Point imageStart;
        public int length { get; set; }
        public static int speed=8;
        public static int height=20;
        static Image grass=Resources.treva2;
        //public Rectangle rect;
        public Platform(Point start,int length)
        {
          this.start=start;
          //imageStart=new Point(start.X,start.Y+20);  
          this.length=length;
          //rect=new Rectangle(start.X,start.Y,length,height);
        }
        public void Draw(Graphics g)
        {
            Brush b=new SolidBrush(Color.Black);
            //g.FillRectangle(b,start.X,start.Y,length,height);
            g.DrawImage(grass,start.X,start.Y,length,40);
            //g.DrawString(start.Y.ToString(),new Font("Arial",40),b,start.X,start.Y);
        }
        public void Move()
        {
            start=new Point(start.X-speed,start.Y);
            //imageStart=new Point(imageStart.X-speed,imageStart.Y);
        }
        public int getEndX()
        {
            return start.X+length;
        }

    }
}
