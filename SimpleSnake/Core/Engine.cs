using SimpleSnake.Enums;
using SimpleSnake.GameObjects;
using System;
using System.Threading;

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
            _direction = Direction.Right; // Set default direction to Right
        }

        public void Run()
        {
            CreateDirections();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    GetNextDirection();
                }

                bool isMoving = _snake.IsMoving(_pointsOFDirections[Convert.ToInt32(_direction)]);

                if (!isMoving)
                {
                    if (!AskUserToRestart()) // if player doesn't choose to restart
                    {
                        break; // exit the game loop
                    }
                    else
                    {
                        RestartGame(); // restart the game
                    }
                }

                _sleepTime -= 0.01;
                Thread.Sleep((int)_sleepTime);
            }
        }

        private void CreateDirections()
        {
            _pointsOFDirections[0] = new Point(1, 0);  // Right
            _pointsOFDirections[1] = new Point(-1, 0); // Left
            _pointsOFDirections[2] = new Point(0, 1);  // Down
            _pointsOFDirections[3] = new Point(0, -1); // Up
        }

        private void GetNextDirection()
        {
            ConsoleKeyInfo userInput = Console.ReadKey();

            if (userInput.Key == ConsoleKey.LeftArrow && _direction != Direction.Right)
            {
                _direction = Direction.Left;
            }
            else if (userInput.Key == ConsoleKey.RightArrow && _direction != Direction.Left)
            {
                _direction = Direction.Right;
            }
            else if (userInput.Key == ConsoleKey.UpArrow && _direction != Direction.Down)
            {
                _direction = Direction.Up;
            }
            else if (userInput.Key == ConsoleKey.DownArrow && _direction != Direction.Up)
            {
                _direction = Direction.Down;
            }

            Console.CursorVisible = false;
        }

        private bool AskUserToRestart()
        {
            Console.SetCursorPosition(_wall.LeftX + 1, 3); // Move cursor out of game area
            Console.WriteLine("Game Over! Would you like to continue? y/n");

            char input = Console.ReadKey().KeyChar;

            if (input == 'y')
            {
                return true; // Restart the game
            }
            else if (input == 'n')
            {
                StopGame(); // Exit game
                return false;
            }

            return false;
        }

        private void RestartGame()
        {
            Console.Clear(); // Clear the console

            // Reinitialize snake, wall, and reset other game settings
            Wall wall = new Wall(60, 20);
            Snake snake = new Snake(wall);

            // Re-initialize the engine with the new game state
            Engine engine = new Engine(wall, snake);

            engine.Run(); // Start the game again
        }

        private void StopGame()
        {
            Console.Clear();
            Console.SetCursorPosition(20, 10);
            Console.WriteLine("Game over! Thanks for playing.");
            Environment.Exit(0);
        }
    }
}
