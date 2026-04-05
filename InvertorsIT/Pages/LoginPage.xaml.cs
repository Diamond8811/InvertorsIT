using InvertorsIT.Connections;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private MainWindow mainWindow;

        public LoginPage(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var user = Conn.inventoryDBEntities.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null && user.IsActive == true)
            {
                UserSession.CurrentUserID = user.UserID;
                UserSession.CurrentUsername = user.Username;
                UserSession.CurrentUserRoleID = user.RoleID;

                var role = Conn.inventoryDBEntities.Roles.FirstOrDefault(r => r.RoleID == user.RoleID);
                if (role != null)
                    UserSession.CurrentUserRole = role.RoleName;
                else
                    UserSession.CurrentUserRole = "Employee";

                mainWindow.LoadMainPage();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль, или аккаунт неактивен!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage(mainWindow));
        }
    }
}
