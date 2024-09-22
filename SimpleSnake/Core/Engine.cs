using SimpleSnake.Enums;
using SimpleSnake.GameObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSnake.Core
{
    public class Engine
    {
        private Wall _wall;
        private Snake _snake;
        private double _sleepTime;
        private Point[] _pointsOFDirections;
        private Direction _direction;

        public Engine(Wall wall, Snake snake)
        {
            _wall = wall;
            _snake = snake;
            _pointsOFDirections = new Point[4];
            _sleepTime = 100;
        }

        public void Run()
        {
            CreateDirections();

            while(true)
            {
                if (Console.KeyAvailable)
                {
                    GetNextDirection();
                }

                bool isMoving = _snake.IsMoving(_pointsOFDirections[Convert.ToInt32(_direction)]);

                if (!isMoving)
                {
                    AskUserToRestard();
                    break;
                }

                _sleepTime -= 0.01;

                Thread.Sleep((int)_sleepTime);  
            }
        }

        private void CreateDirections()
        {
            _pointsOFDirections[0] = new Point(1, 0);
            _pointsOFDirections[1] = new Point(-1, 0);
            _pointsOFDirections[2] = new Point(0,1);
            _pointsOFDirections[3] = new Point(0,-1);
        }

        private void GetNextDirection()
        {
            ConsoleKeyInfo userInput = Console.ReadKey();

            if (userInput.Key == ConsoleKey.LeftArrow)
            {
                if (_direction != Direction.Right)
                {
                    _direction = Direction.Left;
                }
            }
            else if (userInput.Key == ConsoleKey.RightArrow)
            {
                if (_direction != Direction.Left)
                {
                    _direction = Direction.Right;
                }
            }
            else if (userInput.Key == ConsoleKey.UpArrow)
            {
                if(_direction != Direction.Down)
                    {
                    _direction = Direction.Up;
                }
            }
            else if (userInput.Key == ConsoleKey.DownArrow)
            {
                if (_direction != Direction.Up)
                {
                    _direction = Direction.Down;
                }
            }

            Console.CursorVisible = false;
        }

        private void AskUserToRestard()
        {
            int leftX = _wall.LeftX + 1;
            int topY = 3;

            Console.SetCursorPosition(leftX, topY);
            Console.WriteLine("Would you like to continue? y/n");

            //string input = Console.ReadLine()?.Trim().ToLower();

            //if (input == "y")
            //{
            //    Console.Clear();
            //    StartUp.Main();
            //}
            //else if(input == "n") 
            //{
            //    StopGame();
            //}
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Y)
            {
                Console.Clear();
                StartUp.Main();
            }
            else if (key.Key == ConsoleKey.N)
            {
                StopGame();
            }
        }

        private void StopGame()
        {
            Console.SetCursorPosition(20, 10);
            Console.Write("Game over!");
            Environment.Exit(0);
        }
    }
}
