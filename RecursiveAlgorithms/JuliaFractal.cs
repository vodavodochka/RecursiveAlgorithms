using System.Runtime.ConstrainedExecution;
using System.Windows;
using System.Windows.Media;

namespace RecursiveAlgorithms
{
    public class JuliaFractal : FractalBase
    {
        public override void Draw(DrawingContext dc, int width, int height)
        {
            double zoom = 1;
            double moveX = 0, moveY = 0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double zx = 1.5 * (x - width / 2) / (0.5 * zoom * width) + moveX;
                    double zy = (y - height / 2) / (0.5 * zoom * height) + moveY;
                    int i = Iterations;
                    double escapeRadius = 4.0;

                    int result = CalculateJulia(zx, zy, cRe, cIm, escapeRadius, i);

                    Color color = result == 0 ? Colors.Black : Color.FromRgb((byte)(result * 9), (byte)(result * 9), (byte)(255 - result * 5));
                    dc.DrawRectangle(new SolidColorBrush(color), null, new Rect(x, y, 1, 1));
                }
            }
        }

        private int CalculateJulia(double zx, double zy, double cRe, double cIm, double escapeRadius, int depth)
        {
            if (zx * zx + zy * zy > escapeRadius || depth == 0)
            {
                return depth;
            }

            double tmp = zx * zx - zy * zy + cRe;
            zy = 2.0 * zx * zy + cIm;
            zx = tmp;

            return CalculateJulia(zx, zy, cRe, cIm, escapeRadius, depth - 1);
        }
    }
}
