using System;

namespace _2048
{
    public class Tile
    {
        private int number;
        private string colour;
        private int[] position;

        public int Number
        {
            get => number;
            set => number = value;
        }
        
        
        public Tile(int number, string colour, int x, int y)
        {
            this.number = number;
            this.colour = colour;
            position = new int[2];
            position[0] = x;
            position[1] = y;
        }
        
    }
}