using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlayerSnake
{
    class SnakeClass
    {
        //蛇类
        class Snake
        {
            public List<Point> SnakeBody = new List<Point>();//蛇的身体
            Forward Fangxiang,Toforward;//当前方向，将要方向
            Point tail;
            
            public enum Forward
            {
                Up = 1,
                Down = 2,
                Left = 3,
                Right = 4
            }

            public Snake()
            {
                SnakeBody.Add(new Point(5, 2));
                SnakeBody.Add(new Point(5, 3));
                SnakeBody.Add(new Point(5, 4));
                SnakeBody.Add(new Point(5, 5));
                Fangxiang = Forward.Down;
                Toforward = Forward.Down;
                tail = SnakeBody[0];
            }

            public Forward GetFangxiang()
            {
                return Fangxiang;
            }

            //自动前进
            public void AutoMove()
            {
                tail= SnakeBody[0];
                for (int i = 0; i < SnakeBody.Count - 1; i++)
                {
                    SnakeBody[i] = new Point(SnakeBody[i + 1]);
                }

                switch (Toforward)
                {
                    case Forward.Up:
                        SnakeBody[SnakeBody.Count - 1].Y -= 1;
                        break;
                    case Forward.Down:
                        SnakeBody[SnakeBody.Count - 1].Y += 1;
                        break;
                    case Forward.Left:
                        SnakeBody[SnakeBody.Count - 1].X -= 1;
                        break;
                    case Forward.Right:
                        SnakeBody[SnakeBody.Count - 1].X += 1;
                        break;
                }
                Fangxiang = Toforward;
            }
            public void AddTail()//吃球后加上尾巴
            {
                SnakeBody.Insert(0, tail);
            }

            public bool IsDead()//判断死亡
            {
                for(int i=0;i<SnakeBody.Count-1;i++)
                {
                    if(SnakeBody[i].X== SnakeBody.Last().X&& SnakeBody[i].Y == SnakeBody.Last().Y)
                    {
                        return true;
                    }
                }
                return false;
            }

            public bool ChangeForward(ConsoleKey key)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (Fangxiang != Forward.Down&& Fangxiang != Forward.Up)
                        {
                            Toforward = Forward.Up;
                            return true;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (Fangxiang != Forward.Down && Fangxiang != Forward.Up)
                        {
                            Toforward = Forward.Down;
                            return true;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (Fangxiang != Forward.Right && Fangxiang != Forward.Left)
                        {
                            Toforward = Forward.Left;
                            return true;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (Fangxiang != Forward.Right && Fangxiang != Forward.Left)
                        {
                            Toforward = Forward.Right;
                            return true;
                        }
                        break;
                }
                return false;
            }

        }

       

        public SnakeClass()
        {
            Init();
        }

        int MaxX = 20, MaxY = 20;
        bool IsEndGame = false;
        int RefreshTime;//刷新时间
        Snake snake = new Snake();
        int score = 0;
        int Speed = 800;
        int level = 0;


        int Score {
            get { return score; }
            set
            {
                score = value;
                DrawBlock(30, 20, "                     ");
                DrawBlock(30, 20, "Score：" + score);
                if (score%5==0)
                {
                    MoreHarder();
                }
            }
        }
        //Map
        int[,] Map = new int[10, 20];
        //初始化
        void Init()
        {
            Score = 0;
            //初始化地图
            for (int x = -1; x <= MaxX; x++)
            {
                for (int y = -1; y <= MaxY; y++)
                {
                    if (y == -1 || y == MaxY)
                    {
                        MoveMouse(x, y);
                        Console.Write("-");
                    }

                    if (x == -1 || x == MaxX)
                    {
                        MoveMouse(x, y);
                        Console.Write("|");
                    }
                }
            }
        }

        //判断是否吃球
        bool EatBall(List<Point> SnakeBody,Point ball)
        {
            if(SnakeBody.Last().X==ball.X&&SnakeBody.Last().Y==ball.Y)
            {
                CreatNewBall(Map);
                Score++;
                return true;
            }
            return false;
        }

        bool auto;
        //开始游戏主逻辑
        public void StartRun()
        {
            foreach (var sn in snake.SnakeBody)//生成蛇的身体
            {
                Map[sn.X, sn.Y] = 2;
            }
            CreatNewBall(Map);//生成球
            DrawBlock(30, 20, "Score："+score);

            while (!IsEndGame)
            {
                //判断游戏结束?
                if (Score > 80)
                {
                    //获胜
                    DrawBlock(30, 0, "获胜");
                    break;
                }
                if (snake.IsDead())
                {
                    DrawBlock(30, 0, "GAME OVER");
                    break;
                }

                if (Console.KeyAvailable)//当键盘输入
                {
                    var key = Console.ReadKey(true);
                    bool yeah = snake.ChangeForward(key.Key);
                }

                if (Environment.TickCount - RefreshTime > Speed)//蛇移动
                {
                    RefreshTime = Environment.TickCount;
                    Map[snake.SnakeBody[0].X, snake.SnakeBody[0].Y] = 0;
                    snake.AutoMove();

                    if (EatBall(snake.SnakeBody, ball))//判断移动后是否吃了球
                    {
                        snake.AddTail();
                    }
                }

                try
                {
                    foreach (var sn in snake.SnakeBody)//刷新蛇位置
                    {
                        Map[sn.X, sn.Y] = 2;
                    }
                    Map[snake.SnakeBody[snake.SnakeBody.Count - 1].X, snake.SnakeBody[snake.SnakeBody.Count - 1].Y] = 4;
                }
                catch//当超出界限判断死亡
                {
                    DrawBlock(30, 0, "GAME OVER");
                    break;
                }

                for (int i=0;i<10;i++)//生成图像
                {
                    for (int j = 0; j < 20; j++)
                    {
                        if (Map[i, j] == 2)
                        {
                            DrawBlock(i * 2, j,"●");//身体
                        }
                        else if (Map[i, j] == 1)
                        {
                            DrawBlock(i * 2, j, "○");//球
                        }
                        else if (Map[i, j] == 0)
                        {
                            DrawBlock(i * 2, j, " ");//空
                        }
                        else if(Map[i, j] == 4)//蛇头
                        {
                            DrawBlock(i * 2, j, "◆");
                        }
                    }               
                }             
            }
        }

        void MoreHarder()//增加难度
        {
            level++;
            DrawBlock(30, 10, "                     ");
            DrawBlock(30, 10, "LEVEL：" + level);
            switch (level)
            {
                case 1:
                    Speed = 600;
                    break;
                case 2:
                    Speed = 400;
                    break;
                case 3:
                    Speed = 300;
                    break;
                case 4:
                    Speed = 200;
                    break;
                case 5:
                    Speed = 100;
                    break;
                case 6:
                    Speed = 50;
                    break;
            }

        }

        Point ball;
        //生成新的球
        void CreatNewBall(int[,] map)
        {
            List<Point> list = new List<Point>();//在剩余的空格中随机生成一个球
            for(int i=0;i<map.GetLength(0);i++)
            {
                for(int j=0;j<map.GetLength(1);j++)
                {
                    if(map[i,j]==0)
                    {
                        list.Add(new Point(i, j));
                    }
                }
            }
            Random rand = new Random();
            ball = list[rand.Next(list.Count)];
            map[ball.X, ball.Y] = 1;
        }

        void MoveMouse(int x,int y)
        {
            Console.SetCursorPosition(x+5, y+5);
        }

        void DrawBlock(int x,int y)
        {
            MoveMouse(x, y);
            Console.Write("●");
        }
        void DrawBlock(int x, int y,string draw)
        {
            MoveMouse(x, y);
            Console.Write(draw);
        }
        void DrawBall(int x, int y)
        {
            MoveMouse(x, y);
            Console.Write("○");
        }

        void ClearBlock(int x, int y)
        {
            MoveMouse(x, y);
            Console.Write(" ");
        }

        void Test()
        {
            for(int x=0;x<MaxX;x+=2)
            {
                DrawBlock(x, 0);
            }
            for (int y = 0; y < MaxY; y ++)
            {
                DrawBlock(0, y);
            }
        }
    }
}
