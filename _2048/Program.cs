using System;

namespace _2048
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int size = 4;
            var b = new Board(size);
            b.GenRandomTile();
            while (true)
            {
                b.GenRandomTile();
                b.PrintBoard();
                var ch = Console.ReadKey(false).Key;
                switch (ch)
                {
                    case ConsoleKey.RightArrow:
                        Console.WriteLine("right");
                        if (b.IsGameOver())
                        {
                            Console.WriteLine("game over");
                            break;
                        }

                        b.MoveRight();
                        continue;

                    case ConsoleKey.LeftArrow:
                        Console.WriteLine("left");
                        if (b.IsGameOver())
                        {
                            Console.WriteLine("game over");
                            break;
                        }

                        b.MoveLeft();
                        continue;

                    case ConsoleKey.UpArrow:
                        Console.WriteLine("up");
                        if (b.IsGameOver())
                        {
                            Console.WriteLine("game over");
                            break;
                        }

                        b.MoveUp();
                        continue;

                    case ConsoleKey.DownArrow:
                        Console.WriteLine("down");
                        if (b.IsGameOver())
                        {
                            Console.WriteLine("game over");
                            break;
                        }

                        b.MoveDown();
                        continue;
                    default:
                        Console.WriteLine("You stoopid, this is not a valid key bozo");
                        continue;
                    
                }
            }
        }
    }
}