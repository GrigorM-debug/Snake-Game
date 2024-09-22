using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public class Point
    {
        private int _leftX;
        private int _topY;

        public Point(int leftX, int topY)
        {
            _leftX = leftX;
            _topY = topY;
        }

        public int LeftX {  get; set; }

        public int TopY { get; set; }

        public void Draw(char symbol)
        {
            Console.SetCursorPosition(LeftX, TopY);
            Console.Write(symbol);
        }

        public void Draw(int leftX, int topY, char symbol)
        {
            Console.SetCursorPosition(leftX, topY);
            Console.Write(symbol);
        }

        public bool IsAtSamePoint(Point otherPoint)
        {
            return this.LeftX == otherPoint.LeftX && this.TopY == otherPoint.TopY;
        }

    }
}
