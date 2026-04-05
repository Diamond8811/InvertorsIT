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
    /// Логика взаимодействия для EquipmentEditWindow.xaml
    /// </summary>
    public partial class EquipmentEditWindow : Window
    {
        private Equipment editingEquipment;
        private bool isNew;

        public EquipmentEditWindow(Equipment equipment)
        {
            InitializeComponent();
            LoadComboBoxes();

            if (equipment == null)
            {
                isNew = true;
                Title = "Добавление оборудования";
            }
            else
            {
                isNew = false;
                editingEquipment = equipment;
                Title = "Редактирование оборудования";
                LoadEquipmentData();
            }
        }

        private void LoadComboBoxes()
        {
            cboType.ItemsSource = Conn.inventoryDBEntities.EquipmentTypes.ToList();
            cboStatus.ItemsSource = Conn.inventoryDBEntities.EquipmentStatuses.ToList();
            cboUser.ItemsSource = Conn.inventoryDBEntities.Users.ToList();
        }

        private void LoadEquipmentData()
        {
            txtSerialNumber.Text = editingEquipment.SerialNumber;
            txtModel.Text = editingEquipment.Model;
            txtManufacturer.Text = editingEquipment.Manufacturer;
            cboType.SelectedValue = editingEquipment.TypeID;
            cboStatus.SelectedValue = editingEquipment.StatusID;
            if (editingEquipment.ResponsibleUserID != null)
                cboUser.SelectedValue = editingEquipment.ResponsibleUserID;
            if (editingEquipment.PurchaseDate != null)
                dpPurchaseDate.SelectedDate = editingEquipment.PurchaseDate;
            if (editingEquipment.PurchasePrice != null)
                txtPurchasePrice.Text = editingEquipment.PurchasePrice.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string serial = txtSerialNumber.Text.Trim();
            string model = txtModel.Text.Trim();
            string manufacturer = txtManufacturer.Text.Trim();
            int typeId = (int)cboType.SelectedValue;
            int statusId = (int)cboStatus.SelectedValue;
            int? userId = cboUser.SelectedValue as int?;
            DateTime? purchaseDate = dpPurchaseDate.SelectedDate;
            decimal? price = null;
            if (!string.IsNullOrEmpty(txtPurchasePrice.Text))
            {
                decimal parsed;
                if (decimal.TryParse(txtPurchasePrice.Text, out parsed))
                    price = parsed;
            }

            if (string.IsNullOrEmpty(serial) || string.IsNullOrEmpty(model))
            {
                MessageBox.Show("Заполните серийный номер и модель!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (isNew)
            {
                Equipment newEquip = new Equipment
                {
                    SerialNumber = serial,
                    Model = model,
                    Manufacturer = manufacturer,
                    TypeID = typeId,
                    StatusID = statusId,
                    ResponsibleUserID = userId,
                    PurchaseDate = purchaseDate,
                    PurchasePrice = price,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                Conn.inventoryDBEntities.Equipment.Add(newEquip);
            }
            else
            {
                editingEquipment.SerialNumber = serial;
                editingEquipment.Model = model;
                editingEquipment.Manufacturer = manufacturer;
                editingEquipment.TypeID = typeId;
                editingEquipment.StatusID = statusId;
                editingEquipment.ResponsibleUserID = userId;
                editingEquipment.PurchaseDate = purchaseDate;
                editingEquipment.PurchasePrice = price;
                editingEquipment.UpdatedAt = DateTime.Now;
            }

            Conn.inventoryDBEntities.SaveChanges();
            MessageBox.Show("Оборудование сохранено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
