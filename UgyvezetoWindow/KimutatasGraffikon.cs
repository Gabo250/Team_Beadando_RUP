using Model;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace UgyvezetoWindow
{
    /// <summary>
    /// KimutaGraff class
    /// </summary>
    public class KimutatasGraffikon : FrameworkElement
    {
        private List<Kimutatas> operatorok;
        private DataService dataService;
        private int max_elkeszult_bicikli = 0;
        private double width;
        private double height;
        private Dictionary<Kimutatas ,RectangleGeometry> teglalapok;
        Random r = new Random();

        /// <summary>
        /// Kimutatas cons
        /// </summary>
        public KimutatasGraffikon()
        {
            this.Loaded += KimutatasGraffikon_Loaded;            
        }

        private void KimutatasGraffikon_Loaded(object sender, RoutedEventArgs e)
        {
            Initalize();
        }

        /// <summary>
        /// graff rajzolás
        /// </summary>
        /// <param name="drawingContext">draw</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            double kezdoX = width / 2 - 200;
            double kezdY = height / 2 + 200;
            //// oldalra menő vonal
            Point kX = new Point(kezdoX, kezdY);
            Point kY = new Point(kezdoX + 450, kezdY);
            //// fel menő vonal
            Point fX = new Point(kezdoX, kezdY);
            Point fY = new Point(kezdoX, kezdY - 400);
            //// nyil oldalra
            Point nyilvonal1X = new Point(kY.X - 20, kY.Y - 20);
            Point nyilvonal1Y = kY;
            Point nyilvonal2X = new Point(kY.X - 20, kY.Y + 20);
            Point nyilvonal2Y = kY;
            //// nyil fel
            Point nyilfelvonal1X = new Point(fX.X - 20, fY.Y + 20);
            Point nyilfelvonal1Y = fY;
            Point nyilfelvonal2X = new Point(fX.X + 20, fY.Y + 20);
            Point nyilfelvonal2Y = fY;
            //// drawing
            drawingContext.DrawRectangle(Brushes.Gray, null, new Rect(0, 0, width, height));
            drawingContext.DrawLine(new Pen(Brushes.Black, 2), kX, kY);
            drawingContext.DrawLine(new Pen(Brushes.Black, 2), fX, fY);
            drawingContext.DrawLine(new Pen(Brushes.Black, 2), nyilfelvonal1X, nyilfelvonal1Y);
            drawingContext.DrawLine(new Pen(Brushes.Black, 2), nyilfelvonal2X, nyilfelvonal2Y);
            drawingContext.DrawLine(new Pen(Brushes.Black, 2), nyilvonal1X, nyilvonal1Y);
            drawingContext.DrawLine(new Pen(Brushes.Black, 2), nyilvonal2X, nyilvonal2Y);
            drawingContext.DrawText(new FormattedText("Operátorok Teljesítménye", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), 20, Brushes.Green), new Point(width / 2 - 120, 40));
            drawingContext.DrawText(new FormattedText("Elkészített", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), 15, Brushes.White), new Point(kezdoX - 100, kezdY - 200));
            drawingContext.DrawText(new FormattedText("Biciklik", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), 15, Brushes.White), new Point(kezdoX - 90, kezdY - 180));
            drawingContext.DrawText(new FormattedText("Operátor", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), 15, Brushes.White), new Point(kY.X - 280, kezdY + 30));
            drawingContext.DrawText(new FormattedText(dataService.Max_keszitett_Bic(operatorok).ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.RightToLeft, new Typeface("Verdana"), 10, Brushes.Black), new Point(kezdoX - 10, fY.Y + 30));           

            for (int i = 0; i < teglalapok.Count; i++)
            {
                if (i == 0)
                {
                    if (max_elkeszult_bicikli != 0)
                    {
                        double k = (teglalapok.ElementAt(i).Key.Keszbiciklik / max_elkeszult_bicikli) * ((Math.Abs(fY.Y - kezdY) - 40));
                        teglalapok.ElementAt(i).Value.Rect = new Rect(kezdoX + 10, kezdY - (k + 3), 80, k);
                    }
                    else
                    {
                        double k = (teglalapok.ElementAt(i).Key.Keszbiciklik / max_elkeszult_bicikli) * ((Math.Abs(fY.Y - kezdY) - 40));
                        teglalapok.ElementAt(i).Value.Rect = new Rect(kezdoX + 10, kezdY - (k + 3), 80, k);
                    }
                }
                else
                {
                    if (max_elkeszult_bicikli != 0)
                    {
                        double k = (teglalapok.ElementAt(i).Key.Keszbiciklik / max_elkeszult_bicikli) * ((Math.Abs(fY.Y - kezdY) - 40));
                        teglalapok.ElementAt(i).Value.Rect = new Rect(kezdoX + 110 * i, kezdY - (k + 3), 80, k);
                    }
                    else
                    {
                        double k = (teglalapok.ElementAt(i).Key.Keszbiciklik / max_elkeszult_bicikli) * ((Math.Abs(fY.Y - kezdY) - 40));
                        teglalapok.ElementAt(i).Value.Rect = new Rect(kezdoX + 110 * i, kezdY - (k + 3), 80, k);
                    }
                }                          
            }

            foreach (var it in teglalapok)
            {
                byte R = (byte)r.Next(0, 256);
                byte G = (byte)r.Next(0, 256);
                byte B = (byte)r.Next(0, 256);
                drawingContext.DrawGeometry(new SolidColorBrush(Color.FromRgb(R, G, B)), new Pen(new SolidColorBrush(Color.FromRgb(R, G, B)), 2), it.Value);
                drawingContext.DrawText(new FormattedText(it.Key.Nev, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), 10, Brushes.Black), new Point(it.Value.Rect.X, kezdY + 10));
                drawingContext.DrawText(new FormattedText(it.Key.Keszbiciklik.ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), 10, Brushes.Red), new Point(it.Value.Rect.X + 35, kezdY - it.Value.Rect.Height - 15));
            }
            
        }

        /// <summary>
        /// alap adatok inicilazilása
        /// </summary>
        private void Initalize()
        {
            dataService = new DataService();
            operatorok = dataService.Operator_Kimutatasok();
            teglalapok = new Dictionary<Kimutatas, RectangleGeometry>();            
            width = this.ActualWidth;
            height = this.ActualHeight;
            foreach (var it in operatorok)
            {
                max_elkeszult_bicikli += it.Keszbiciklik;
            }

            foreach (var it in operatorok)
            {
                teglalapok.Add(it, new RectangleGeometry());
            }

            InvalidateVisual();
        }    
    }
}
