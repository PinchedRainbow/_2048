using System;

namespace _2048
{
    public class Board
    {
        private readonly Tile[,] _board;
        private readonly int _size;

        public Board(int size)
        {
            _size = size;
            _board = new Tile[size, size];

            for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                _board[i, j] = new Tile(0, "", i, j);

            //genInitTiles();
        }


        public void PrintBoard()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                    if (_board[i, j].Number.ToString().Length == 5) Console.Write(_board[i, j].Number + " ");
                    else if (_board[i, j].Number.ToString().Length == 4) Console.Write(_board[i, j].Number + "  ");
                    else if (_board[i, j].Number.ToString().Length == 3) Console.Write(_board[i, j].Number + "   ");
                    else if (_board[i, j].Number.ToString().Length == 2) Console.Write(_board[i, j].Number + "    ");
                    else Console.Write(_board[i, j].Number + "     ");
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public void GenRandomTile()
        {
            int x, y;
            var rnd = new Random();
            while (true)
            {
                x = rnd.Next(0, _size);
                y = rnd.Next(0, _size);
                if (_board[x, y].Number != 0) continue;
                int value = rnd.Next(0, 10) < 9 ? 2 : 4;
                _board[x, y].Number = value;
                break;
            }
        }

        public bool IsGameOver()
        {
            bool ans = true;

            for (int i = 0; i < _size; i++)
            for (int j = 0; j < _size; j++)
                if (_board[i, j].Number == 0)
                    ans = false;

            for (int i = 0; i < _size - 1; i++)
            for (int j = 0; j < _size - 1; j++)
                if (_board[i, j].Number == _board[i + 1, j].Number)
                    ans = false;
                else if (_board[i, j].Number == _board[i, j + 1].Number) ans = false;

            for (int i = 0; i < _size - 1; i++)
                if (_board[_size - 1, i].Number == _board[_size - 1, i + 1].Number)
                    ans = false;
                else if (_board[i, _size - 1].Number == _board[i + 1, _size - 1].Number) ans = false;

            return ans;
        }

        public void MoveLeft()
        {
            //merge the tiles
            mergeTilesOnLeft();
            //please move the tiles to the left
            for (int i = 0; i < _size; i++)
            for (int j = 0; j < _size; j++)
                if (_board[i, j].Number == 0)
                    for (int k = j + 1; k < _size; k++)
                        if (_board[i, k].Number != 0)
                        {
                            _board[i, j].Number = _board[i, k].Number;
                            _board[i, k].Number = 0;
                            break;
                        }
        }

        public void MoveRight()
        {
            mergeTilesOnRight();
            for (int i = 0; i < _size - 1; i++)
            for (int j = _size - 1; j > 0; j--)
                if (_board[i, j].Number == 0)
                    for (int k = j - 1; k >= 0; k--)
                        if (_board[i, k].Number != 0)
                        {
                            _board[i, j].Number = _board[i, k].Number;
                            _board[i, k].Number = 0;
                            break;
                        }
        }

        public void MoveUp()
        {
            mergeTilesOnUp();
            //please move the tiles to the up
            for (int i = 0; i < _size; i++)
            for (int j = 0; j < _size; j++)
            {
                if (_board[j, i].Number != 0) continue;

                for (int k = j + 1; k < _size; k++)
                {
                    if (_board[k, i].Number == 0) continue;
                    _board[j, i].Number = _board[k, i].Number;
                    _board[k, i].Number = 0;
                    break;
                }
            }
        }

        public void MoveDown()
        {
            mergeTilesOnDown();
            // PLEASE MOVE TILES TO THE DOWN
            for (int i = 0; i < _size; i++)
            for (int j = _size - 1; j > 0; j--)
                if (_board[j, i].Number == 0)
                    for (int k = j - 1; k >= 0; k--)
                        if (_board[k, i].Number != 0)
                        {
                            _board[j, i].Number = _board[k, i].Number;
                            _board[k, i].Number = 0;
                            break;
                        }
        }

        private void mergeTilesOnDown()
        {
            for (int i = 0; i < _size; i++)
            for (int j = _size - 1; j >= 0; j--)
            {
                if (_board[j, i].Number == 0) continue;
                for (int k = j - 1; k >= 0; k--)
                {
                    if (_board[k, i].Number == 0) continue;
                    if (_board[j, i].Number == _board[k, i].Number)
                    {
                        _board[j, i].Number += _board[k, i].Number;
                        _board[k, i].Number = 0;
                        break;
                    }
                }
            }
        }

        private void mergeTilesOnRight()
        {
            // merge tiles that have been moved to the right, check if adjacant tiles are the same, if so, merge them and add the value of the two tiles to the first tile all the way to the right
            for (int i = 0; i < _size; i++)
            for (int j = _size - 1; j >= 0; j--)
            {
                if (_board[i, j].Number == 0) continue;

                for (int k = j - 1; k >= 0; k--)
                {
                    if (_board[i, k].Number == 0) continue;

                    if (_board[i, j].Number != _board[i, k].Number) continue;
                    _board[i, j].Number *= 2;
                    _board[i, k].Number = 0;
                    break;
                }
            }
        }

        private void mergeTilesOnUp()
        {
            // merge tiles that have been moved towards up, check if adjacant tiles are the same, if so, merge them and add the value of the two tiles to the first tile all the way to the top
            for (int i = 0; i < _size; i++)
            for (int j = 0; j < _size; j++)
            {
                if (_board[j, i].Number == 0) continue;
                for (int k = j + 1; k < _size; k++)
                {
                    if (_board[k, i].Number == 0) continue;
                    if (_board[j, i].Number == _board[k, i].Number)
                    {
                        _board[j, i].Number += _board[k, i].Number;
                        _board[k, i].Number = 0;
                        break;
                    }
                }
            }
        }

        private void mergeTilesOnLeft()
        {
            // merge tiles that have been moved towards the left, check if adjacent tiles are the same, if so, merge them and make one tile with the sum of the two tiles all the way to the left
            for (int i = 0; i < _size; i++)
            for (int j = 0; j < _size; j++)
            {
                if (_board[i, j].Number == 0) continue;

                for (int k = j + 1; k < _size; k++)
                {
                    if (_board[i, k].Number == 0) continue;
                    if (_board[i, j].Number == _board[i, k].Number)
                    {
                        _board[i, j].Number *= 2;
                        _board[i, k].Number = 0;
                        break;
                    }
                }
            }
        }
    }
}