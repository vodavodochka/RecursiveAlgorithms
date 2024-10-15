using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;

namespace RecursiveAlgorithms
{
    public partial class MainWindow : Window
    {
        private FractalBase _currentFractal;
        private Dictionary<string, FractalBase> _fractals;
        private int _iterations = 100;

        public MainWindow()
        {
            InitializeComponent();

            _fractals = new Dictionary<string, FractalBase>
            {
                { "Julia Fractal", new JuliaFractal() }
            };

            FractalComboBox.ItemsSource = _fractals.Keys;
            FractalComboBox.SelectedIndex = 0;
            _currentFractal = _fractals[FractalComboBox.SelectedItem.ToString()];
            DataContext = _currentFractal;
        }

        private void OnFractalChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentFractal = _fractals[FractalComboBox.SelectedItem.ToString()];
            DataContext = _currentFractal;
        }

        private void OnRealTextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(RealTextBox.Text, out double cRe))
            {
                _currentFractal.cRe = cRe;
            }
            else
            {
                RealTextBox.Text = _currentFractal.cRe.ToString();
            }
        }

        private void OnImagineTextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(ImagineTextBox.Text, out double cIm))
            {
                _currentFractal.cIm = cIm;
            }
            else
            {
                ImagineTextBox.Text = _currentFractal.cIm.ToString();
            }
        }

        private void OnIterationsTextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(IterationsTextBox.Text, out int iterations) && iterations > 0)
            {
                _iterations = iterations;
            }
            else
            {
                IterationsTextBox.Text = _iterations.ToString();
            }
        }

        private void OnDrawButtonClick(object sender, RoutedEventArgs e)
        {
            if (_currentFractal != null)
            {
                _currentFractal.Iterations = _iterations;

                RenderTargetBitmap bitmap = new RenderTargetBitmap((int)MainCanvas.ActualWidth, (int)MainCanvas.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                DrawingVisual visual = new DrawingVisual();
                using (DrawingContext dc = visual.RenderOpen())
                {
                    // Убедитесь, что _currentFractal не равен null и имеет метод Draw
                    if (_currentFractal != null)
                    {
                        _currentFractal.Draw(dc, (int)MainCanvas.ActualWidth, (int)MainCanvas.ActualHeight);
                    }
                    else
                    {
                        throw new InvalidOperationException("Current fractal is not set.");
                    }
                }
                bitmap.Render(visual);

                MainCanvas.Children.Clear();
                MainCanvas.Children.Add(new Image { Source = bitmap });
            }
        }


    }
}