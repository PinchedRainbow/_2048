namespace _2048
{
    /// <summary>
    /// Establishes the logic and workings of Harvey, the 2048 AI.
    /// </summary>
    public class Harvey
    {
        
        public Harvey()
        {
        }
        
        /// <summary>
        /// Returns a choice of move based on simple, score-based logic.
        /// Different board states for each possible choice are created and then scored to give the response.
        /// </summary>
        /// <param name="board">The current board to consider</param>
        /// <returns>The directional arrow key chosen</returns>
        public System.ConsoleKey GetAction(Board board)
        {
            System.ConsoleKey choice = System.ConsoleKey.RightArrow;

            return choice;
        }
    }
}