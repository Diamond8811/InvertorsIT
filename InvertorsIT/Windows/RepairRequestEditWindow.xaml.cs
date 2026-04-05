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
    /// Логика взаимодействия для RepairRequestEditWindow.xaml
    /// </summary>
    public partial class RepairRequestEditWindow : Window
    {
        private RepairRequests editingRequest;
        private bool isNew;

        public RepairRequestEditWindow(RepairRequests request)
        {
            InitializeComponent();
            LoadComboBoxes();

            if (request == null)
            {
                isNew = true;
                Title = "Добавление заявки на ремонт";
            }
            else
            {
                isNew = false;
                editingRequest = request;
                Title = "Редактирование заявки на ремонт";
                LoadRequestData();
            }
        }

        private void LoadComboBoxes()
        {
            var equipmentList = Conn.inventoryDBEntities.Equipment.ToList();
            cboEquipment.ItemsSource = equipmentList;

            var usersList = Conn.inventoryDBEntities.Users.ToList();
            cboRequester.ItemsSource = usersList;

            var statusesList = Conn.inventoryDBEntities.RequestStatuses.ToList();
            cboRequestStatus.ItemsSource = statusesList;
        }

        private void LoadRequestData()
        {
            cboEquipment.SelectedValue = editingRequest.EquipmentID;
            cboRequester.SelectedValue = editingRequest.RequestedByUserID;
            txtIssueDescription.Text = editingRequest.IssueDescription;
            cboRequestStatus.SelectedValue = editingRequest.RequestStatusID;
            txtResolutionNotes.Text = editingRequest.ResolutionNotes;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cboEquipment.SelectedValue == null)
            {
                MessageBox.Show("Выберите оборудование!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (cboRequester.SelectedValue == null)
            {
                MessageBox.Show("Выберите заявителя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtIssueDescription.Text))
            {
                MessageBox.Show("Введите описание проблемы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (cboRequestStatus.SelectedValue == null)
            {
                MessageBox.Show("Выберите статус заявки!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int equipmentId = (int)cboEquipment.SelectedValue;
            int requesterId = (int)cboRequester.SelectedValue;
            int statusId = (int)cboRequestStatus.SelectedValue;
            string issueDesc = txtIssueDescription.Text.Trim();
            string resolutionNotes = txtResolutionNotes.Text.Trim();

            if (isNew)
            {
                RepairRequests newRequest = new RepairRequests
                {
                    EquipmentID = equipmentId,
                    RequestedByUserID = requesterId,
                    IssueDescription = issueDesc,
                    RequestStatusID = statusId,
                    ResolutionNotes = resolutionNotes,
                    RequestDate = DateTime.Now
                };
                Conn.inventoryDBEntities.RepairRequests.Add(newRequest);
            }
            else
            {
                editingRequest.EquipmentID = equipmentId;
                editingRequest.RequestedByUserID = requesterId;
                editingRequest.IssueDescription = issueDesc;
                editingRequest.RequestStatusID = statusId;
                editingRequest.ResolutionNotes = resolutionNotes;
                if (statusId == 3 || statusId == 4)
                {
                    if (editingRequest.ResolvedDate == null)
                        editingRequest.ResolvedDate = DateTime.Now;
                }
            }

            Conn.inventoryDBEntities.SaveChanges();
            MessageBox.Show("Заявка сохранена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
