using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace UgyvezetoWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class UgyWindow : Window
    {
        private UgyvezetoViewModel ugyvezetoViewModel;

        /// <summary>
        /// ugyvezeto window cons
        /// </summary>
        /// <param name="akt">aktuális ugyvezeto</param>
        public UgyWindow(User akt)
        {
            InitializeComponent();
            ugyvezetoViewModel = new UgyvezetoViewModel(akt);
            this.DataContext = ugyvezetoViewModel;               
        }

        private void bike_hozzaad(object sender, RoutedEventArgs e)
        {
            if (darab.Text.ToString() != "" && darab.Text.ToString() != String.Empty && int.Parse(darab.Text.ToString()) > 0)
            {
                BicikliTipus tip = (BicikliTipus)biciklitipus.SelectedItem;
                ugyvezetoViewModel.RendhezBicikli(tip, int.Parse(darab.Text.ToString()));
                MessageBox.Show("Bicikli Rendeléshez adva");
            }
            else
            {
                MessageBox.Show("Rosz input!");
            }
        }

        private void ren_felvetel(object sender, RoutedEventArgs e)
        {
            ugyvezetoViewModel.Ujrendeles = new Megrendeles(++ugyvezetoViewModel.RendMaxID, ugyvezetoViewModel.Rendeltbiciklik.ToList(), Allapot.Operatorra_var, rendelo.Text.ToString(), "");
            ugyvezetoViewModel.UjrendelesFelvetele();
            MessageBox.Show("Rendelés rögzitve");   
        }

        private void bezar(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void csakszam(object sender, TextCompositionEventArgs e)
        {
            Regex vmi = new Regex("[^0-9]+");
            e.Handled = vmi.IsMatch(e.Text);
        }

        private void kuldes(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedIndex != -1 && textBox.Text.ToString() != "" && textBox.Text.ToString() != String.Empty)
            {
                User u = (User)comboBox.SelectedItem;
                ugyvezetoViewModel.Uzenet = new Uzenet(ugyvezetoViewModel.Felhasznalo.Nev, u.Nev, "Ariel", textBox.Text.ToString(), DateTime.Now);
                ugyvezetoViewModel.UzenetKuldes();
                MessageBox.Show("Üzenet elküldve");
            }
            else
            {
                MessageBox.Show("Hiányos adat");
            }
        }
    }
}
