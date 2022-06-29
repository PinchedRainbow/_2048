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
            for (int j = 0; j < size; j++)
                board[i, j] = new Tile(0, "", i, j);

            //genInitTiles();
        }


        public void PrintBoard()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++) Console.Write(board[i, j].Number + " ");

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
                board[x, y].Number = 2;
                break;
            }
        }

        public bool isGameOver()
        {
            for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                if (board[i, j].Number == 0)
                    return false;

            return true;
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
            
            //merge the tiles
            mergeTilesOnLeft();
            
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
            
            mergeTilesOnRight();
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
            
            mergeTilesOnUp();
            
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
            mergeTilesOnDown();
        }

        public void mergeTilesOnDown()
        {
           
        }

        public void mergeTilesOnRight()
        {
            // merge tiles that have been moved to the right, check if adjacant tiles are the same, if so, merge them and add the value of the two tiles to the first tile all the way to the right
            
            
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