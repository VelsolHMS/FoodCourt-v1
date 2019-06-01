using Foodcourt.Model;
using Foodcourt.ViewModel;
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

namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for OffersZone.xaml
    /// </summary>
    public partial class OffersZone : Page
    {
        Offers offers = new Offers();
        public int error = 0;
        public OffersZone()
        {
            DataF.COUNT = 0;
            InitializeComponent();
            DataF.COUNT = 1;
            txtId.Text = Convert.ToString(offers.id());
        }
        public void Clear()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtPercentage.Text = "";
            txtAmount.Text = "";
            txtrpt.Text = "";
            txtstatus.SelectedIndex = 0;
        }
        public string a, b;
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                error++;
            else
                error--;
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (error != 0 || (string.IsNullOrWhiteSpace(txtId.Text)) || (string.IsNullOrWhiteSpace(txtName.Text)) || (string.IsNullOrWhiteSpace(txtPercentage.Text)) || (string.IsNullOrWhiteSpace(txtstatus.Text)))
                {
                    MessageBox.Show("Please fill all fields");
                }
                else
                {
                    offers.OFF_ID = Convert.ToInt32(txtId.Text);
                    offers.OFF_Name = txtName.Text;
                    offers.OFF_Percentage = txtPercentage.Text;
                    offers.OFF_MaxAmount = Convert.ToDecimal(txtAmount.Text);
                    offers.OFF_ReportingName = txtrpt.Text;
                    offers.OFF_Status = txtstatus.Text;
                    string a = "Save"; b = Convert.ToString(btnSave.Content);
                    if (a == b)
                    {
                        offers.Insert();
                    }
                    else
                    {
                        offers.UPDATE();
                    }
                    //DataTable dtt = it.FillDataGrid();
                    //dgctg.ItemsSource = dtt.DefaultView;
                    Clear();
                    this.NavigationService.Refresh();
                    MessageBox.Show("Saved successfully");
                    btnSave.Content = "Save";
                }
            }
            catch (SystemException)
            { }
        }

        private void Dgoff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            btnSave.Content = "Save";
            this.NavigationService.Refresh();
        }
    }
}
