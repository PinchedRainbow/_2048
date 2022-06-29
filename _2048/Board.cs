using System;
using System.Collections.Generic;
using System.Drawing;

namespace _2048
{
    public class Board
    {
        private int size;
        Tile[,] board;

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

            genInitTiles();
        }


        public void PrintBoard()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(board[i, j].Number + " ");
                }

                Console.WriteLine();
            }
        }


        private void genInitTiles()
        {
            //generate 2 random numbers
            int x1, x2, y1, y2;
            do
            {
                Random rnd = new Random();
                x1 = rnd.Next(0, size - 1);
                y1 = rnd.Next(0, size - 1);
                x2 = rnd.Next(0, size - 1);
                y2 = rnd.Next(0, size - 1);
            } while (x1 == x2 && y1 == y2);

            // set the numbers to the tiles
            board[x1, y1].Number = 2;
            board[x2, y2].Number = 2;

        }
        public bool isGameOver()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j].Number == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        
    }
}