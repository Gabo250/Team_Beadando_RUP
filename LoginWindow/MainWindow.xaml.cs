using Adatbazis;
using LogisztikusWindow;
using Model;
using OperatorWindow;
using Services;
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
using UgyvezetoWindow;

namespace LoginWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LoginViewModel loginvm;        

        /// <summary>
        /// Login cons
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            loginvm = new LoginViewModel();
            this.DataContext = loginvm;
        }

        private void OnClick_Login(object sender, RoutedEventArgs e)
        {
            try
            {
                loginvm.Password = passwordBox.Password;
                loginvm.User = loginvm.Logining();
                if (loginvm.User.Privilege == Role.Operator)
                {
                    OpWindow ow = new OpWindow(loginvm.User);
                    ow.Show();
                    shutdown();
                }
                else if (loginvm.User.Privilege == Role.Logisztikus)
                {
                    LogiWindow lw = new LogiWindow(loginvm.User);
                    lw.Show();
                    shutdown();
                }
                else
                {
                    UgyWindow uw = new UgyWindow(loginvm.User);
                    uw.Show();
                    shutdown();
                }             
            }
            catch (InvalidPassorUernameException k)
            {
                MessageBox.Show(k.Message);
            }
        }

        private void shutdown()
        {
            this.Visibility = Visibility.Hidden;
        }
        
    }
}
