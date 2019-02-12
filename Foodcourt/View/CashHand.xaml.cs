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
using DAL;
using Foodcourt.Model;
using Foodcourt.View;
using Foodcourt.ViewModel;

namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for CashHand.xaml
    /// </summary>
    public partial class CashHand : Page
    {
        cashhand ch = new cashhand();
        public CashHand()
        {
            InitializeComponent();   
        }
        public static decimal cashtotal;
        private void submit_Click(object sender, RoutedEventArgs e)
        {
            cashtotal = 0;
            ch.CH_Name = txtname.Text;
            ch.CH_Amount = txtamount.Text;
            ch.CH_Description = txtreason.Text;
            ch.Insert();
            MessageBox.Show("Saved Successfully");
            Clear();
            this.NavigationService.Refresh();
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }
        public void Clear()
        {
            txtname.Text = "";
            txtamount.Text = "";
            txtreason.Text = "";
        }
    }
}
