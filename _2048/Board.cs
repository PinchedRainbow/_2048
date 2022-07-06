using System;
using System.Linq;

namespace _2048
{
    public class Board
    {
        private Tile[,] board;
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

            //genInitTiles();
        }

        public Tile[,] getTiles()
        {
            return board;
        }
        
        public int getSize()
        {
            return size;
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
                Console.WriteLine();
                Console.WriteLine();
            }
        }
        
        public void genRandomTile()
        {
            int x, y;
            var rnd = new Random();
            while (true)
            {
                x = rnd.Next(0, size);
                y = rnd.Next(0, size);
                if (board[x, y].Number != 0) continue;
                var value = rnd.Next(0,10) < 9 ? 2 : 4;
                board[x, y].Number = value;
                break;
            }
        }

        public bool isGameOver()
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

        /// <summary>
        /// A method which makes a move on the board and then returns the resultant board.
        /// </summary>
        /// <param name="consoleKey">The move to make</param>
        /// <returns>The resultant board</returns>
        public Board move(ConsoleKey consoleKey)
        {
            switch (consoleKey)
            {
                case ConsoleKey.UpArrow:
                    moveUp();
                    break;
                case ConsoleKey.DownArrow:
                    moveDown();
                    break;
                case ConsoleKey.LeftArrow:
                    moveLeft();
                    break;
                case ConsoleKey.RightArrow:
                    moveRight();
                    break;
                default:
                    break;
            }
            return this;
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
