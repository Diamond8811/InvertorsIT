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
    /// Логика взаимодействия для RepairRequestsPage.xaml
    /// </summary>
    public partial class RepairRequestsPage : Page
    {
        public RepairRequestsPage()
        {
            InitializeComponent();
            LoadRequests();
        }

        private void LoadRequests()
        {
            var requests = Conn.inventoryDBEntities.RepairRequests.ToList();
            dgRequests.ItemsSource = requests;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            RepairRequestEditWindow window = new RepairRequestEditWindow(null);
            window.ShowDialog();
            LoadRequests();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgRequests.SelectedItem == null)
            {
                MessageBox.Show("Выберите заявку!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            RepairRequests selected = (RepairRequests)dgRequests.SelectedItem;
            RepairRequestEditWindow window = new RepairRequestEditWindow(selected);
            window.ShowDialog();
            LoadRequests();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgRequests.SelectedItem == null) return;
            RepairRequests selected = (RepairRequests)dgRequests.SelectedItem;
            if (MessageBox.Show("Удалить заявку?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Conn.inventoryDBEntities.RepairRequests.Remove(selected);
                Conn.inventoryDBEntities.SaveChanges();
                LoadRequests();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadRequests();
        }
    }
}
