using System;

namespace _2048
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int Size = 4;
            var b = new Board(Size);
            b.genRandomTile();
            while (true)
            {
                b.genRandomTile();
                b.PrintBoard();
                var ch = Console.ReadKey(false).Key;
                switch (ch)
                {
                    case ConsoleKey.RightArrow:
                        Console.WriteLine("right");
                        b.moveRight();
                        if (b.isGameOver())
                        {
                            Console.WriteLine("game over");
                            break;
                        }

                        continue;
                    case ConsoleKey.LeftArrow:
                        Console.WriteLine("left");
                        b.moveLeft();
                        if (b.isGameOver())
                        {
                            Console.WriteLine("game over");
                            break;
                        }

                        continue;
                    case ConsoleKey.UpArrow:
                        Console.WriteLine("up");
                        b.moveUp();
                        if (b.isGameOver())
                        {
                            Console.WriteLine("game over");
                            break;
                        }

                        continue;
                    case ConsoleKey.DownArrow:
                        Console.WriteLine("down");
                        b.moveDown();
                        if (b.isGameOver())
                        {
                            Console.WriteLine("game over");
                            break;
                        }

                        continue;
                }

            }
        }
    }
}