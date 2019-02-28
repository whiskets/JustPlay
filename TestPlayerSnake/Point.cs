using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlayerSnake
{
    //坐标点
    public class Point
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
}
