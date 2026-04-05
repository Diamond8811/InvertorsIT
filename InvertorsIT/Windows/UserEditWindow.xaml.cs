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
using System.Windows.Shapes;

namespace InvertorsIT.Windows
{
    /// <summary>
    /// Логика взаимодействия для UserEditWindow.xaml
    /// </summary>
    public partial class UserEditWindow : Window
    {
        private Users editingUser;
        private bool isNewUser;

        public UserEditWindow(Users user)
        {
            InitializeComponent();
            LoadRoles();

            if (user == null)
            {
                isNewUser = true;
                Title = "Добавление пользователя";
            }
            else
            {
                isNewUser = false;
                editingUser = user;
                Title = "Редактирование пользователя";
                LoadUserData();
            }
        }

        private void LoadRoles()
        {
            var roles = Conn.inventoryDBEntities.Roles.ToList();
            cboRole.ItemsSource = roles;
            cboRole.SelectedValuePath = "RoleID";
            cboRole.DisplayMemberPath = "RoleName";
        }

        private void LoadUserData()
        {
            txtUsername.Text = editingUser.Username;
            txtFullName.Text = editingUser.FullName;
            txtEmail.Text = editingUser.Email;
            cboRole.SelectedValue = editingUser.RoleID;
            chkActive.IsChecked = editingUser.IsActive;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            int roleId = (int)cboRole.SelectedValue;
            bool isActive = chkActive.IsChecked == true;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Заполните обязательные поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (isNewUser)
            {
                Users newUser = new Users
                {
                    Username = username,
                    Password = "123456",
                    FullName = fullName,
                    Email = email,
                    RoleID = roleId,
                    IsActive = isActive,
                    CreatedAt = DateTime.Now
                };
                Conn.inventoryDBEntities.Users.Add(newUser);
            }
            else
            {
                editingUser.Username = username;
                editingUser.FullName = fullName;
                editingUser.Email = email;
                editingUser.RoleID = roleId;
                editingUser.IsActive = isActive;

                if (!string.IsNullOrEmpty(txtPassword.Password))
                {
                    editingUser.Password = txtPassword.Password;
                }
            }

            Conn.inventoryDBEntities.SaveChanges();
            MessageBox.Show("Данные сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
