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
using Foodcourt.ViewModel;
using Foodcourt.View;
using Foodcourt.Model;

namespace Foodcourt.View.Oprs
{
    /// <summary>
    /// Interaction logic for PettyCash.xaml
    /// </summary>
    public partial class PettyCash : Page
    {
        Pettycash pc = new Pettycash();
        amount a = new amount();
        public int error = 0;
        public PettyCash()
        {
            DataF.COUNT = 0;
            InitializeComponent();
            DataF.COUNT = 1;
            //a.Amount();
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
            Clear();
            this.NavigationService.Refresh(); 
        }
        public void Clear()
        {
            nametxt.Text = "";
            amounttxt.Text = "";
            detailstxt.Text = "";
        }
        public static decimal pettotal,total,total1,tot;
        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            pettotal = 0;
            try
            {
                if (error != 0 || nametxt.Text == "" || amounttxt.Text == "")
                {
                    if (nametxt.Text == "")
                    { nametxt.Text = ""; }
                    if (amounttxt.Text == "")
                    { amounttxt.Text = ""; }
                    //if (detailstxt.Text == "")
                    //{ detailstxt.Text = ""; }
                }
                else
                {
                    pc.PCH_Name = nametxt.Text;
                    pc.PCH_Amount = amounttxt.Text;
                    pc.PCH_Details = detailstxt.Text;
                    pc.INSERT();
                    MessageBox.Show("Saved successfully");
                    Clear();
                }
            }
            catch(SystemException)
            {

            }
        }
    }
}
