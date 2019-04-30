using Foodcourt.Model;
using Foodcourt.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Tax.xaml
    /// </summary>
    public partial class Tax : Page
    {
        tax T = new tax();
        public int error = 0;
        public int taxId = 0;
        DataTable dt_taxes;
        public Tax()
        {
            DataF.COUNT = 0;
            InitializeComponent();
            DataF.COUNT = 1;
            btnsave.Content = "Save";
            dt_taxes = T.GetTax();
            taxes.ItemsSource = dt_taxes.DefaultView;
        }
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                error++;
            else
                error--;
        }
        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (error != 0 || (string.IsNullOrWhiteSpace(percentagetxt.Text)) || (string.IsNullOrWhiteSpace(nametxt.Text)) || (string.IsNullOrWhiteSpace(reportingnametxt.Text)))
                {
                    if (percentagetxt.Text == "")
                    { percentagetxt.Text = ""; }
                    if (nametxt.Text == "")
                    { nametxt.Text = ""; }
                    if (reportingnametxt.Text == "")
                    { reportingnametxt.Text = ""; }
                }
                else
                {
                    T.TAX_Percentage = Convert.ToDecimal(percentagetxt.Text);
                    T.TAX_Name = nametxt.Text;
                    T.Tax_ReportingName = reportingnametxt.Text;
                    if (btnsave.Content.ToString() == "Save")
                    {
                        T.Insert();
                        MessageBox.Show("Inserted Succesfully.!");
                    }
                    else
                    {
                        T.TAX_ID = taxId.ToString();
                        T.Update();
                        MessageBox.Show("Updated Succesfully.!");
                    }
                    this.NavigationService.Refresh();
                }
            }
            catch(SystemException)
            { }
        }
        public void Clear()
        {
            percentagetxt.Text = "";
            nametxt.Text = "";
            reportingnametxt.Text = "";
            btnsave.Content = "Save";
        }
        private void btnclear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            this.NavigationService.Refresh();
        }

        private void Taxes_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int selected_index = taxes.SelectedIndex;
            if (selected_index < 0)
            {
            }
            else
            {
                btnsave.Content = "Update";
                dt_taxes = T.GetTax();
                taxId = Convert.ToInt32(dt_taxes.Rows[selected_index]["TAX_ID"]);
                nametxt.Text = dt_taxes.Rows[selected_index]["TAX_Name"].ToString();
                percentagetxt.Text = dt_taxes.Rows[selected_index]["TAX_Percentage"].ToString();
                reportingnametxt.Text = dt_taxes.Rows[selected_index]["Tax_ReportingName"].ToString();
            }
        }
    }
}

