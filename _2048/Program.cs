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
                        if (b.isGameOver())
                        {
                            Console.WriteLine("game over");
                            break;
                        }
                        b.moveRight();
                        continue;

                    case ConsoleKey.LeftArrow:
                        Console.WriteLine("left");
                        if (b.isGameOver())
                        {
                            Console.WriteLine("game over");
                            break;
                        }
                        b.moveLeft();
                        continue;

                    case ConsoleKey.UpArrow:
                        Console.WriteLine("up");
                        if (b.isGameOver())
                        {
                            Console.WriteLine("game over");
                            break;
                        }
                        b.moveUp();
                        continue;

                    case ConsoleKey.DownArrow:
                        Console.WriteLine("down");
                        if (b.isGameOver())
                        {
                            Console.WriteLine("game over");
                            break;
                        }
                        b.moveDown();
                        continue;
                }

            }
        }
    }
}
