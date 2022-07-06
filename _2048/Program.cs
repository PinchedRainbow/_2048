using System;
using System.IO;

namespace _2048
{
    internal class Program
    {
        // initialising variables
        private static bool _quit = false;
        private static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), @"2048_Console\");
        
        private static void Main(string[] args)
        {
            // check if directory for saving game exists
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            // ask user if he wants to load game or start new one
            Console.WriteLine("Do you want to load game or start new one? (L/N)");
            string answer = Console.ReadLine();
            if (answer.ToLower() == "l")
            {
                try
                {
                    // ask user for file name
                    Console.WriteLine("Enter file name:");
                    string fileName = Console.ReadLine();
                    LoadGame(fileName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
            else
            {
                PlayGame(new Board(GetBoardSize()));
            }
        }

        private static void LoadGame(string fileName)
        {
            // get file path
            string gamePath = FilePath + fileName + ".txt";
            // read file from path
            string game = File.ReadAllText(gamePath);
            // read first line of file to get board size
            int boardSize = int.Parse(game.Split('\n')[0]);
            var b = new Board(boardSize);
            // parse file to board
            b.ReadBoard(game);
            PlayGame(b);
        }
        
        private static void PlayGame(Board b)
        {
            while (true)
            {
                b.GenRandomTile();
                b.PrintBoard();
                
                // show the different options for the game
                Console.WriteLine("(L)eft, (R)ight, (U)p, (D)own, (S)ave or (Q)uit");
                var ch = Console.ReadKey(false).Key;
                switch (ch)
                {
                    case ConsoleKey.RightArrow:
                        Console.WriteLine("Right move made");
                        CheckForGameOver(b);
                        b.moveRight();
                        continue;

                    case ConsoleKey.LeftArrow:
                        Console.WriteLine("Left move made");
                        CheckForGameOver(b);
                        b.moveLeft();
                        continue;

                    case ConsoleKey.UpArrow:
                        Console.WriteLine("Up move made");
                        CheckForGameOver(b);
                        b.moveUp();
                        continue;

                    case ConsoleKey.DownArrow:
                        Console.WriteLine("Down move made");
                        CheckForGameOver(b);
                        b.moveDown();
                        continue;
                    
                    case ConsoleKey.S:
                        // save game
                        SaveGame(b);
                        continue;
                    
                    case ConsoleKey.Q:
                        // quits game
                        Console.WriteLine("\nQuitting game...");       
                        _quit = true;
                        break;
                }

                if (_quit) break;
            }
        }

        private static void CheckForGameOver(Board b)
        {
            if (b.IsGameOver())
            {
                Console.WriteLine("Game over");
                _quit = true;
            }
        }
        
        private static int GetBoardSize()
        {
            int size = 0;
            Console.WriteLine("Enter a board size (leave empty for default of 4) : ");
            string boardSize = Console.ReadLine();
            if (boardSize == "")
            {
                size = 4;
            }
            // ensure that board size is between 3 and 10
            while (size < 3 || size > 10)
            {
                Console.WriteLine("Invalid board size. Enter a board size: ");
                size = int.Parse(Console.ReadLine()!);
            }
            
            return size;
        }

        private static void SaveGame(Board board)
        {
            Console.WriteLine("\nEnter file name: ");
            string fileName = Console.ReadLine() + ".txt";
            string currentGame = board.ToString();
            string fullfilePath = FilePath + fileName;
                        
            // check if file exists
            if (File.Exists(fullfilePath))
            {
                Console.WriteLine("File already exists. Do you want to overwrite it? (y/n)");
                string answer = Console.ReadLine();
                if (answer == "y")
                {
                    File.WriteAllText(fullfilePath, currentGame);
                    Console.WriteLine("\nGame saved to " + FilePath + fileName);
                    Console.WriteLine("Enter any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Game not saved!");
                    Console.WriteLine("Enter any key to continue...");
                    Console.ReadKey();
                }
            }
            else
            {
                File.WriteAllText(fullfilePath, currentGame);
                Console.WriteLine("\nGame saved to " + FilePath + fileName);
                Console.WriteLine("Enter any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
