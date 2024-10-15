using System.Windows.Media;

namespace RecursiveAlgorithms
{
    public abstract class FractalBase
    {
        public int Iterations { get; set; } = 100;
        public double cRe { get; set; } = -0.74543;
        public double cIm { get; set; } = 0.11301;

        public abstract void Draw(DrawingContext dc, int width, int height);
    }
}
