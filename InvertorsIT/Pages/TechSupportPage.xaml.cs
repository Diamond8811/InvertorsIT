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
    /// Логика взаимодействия для TechSupportPage.xaml
    /// </summary>
    public partial class TechSupportPage : Page
    {
        private MainWindow mainWindow;
        public TechSupportPage(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
            LoadRequests();
        }

        private void LoadRequests()
        {
            var requests = Conn.inventoryDBEntities.RepairRequests.Where(r => r.RequestStatuses.StatusName != "Завершена" && r.RequestStatuses.StatusName != "Отклонена").ToList();
            dgRequests.ItemsSource = requests;
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            if (dgRequests.SelectedItem == null)
            {
                MessageBox.Show("Выберите заявку для обработки!");
                return;
            }
            RepairRequests selected = (RepairRequests)dgRequests.SelectedItem;
            ProcessRequestWindow window = new ProcessRequestWindow(selected);
            window.ShowDialog();
            LoadRequests();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e) => LoadRequests();
    }
}
