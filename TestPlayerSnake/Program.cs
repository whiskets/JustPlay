using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlayerSnake
{
    class Program
    {
        static void Main(string[] args)
        {/*
            List<Point> np = new List<Point>();
            np.Add(new Point(1, 1));
            np.Add(new Point(2, 2));
            np.Add(new Point(3, 3));
            np.Add(new Point(4, 4));
            Point mm = np[0];
            for(int i=0;i<np.Count-1;i++)
            {
                np[i] = np[i + 1];
            }

            Console.WriteLine(mm.X);
            Console.WriteLine(np[0].X);
            Console.ReadKey();*/
            
            Console.CursorVisible = false;
            SnakeClass game = new SnakeClass();
            game.StartRun();

            while(true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                    break;
            }
        }
    }
}
