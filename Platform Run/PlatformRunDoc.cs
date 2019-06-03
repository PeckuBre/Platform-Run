using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Run
{
    class PlatformRunDoc
    {
        public Queue<Platform> platforms;
        public List<Monster> monsters;
        public List<Bullet> bullets;
        Random rnd;
        public bool gameOver { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Platform lastPlatform {get;set;}
        public Player player { get; set; }
        public static int playerX=125;
        public int kills;
        public int score;
        private int maxdx;
        public int maxdy;
        private int gt2;
        public PlatformRunDoc(int width,int height)
        {
            rnd=new Random();
            gameOver=false;
            this.width=width;
            this.height=height;
            platforms=new Queue<Platform>();
            monsters=new List<Monster>();
            bullets=new List<Bullet>();
            kills=0;
            score=0;
            maxdx=Platform.speed*2*(int)((Player.jumpSpeed*2/3)/Player.GRAVITY);
            int t=(int)(Player.jumpSpeed/Player.GRAVITY);
            gt2=(int)(Player.GRAVITY*Math.Pow(t,2)/2);
            maxdy=t*Player.jumpSpeed-gt2; 
            //lastPlatform=new Platform(new Point(playerX,rnd.Next(rnd.Next(maxdy+50,height-100))),rnd.Next(125)+150);
            lastPlatform=new Platform(new Point(playerX,height/2),rnd.Next(125)+200);
            platforms.Enqueue(lastPlatform);
            player=new Player(lastPlatform);
            while (lastPlatform.start.X + lastPlatform.length < width + 150)
            {
                lastPlatform=generatePlatform();
                platforms.Enqueue(lastPlatform);
            }

        }
        public Monster generateMonsterOnPlatform(Platform pl)
        {
            int roll=rnd.Next(100);
            if(roll>=CommonMonster.SpawnChance)
                return null;
            Point position=new Point(rnd.Next(pl.start.X,pl.start.X+pl.length-Monster.WIDTH),pl.start.Y-Monster.HEIGHT);
            if(roll<EpicMonster.SpawnChance)
                return new EpicMonster(position);
            else if(roll<RareMonster.SpawnChance)
                return new RareMonster(position);
            else return new CommonMonster(position);
        }
        public Platform generatePlatform()
        {
            //rnd=new Random();
            //int y=rnd.Next(lastPlatform.start.Y-(int)(maxdy*0.8),lastPlatform.start.Y+(int)(maxdy*0.8));
           // int dy=rnd.Next((int)(-(0.8*maxdy)),(int)(0.8*maxdy));
            int dy=rnd.Next((int)maxdy/3,(int)(maxdy*0.8));
            
            //=rnd.Next((int)(maxdy*1.6))-(int)(0.8*maxdy);
            /*while(maxdy>lastPlatform.start.Y+dy&&lastPlatform.start.Y-dy>maxdy)
            dy=rnd.Next((int)maxdy/3,(int)(maxdy*0.8));
            while(height-100<dy+lastPlatform.start.Y&&lastPlatform.start.Y-dy>height-100)
            dy=rnd.Next((int)maxdy/3,(int)(maxdy*0.8));*/
            int mul=rnd.Next(2);
            if(mul==0)
                dy=-dy;
            int y;
            if(maxdy>lastPlatform.start.Y+dy&&lastPlatform.start.Y+dy<lastPlatform.start.Y-dy)
                y=lastPlatform.start.Y-dy;
            else if(height-100<dy+lastPlatform.start.Y&&lastPlatform.start.Y+dy>lastPlatform.start.Y-dy)
                y=lastPlatform.start.Y-dy;
            else
                y=lastPlatform.start.Y+dy;
            Point start=new Point(lastPlatform.getEndX()+maxdx,y);
            int length = rnd.Next(125)+200;
            return new Platform(start,length);

        }
        public void addPlatform()
        {
            while(lastPlatform.start.X+lastPlatform.length<width){
            Point start=new Point(width+100,rnd.Next(height-200)+100);
            lastPlatform=generatePlatform();
            Monster m=generateMonsterOnPlatform(lastPlatform);
            if(m!=null)
            monsters.Add(m);
            platforms.Enqueue(lastPlatform); }
        }
        public void Move()
        {
            score++;
            player.Move();
            foreach(Platform pl in platforms)
            {
               pl.Move();
               player.TryLand(pl);
            }
            foreach(Monster m in monsters)
                m.Move();
            foreach(Bullet  b in bullets)
                b.Move();
            for (int j = 0; j < monsters.Count; j++){
                if (player.TryJumpOnMonster(monsters[j]))
                {
                    score+=monsters[j].getPoints();
                    monsters.RemoveAt(j--);
                    continue;
                }
                 for(int i = 0; i < bullets.Count; i++)
                    if (monsters[j].isHit(bullets[i].center))
                    {
                        if(--monsters[j].health<=0){
                            score+=monsters[j].getPoints();
                            monsters.RemoveAt(j--); 
                            kills++;
                            }
                        bullets.RemoveAt(i--);
                        break;
                    }
                 //if(j>=0&&monsters[j]!=null&&monsters[j].isHit(player.end))
                   // gameOver=true;
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                if(height<bullets[i].center.Y||bullets[i].center.Y<0||bullets[i].center.X>width||bullets[i].center.X<0)
                    bullets.RemoveAt(i);
            }
            if(player.end.Y>height)
                gameOver=true;
            
            if(platforms.Count!=0&&platforms.Peek().getEndX()<0)
                platforms.Dequeue();
            if(monsters.Count>0&&monsters[0].position.X+Monster.WIDTH<0)
                monsters.RemoveAt(0);
        }
        public void Draw(Graphics g)
        {
            foreach(Platform pl in platforms)
            {
                pl.Draw(g);
            }
            foreach(Monster m in monsters)
                m.Draw(g);
            
            foreach(Bullet b in bullets)
                b.Draw(g);
            player.Draw(g);

        }
        public double dist(Point a,Point b)
        {
            return Math.Sqrt((a.X-b.X)*(a.X-b.X)+(a.Y-b.Y)*(a.Y-b.Y));
        }
        public void Shoot(Point to)
        {
            if (player.weapon)
            {
                int x=player.end.X;
                int y=player.end.Y-Player.HEIGHT/2;
                Point start=new Point(x,y);
                double c=dist(start,to);
                double a=to.X-start.X;
                double b=to.Y-start.Y;
                bullets.Add(new Bullet((float)(Bullet.speed*a/c),(float)(Bullet.speed*b/c),start,(float)(Platform.speed*a/c)));
            }
        }
        public void Jump()
        {
            player.Jump();
        }
        public void Duck()
        {
            player.Duck();
        }
    }
}
