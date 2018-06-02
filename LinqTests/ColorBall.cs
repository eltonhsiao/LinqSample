namespace LinqTests
{
    internal enum Color
    {
        Purple,
        Blue,
        Yellow,
        Green
    }

    internal class ColorBall
    {
        public Color Color { get; set; }
        public string Size { get; set; }
        public int Prize { get; set; }
    }
}