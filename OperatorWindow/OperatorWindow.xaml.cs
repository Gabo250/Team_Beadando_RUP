using Model;
using System;
using System.Collections.Generic;
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

namespace OperatorWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class OpWindow : Window
    {
        OperatorViewModel vm;

        /// <summary>
        /// operator window cons
        /// </summary>
        /// <param name="op">aktualis operator</param>
        public OpWindow(User op)
        {
            InitializeComponent();
            vm = new OperatorViewModel(op);
            this.DataContext = vm;
        }

        private void vallal(object sender, RoutedEventArgs e)
        {
            if (vm.Bikes.Count == 0)
            {
                Megrendeles akt = (Megrendeles)listBox.SelectedItem;
                List<Alkatresz> kell = vm.KelloAlkMeghatarozas(akt.Biciklik);               

                if (vm.Raktar_kello_alkatreszek_Osszhasonlitas(kell))
                {
                    foreach (var it in akt.Biciklik)
                    {
                        vm.Bikes.Add(it);
                    }

                    vm.Vallalt = akt;
                    akt.Status = Allapot.Folyamatban;
                    akt.Dolgozo = vm.Operator.Nev;
                    vm.Rakup(kell);
                    vm.Updaterend(akt);
                }
                else
                {
                    MessageBox.Show("Nincs elég alkatrész hozzá a raktárban, Üzenj a logisztikusnak, hogy rendeljen gyorsan, vagy várd meg míg rendel");
                }              
            }
            else
            {
                MessageBox.Show("Először azt aktuálisan elvállalt megrendelést be kell fejezned!");
            }
        }

        private void Kuldes(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedIndex != -1 && textBox.Text.ToString() != "" && textBox.Text.ToString() != String.Empty)
            {
                User u = (User)comboBox.SelectedItem;
                vm.Uzenet = new Uzenet(vm.Operator.Nev, u.Nev, "Ariel", textBox.Text.ToString(), DateTime.Now);
                vm.Senduzi();
                MessageBox.Show("Üzenet elküldve");
            }
            else
            {
                MessageBox.Show("Hiányos adat");
            }
        }

        private void kesz(object sender, RoutedEventArgs e)
        {
            Bicikli bic = (Bicikli)bikebox.SelectedItem;
            if (!bic.Keszvan)
            {
                bic.Keszvan = true;
                vm.UpdateBikeStatus(bic);
                vm.UpdateKimut();
                if (vm.OsszbicikliElkeszult())
                {
                    vm.BikeElkeszult();
                    MessageBox.Show("Ügyes, az összes Biciklit a rendeléshez elkészítetted!");                   
                }
            }
            else
            {
                MessageBox.Show("Ez a bicikli már elkészűlt!");
            }
        }

        private void bezar(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
