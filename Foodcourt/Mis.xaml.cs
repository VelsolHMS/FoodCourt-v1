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
using System.Data;
using System.Data.SqlClient;

namespace Foodcourt
{
    /// <summary>
    /// Interaction logic for Mis.xaml
    /// </summary>
    public partial class Mis : Page
    {
        MisSales missales = new MisSales();
        public int error = 0;
        public static decimal mistotal = 0;
        public Mis()
        {
            DataF.COUNT = 0;
            InitializeComponent();
            DataF.COUNT = 1;
            vocheridtxt.Text =  missales.GetVocherId().ToString();
            vocheridtxt.IsReadOnly = true;
        }
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                error++;
            else
                error--;
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            mistotal = 0;
            try
            {
                if (error != 0 || (string.IsNullOrWhiteSpace(nametxt.Text)) || (string.IsNullOrWhiteSpace(amounttxt.Text)) || (string.IsNullOrWhiteSpace(detailstxt.Text)))
                {
                    MessageBox.Show("Please fill all fields.!");
                }
                else
                {
                    missales.MIS_Name = nametxt.Text;
                    missales.MIS_Amount = amounttxt.Text;
                    missales.MIS_Details = detailstxt.Text;
                    missales.Insert();
                    MessageBox.Show("Inserted Succesfully");
                    Clear();
                    this.NavigationService.Refresh();
                }
            }
            catch (SystemException)
            {
            }
        }
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            this.NavigationService.Refresh();
        }
        public void Clear()
        {
            nametxt.Text = "";
            amounttxt.Text = "";
            detailstxt.Text = "";
        }
    }
}
