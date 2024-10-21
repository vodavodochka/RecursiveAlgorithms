using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Threading;
using System.Diagnostics;

namespace HanoiTowers
{
    public class HanoiTower
    {
        public Canvas towerCanvas;
        private double baseHeight = 20; // Высота основания башни
        private double baseWidth; // Ширина основания
        private double poleHeight; // Высота стержня
        private double diskHeight = 30; // Высота каждого диска
        private double poleWidth = 10; // Ширина стержня

        public HanoiTower(Canvas canvas)
        {
            this.towerCanvas = canvas;
            this.baseWidth = 230; // Начальная ширина основания
            UpdatePoleHeight(300); // Устанавливаем начальную высоту стержня
            DrawBaseAndPole();
        }

        // Метод для обновления высоты стержня
        public void UpdatePoleHeight(double height)
        {
            poleHeight = height; // Устанавливаем новую высоту стержня
            DrawBaseAndPole(); // Перерисовываем основание и стержень
        }

        // Метод для установки ширины основания на основе самого большого диска
        public void SetBaseWidth(double maxDiskWidth)
        {
            baseWidth = maxDiskWidth + 20; // Основание шире самого большого диска на 20 пикселей
            DrawBaseAndPole(); // Перерисовываем основание
        }

        // Метод для отрисовки основания и стержня
        private void DrawBaseAndPole()
        {
            towerCanvas.Children.Clear(); // Очищаем канвас

            // Отрисовка основания башни
            Rectangle towerBase = new Rectangle
            {
                Width = baseWidth,
                Height = baseHeight,
                Fill = Brushes.DarkGray
            };
            Canvas.SetTop(towerBase, towerCanvas.Height - baseHeight); // Располагаем основание внизу
            Canvas.SetLeft(towerBase, (towerCanvas.Width / 2) - (baseWidth / 2)); // Центрируем основание

            // Отрисовка стержня башни
            Rectangle towerPole = new Rectangle
            {
                Width = poleWidth,
                Height = poleHeight,
                Fill = Brushes.DarkGray
            };
            Canvas.SetTop(towerPole, towerCanvas.Height - baseHeight - poleHeight); // Стержень растет вверх от основания
            Canvas.SetLeft(towerPole, (towerCanvas.Width / 2) - (poleWidth / 2)); // Центрируем стержень

            // Добавляем основание и стержень на канвас
            towerCanvas.Children.Add(towerBase);
            towerCanvas.Children.Add(towerPole);
        }

        // Метод для очистки дисков на башне
        public void ClearDisks()
        {
            // Очищаем только диски, оставляя стержень и основание
            var elementsToRemove = new List<UIElement>();
            foreach (UIElement element in towerCanvas.Children)
            {
                if (element is Rectangle disk && disk.Height == diskHeight)
                {
                    elementsToRemove.Add(disk);
                }
            }
            foreach (var element in elementsToRemove)
            {
                towerCanvas.Children.Remove(element);
            }
        }

        // Метод для добавления дисков
        // Метод для добавления дисков
        public void AddDisk(Rectangle disk)
        {
            // Проверка, если есть диски на башне
            if (DiskCount() > 0)
            {
                // Получаем текущий верхний диск на башне
                Rectangle topDisk = GetTopDisk();
            }

            // Найдем позицию для следующего диска
            double nextDiskTop = GetNextDiskHeight();

            // Размещаем диск на башне
            Canvas.SetTop(disk, nextDiskTop);
            Canvas.SetLeft(disk, (towerCanvas.Width / 2) - (disk.Width / 2)); // Центрируем диск
            towerCanvas.Children.Add(disk);
        }

        public Rectangle GetTopDisk()
        {
            Rectangle topDisk = null;
            double topPosition = double.MaxValue;

            foreach (UIElement element in towerCanvas.Children)
            {
                if (element is Rectangle disk && disk.Height == diskHeight)
                {
                    double currentPosition = Canvas.GetTop(disk);
                    if (currentPosition < topPosition)
                    {
                        topPosition = currentPosition;
                        topDisk = disk;
                    }
                }
            }
            return topDisk;
        }

        // Метод для получения высоты для следующего диска
        public double GetNextDiskHeight()
        {
            // Считаем количество дисков, уже находящихся на башне
            int diskCount = DiskCount();

            // Возвращаем позицию для следующего диска
            return towerCanvas.Height - baseHeight - (diskCount * diskHeight);
        }

        // Метод для удаления диска с башни
        // Метод для удаления диска с башни
        public Rectangle RemoveDisk()
        {
            // Ищем самый верхний диск (по высоте)
            Rectangle topDisk = null;
            double topPosition = double.MaxValue;

            foreach (UIElement element in towerCanvas.Children)
            {
                if (element is Rectangle disk && disk.Height == diskHeight)
                {
                    double currentPosition = Canvas.GetTop(disk);
                    if (currentPosition < topPosition)
                    {
                        topPosition = currentPosition;
                        topDisk = disk;
                    }
                }
            }

            if (topDisk != null)
            {
                towerCanvas.Children.Remove(topDisk);
            }

            return topDisk;
        }


        // Метод для получения количества дисков на башне
        public int DiskCount()
        {
            int count = 0;
            foreach (UIElement element in towerCanvas.Children)
            {
                if (element is Rectangle disk && disk.Height == diskHeight)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
