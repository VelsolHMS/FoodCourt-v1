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
using Foodcourt.Model;
using Foodcourt.ViewModel;
using System.Data;


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

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (error != 0 || (string.IsNullOrWhiteSpace(txtId.Text)) || (string.IsNullOrWhiteSpace(txtName.Text)))
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
                        //stl.UPDATE();
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

        }
    }
}
