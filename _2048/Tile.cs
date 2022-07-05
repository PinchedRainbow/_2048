namespace _2048
{
    public class Tile
    {
        private readonly int[] position;

        public Tile(int number, string colour, int x, int y)
        {
            this.Number = number;
            this.Colour = colour;
            position = new int[2];
            position[0] = x;
            position[1] = y;
        }

        public int Number { get; set; }

        public int X
        {
            get => position[0];
            set => position[0] = value;
        }

        public int Y
        {
            get => position[1];
            set => position[1] = value;
        }


        public string Colour { get; set; }
    }
}