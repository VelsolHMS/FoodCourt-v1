using Foodcourt.Model;
using Foodcourt.ViewModel;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for Stalls.xaml
    /// </summary>
    public partial class Stalls : Page
    {
        public int error = 0;
        stalls stl = new stalls();
        DataTable dt = new DataTable();
        DataTable getDetails;
        public Stalls()
        {
            DataF.COUNT = 0;
            InitializeComponent();
            DataF.COUNT = 1;
            txtId.Text = stl.GetItemId().ToString();
            txtId.IsReadOnly = true;
            dt = stl.FillGrid();
            dgstall.ItemsSource = dt.DefaultView;
        }
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                error++;
            else
                error--;
        }
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            this.NavigationService.Refresh();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (error != 0 || (string.IsNullOrWhiteSpace(txtId.Text)) || (string.IsNullOrWhiteSpace(txtName.Text)) || (string.IsNullOrWhiteSpace(txtrpt.Text)) || (string.IsNullOrWhiteSpace(txtdts.Text)) || (string.IsNullOrWhiteSpace(txtstatus.Text)))
                {
                    MessageBox.Show("Please fill all fields");
                }
                else
                {
                    stl.STL_Name = txtName.Text;
                    stl.STL_ReportingName = txtrpt.Text;
                    stl.STL_Description = txtdts.Text;
                    stl.STL_Status = txtstatus.Text;
                    if (btnSave.Content.ToString() == "Update")
                    {
                        stl.STL_ID = txtId.Text;
                        stl.UPDATE();
                        MessageBox.Show("Updated Succesfully");
                    }
                    else
                    {
                        stl.INSERT();
                        MessageBox.Show("Inserted Succesfully");
                    }
                    Clear();
                    this.NavigationService.Refresh();
                    txtId.Text = stl.GetItemId().ToString();
                    txtId.IsReadOnly = true;
                }
            }
            catch (SystemException)
            {
            }
        }

        public void Clear()
        {
            txtName.Text = "";
            txtrpt.Text = "";
            txtdts.Text = "";
            txtstatus.Text = "";
            btnSave.Content = "Save";
        }
        private void Dgstall_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int selected_index = dgstall.SelectedIndex;
            if (selected_index < 0)
            {

            }
            else
            {
                stl.STL_ID = dt.Rows[selected_index]["STL_ID"].ToString();
                getDetails = stl.GetAllDetails();
                txtId.Text = getDetails.Rows[0]["STL_ID"].ToString();
                txtName.Text = getDetails.Rows[0]["STL_Name"].ToString();
                txtrpt.Text = getDetails.Rows[0]["STL_ReportingName"].ToString();
                txtdts.Text = getDetails.Rows[0]["STL_Description"].ToString();
                txtstatus.Text = getDetails.Rows[0]["STL_Status"].ToString();
                btnSave.Content = "Update";
            }
        }
    }
}
