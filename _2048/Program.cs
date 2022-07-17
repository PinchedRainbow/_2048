using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace _2048
{
    internal class Program
    {
        // initialising variables
        private static bool _quit = false;
        private static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), @"2048_Console\");
        private static bool AI = false;
        
        private static void Main(string[] args)
        {
            var connection = new SqliteConnection("Data Source=Leaderboard.db");
            if (!File.Exists("Leaderboard.db"))
            {

                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "CREATE TABLE Leaderboard(Name VARCHAR(100), Score INTEGER, BoardSize INTEGER, PRIMARY KEY (Name))";
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            // check if directory for saving game exists
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            
            // ask if user wants to play against AI
            Console.WriteLine("Do you want the moves to be random {Faheem's AI}? (y/n)");
            string aiAns = Console.ReadLine();
            if (aiAns == "y")
            {
                AI = true;
            }

            // ask user if they want to see leaderboard
            Console.WriteLine("Do you want to see the leaderboard? \n1. View all scores \n2. View individual score \n3. No");
            int tempNum = Convert.ToInt32(Console.ReadLine());
            switch (tempNum)
            {
                case 1:
                    Console.WriteLine("Enter the size of the board you would like to view the leaderboard for:");
                    int tempsize = Convert.ToInt32(Console.ReadLine());
                    ViewAllScores(connection, tempsize);
                    break;

                case 2:
                    Console.WriteLine("Enter your name:");
                    string tempname = Console.ReadLine();
                    Console.WriteLine("Enter the size of the board you would like to view the score for:");
                    int tempsize2 = Convert.ToInt32(Console.ReadLine());
                    DisplayIndividualScore(connection, tempname, tempsize2);
                    break;

                case 3:
                    break;
            }



            // ask user if they want to load game or start new one
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
                PlayGame(new Board(GetBoardSize(),0), false, AI);
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
            // read second line of file to get score
            int score = int.Parse(game.Split('\n')[1]);
            var b = new Board(boardSize,score);
            // parse file to board
            b.ReadBoard(game);
            PlayGame(b, true, AI);
        }
        
        private static void PlayGame(Board b, bool loadedGame, bool AI)
        {
            while (true)
            {
                // checks if the game was loaded from file or not
                // if the game was loaded from file, the board is already initialized and it doesnt need to have a random tile added to it
                if (loadedGame) loadedGame = false;
                else b.GenRandomTile();
                
                b.PrintBoard();
                Console.WriteLine("Your current score is {0}", b.Score);
                Console.WriteLine("(L)eft, (R)ight, (U)p, (D)own, (S)ave or (Q)uit");
                
                if (AI)
                {
                    // make random move for AI
                    // choose random direction
                    var rand = new Random();
                    int randDir = rand.Next(0, 4);
                    // make move
                    switch (randDir)
                    {
                        case 0:
                            // left
                            LeftFunction(b);
                            break;
                        case 1:
                            // right
                            RightFunction(b);
                            break;
                        case 2:
                            // up
                            UpFunction(b);
                            break;
                        case 3:
                            // down
                            DownFunction(b);
                            break;
                    }
                }
                else
                {
                    // human is playing, get input from user
                    var ch = Console.ReadKey(false).Key;
                    switch (ch)
                    {
                        case ConsoleKey.RightArrow:
                            RightFunction(b);
                            continue;

                        case ConsoleKey.LeftArrow:
                            LeftFunction(b);
                            continue;

                        case ConsoleKey.UpArrow:
                            UpFunction(b);
                            continue;

                        case ConsoleKey.DownArrow:
                            DownFunction(b);
                            continue;
                    
                        case ConsoleKey.S:
                            // save game
                            SaveGame(b);
                            continue;
                    
                        case ConsoleKey.Q:
                            // quits game
                            _quit = true;
                            break;
                    }
                    
                }
                
                if (_quit)
                {
                    Console.WriteLine("\nQuitting game...");
                    AskToSaveScore(new SqliteConnection("Data Source=Leaderboard.db"), b.Score, b.getSize());
                    break;
                }
            }
        }


        private static void LeftFunction(Board b)
        {
            Console.WriteLine("Left move made");
            CheckForGameOver(b);
            b.moveLeft();
            b.addScore(b.mergeTilesOnLeft());
            b.moveLeft();
        }
        
        private static void UpFunction(Board b)
        {
            Console.WriteLine("Up move made");
            CheckForGameOver(b);
            b.moveUp();
            b.addScore(b.mergeTilesOnUp());
            b.moveUp();
        }

        private static void DownFunction(Board b)
        {
            Console.WriteLine("Down move made");
            CheckForGameOver(b);
            b.moveDown();
            b.addScore(b.mergeTilesOnDown());
            b.moveDown();
        }

        private static void RightFunction(Board b)
        {
            Console.WriteLine("Right move made");
            CheckForGameOver(b);
            b.moveRight();
            b.addScore(b.mergeTilesOnRight());
            b.moveRight();
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
            else
            {
                // ensure that board size is between 2 and 20
                size = int.Parse(boardSize);
                while (size < 2 || size > 20)
                {
                    Console.WriteLine("Invalid board size. Enter a board size: ");
                    size = int.Parse(Console.ReadLine()!);
                }
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

        private static void AskToSaveScore(SqliteConnection conn, int Score, int BoardSize)
        {
            Console.WriteLine("Would you like to save score? Y/N");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                Console.WriteLine("Enter your name");
                string name = Console.ReadLine();
                InsertValues(conn, name, Score, BoardSize);
            }
            else
            {
                Console.WriteLine("Score not saved");
            }
        }

        static bool CheckIfUserExists(SqliteConnection conn, string Name)
        {
            bool ans = false;
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM Leaderboard WHERE Name='" + Name + "'";
            int userCount = Convert.ToInt32(cmd.ExecuteScalar());
            if (userCount > 0)
            {
                ans = true;
            }
            return ans;
        }

        private static void InsertValues(SqliteConnection conn, string Name, int Score, int BoardSize)
        {
            if (CheckIfUserExists(conn, Name) && Score > GetScoreFromDB(conn, Name, BoardSize))
            {
                Console.WriteLine("There already exists a lower score with the current name, update the score? Y/N");
                if (Console.ReadLine().ToUpper() == "Y")
                {
                    conn.Open();
                    var Cmd = conn.CreateCommand();
                    Cmd.CommandText = "UPDATE Leaderboard SET Score='" + Score + "' WHERE Name='" + Name + "'";
                    Cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            else
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Leaderboard VALUES (@name, @score, @boardsize)";
                cmd.Parameters.AddWithValue("@name", Name);
                cmd.Parameters.AddWithValue("@score", Score);
                cmd.Parameters.AddWithValue("@boardsize", BoardSize);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private static void DisplayIndividualScore(SqliteConnection conn, string Name, int BoardSize)
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Leaderboard WHERE Name='" + Name + "' AND BoardSize='" + BoardSize + "'";
            var reader = cmd.ExecuteReader();
            reader.Read();
            Console.WriteLine("Name = {0} \nScore = {1}", reader["Name"].ToString(), reader["Score"].ToString());
            conn.Close();
        }

        private static int GetScoreFromDB(SqliteConnection conn, string Name, int BoardSize)
        {
            int score;
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Leaderboard WHERE Name='" + Name + "' AND BoardSize='" + BoardSize + "'";
            var reader = cmd.ExecuteReader();
            reader.Read();
            score = Convert.ToInt32(reader["Score"]);
            conn.Close();
            return score;
        }
        private static void ViewAllScores(SqliteConnection conn, int BoardSize)
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Leaderboard WHERE BoardSize='" + BoardSize + "' ORDER BY Score ASC";
            var reader = cmd.ExecuteReader();
            reader.Read();

            conn.Close();
            //Idk what to do from here;

        }
    }
}
