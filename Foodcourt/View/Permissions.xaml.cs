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
    /// Interaction logic for Permissions.xaml
    /// </summary>
    public partial class Permissions : Page
    {
        userpermissions U = new userpermissions();
        registration R = new registration();
        public Permissions()
        {
            InitializeComponent();
            DataTable dt = U.username();
            cmbuser.ItemsSource = dt.DefaultView;
            btnsave.Content = "Save";
            btnoprsave.Content = "Save";
            btnresave.Content = "Save";
        }
        public static string u;
        private void cmbpermission_DropDownClosed(object sender, EventArgs e)
        {
            if(cmbuser.Text == "")
            {
                MessageBox.Show("Please Select UserName");
                this.NavigationService.Refresh();
            }
            if(cmbpermission.Text== "Masters")
            {
                Master.Visibility = Visibility.Visible;
                Operations.Visibility = Visibility.Hidden;
                Reports.Visibility = Visibility.Hidden;
                U.USERNAME = cmbuser.Text;
                DataTable d = U.MASTER(); 
                if (d.Rows.Count ==0)
                {
                    Master.Visibility = Visibility.Visible;
                }
                else
                {
                    int PROPERTY = int.Parse(d.Rows[0]["PROPERTY"].ToString());
                    if(PROPERTY==1)
                    {
                        property.IsChecked = true;
                    }
                    else
                    {
                        property.IsChecked = false;
                    }
                    int USER_REGISTRATION = int.Parse(d.Rows[0]["USER_REGISTRATION"].ToString());
                    if(USER_REGISTRATION==1)
                    {
                        reg.IsChecked = true;
                    }
                    else
                    {
                        reg.IsChecked = false;
                    }
                    int TAX = int.Parse(d.Rows[0]["TAX"].ToString());
                    if(TAX==1)
                    {
                        tax.IsChecked = true;
                    }
                    else
                    {
                        tax.IsChecked = false;
                    }
                    int ITEM_CATEGORY = int.Parse(d.Rows[0]["ITEM_CATEGORY"].ToString());
                    if (ITEM_CATEGORY == 1)
                    {
                        itemcategory.IsChecked = true;
                    }
                    else
                    {
                        itemcategory.IsChecked = false;
                    }
                    int ITEM_NAME = int.Parse(d.Rows[0]["ITEM_NAME"].ToString());
                    if (ITEM_NAME == 1)
                    {
                        itemname.IsChecked = true;
                    }
                    else
                    {
                        itemname.IsChecked = false;
                    }
                    int USER_PERMISSIONS = int.Parse(d.Rows[0]["USER_PERMISSIONS"].ToString());
                    if (USER_PERMISSIONS == 1)
                    {
                        permissions.IsChecked = true;
                    }
                    else
                    {
                        permissions.IsChecked = false;
                    }
                }
            }
            else if(cmbpermission.Text == "Operations")
            {
                Master.Visibility = Visibility.Hidden;
                Operations.Visibility = Visibility.Visible;
                Reports.Visibility = Visibility.Hidden;
                U.USERNAME = cmbuser.Text;
                
                DataTable dt = U.OPERATION();
                if (dt.Rows.Count == 0)
                {
                    Operations.Visibility = Visibility.Visible;
                }
                else
                {
                    int BILL_VIEW = int.Parse(dt.Rows[0]["BILL_VIEW"].ToString());
                    if(BILL_VIEW == 1)
                    {
                        billview.IsChecked = true;
                    }
                    else
                    {
                        billview.IsChecked = false;
                    }
                    int PETTY_CASH = int.Parse(dt.Rows[0]["PETTY_CASH"].ToString());
                    if (PETTY_CASH == 1)
                    {
                        pettycash.IsChecked = true;
                    }
                    else
                    {
                        pettycash.IsChecked = false;
                    }
                    int PAIDOUTS = int.Parse(dt.Rows[0]["PAIDOUTS"].ToString());
                    if (PAIDOUTS == 1)
                    {
                        paidouts.IsChecked = true;
                    }
                    else
                    {
                        paidouts.IsChecked = false;
                    }
                    int MIS_COLLECTION = int.Parse(dt.Rows[0]["MIS_COLLECTION"].ToString());
                    if (MIS_COLLECTION == 1)
                    {
                        miscollection.IsChecked = true;
                    }
                    else
                    {
                        miscollection.IsChecked = false;
                    }
                    int HOME = int.Parse(dt.Rows[0]["HOME"].ToString());
                    if (HOME == 1)
                    {
                        home.IsChecked = true;
                    }
                    else
                    {
                        home.IsChecked = false;
                    }
                    int CAN_DELETE_BILL = int.Parse(dt.Rows[0]["CAN_DELETE_BILL"].ToString());
                    if (CAN_DELETE_BILL == 1)
                    {
                        candeletebill.IsChecked = true;
                    }
                    else
                    {
                        candeletebill.IsChecked = false;
                    }
                    int CAN_REPRINT_BILL = int.Parse(dt.Rows[0]["CAN_REPRINT_BILL"].ToString());
                    if (CAN_REPRINT_BILL == 1)
                    {
                        canreprintbill.IsChecked = true;
                    }
                    else
                    {
                        canreprintbill.IsChecked = false;
                    }
                    int DASHBOARD = int.Parse(dt.Rows[0]["DASHBOARD"].ToString());
                    if (DASHBOARD == 1)
                    {
                        dashboard.IsChecked = true;
                    }
                    else
                    {
                        dashboard.IsChecked = false;
                    }

                }
            }
            else if(cmbpermission.Text == "Reports")
            {
                Master.Visibility = Visibility.Hidden;
                Operations.Visibility = Visibility.Hidden;
                Reports.Visibility = Visibility.Visible;
                U.USERNAME = cmbuser.Text;
                

                DataTable d = U.Report();
                if(d.Rows.Count ==0)
                {
                    Reports.Visibility = Visibility.Visible;
                }
                else
                {
                    int ITEM_RATE_LIST = int.Parse(d.Rows[0]["ITEM_RATE_LIST"].ToString());
                    if(ITEM_RATE_LIST == 1)
                    {
                        itemratelist.IsChecked = true;
                    }
                    else
                    {
                        itemratelist.IsChecked = false;
                    }
                    int ITEM_WISE_SALES = int.Parse(d.Rows[0]["ITEM_WISE_SALES"].ToString());
                    if(ITEM_WISE_SALES == 1)
                    {
                        itemwisesales.IsChecked = true;
                    }
                    else
                    {
                        itemwisesales.IsChecked = false;
                    }
                    int DAY_WISE_SALES = int.Parse(d.Rows[0]["DAY_WISE_SALES"].ToString());
                    if(DAY_WISE_SALES ==1)
                    {
                        daywisesales.IsChecked = true;
                    }
                    else
                    {
                        daywisesales.IsChecked = false;
                    }
                    int MONTH_WISE_SALE = int.Parse(d.Rows[0]["MONTH_WISE_SALE"].ToString());
                    if(MONTH_WISE_SALE==1)
                    {
                        monthwisesales.IsChecked = true;
                    }
                    else
                    {
                        monthwisesales.IsChecked = false;
                    }
                    int TAX_REPORT = int.Parse(d.Rows[0]["TAX_REPORT"].ToString());
                    if(TAX_REPORT==1)
                    {
                        taxreport.IsChecked = true;
                    }
                    else
                    {
                        taxreport.IsChecked = false;
                    }
                    int BILL_WISE_SALES = int.Parse(d.Rows[0]["BILL_WISE_SALES"].ToString());
                    if(BILL_WISE_SALES==1)
                    {
                        billwisesales.IsChecked = true;
                    }
                    else
                    {
                        billwisesales.IsChecked = false;
                    }
                    int MIS_COLLECTION = int.Parse(d.Rows[0]["MIS_COLLECTION"].ToString());
                    if (MIS_COLLECTION == 1)
                    {
                        miscollect.IsChecked = true;
                    }
                    else
                    {
                        miscollect.IsChecked = false;
                    }
                }
            }
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            U.USERNAME = cmbuser.Text;
         //   U.DESIGNATION = cmbpermission.Text;
            if(property.IsChecked == true)
            {
                property.Content = 1;
                U.PROPERTY = Convert.ToString(property.Content);
            }
            else if(property.IsChecked == false)
            {
                property.Content = 0;
                U.PROPERTY = Convert.ToString(property.Content);
            }
            if(reg.IsChecked == true)
            {
                reg.Content = 1;
                U.USER_REGISTRATION = Convert.ToString(reg.Content);
            }
            else if (reg.IsChecked == false)
            {
                reg.Content = 0;
                U.USER_REGISTRATION = Convert.ToString(reg.Content);
            }
            if (tax.IsChecked == true)
            {
                tax.Content = 1;
                U.TAX = Convert.ToString(tax.Content);
            }
            else if (tax.IsChecked == false)
            {
                tax.Content = 0;
                U.TAX = Convert.ToString(tax.Content);
            }
            if (itemcategory.IsChecked == true)
            {
                itemcategory.Content = 1;
                U.ITEM_CATEGORY = Convert.ToString(itemcategory.Content);
            }
            else if (itemcategory.IsChecked == false)
            {
                itemcategory.Content = 0;
                U.ITEM_CATEGORY = Convert.ToString(itemcategory.Content);
            }
            if (itemname.IsChecked == true)
            {
                itemname.Content = 1;
                U.ITEM_NAME = Convert.ToString(itemname.Content);
            }
            else if (itemname.IsChecked == false)
            {
                itemname.Content = 0;
                U.ITEM_NAME = Convert.ToString(itemname.Content);
            }
            if (permissions.IsChecked == true)
            {
                permissions.Content = 1;
                U.USER_PERMISSIONS = Convert.ToString(permissions.Content);
            }
            else if (permissions.IsChecked == false)
            {
                permissions.Content = 0;
                U.USER_PERMISSIONS = Convert.ToString(permissions.Content);
            }
            if (rbadmin.IsChecked == true)
            {
                U.DESIGNATION = Convert.ToString(rbadmin.Content);
            }
            else if(rbuser.IsChecked == true)
            {
                U.DESIGNATION = rbuser.Content.ToString();
            }
            DataTable dt = U.MASTER();
            string a = "Save"; string b = Convert.ToString(btnsave.Content);
            if (dt.Rows.Count < 1)
            {
                if (a == b)
                {
                    U.MasterInsert();
                    MessageBox.Show("Saved Successfully");
                }
            }
            else
            {
                btnsave.Content = "Update";
                U.MasterUpdate();
                MessageBox.Show("Updated Successfully");
                btnsave.Content = "Save";
            }
            cmbuser.Text = "";
            cmbpermission.Text = "";
            this.NavigationService.Refresh();
        }
        public static int s;
        public static string us;
        private void cmbuser_DropDownClosed(object sender, EventArgs e)
        {
            if(cmbuser.Text=="")
            {
                MessageBox.Show("Please Select UserName");
                this.NavigationService.Refresh();
            }
            rbadmin.IsEnabled = true;
            rbuser.IsEnabled = true;
            Master.Visibility = Visibility.Hidden;
            Operations.Visibility = Visibility.Hidden;
            Reports.Visibility = Visibility.Hidden;
            U.REG_UserName = cmbuser.Text; 
            DataTable dt = U.MASTERUSER();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows.Count == 0)
                {
                    break;
                }
                else
                {
                    us = dt.Rows[i]["USERNAME"].ToString();
                    if (U.REG_UserName == us)
                    {
                        btnsave.Content = "Update";
                        break;
                    }
                }
            }
            DataTable dt2 = U.OPERATIONUSER();
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                if (dt2.Rows.Count == 0)
                {
                    btnoprsave.Content = "Save";
                    break;
                }
                else
                {
                    us = dt2.Rows[i]["USERNAME"].ToString();
                    if (U.REG_UserName == us)
                    {
                        btnoprsave.Content = "Update";
                        break;
                    }
                }
            }
            DataTable dt3 = U.ReportUser();
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                if (dt3.Rows.Count == 0)
                {
                    btnresave.Content = "Save"; 
                    break;
                }
                else
                {
                    us = dt3.Rows[i]["USERNAME"].ToString();
                    if (U.REG_UserName == us)
                    {
                        btnresave.Content = "Update";
                        break;
                    }
                }
            }
                DataTable dt1 = U.regdes();
            if (dt1.Rows.Count == 0)
            {

            }
            else
            {
                R.DESIGNATION = dt1.Rows[0]["DESIGNATION"].ToString();
            }
            if(R.DESIGNATION == null)
                {
                        
                }
                else if (R.DESIGNATION!="")
                {
                    
                    s = int.Parse(dt1.Rows[0]["DESIGNATION"].ToString());
                    if (s == 1)
                    {
                        rbadmin.IsChecked = true;
                        rbuser.IsEnabled = false;
                    }
                    else if (s == 0)
                    {
                        rbuser.IsChecked = true;
                        rbadmin.IsEnabled = false;
                    }
                    
                }
        }

        private void btnoprsave_Click(object sender, RoutedEventArgs e)
        {
            U.USERNAME = cmbuser.Text;
            if (billview.IsChecked == true)
            {
                billview.Content = 1;
                U.BILL_VIEW = Convert.ToString(billview.Content);
            }
            else if (billview.IsChecked == false)
            {
                billview.Content = 0;
                U.BILL_VIEW = Convert.ToString(billview.Content);
            }
            if (pettycash.IsChecked == true)
            {
                pettycash.Content = 1;
                U.PETTY_CASH = Convert.ToString(pettycash.Content);
            }
            else if (pettycash.IsChecked == false)
            {
                pettycash.Content = 0;
                U.PETTY_CASH = Convert.ToString(pettycash.Content);
            }
            if (paidouts.IsChecked == true)
            {
                paidouts.Content = 1;
                U.PAIDOUTS = Convert.ToString(paidouts.Content);
            }
            else if (paidouts.IsChecked == false)
            {
                paidouts.Content = 0;
                U.PAIDOUTS = Convert.ToString(paidouts.Content);
            }
            if (miscollection.IsChecked == true)
            {
                miscollection.Content = 1;
                U.MIS_COLLECTION = Convert.ToString(miscollection.Content);
            }
            else if (miscollection.IsChecked == false)
            {
                miscollection.Content = 0;
                U.MIS_COLLECTION = Convert.ToString(miscollection.Content);
            }
            if (home.IsChecked == true)
            {
                home.Content = 1;
                U.HOME = Convert.ToString(home.Content);
            }
            else if (home.IsChecked == false)
            {
                home.Content = 0;
                U.HOME = Convert.ToString(home.Content);
            }
            if (candeletebill.IsChecked == true)
            {
                candeletebill.Content = 1;
                U.CAN_DELETE_BILL = Convert.ToString(candeletebill.Content);
            }
            else if (candeletebill.IsChecked == false)
            {
                candeletebill.Content = 0;
                U.CAN_DELETE_BILL = Convert.ToString(candeletebill.Content);
            }
            if (canreprintbill.IsChecked == true)
            {
                canreprintbill.Content = 1;
                U.CAN_REPRINT_BILL = Convert.ToString(canreprintbill.Content);
            }
            else if (canreprintbill.IsChecked == false)
            {
                canreprintbill.Content = 0;
                U.CAN_REPRINT_BILL = Convert.ToString(canreprintbill.Content);
            }
            if (dashboard.IsChecked == true)
            {
                dashboard.Content = 1;
                U.DASHBOARD = Convert.ToString(dashboard.Content);
            }
            else if (dashboard.IsChecked == false)
            {
                dashboard.Content = 0;
                U.DASHBOARD = Convert.ToString(dashboard.Content);
            }
            if (rbadmin.IsChecked == true)
            {
                U.DESIGNATION = Convert.ToString(rbadmin.Content);
            }
            else if (rbuser.IsChecked == true)
            {
                U.DESIGNATION = rbuser.Content.ToString();
            }
            DataTable dt = U.OPERATION();
            string a = "Save"; string b = Convert.ToString(btnoprsave.Content);
            if (dt.Rows.Count < 1)
            { 
                if (a == b)
                {
                    U.OperationInsert();
                    MessageBox.Show("Saved Successfully");
                }
            }
            else 
            {
                btnoprsave.Content = "Update";
                U.OperationUpdate();
                MessageBox.Show("Updated Successfully");
                btnoprsave.Content = "Save";
            }
                cmbuser.Text = "";
                cmbpermission.Text = "";
                this.NavigationService.Refresh();
            
        }

        private void btnresave_Click(object sender, RoutedEventArgs e)
        {
            U.USERNAME = cmbuser.Text;
            if(itemratelist.IsChecked == true)
            {
                itemratelist.Content = 1;
                U.ITEM_RATE_LIST = Convert.ToString(itemratelist.Content);
            }
            else if(itemratelist.IsChecked ==false)
            {
                itemratelist.Content = 0;
                U.ITEM_RATE_LIST = Convert.ToString(itemratelist.Content);
            }
            if(itemwisesales.IsChecked == true)
            {
                itemwisesales.Content = 1;
                U.ITEM_WISE_SALES = Convert.ToString(itemwisesales.Content);
            }
            else if(itemwisesales.IsChecked == false)
            {
                itemwisesales.Content = 0;
                U.ITEM_WISE_SALES = Convert.ToString(itemwisesales.Content);
            }
            if(daywisesales.IsChecked == true)
            {
                daywisesales.Content = 1;
                U.DAY_WISE_SALES = Convert.ToString(daywisesales.Content);
            }
            else if(daywisesales.IsChecked == false)
            {
                daywisesales.Content = 0;
                U.DAY_WISE_SALES = Convert.ToString(daywisesales.Content);
            }
            if(monthwisesales.IsChecked == true)
            {
                monthwisesales.Content = 1;
                U.MONTH_WISE_SALE = Convert.ToString(monthwisesales.Content);
            }
            else if(monthwisesales.IsChecked == false)
            {
                monthwisesales.Content = 0;
                U.MONTH_WISE_SALE = Convert.ToString(monthwisesales.Content);
            }
            if(taxreport.IsChecked == true)
            {
                taxreport.Content = 1;
                U.TAX_REPORT = Convert.ToString(taxreport.Content);
            }
            else if(taxreport.IsChecked == false)
            {
                taxreport.Content = 0;
                U.TAX_REPORT = Convert.ToString(taxreport.Content);
            }
            if(billwisesales.IsChecked == true)
            {
                billwisesales.Content = 1;
                U.BILL_WISE_SALES = Convert.ToString(billwisesales.Content);
            }
            else if(billwisesales.IsChecked == false)
            {
                billwisesales.Content = 0;
                U.BILL_WISE_SALES = Convert.ToString(billwisesales.Content);
            }
            if (miscollect.IsChecked == true)
            {
                miscollect.Content = 1;
                U.MIS_COLLECTION = Convert.ToString(miscollect.Content);
            }
            else if (miscollect.IsChecked == false)
            {
                miscollect.Content = 0;
                U.MIS_COLLECTION = Convert.ToString(miscollect.Content);
            }
            if (rbadmin.IsChecked == true)
            {
                U.DESIGNATION = Convert.ToString(rbadmin.Content);
            }
            else if (rbuser.IsChecked == true)
            {
                U.DESIGNATION = rbuser.Content.ToString();
            }
            DataTable dt = U.Report();
            string a = "Save"; string b = Convert.ToString(btnresave.Content);
            if (dt.Rows.Count < 1)
            {
                if (a == b)
                {
                    U.ReportInsert();
                    MessageBox.Show("Saved Successfully");
                }
            }
            else
            {
                btnresave.Content = "Update";
                U.ReportUpdate();
                //MessageBox.Show("Updated Successfully");
                btnresave.Content = "Save";
            }
            cmbuser.Text = "";
            cmbpermission.Text = "";
            this.NavigationService.Refresh();
        }
    }
}
