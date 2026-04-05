using InvertorsIT.Connections;
using InvertorsIT.Pages;
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

namespace InvertorsIT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (UserSession.CurrentUserID == 0)
            {
                MainFrame.Navigate(new LoginPage(this));
            }
            else
            {
                LoadMainPage();
            }
        }

        public void LoadMainPage()
        {
            if (UserSession.CurrentUserRole == "Admin")
            {
                MainFrame.Navigate(new AdminDashboardPage(this));
            }
            else if (UserSession.CurrentUserRole == "TechSupport")
            {
                MainFrame.Navigate(new TechSupportPage(this));
            }
            else
            {
                MainFrame.Navigate(new EmployeePage(this));
            }
        }


        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LoginPage(this));
        }
    }
}
