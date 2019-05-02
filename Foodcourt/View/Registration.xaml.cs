using System;
using System.Windows;
using System.Windows.Controls;
using Foodcourt.Model;
using System.Data;
using Foodcourt.ViewModel;

namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        registration R = new registration();
        userpermissions U = new userpermissions();
        public int error = 0;
        public Registration()
        {
            DataF.COUNT = 0;
            InitializeComponent();
            DataF.COUNT = 1;
            DataTable dt = R.UserGrid();
            Register.ItemsSource = dt.DefaultView;
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
            clear();
            this.NavigationService.Refresh();
        }
        public void clear()
        {
            usertxt.Text = "";
            nametxt.Text = "";
            phonetxt.Text = "";
            passwordtxt.Password = "";
            repasswordtxt.Password = "";
        }
        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rbadmin.IsChecked == false && rbuser.IsChecked == false)
                {
                    MessageBox.Show("Select Admin Or User");
                }
                else
                {
                    if (error != 0 || (string.IsNullOrWhiteSpace(nametxt.Text)) || (string.IsNullOrWhiteSpace(usertxt.Text)) || (string.IsNullOrWhiteSpace(phonetxt.Text)) || (string.IsNullOrWhiteSpace(passwordtxt.Password)) || (string.IsNullOrWhiteSpace(repasswordtxt.Password)))
                    {
                        if (nametxt.Text == "")
                        { nametxt.Text = ""; }
                        if (usertxt.Text == "")
                        { usertxt.Text = ""; }
                        if (phonetxt.Text == "")
                        { phonetxt.Text = ""; }
                        if (passwordtxt.Password == "")
                        { passwordtxt.Password = ""; }
                        if (repasswordtxt.Password == "")
                        { repasswordtxt.Password = ""; }
                        MessageBox.Show("Please Fill All Fields");
                    }
                    else
                    {
                        if (passwordtxt.Password == repasswordtxt.Password)
                        {
                            R.REG_Name = nametxt.Text;
                            R.REG_UserName = usertxt.Text;
                            R.REG_Phone = phonetxt.Text;
                            R.REG_Password = passwordtxt.Password;
                            R.REG_RePassword = repasswordtxt.Password;
                            if (rbadmin.IsChecked == true)
                            {
                                R.DESIGNATION = "1";
                                U.USERNAME = usertxt.Text;
                                U.DESIGNATION = "Admin";
                                U.PROPERTY = "1";
                                U.USER_PERMISSIONS = "1";
                                U.USER_REGISTRATION = "1";
                                U.TAX = "1";
                                U.ITEM_CATEGORY = "1";
                                U.ITEM_NAME = "1";
                                U.HOME = "1";
                                U.BILL_VIEW = "1";
                                U.CAN_REPRINT_BILL = "1";
                                U.CAN_DELETE_BILL = "0";
                                U.PETTY_CASH = "1";
                                U.MIS_COLLECTION = "1";
                                U.PAIDOUTS = "1";
                                U.DASHBOARD = "1";
                                U.ITEM_RATE_LIST = "1";
                                U.ITEM_WISE_SALES = "1";
                                U.DAY_WISE_SALES = "1";
                                U.MONTH_WISE_SALE = "1";
                                U.TAX_REPORT = "1";
                                U.BILL_WISE_SALES = "1";
                                U.MasterInsert();
                                U.OperationInsert();
                                U.ReportInsert();
                            }

                            else if (rbuser.IsChecked == true)
                            {
                                R.DESIGNATION = "0";
                                U.USERNAME = usertxt.Text;
                                U.DESIGNATION = "User";
                                U.PROPERTY = "0";
                                U.USER_PERMISSIONS = "0";
                                U.USER_REGISTRATION = "0";
                                U.TAX = "0";
                                U.ITEM_CATEGORY = "0";
                                U.ITEM_NAME = "0";
                                U.HOME = "1";
                                U.BILL_VIEW = "1";
                                U.CAN_REPRINT_BILL = "0";
                                U.CAN_DELETE_BILL = "0";
                                U.PETTY_CASH = "1";
                                U.MIS_COLLECTION = "1";
                                U.PAIDOUTS = "0";
                                U.DASHBOARD = "0";
                                U.ITEM_RATE_LIST = "0";
                                U.ITEM_WISE_SALES = "1";
                                U.DAY_WISE_SALES = "1";
                                U.MONTH_WISE_SALE = "0";
                                U.TAX_REPORT = "0";
                                U.BILL_WISE_SALES = "1";
                                U.MasterInsert();
                                U.OperationInsert();
                                U.ReportInsert();
                            }
                            R.REG_InsertDate = DateTime.Today;
                            R.REG_UpdateDate = DateTime.Today;
                            string a = "Save"; string b = Convert.ToString(btnsave.Content);
                            if (a == b)
                            {
                                R.Insert();
                                MessageBox.Show("Saved Successfully");
                                this.NavigationService.Refresh();
                            }
                            else
                            {
                                R.Update();
                                MessageBox.Show("Updated Successfully");
                                this.NavigationService.Refresh();
                            }
                        }
                        else
                        {
                            repasswordtxt.Password = "";
                            MessageBox.Show("Password Fields are not Matched");
                        }
                    }
                }
            }
            catch(SystemException)
            { }
        }
        public static string name;
        private void usertxt_LostFocus(object sender, RoutedEventArgs e)
        {
            R.REG_UserName = usertxt.Text;
            DataTable DT = R.user();
            if (DT.Rows.Count == 0)
            {

            }
            else
            {
                btnsave.Content = "Update";
                nametxt.Text = DT.Rows[0]["REG_Name"].ToString();
                phonetxt.Text = DT.Rows[0]["REG_Phone"].ToString();
                passwordtxt.Password = DT.Rows[0]["REG_Password"].ToString();
                repasswordtxt.Password = DT.Rows[0]["REG_RePassword"].ToString();
                if (DT.Rows[0]["DESIGNATION"].ToString() == "1")
                {
                    rbadmin.IsChecked = true;
                }
                else if(DT.Rows[0]["DESIGNATION"].ToString() == "0")
                {
                    rbuser.IsChecked = true;
                }
            }
        }
        public int regid = 0,designation; 
        private void Register_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int i = Register.SelectedIndex;
            if (i < 0)
            {
            }
            else
            {
                btnsave.Content = "Update";
                DataTable dt = R.UserGrid();
                regid = Convert.ToInt32(dt.Rows[i]["REG_Id"]);
                usertxt.Text = dt.Rows[i]["REG_UserName"].ToString();
                nametxt.Text = dt.Rows[i]["REG_Name"].ToString();
                phonetxt.Text = dt.Rows[i]["REG_Phone"].ToString();
                designation = Convert.ToInt32(dt.Rows[i]["DESIGNATION"].ToString());
                passwordtxt.Password = dt.Rows[i]["REG_Password"].ToString();
                repasswordtxt.Password = dt.Rows[i]["REG_RePassword"].ToString();
                if (designation == 1)
                {
                    rbadmin.IsChecked = true;
                    rbuser.IsEnabled = false;
                }
                else if (designation == 0)
                {
                    rbuser.IsChecked = true;
                    rbadmin.IsEnabled = false;
                }
            }
        }
    }
}
