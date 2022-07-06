using System;
using System.Diagnostics;
using System.Linq;

namespace _2048
{
    public class Board
    {
        private readonly Tile[,] board;
        private readonly int size;
        public int Score { get; set; }

        
        
        public Board(int size, int Score)
        {
            this.size = size;
            this.Score = Score;
            board = new Tile[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = new Tile(0, "", i, j);
                }
            }
        }

        public string BoardString()
        {
            var query = board.Cast<Tile>().Select(t => t.Number.ToString());
            //var query = from Tile t in board select t.Number.ToString();
            return String.Join(" ", query);
        }

        // save the current board into a string 
        public override string ToString()
        {
            string save = "";
            // put board size first, then score on the next line.
            save += size + "\n" + Score + "\n";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    save += board[i, j].Number + " ";
                }
                save += "\n";
            }
            
            return save;
        }

        // method to read the board from a string
        public void ReadBoard(string save)
        {
            string[] lines = save.Split('\n');
            for (int i = 2; i < size + 2; i++)
            {
                string[] numbers = lines[i].Split(' ');
                for (int j = 0; j < size; j++)
                {
                    board[i - 2, j].Number = int.Parse(numbers[j]);
                }
            }
        }
        

        public void PrintBoard()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j].Number.ToString().Length == 5) Console.Write(board[i, j].Number + " ");
                    else if (board[i, j].Number.ToString().Length == 4) Console.Write(board[i, j].Number + "  ");
                    else if (board[i, j].Number.ToString().Length == 3) Console.Write(board[i, j].Number + "   ");
                    else if (board[i, j].Number.ToString().Length == 2) Console.Write(board[i, j].Number + "    ");
                    else Console.Write(board[i, j].Number + "     ");
                }
                Console.WriteLine("\n\n");
            }
        }
        
        public int GenRandomTile()
        {
            int x, y, value;
            var rnd = new Random();
            while (true)
            {
                x = rnd.Next(0, size);
                y = rnd.Next(0, size);
                if (board[y, x].Number != 0) continue;
                value = rnd.Next(0,10) < 9 ? 2 : 4;
                board[y, x].Number = value;
                Console.WriteLine("Generated tile at row:{0}, column:{1}", y,x);
                break;
            }
            return value;
        }

        public bool IsGameOver()
        {
            if (BoardString().Contains("0"))
            {
                return false;
            }
            
            for (int y = 1; y < size - 1; y++)
            {
                for (int x = 1; x < size - 1; x++)
                {
                    if (board[y, x].Number == board[y + 1, x].Number ||
                        board[y, x].Number == board[y - 1, x].Number ||
                        board[y, x].Number == board[y, x + 1].Number ||
                        board[y, x].Number == board[y, x - 1].Number)
                    {
                        return false;
                    }
                }
            }

            for (int x = 0; x < size - 1; x++)
            {
                if (board[0, x].Number == board[0, x + 1].Number)
                {
                    return false;
                }
                if (board[size - 1, x].Number == board[size - 1 , x + 1].Number)
                {
                    return false;
                }
            }

            for (int y = 0; y < size - 1; y++)
            {
                if (board[y, 0].Number == board[y + 1, 0].Number)
                {
                    return false;
                }
                if (board[y, size - 1].Number == board[y + 1, size - 1].Number)
                {
                    return false;
                }
            }
            return true;
        }

        public void addScore(int x)
        {
            Score += x;
        }

        public void moveLeft()
        {
            //please move the tiles to the left
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j].Number == 0)
                    {
                        for (int k = j + 1; k < size; k++)
                        {
                            if (board[i, k].Number != 0)
                            {
                                board[i, j].Number = board[i, k].Number;
                                board[i, k].Number = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void moveRight()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = size - 1; j > 0; j--)
                {
                    if (board[i, j].Number == 0)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (board[i, k].Number != 0)
                            {
                                board[i, j].Number = board[i, k].Number;
                                board[i, k].Number = 0;
                                break;
                            }
                        }
                    }
                }
            }
            
            
        }

        public void moveUp()
        {
            //please move the tiles to the up
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[j, i].Number != 0)
                    {
                        continue;
                    }

                    for (int k = j + 1; k < size; k++)
                    {
                        if (board[k, i].Number == 0)
                        {
                            continue;
                        }
                        board[j, i].Number = board[k, i].Number;
                        board[k, i].Number = 0;
                        break;
                    }
                }
            }
            
            
            
        }

        public void moveDown()
        {
            // PLEASE MOVE TILES TO THE DOWN
            for (int i = 0; i < size; i++)
            {
                for (int j = size - 1; j > 0; j--)
                {
                    if (board[j, i].Number == 0)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (board[k, i].Number != 0)
                            {
                                board[j, i].Number = board[k, i].Number;
                                board[k, i].Number = 0;
                                break;
                            }
                        }
                    }
                }
            }
            
        }

        public int mergeTilesOnDown()
        {
            int total = 0;

            for (int x = 0; x < size; x++)
            {
                for (int y = size - 1; y > 0; y--)
                {
                    if (board[y, x].Number == 0)
                    {
                        continue;
                    }
                    
                    if (board[y, x].Number == board[y - 1, x].Number)
                    {
                        board[y, x].Number *= 2;
                        total += board[y, x].Number;
                        board[y - 1, x].Number = 0;
                    }
                }
            }
            return total;
        }
        public int mergeTilesOnUp()
        {
            int total = 0;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size - 1; y++)
                {
                    if (board[y, x].Number == 0)
                    {
                        continue;
                    }
                    
                    if (board[y, x].Number == board[y + 1, x].Number)
                    {
                        board[y, x].Number *= 2;
                        total += board[y, x].Number;
                        board[y + 1, x].Number = 0;
                    }
                }
            }
            return total;
        }
        
        
        public int mergeTilesOnRight()
        {
            int total = 0;
            for (int y = 0; y < size; y++)
            {
                for (int x = size - 1; x > 0; x--)
                {
                    if (board[y, x].Number == 0)
                    {
                        continue;
                    }
                    
                    if (board[y, x].Number == board[y, x - 1].Number)
                    {
                        board[y, x].Number *= 2;
                        total += board[y, x].Number;
                        board[y, x - 1].Number = 0;
                    }
                }
            }

            return total;
        }
        
        public int mergeTilesOnLeft()
        {
            int total = 0;
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size - 1; x++)
                {
                    if (board[y, x].Number == 0)
                    {
                        continue;
                    }
                    if (board[y, x].Number == board[y, x + 1].Number)
                    {
                        board[y, x].Number *= 2;
                        total += board[y, x].Number;
                        board[y, x + 1].Number = 0;
                    }
                }
            }
            return total;
        }
  
    }
}
