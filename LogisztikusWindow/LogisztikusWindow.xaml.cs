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

namespace LogisztikusWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LogiWindow : Window
    {
        LogisztikusViewModel logisztikusViewModel;

        public LogiWindow(User us)
        {
            InitializeComponent();
            logisztikusViewModel = new LogisztikusViewModel(us);
            this.DataContext = logisztikusViewModel;
        }

        private void add(object sender, RoutedEventArgs e)
        {
            if (logisztikusViewModel.Hanydb > 0)
            {
                Alkatresz akt = (Alkatresz)comboBox.SelectedItem;
                akt.Mennyi = logisztikusViewModel.Hanydb;
                logisztikusViewModel.RendelendoAlkatreszek.Add(akt);
            }
            else
            {
                MessageBox.Show("0-nál többet kell belőle rendelned!");
            }
        }

        private void rendel(object sender, RoutedEventArgs e)
        {
            if (logisztikusViewModel.AlkatreszRendelesek.Count == 0)
            {
                foreach (var it in logisztikusViewModel.RendelendoAlkatreszek)
                {
                    logisztikusViewModel.AlkatreszRendeles = new AlkatreszRendeles(it, it.Mennyi);
                    logisztikusViewModel.AlkatreszRendelesek.Add(logisztikusViewModel.AlkatreszRendeles);
                }

                logisztikusViewModel.RendelendoAlkatreszek = new System.Collections.ObjectModel.ObservableCollection<Alkatresz>();
            }
            else
            {
                MessageBox.Show("Meg kell várni míg az előző rendelés megérkezik !");
            }          
        }

        private void raktarba(object sender, RoutedEventArgs e)
        {
            logisztikusViewModel.Sendalkatreszrendeles();
            logisztikusViewModel.AlkatreszRendelesek = new System.Collections.ObjectModel.ObservableCollection<AlkatreszRendeles>();
        }

        private void kuldes(object sender, RoutedEventArgs e)
        {
            if (comboBox3.SelectedIndex != -1 && textBox3.Text.ToString() != "" && textBox3.Text.ToString() != String.Empty)
            {
                User u = (User)comboBox3.SelectedItem;
                logisztikusViewModel.Uzenet = new Uzenet(logisztikusViewModel.Felhasznalo.Nev, u.Nev, "Ariel", textBox3.Text.ToString(), DateTime.Now);               
                logisztikusViewModel.Senduzenet();
                MessageBox.Show("Üzenet elküldve");
            }
            else
            {
                MessageBox.Show("Hiányos adat");
            }
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
    }
}
