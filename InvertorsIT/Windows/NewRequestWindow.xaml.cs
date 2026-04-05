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
    /// Логика взаимодействия для NewRequestWindow.xaml
    /// </summary>
    public partial class NewRequestWindow : Window
    {
        public NewRequestWindow()
        {
            InitializeComponent();
            var myEquipment = Conn.inventoryDBEntities.Equipment.Where(e => e.ResponsibleUserID == UserSession.CurrentUserID).ToList();
            cboEquipment.ItemsSource = myEquipment;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (cboEquipment.SelectedItem == null)
            {
                MessageBox.Show("Выберите оборудование!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtIssue.Text))
            {
                MessageBox.Show("Опишите проблему!");
                return;
            }

            int equipmentId = (int)cboEquipment.SelectedValue;
            int newStatusId = Conn.inventoryDBEntities.RequestStatuses.First(s => s.StatusName == "Новая").RequestStatusID;

            RepairRequests newRequest = new RepairRequests
            {
                EquipmentID = equipmentId,
                RequestedByUserID = UserSession.CurrentUserID,
                IssueDescription = txtIssue.Text.Trim(),
                RequestStatusID = newStatusId,
                RequestDate = DateTime.Now
            };

            Conn.inventoryDBEntities.RepairRequests.Add(newRequest);
            Conn.inventoryDBEntities.SaveChanges();

            MessageBox.Show("Заявка отправлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
