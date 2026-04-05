using InvertorsIT.Connections;
using InvertorsIT.Windows;
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

namespace InvertorsIT.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        private MainWindow mainWindow;

        public EmployeePage(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
            LoadMyEquipment();
            LoadMyRequests();
        }


        private void LoadMyEquipment()
        {
            var myEquipment = Conn.inventoryDBEntities.Equipment
                .Where(e => e.ResponsibleUserID == UserSession.CurrentUserID)
                .ToList();
            dgMyEquipment.ItemsSource = myEquipment;
        }

        private void LoadMyRequests()
        {
            var myRequests = Conn.inventoryDBEntities.RepairRequests
                .Where(r => r.RequestedByUserID == UserSession.CurrentUserID)
                .OrderByDescending(r => r.RequestDate)
                .ToList();
            dgMyRequests.ItemsSource = myRequests;
        }

        private void btnNewRequest_Click(object sender, RoutedEventArgs e)
        {
            NewRequestWindow window = new NewRequestWindow();
            window.ShowDialog();
            LoadMyEquipment();
            LoadMyRequests();
        }

        private void btnRefreshEquipment_Click(object sender, RoutedEventArgs e)
        {
            LoadMyEquipment();
        }

        private void btnRefreshRequests_Click(object sender, RoutedEventArgs e)
        {
            LoadMyRequests();
        }
    }
}
