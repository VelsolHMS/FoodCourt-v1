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

namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for Property.xaml
    /// </summary>
    public partial class Property : Page
    {
        property P = new property();
        public int error = 0;
        public Property()
        {
            DataF.COUNT = 0;
            InitializeComponent();
            DataF.COUNT = 1;
            DataTable dt = P.NAME();
            if (dt.Rows.Count == 1)
            {
                btnsave.Content = "Update";
                nametxt.Text = dt.Rows[0]["PRPT_Name"].ToString(); ;
                statetxt.Text=dt.Rows[0]["PRPT_State"].ToString();
                addresstxt.Text= dt.Rows[0]["PRPT_Address"].ToString();
                countrytxt.Text= dt.Rows[0]["PRPT_Country"].ToString() ;
                gsttxt.Text= dt.Rows[0]["PRPT_GST"].ToString();
                phonetxt.Text= dt.Rows[0]["PRPT_Phone"].ToString();
                P.PRPT_UpdateDate = DateTime.Today;
            }
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
            phonetxt.Text = "";
            gsttxt.Text = "";
            addresstxt.Text = "";
            this.NavigationService.Refresh();
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (error != 0 || (string.IsNullOrWhiteSpace(nametxt.Text)) || (string.IsNullOrWhiteSpace(phonetxt.Text)))
                {
                    if (nametxt.Text == "")
                    { nametxt.Text = ""; }
                    if (phonetxt.Text == "")
                    { phonetxt.Text = ""; }
                }
                else
                {
                    P.PRPT_Name = nametxt.Text;
                    P.PRPT_State = statetxt.Text;
                    P.PRPT_Address = addresstxt.Text;
                    P.PRPT_Country = countrytxt.Text;
                    P.PRPT_GST = gsttxt.Text;
                    P.PRPT_Phone = phonetxt.Text;
                    P.PRPT_InsertDate = DateTime.Today;
                    P.PRPT_UpdateDate = DateTime.Today;
                    DataTable dt = P.NAME();
                    string a = "Save"; string b = Convert.ToString(btnsave.Content);
                    if (dt.Rows.Count < 1)
                    {

                        if (a == b)
                        {
                            P.Insert();
                            MessageBox.Show("Saved Successfully");
                            Clear();
                            this.NavigationService.Refresh();
                        }
                    }
                    else if(dt.Rows.Count==1)
                    {
                        P.Update();
                        MessageBox.Show("Updated Successfully");
                        Clear();
                        btnsave.Content = "Save";
                        this.NavigationService.Refresh();
                    }
                }
            }
            catch (SystemException)
            { }
        }
        public void Clear()
        {
            nametxt.Text = "";
            statetxt.Text = "";
            addresstxt.Text = "";
            countrytxt.Text = "";
            gsttxt.Text = "";
            phonetxt.Text = "";
        }
    }
}
