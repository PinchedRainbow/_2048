using System;

namespace _2048
{
    class Program
    {
        static void Main(string[] args)
        {
            int Size = 4;

            Board b = new Board(Size);
            b.PrintBoard();
            
            while (true)
            {
                var ch = Console.ReadKey(false).Key;
                switch(ch)
                {
                    case ConsoleKey.RightArrow:
                        b.isGameOver();
                        Console.WriteLine("right");
                        continue;
                    case ConsoleKey.LeftArrow:
                        b.isGameOver();
                        Console.WriteLine("left");
                        continue;
                    case ConsoleKey.UpArrow:
                        b.isGameOver();
                        Console.WriteLine("up");
                        continue;
                    case ConsoleKey.DownArrow:
                        b.isGameOver();
                        Console.WriteLine("down");
                        continue;
                }
            }


        }
    }
}