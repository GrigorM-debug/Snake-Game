using SimpleSnake.GameObjects.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleSnake.GameObjects
{
    public class Snake
    {
        private Wall _wall;
        private Queue<Point> _snakeElements;
        private Food[] _food;
        private int _foodIndex;
        private int _nextLeftX;
        private int _nextTopY;
        private const char _snakeSymbol = '\u25CF';

        public Snake(Wall wall)
        {
            _wall = wall;
            _snakeElements = new Queue<Point>();
            _food = new Food[3];
            _foodIndex = RandomFoodNumber;
            CreateSnake();
            GetFood();
        }

        private void CreateSnake()
        {
            for (int topY = 0; topY < 6; topY++)
            {
                _snakeElements.Enqueue(new Point(2, topY));
            }
        }

        private int RandomFoodNumber => new Random().Next(0, _food.Length);

        private void GetFood()
        {
            _food[0] = new FoodHash(_wall);
            _food[1] = new FoodDollar(_wall);
            _food[2] = new FoodDollar(_wall);
        }

        private void GetNextPoint(Point direction, Point snakeHead)
        {
            _nextLeftX = snakeHead.LeftX + direction.LeftX;
            _nextTopY = snakeHead.TopY + direction.TopY;
        }

        public bool IsMoving(Point direction) 
        { 
            Point currentSnakeHead = _snakeElements.Last();

            GetNextPoint(direction, currentSnakeHead);

            bool isPointOfSnake = _snakeElements.Any(x=>x.LeftX == _nextLeftX || x.TopY == _nextTopY);

            if (isPointOfSnake)
            {
                return false;
            }

            Point snakeNewHead = new Point(_nextLeftX, _nextTopY);

            if (_wall.IsPointOfWall(snakeNewHead))
            {
                return false;
            }

            _snakeElements.Enqueue(snakeNewHead);
            snakeNewHead.Draw(_snakeSymbol);

            if (_food[_foodIndex].IsFoodPoint(currentSnakeHead))
            {
                Eat(direction, currentSnakeHead);
            }

            Point snakeTail = _snakeElements.Dequeue();
            snakeTail.Draw(' ');

            return true;
        }

        private void Eat(Point direction, Point currentSnakeHead)
        {
            int lenght = _food[_foodIndex].FoodPoints;

            for (int i = 0; i < lenght; i++)
            {
                _snakeElements.Enqueue(new Point(_nextLeftX, _nextTopY));
                GetNextPoint(direction, currentSnakeHead);
            }

            _foodIndex = RandomFoodNumber;
            _food[_foodIndex].SetRandomPosition(_snakeElements);
        }
    }
}
