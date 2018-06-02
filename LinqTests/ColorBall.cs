using System.Collections.Generic;

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

    internal class ColorBallComparer : IEqualityComparer<ColorBall>
    {
        public bool Equals(ColorBall x, ColorBall y)
        {
            return x.Color == y.Color
                && x.Prize == y.Prize
                && x.Size == y.Size;
        }

        public int GetHashCode(ColorBall obj)
        {
            return 0;
        }
    }
}