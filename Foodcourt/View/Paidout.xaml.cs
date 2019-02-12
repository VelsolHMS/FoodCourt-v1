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
using System.Data.SqlClient;
using System.Data;
using Foodcourt.View;
using Foodcourt.ViewModel;

namespace Foodcourt.View.Oprs
{
    /// <summary>
    /// Interaction logic for Paidout.xaml
    /// </summary>
    public partial class Paidout : Page
    {
        paidout paid = new paidout();
        public int error = 0;
        public static decimal paidtotal;
        public Paidout()
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
        private void btnclear_Click(object sender, RoutedEventArgs e)
        {
            nametxt.Text = "";
            amounttxt.Text = "";
            descriptiontxt.Text = "";
        }
        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            paidtotal = 0;
            try
            {
                if(error!=0 || (string.IsNullOrWhiteSpace(nametxt.Text)) || (string.IsNullOrWhiteSpace(amounttxt.Text)))
                {
                    if (nametxt.Text == "")
                    {
                        nametxt.Text = "";
                    }
                    if(amounttxt.Text == "")
                    {
                        amounttxt.Text="";
                    }
                }
                else
                {
                    paid.PO_Name = nametxt.Text;
                    paid.PO_Amount = amounttxt.Text;
                    paid.PO_Description = descriptiontxt.Text;
                    paid.PO_InsertDate = DateTime.Today;
                    paid.Insert();
                    MessageBox.Show("Saved Successfully");
                    this.NavigationService.Refresh();
                }
            }
            catch(SystemException )
            { }
        }
    }
}
