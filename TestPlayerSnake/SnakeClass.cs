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
            public int Speed = 50;
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
            }

            public Forward GetFangxiang()
            {
                return Fangxiang;
            }

            //自动前进
            public void AutoMove()
            {
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

        //坐标点
        class Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
            public Point(Point point)
            {
                X = point.X;
                Y = point.Y;
            }
            public int X { get; set; }
            public int Y { get; set; }
        }




        public SnakeClass()
        {
            Init();
        }

        int MaxX = 20, MaxY = 20;
        bool IsEndGame = false;
        int RefreshTime;//刷新时间
        Snake snake = new Snake();
        int Score = 0;

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

        bool auto;
        //开始游戏主逻辑
        public void StartRun()
        {
            while (!IsEndGame)
            {
                if(Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    bool yeah=snake.ChangeForward(key.Key);
                    /*if (yeah)
                    {
                        RefreshTime = Environment.TickCount;
                        Map[snake.SnakeBody[0].X, snake.SnakeBody[0].Y] = 0;
                        snake.AutoMove();
                    }*/
                }
                //else
                if(Environment.TickCount-RefreshTime> snake.Speed)
                {
                    RefreshTime = Environment.TickCount;
                    Map[snake.SnakeBody[0].X, snake.SnakeBody[0].Y] = 0;
                    snake.AutoMove();
                }

                foreach(var sn in snake.SnakeBody)
                {
                    Map[sn.X, sn.Y] = 2;                                   
                }

                Map[snake.SnakeBody[snake.SnakeBody.Count-1].X, snake.SnakeBody[snake.SnakeBody.Count - 1].Y] = 4;
                for (int i=0;i<10;i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        if (Map[i, j] == 2)
                        {
                            DrawBlock(i * 2, j,"●");
                        }
                        else if (Map[i, j] == 1)
                        {
                            DrawBlock(i * 2, j, "○");
                        }
                        else if (Map[i, j] == 0)
                        {
                            DrawBlock(i * 2, j, " ");
                        }
                        else if(Map[i, j] == 4)
                        {
                            DrawBlock(i * 2, j, "◆");
                        }
                    }               
                }
            }
        }

        //生成新的球
        void CreatNewBall()
        {
            Random rand = new Random();
            int x = rand.Next(0, 11);
            int y = rand.Next(0, 11);
            Point ball = new Point(x,y);
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
