namespace _2048
{
    public class Tile
    {
        private readonly int[] _position;

        public Tile(int number, string colour, int x, int y)
        {
            Number = number;
            Colour = colour;
            _position = new int[2];
            _position[0] = x;
            _position[1] = y;
        }

        public int Number { get; set; }

        public int X
        {
            get => _position[0];
            set => _position[0] = value;
        }

        public int Y
        {
            get => _position[1];
            set => _position[1] = value;
        }


        private string Colour { get; set; }
    }
}