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
    /// Логика взаимодействия для ProcessRequestWindow.xaml
    /// </summary>
    public partial class ProcessRequestWindow : Window
    {
        private RepairRequests request;

        public ProcessRequestWindow(RepairRequests req)
        {
            InitializeComponent();
            request = req;
            txtEquipment.Text = req.Equipment.Model + " (" + req.Equipment.SerialNumber + ")";
            txtDescription.Text = req.IssueDescription;
            var statuses = Conn.inventoryDBEntities.RequestStatuses.ToList();
            cboStatus.ItemsSource = statuses;
            cboStatus.SelectedValue = req.RequestStatusID;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            request.RequestStatusID = (int)cboStatus.SelectedValue;
            request.ResolutionNotes = txtResolution.Text.Trim();
            request.ResolvedByUserID = UserSession.CurrentUserID;
            request.ResolvedDate = DateTime.Now;

            Conn.inventoryDBEntities.SaveChanges();
            MessageBox.Show("Заявка обновлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
