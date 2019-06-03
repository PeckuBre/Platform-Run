using Platform_Run.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Run
{
    public abstract class Monster
    {
        public enum MonsterType
        {
            CommonMonster,RareMonster,EpicMonster
        }
        public static int HPpix=10;
        public int health { get; set; }
        public Point position { get; set; }
        public static int speed=Platform.speed;
        public static int HEIGHT=40;
        public static int WIDTH=40;
        //public Rectangle rect;
        public abstract  void Draw(Graphics g);

        protected Monster(int health, Point position)
        {
            //rect=new Rectangle(position.X,position.Y,WIDTH,HEIGHT);
            this.health = health;
            this.position = position;
        }
        public abstract int getPoints();
        public virtual void Move()
        {
            position=new Point(position.X-speed,position.Y);
            //rect=new Rectangle(position.X,position.Y,WIDTH,HEIGHT);
        }

        public bool isHit(Point a)
        {
            //moze da treba da se debagira spored brzinite na kursumot i monsterot
            return a.Y>=position.Y&&a.Y<=position.Y+HEIGHT&&a.X>=position.X&&a.X<=position.X+WIDTH;
        }
        protected void drawHealthBar(Graphics g)
        {
            Point start=new Point(position.X,position.Y-HPpix-4);
            Brush b=new SolidBrush(Color.Red);
            for (int i = 0; i < health; i++)
            {
                g.FillRectangle(b,start.X,start.Y,HPpix,HPpix);
                start=new Point(start.X+HPpix+2,start.Y);
                
            }
        }

    }
    public class CommonMonster : Monster
    {
        public static int SpawnChance=40;
        public static Image slika=Resources.creeper;
        public CommonMonster(Point position):  base(1, position){}
        public override void Draw(Graphics g)
        {
            g.DrawImage(slika,position.X,position.Y,WIDTH,HEIGHT);
            drawHealthBar(g);
        }
        public override int getPoints()
        {
            return 15;
        }
    }
    public class RareMonster : Monster
    {
        public static int SpawnChance=10;
        public static Image slika=Resources.enderman;
        public RareMonster(Point position): base(2,position){ }
        public override void Draw(Graphics g)
        {
            g.DrawImage(slika,position.X,position.Y,WIDTH,HEIGHT);
            drawHealthBar(g);
        }
        public override int getPoints()
        {
            return 30;
        }
    }
    public class EpicMonster : Monster
    {
        public static int SpawnChance=5;
        public static Image slika=Resources.blaze;
        public EpicMonster(Point position): base(3,position){ }
        public override void Draw(Graphics g)
        {
            g.DrawImage(slika,position.X,position.Y,WIDTH,HEIGHT);
            drawHealthBar(g);
        }
        public override int getPoints()
        {
            return 45;
        }
    }
}
