using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleSnake.GameObjects
{
    public class Wall : Point
    {
        private const char wallSymbol = '\u25A0';

        public Wall(int leftX, int topY) : base(leftX, topY)
        {
            InitializeWallBordes();
        }

        private void SetHorizontalLine(int topY)
        {
            for(int leftx = 0; leftx < this.LeftX; leftx++)
            {
                this.Draw(leftx, topY, wallSymbol);
            }
        }
        private void SetVerticalLine(int leftX)
        {
            for (int topY = 0; topY < this.TopY; topY++)
            {
                this.Draw(leftX, topY, wallSymbol);
            }
        }

        private void InitializeWallBordes()
        {
            SetHorizontalLine(0);
            SetHorizontalLine(this.TopY);

            SetVerticalLine(0);
            SetVerticalLine(LeftX - 1);
        }

        public bool IsPointOfWall(Point snake)
        {
            return snake.TopY == 0 || snake.LeftX == 0 || snake.LeftX == LeftX || snake.TopY == TopY;
        }
    }
}
