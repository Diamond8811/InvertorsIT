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
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            var users = Conn.inventoryDBEntities.Users.ToList();
            dgUsers.ItemsSource = users;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            UserEditWindow window = new UserEditWindow(null);
            window.ShowDialog();
            LoadUsers();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя для редактирования!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Users selectedUser = (Users)dgUsers.SelectedItem;
            UserEditWindow window = new UserEditWindow(selectedUser);
            window.ShowDialog();
            LoadUsers();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя для удаления!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Users selectedUser = (Users)dgUsers.SelectedItem;

            MessageBoxResult result = MessageBox.Show($"Удалить пользователя {selectedUser.Username}?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Conn.inventoryDBEntities.Users.Remove(selectedUser);
                Conn.inventoryDBEntities.SaveChanges();
                LoadUsers();
                MessageBox.Show("Пользователь удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }
    }
}
