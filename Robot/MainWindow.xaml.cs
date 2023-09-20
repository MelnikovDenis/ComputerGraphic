using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Robot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int m { get; set; } = 300;
        private int n { get; set; } = 50;
        private int h { get; set; } = 50;
        private int l { get; set; } = 50;
        private double alpha { get; set; } = 0;
        private double beta { get; set; } = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            Render();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Render();
        }
        private void m_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            m = (int)e.NewValue;
            Render();
        }
        private void n_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {            
            n = (int)e.NewValue;
            Render();
        }
        private void h_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            h = (int)e.NewValue;
            Render();
        }
        private void l_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            l = (int)e.NewValue;
            Render();
        }
        private void alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            alpha = e.NewValue;
            Render();
        }
        private void beta_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            beta = e.NewValue;
            Render();
        }
        void Render()
        {
            if(MainCanvas == null)
                return;
            using (var bmp = new Bitmap((int)MainCanvas.ActualWidth, (int)MainCanvas.ActualHeight))
            using (var gfx = Graphics.FromImage(bmp))
            using (var pen = new System.Drawing.Pen(System.Drawing.Color.Black, 2f))
            {
                // draw one thousand random white lines on a dark blue background
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gfx.Clear(System.Drawing.Color.White);

                var horizonPt1 = new System.Drawing.Point(0, bmp.Height - 100);
                var horizonPt2 = new System.Drawing.Point(bmp.Width, bmp.Height - 100);

                var A = new System.Drawing.Point(m, bmp.Height - 100);
                var B = new System.Drawing.Point(A.X, A.Y - n);
                var C = new System.Drawing.Point((int)(Math.Cos(alpha) * h + A.X), (int)(B.Y - Math.Sin(alpha) * h));
                var gamma = beta - (1.57 - alpha);
                var D = new System.Drawing.Point((int)(Math.Cos(gamma) * l + C.X), (int)(C.Y - Math.Sin(gamma) * l));

                gfx.DrawLine(pen, horizonPt1, horizonPt2);
                gfx.DrawLine(pen, A, B);
                gfx.DrawLine(pen, B, C);
                gfx.DrawLine(pen, C, D);

                // copy the bitmap to the picturebox
                MainImage.Source = BmpImageFromBmp(bmp);
            }
        }

        private static BitmapImage BmpImageFromBmp(Bitmap bmp)
        {
            using (var memory = new System.IO.MemoryStream())
            {
                bmp.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
