using SimpleSnake.GameObjects.Foods;
using System;
using System.Collections.Generic;
using System.Linq;

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
            _food[2] = new FoodAsterisk(_wall);
        }

        private Point GetNextPoint(Point direction, Point snakeHead)
        {
            _nextLeftX = snakeHead.LeftX + direction.LeftX;
            _nextTopY = snakeHead.TopY + direction.TopY;

            return new Point(_nextLeftX, _nextTopY);
        }

        public bool IsMoving(Point direction)
        {
            Point currentSnakeHead = _snakeElements.Last();

            Point nextPosition = GetNextPoint(direction, currentSnakeHead);

            // Check for collision with wall
            if (_wall.IsPointOfWall(nextPosition))
            {
                return false;  // Game over
            }

            // Check for collision with itself (snake's body)
            if (this._snakeElements.Any(s => s.IsAtSamePoint(nextPosition)))
            {
                return false;  // Game over
            }

            // If no collision, snake moves forward
            _snakeElements.Enqueue(nextPosition);  // Move snake forward by adding new head

            Point tail = _snakeElements.Dequeue();  // Remove tail to simulate movement

            // Draw new snake head
            nextPosition.Draw(_snakeSymbol);

            // Clear the old tail position
            tail.Draw(' ');

            return true;
        }

        private void Eat(Point direction, Point currentSnakeHead)
        {
            int length = _food[_foodIndex].FoodPoints;

            for (int i = 0; i < length; i++)
            {
                _snakeElements.Enqueue(new Point(_nextLeftX, _nextTopY));
                GetNextPoint(direction, currentSnakeHead);
            }

            _foodIndex = RandomFoodNumber;
            _food[_foodIndex].SetRandomPosition(_snakeElements);
        }


    }
}
