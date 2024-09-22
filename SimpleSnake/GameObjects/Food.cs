using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public abstract class Food : Point
    {
        private Wall _wall;
        private Random _random;
        private char _foodSymbol;

        protected Food(Wall wall, char footSymbol, int points) : base(wall.LeftX, wall.TopY)
        {
            _wall = wall;
            _random = new Random();
            _foodSymbol = footSymbol;
            FoodPoints = points;
        }

        public int FoodPoints { get; set; }

        public void SetRandomPosition(Queue<Point> snakeElements)
        {
            LeftX = _random.Next(2, _wall.LeftX - 2);
            TopY = _random.Next(2, _wall.TopY - 2);

            bool isPointOfSnake = snakeElements.Any(x=> x.LeftX == LeftX && x.TopY == TopY);

            while (isPointOfSnake)
            {
                LeftX = _random.Next(2, _wall.LeftX - 2);
                TopY = _random.Next(2, _wall.TopY - 2);

                isPointOfSnake = snakeElements.Any(x => x.LeftX == LeftX && x.TopY == TopY);
            }

            Console.BackgroundColor = ConsoleColor.Red;
            Draw(_foodSymbol);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public bool IsFoodPoint(Point snake)
        {
            return snake.TopY == TopY && snake.LeftX == LeftX;
        }
    }
}
