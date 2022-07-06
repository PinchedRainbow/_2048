using System;
using System.Linq;

namespace _2048
{
    public class Board
    {
        private readonly Tile[,] board;
        private readonly int size;

        public Board(int size)
        {
            this.size = size;
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
            // put board size first 
            save += size + "\n";
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
            for (int i = 1; i < size + 1; i++)
            {
                string[] numbers = lines[i].Split(' ');
                for (int j = 0; j < size; j++)
                {
                    board[i - 1, j].Number = int.Parse(numbers[j]);
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
        
        public void GenRandomTile()
        {
            int x, y;
            var rnd = new Random();
            while (true)
            {
                x = rnd.Next(0, size);
                y = rnd.Next(0, size);
                if (board[x, y].Number != 0) continue;
                int value = rnd.Next(0,10) < 9 ? 2 : 4;
                board[x, y].Number = value;
                break;
            }
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

        public void moveLeft()
        {
            //merge the tiles
            mergeTilesOnLeft();
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
            mergeTilesOnRight();
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
            mergeTilesOnUp();
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
            mergeTilesOnDown();
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

        public void mergeTilesOnDown()
        {
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
                            board[k, i].Number = 0;
                            break;
                        }
                    }
                }
            }
        }

        public void mergeTilesOnRight()
        {
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
                            board[i, j].Number *= 2;
                            board[i, k].Number = 0;
                            break;
                        }
                    }
                }
            }

        }

        public void mergeTilesOnUp()
        {
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
                            board[k, i].Number = 0;
                            break;
                        }
                    }
                }
            }
        }

        public void mergeTilesOnLeft()
        {
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
                            board[i, j].Number *= 2;
                            board[i, k].Number = 0;
                            break;
                        }
                    }
                }
            }
        }
    }
}
