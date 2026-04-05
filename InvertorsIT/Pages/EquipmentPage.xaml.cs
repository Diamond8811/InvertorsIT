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
    /// Логика взаимодействия для EquipmentPage.xaml
    /// </summary>
    public partial class EquipmentPage : Page
    {
        public EquipmentPage()
        {
            InitializeComponent();
            LoadEquipment();
        }

        private void LoadEquipment()
        {
            var equipment = Conn.inventoryDBEntities.Equipment.ToList();
            dgEquipment.ItemsSource = equipment;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EquipmentEditWindow window = new EquipmentEditWindow(null);
            window.ShowDialog();
            LoadEquipment();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgEquipment.SelectedItem == null)
            {
                MessageBox.Show("Выберите оборудование для редактирования!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Equipment selected = (Equipment)dgEquipment.SelectedItem;
            EquipmentEditWindow window = new EquipmentEditWindow(selected);
            window.ShowDialog();
            LoadEquipment();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgEquipment.SelectedItem == null)
            {
                MessageBox.Show("Выберите оборудование для удаления!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Equipment selected = (Equipment)dgEquipment.SelectedItem;
            MessageBoxResult result = MessageBox.Show($"Удалить оборудование {selected.SerialNumber}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Conn.inventoryDBEntities.Equipment.Remove(selected);
                Conn.inventoryDBEntities.SaveChanges();
                LoadEquipment();
                MessageBox.Show("Оборудование удалено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadEquipment();
        }
    }
}
