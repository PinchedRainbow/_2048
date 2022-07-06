using System;
using System.Collections.Generic;
using System.Linq;

namespace _2048
{
    /// <summary>
    /// Establishes the logic and workings of Harvey, the 2048 AI.
    /// </summary>
    public class Harvey
    {
        
        // The weightings which affect bias towards attributes of possible board states.
        private readonly double _mergeWeight;
        private readonly double _emptyWeight;

        /// <summary>
        /// Initialises the respective weights of the AI.
        /// </summary>
        public Harvey(double mergeWeight, double emptyWeight)
        {
            _mergeWeight = mergeWeight;
            _emptyWeight = emptyWeight;
        }

        /// <summary>
        /// Returns a choice of move based on simple, score-based logic.
        /// Different board states for each possible choice are created and then scored to give the response.
        /// </summary>
        /// <param name="b">The current board to consider</param>
        /// <returns>The directional arrow key chosen</returns>
        public ConsoleKey getAction(Board b)
        {
            // Creates a list of all possible moves
            var moves = new List<ConsoleKey>
            {
                ConsoleKey.UpArrow,
                ConsoleKey.DownArrow,
                ConsoleKey.LeftArrow,
                ConsoleKey.RightArrow
            };
            
            // Get the board string of the current board
            var boardString = b.getBoardString();
            
            // Create a list of all possible board states
            var boardStates = new List<Tile[,]>();
            int size = b.getSize();
            
            // For each possible move, create a new board state and add it to the list of board states
            foreach (var move in moves)
            {
                var newBoard = new Board(size);
                newBoard.readBoardString(boardString);
                newBoard.move(move);
                boardStates.Add(newBoard.getTiles());
            }

            //Create a list of scores for each board state
            List<double> scores = boardStates.Select(getScore).ToList();
            
            // Display all scores for debugging purposes
            //Console.WriteLine(string.Join(", ", scores));

            //Find the highest score
            double max = scores[0];
            var maxIndex = 0;
            for (int i = 1; i < scores.Count; i++)
            {
                if (!(scores[i] > max)) continue;
                max = scores[i];
                maxIndex = i;
            }
            
            // Displays information about the best board:
            Console.WriteLine("### BEST BOARD STATISTICS ###");
            Console.WriteLine($"Total Score: {max}\n" +
                              $"Merge Score: {getMergeScore(boardStates[maxIndex])}\n" +
                              $"Empty Score: {getEmptyScore(boardStates[maxIndex])}\n" +
                              $"Actual Score: {getActualScore(boardStates[maxIndex])}\n" +
                              $"Move: {moves[maxIndex]}\n");
            Console.WriteLine("### END BOARD STATISTICS ###\n");
            
            
            //Return the move that corresponds to the highest score
            return moves[maxIndex];
        }

        /// <summary>
        /// Calculates the total score of a board by summing the individual, weighted scores of said board.
        /// </summary>
        /// <param name="board">The board to calculate the total score for.</param>
        /// <returns>The total score of a board.</returns>
        private double getScore(Tile[,] board)
        {
            // calculate a score based on weightings of the board state
            double score = 0.0;
            score += getMergeScore(board);
            score += getEmptyScore(board);
            // needs scaling to be in line with other values
            score += getActualScore(board);
            
            return score;
        }

        /// <summary>
        /// Calculates an estimate of the actual in-game 2048 score of a board.
        /// </summary>
        /// <param name="board">The board to estimate the actual score for.</param>
        /// <returns>The estimated actual score.</returns>
        private static double getActualScore(Tile[,] board)
        {
            double score = 0.0;
            
            // In 2048, score is only gained from merging tiles together. We can estimate the total score gained by merging up to the value of a tile below.
            // Note: This is not a true score, but rather an estimate of the score gained by merging tiles, as we assume that only 2 tiles are generated.
            score += (from Tile tile in board // iterates through each tile in the board
                where tile.Number != 0 && tile.Number != 2 // ignore empty and 2 tiles
                select Math.Log2(tile.Number) into n // calculates the log2 of the tile's number
                select (float)((n - 1) * Math.Pow(2, n))).Sum(); // adds the total score of the tile to the total score

            /* note to the above: the value of a tile is not the score you get from having the tile on the board.
             * consider an 8 tile. An 8 tile is created by merging two 4 tiles together. This merge would increase the score by 8.
             * however, to get the two 4 tiles, you need to merge four 2 tiles together, which by itself would increase the score by 8 too.
             * and so the total resultant score from an 8 tile being on the board is 16.
             *
             * Every tile value in 2048 is a power of 2, and so can be represented as 2ⁿ.
             * The total score of a value forms a sequence if you're interested, where the total score, aₙ, is aₙ = 2 aₙ-₁ + 2ⁿ.
             * ^ in words: the total score of a tile = the total score of the 2 tiles that merged to make it + the value of the tile itself.
             * which is the same as saying: the total score of a tile = 2 * the total score of the tile before it + the value of the tile itself.
             * This is equivalent to the formula aₙ = (n-1) * 2ⁿ, which is used above, and avoids recursion.
             */
            
            // scale the score to be in line with other calculated scores, maybe?
            return Math.Log2(score);
        }

        /// <summary>
        /// Calculates a score based on the total number of empty tiles on the board.
        /// </summary>
        /// <param name="board">The board to calculate a score for.</param>
        /// <returns>The calculated empty score.</returns>
        private double getEmptyScore(Tile[,] board)
        {
            // calculate a score based on the number of empty tiles
            double score = 0.0;
            int emptyCount = board.Cast<Tile>().Count(tile => tile.Number == 0);
            score += _emptyWeight * emptyCount;
            return score;
        }

        /// <summary>
        /// Calculates a score based on the number of tiles that can be merged together.
        /// </summary>
        /// <param name="board">The board to calculate a score for.</param>
        /// <returns>The calculated merge score.</returns>
        private double getMergeScore(Tile[,] board)
        {
            // calculate a score based on the number of possible merges on the board
            double score = 0.0;
            int mergeCount = 0;
            
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i, j].Number == 0) continue;
                    
                    //check for out of bounds
                    if (i + 1 < board.GetLength(0))
                    {
                        if (board[i + 1, j].Number == board[i, j].Number)
                        {
                            mergeCount++;
                        }
                    }
                    if (i - 1 >= 0)
                    {
                        if (board[i - 1, j].Number == board[i, j].Number)
                        {
                            mergeCount++;
                        }
                    }
                    if (j + 1 < board.GetLength(0))
                    {
                        if (board[i, j + 1].Number == board[i, j].Number)
                        {
                            mergeCount++;
                        }
                    }
                    if (j - 1 >= 0)
                    {
                        if (board[i, j - 1].Number == board[i, j].Number)
                        {
                            mergeCount++;
                        }
                    }
                }
            }
            score += _mergeWeight * mergeCount;
            
            return score;
        }
    }
}