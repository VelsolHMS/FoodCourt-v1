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
    /// Interaction logic for Tax.xaml
    /// </summary>
    public partial class Tax : Page
    {
        tax T = new tax();
        public int error = 0;
        public Tax()
        {
            DataF.COUNT = 0;
            InitializeComponent();
            DataF.COUNT = 1;
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
                    T.Insert();
                    Clear();
                    MessageBox.Show("Inserted Succesfully.!");
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
        }
        private void btnclear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            this.NavigationService.Refresh();
        }
    }
}

