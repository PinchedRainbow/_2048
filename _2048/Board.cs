using System;
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
                if (board[x, y].Number != 0) continue;
                value = rnd.Next(0,10) < 9 ? 2 : 4;
                board[x, y].Number = value;
                break;
            }
            return value;
        }

        public bool IsGameOver()
        {
            bool ans = true;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j].Number == 0)
                    {
                        ans = false;
                    }
                }
            }

            for (int i = 0; i < size - 1; i++)
            {
                for (int j = 0; j < size - 1; j++)
                {
                    if (board[i, j].Number == board[i+1,j].Number)
                    {
                        ans = false;
                    }
                    else if (board[i, j].Number == board[i, j + 1].Number)
                    {
                        ans = false;
                    }
                }
            }

            for (int i = 0; i < size - 1; i++)
            {
                if (board[size-1, i].Number == board[size-1, i + 1].Number)
                {
                    ans = false;
                }
                else if (board[i,size-1].Number == board[i+1,size-1].Number)
                {
                    ans = false;
                }
            }

            return ans;
        }

        public void addScore(int x)
        {
            Score += x;
        }

        public void moveLeft()
        {
            //please move the tiles to the left
            for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                if (board[i, j].Number == 0)
                    for (int k = j + 1; k < size; k++)
                        if (board[i, k].Number != 0)
                        {
                            board[i, j].Number = board[i, k].Number;
                            board[i, k].Number = 0;
                            break;
                        }
        }

        public void moveRight()
        {
            for (int i = 0; i < size - 1; i++)
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
            for (int i = 0; i < size; i++)
            {
                for (int j = size-1; j >= 0; j--)
                {
                    if (board[j, i].Number == 0)
                    {
                        continue;
                    }
                    for (int k = j - 1; k >= 0; k--)
                    {
                        if (board[k, i].Number == 0)
                        {
                            continue;
                        }
                        if (board[j, i].Number == board[k, i].Number)
                        {
                            board[j, i].Number += board[k, i].Number;
                            total += board[j, i].Number;
                            board[k, i].Number = 0;
                            break;
                        }
                    }
                }
            }
            return total;
        }

        public int mergeTilesOnRight()
        {
            int total = 0;
            // merge tiles that have been moved to the right, check if adjacant tiles are the same, if so, merge them and add the value of the two tiles to the first tile all the way to the right
            for (int i = 0; i < size; i++)
            {
                for (int j = size-1; j >=0; j--)
                {
                    if (board[i, j].Number == 0)
                    {
                        continue;
                    }

                    for (int k = j - 1; k >= 0; k--)
                    {
                        if (board[i, k].Number == 0)
                        {
                            continue;
                        }
                        if (board[i, j].Number == board[i, k].Number)
                        {
                            board[i, j].Number += board[i,k].Number;
                            total += board[i, j].Number;
                            board[i, k].Number = 0;
                            break;
                        }
                    }
                }
            }
            return total;
        }

        public int mergeTilesOnUp()
        {
            int total = 0;
            // merge tiles that have been moved towards up, check if adjacant tiles are the same, if so, merge them and add the value of the two tiles to the first tile all the way to the top
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[j, i].Number == 0)
                    {
                        continue;
                    }
                    for (int k = j + 1; k < size; k++)
                    {
                        if (board[k, i].Number == 0)
                        {
                            continue;
                        }
                        if (board[j, i].Number == board[k, i].Number)
                        {
                            board[j, i].Number += board[k, i].Number;
                            total += board[j, i].Number;
                            board[k, i].Number = 0;
                            break;
                        }
                    }
                }
            }
            return total;
        }

        public int mergeTilesOnLeft()
        {
            int total = 0;
            // merge tiles that have been moved towards the left, check if adjacent tiles are the same, if so, merge them and make one tile with the sum of the two tiles all the way to the left
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j].Number == 0)
                    {
                        continue;
                    }

                    for (int k = j + 1; k < size; k++)
                    {
                        if (board[i, k].Number == 0)
                        {
                            continue;
                        }
                        if (board[i, j].Number == board[i, k].Number)
                        {
                            board[i, j].Number += board[i, k].Number;
                            total += board[i, j].Number;
                            board[i, k].Number = 0;
                            break;
                        }
                    }
                }
            }
            return total;
        }
    }
}
