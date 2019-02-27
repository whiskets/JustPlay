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
        {
            Console.CursorVisible = false;
            SnakeClass game = new SnakeClass();
            game.StartRun();
        }
    }
}
