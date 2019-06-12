using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Data;
using Foodcourt.Model;
using Foodcourt.ViewModel;
using CrystalDecisions.CrystalReports.Engine;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Foodcourt.View.Oprs
{
    /// <summary>
    /// Interaction logic for POS.xaml
    /// </summary>
    public partial class POS : Page
    {
        BillVIEW BV = new BillVIEW();
        POS1 pos = new POS1();
        property p = new property();
        ItemCategory ic = new ItemCategory();
        ItemsCAT it = new ItemsCAT();
        public static string aa;
        public DataTable dt;
        public int error = 0;
        Regex alp = new Regex(@"^[a-zA-Z0-9 -():]+$");
        Regex num = new Regex(@"^[0-9]+$");
        public POS()
        {
            DataF.COUNT = 0;
            InitializeComponent();
            DataF.COUNT = 1;
            DataTable dt = pos.GETPROPERTY();
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                txtname.Text = dt.Rows[0]["PRPT_Name"].ToString();
                txtbillno.Text = pos.BILLID().ToString();
                txttime.Text = DateTime.Now.ToShortTimeString();
            }
            sp.Visibility = Visibility.Visible;
            itemname.Focus();
            cou = 0;
            clear();
        }
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                error++;
            else
                error--;
        }
        public static string stlid,ctgname;
        
        public static int COUNT = 0;
        public static decimal CGST, SGST,tax;
        public static List<string> checkbox_checks = new List<string>();
        public static List<string> qty = new List<string>();
        public static List<int> tt = new List<int>();
        public static string sa, sa1, NAM_Name, A, a, check;
        public static int NAM_Rate, Quantity, SUM = 0, tot, Total11;
        private void txttotal_GotFocus(object sender, RoutedEventArgs e)
        {
        }
        DataTable d = new DataTable();
        public decimal dis, ins,gndtot,ttam;
        public DataTable Billprint()
        {
            DataTable billprint = new DataTable();
            billprint.Columns.Add("Name", typeof(string));
            billprint.Columns.Add("Address", typeof(string));
            billprint.Columns.Add("GstNo", typeof(string));
            billprint.Columns.Add("BillNo", typeof(string));
            billprint.Columns.Add("Total", typeof(decimal));
            billprint.Columns.Add("Cgst", typeof(decimal));
            billprint.Columns.Add("Sgst", typeof(decimal));
            billprint.Columns.Add("GrandTotal", typeof(decimal));
            billprint.Columns.Add("Discount", typeof(decimal));
            DataRow DROW = billprint.NewRow();
            DataTable dt1 = pos.Address();
            DataTable dt2 = pos.items();
            DROW["Name"] = dt1.Rows[0]["PRPT_Name"].ToString();
            DROW["Address"] = dt1.Rows[0]["PRPT_Address"].ToString();
            DROW["GstNO"] = dt1.Rows[0]["PRPT_GST"].ToString();
            DROW["BillNo"] =txtbillno.Text;
            ttam = Convert.ToDecimal(dt2.Rows[0]["BILL_Amount"]);
            DROW["Total"] = (int)Math.Round(ttam);
            decimal tax =Convert.ToDecimal(dt2.Rows[0]["BILL_Tax"].ToString());
            //DROW["Cgst"] = tax / 2;
            //DROW["Sgst"] = tax / 2;
            DROW["Cgst"] = (int)Math.Round(tax5sum);
            DROW["Sgst"] = (int)Math.Round(tax18sum);
            dis = Convert.ToDecimal(dt2.Rows[0]["BILL_Discount"].ToString());
            ins = Convert.ToDecimal(dt2.Rows[0]["Bill_InstantDis"].ToString());
            DROW["Discount"] = (int)Math.Round(dis + ins);
            gndtot = Convert.ToDecimal(dt2.Rows[0]["BILL_Total"].ToString());
            DROW["GrandTotal"] = (int)Math.Round(gndtot);
            billprint.Rows.Add(DROW);
            return billprint;
        }
        public DataTable Billprint1()
        {
            DataTable d = new DataTable();
            d.Columns.Add("Item",typeof(string));
            d.Columns.Add("Rate", typeof(decimal));
            d.Columns.Add("Quantity", typeof(int));
            d.Columns.Add("Amount", typeof(decimal));
            DataTable s = pos.PrintBill();
            for (int k = 0; k < s.Rows.Count; k++)
            {
                DataRow row = d.NewRow();
                row["Item"] = s.Rows[k]["BILITM_Name"].ToString();
                row["Rate"] = s.Rows[k]["BILITM_Rate"].ToString();
                row["Quantity"] = s.Rows[k]["BILLITM_Quanty"].ToString();
                decimal a = Convert.ToDecimal(s.Rows[k]["BILITM_Rate"].ToString());
                int b = Convert.ToInt32(s.Rows[k]["BILLITM_Quanty"].ToString());
                row["Amount"] = a * b;
                d.Rows.Add(row);
            }
            return d;
        }
        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            DataTable DT = pos.itmnames();
            DT.Rows.Clear();
        }
        public static string check1, t1,t2;
        public static string bid;
        public static DataTable subdata;
        public static int cou = 0;
        public string sri;
        public int s;
        public decimal rate,rate1,rate2,rate3, rate4, rate5, rate6, rate7, rate8, rate9, rate10, rate11, rate12, rate13, rate14, rate15,rate16,rate17,rate18,rate19,rate20,rate21;
        //private void btnsave_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        pos.NAME = txtnamee.Text;
        //        pos.MOBILE_NO = txtphone.Text;
        //        pos.EMAIL = txtemail.Text;
        //        pos.CITY = txtaddress.Text;
        //        pos.ADDRESS = txtadd.Text;
        //        string a = "Submit"; string b = Convert.ToString(btnsave.Content);
        //        if (a == b)
        //        {
        //            pos.custinfo();
        //            MessageBox.Show("Saved Successfully");
        //            clear1();
        //        }
        //        else
        //        {
        //            btnsave.Content = "Update";
        //            MessageBox.Show("Updated Successfully");
        //            clear1();
        //            btnsave.Content = "Submit";
        //        }
        //    }
        //    catch(SystemException )
        //    {
        //    }
        //}
        public static decimal tax5sum,tax18sum,ttotal, maxamount,disper,amdis;
        public decimal tax15, tax25, tax35, tax45, tax55, tax65, tax75, tax85, tax95, tax105, tax115, tax125, tax135, tax145, tax155, tax165, tax175, tax185, tax195, tax205;
        private void Itemname_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname.Text = "";
                TOTITM.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM.Focus();
                TOTITM.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname.Text == "")
                {
                    quantity.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM.ItemsSource = dt.DefaultView;
                }
            }
        }
        public static string id,itm,aaid;
        private void Itemname_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname.Text == "")
                    { }
                    else
                    {
                        itemname.Text = "";
                    }
                }
            }
            else { itemname.Text = ""; }
            itm = itemname.Text;
        }
        private void Itemname_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname.Text.Trim() != "")
            {
                aaid = itemname.Text;
                pos.likea = itemname.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM.Visibility = Visibility.Visible;
                    itemname.Text = aaid;
                    if (TOTITM.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM.ItemsSource = dt.DefaultView;
                    TOTITM.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname1_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname1.Text = "";
                TOTITM1.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM1.Focus();
                TOTITM1.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity1.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM1.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname1.Text == "")
                {
                    quantity1.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM1.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname1_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname1.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname1.Text == "")
                    { }
                    else
                    {
                        itemname1.Text = "";
                    }
                }
            }
            else { itemname1.Text = ""; }
            itm = itemname1.Text;
        }
        private void Itemname1_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname1.Text.Trim() != "")
            {
                aaid = itemname1.Text;
                pos.likea = itemname1.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM1.Visibility = Visibility.Visible;
                    itemname1.Text = aaid;
                    if (TOTITM1.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM1.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM1.ItemsSource = dt.DefaultView;
                    TOTITM1.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM1.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname2_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname2.Text = "";
                TOTITM2.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM2.Focus();
                TOTITM2.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity2.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM2.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname2.Text == "")
                {
                    quantity2.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM2.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname2_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname2.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname2.Text == "")
                    { }
                    else
                    {
                        itemname2.Text = "";
                    }
                }
            }
            else { itemname2.Text = ""; }
            itm = itemname2.Text;
        }

        private void Itemname2_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname2.Text.Trim() != "")
            {
                aaid = itemname2.Text;
                pos.likea = itemname2.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM2.Visibility = Visibility.Visible;
                    itemname2.Text = aaid;
                    if (TOTITM2.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM2.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM2.ItemsSource = dt.DefaultView;
                    TOTITM2.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM2.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname3_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname3.Text = "";
                TOTITM3.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM3.Focus();
                TOTITM3.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity3.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM3.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname3.Text == "")
                {
                    quantity3.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM3.ItemsSource = dt.DefaultView;
                }
            }
        }

        private void Itemname3_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname3.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname3.Text == "")
                    { }
                    else
                    {
                        itemname3.Text = "";
                    }
                }
            }
            else { itemname3.Text = ""; }
            itm = itemname3.Text;
        }

        private void Itemname3_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname3.Text.Trim() != "")
            {
                aaid = itemname3.Text;
                pos.likea = itemname3.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM3.Visibility = Visibility.Visible;
                    itemname3.Text = aaid;
                    if (TOTITM3.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM3.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM3.ItemsSource = dt.DefaultView;
                    TOTITM3.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM3.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname4_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname4.Text = "";
                TOTITM4.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM4.Focus();
                TOTITM4.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity4.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM4.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname4.Text == "")
                {
                    quantity4.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM4.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname4_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname4.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname4.Text == "")
                    { }
                    else
                    {
                        itemname4.Text = "";
                    }
                }
            }
            else { itemname4.Text = ""; }
            itm = itemname4.Text;
        }
        private void Itemname4_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname4.Text.Trim() != "")
            {
                aaid = itemname4.Text;
                pos.likea = itemname4.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM4.Visibility = Visibility.Visible;
                    itemname4.Text = aaid;
                    if (TOTITM4.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM4.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM4.ItemsSource = dt.DefaultView;
                    TOTITM4.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM4.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname5_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname5.Text = "";
                TOTITM5.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM5.Focus();
                TOTITM5.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity5.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM5.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname5.Text == "")
                {
                    quantity5.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM5.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname5_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname5.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname5.Text == "")
                    { }
                    else
                    {
                        itemname5.Text = "";
                    }
                }
            }
            else { itemname5.Text = ""; }
            itm = itemname5.Text;
        }
        private void Itemname5_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname5.Text.Trim() != "")
            {
                aaid = itemname5.Text;
                pos.likea = itemname5.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM5.Visibility = Visibility.Visible;
                    itemname5.Text = aaid;
                    if (TOTITM5.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM5.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM5.ItemsSource = dt.DefaultView;
                    TOTITM5.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM5.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname6_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname6.Text = "";
                TOTITM6.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM6.Focus();
                TOTITM6.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity6.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM6.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname6.Text == "")
                {
                    quantity6.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM6.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname6_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname6.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname6.Text == "")
                    { }
                    else
                    {
                        itemname6.Text = "";
                    }
                }
            }
            else { itemname6.Text = ""; }
            itm = itemname6.Text;
        }
        private void Itemname6_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname6.Text.Trim() != "")
            {
                aaid = itemname6.Text;
                pos.likea = itemname6.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM6.Visibility = Visibility.Visible;
                    itemname6.Text = aaid;
                    if (TOTITM6.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM6.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM6.ItemsSource = dt.DefaultView;
                    TOTITM6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM6.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname7_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname7.Text = "";
                TOTITM7.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM7.Focus();
                TOTITM7.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity7.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM7.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname7.Text == "")
                {
                    quantity7.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM7.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname7_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname7.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname7.Text == "")
                    { }
                    else
                    {
                        itemname7.Text = "";
                    }
                }
            }
            else { itemname7.Text = ""; }
            itm = itemname7.Text;
        }

        private void Itemname7_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname7.Text.Trim() != "")
            {
                aaid = itemname7.Text;
                pos.likea = itemname7.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM7.Visibility = Visibility.Visible;
                    itemname7.Text = aaid;
                    if (TOTITM7.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM7.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM7.ItemsSource = dt.DefaultView;
                    TOTITM7.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM7.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname8_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname8.Text = "";
                TOTITM8.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM8.Focus();
                TOTITM8.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity8.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM8.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname8.Text == "")
                {
                    quantity8.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM8.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname8_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname8.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname8.Text == "")
                    { }
                    else
                    {
                        itemname8.Text = "";
                    }
                }
            }
            else { itemname8.Text = ""; }
            itm = itemname8.Text;
        }
        private void Itemname8_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname8.Text.Trim() != "")
            {
                aaid = itemname8.Text;
                pos.likea = itemname8.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM8.Visibility = Visibility.Visible;
                    itemname8.Text = aaid;
                    if (TOTITM8.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM8.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM8.ItemsSource = dt.DefaultView;
                    TOTITM8.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM8.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname9_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname9.Text = "";
                TOTITM9.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM9.Focus();
                TOTITM9.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity9.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM9.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname9.Text == "")
                {
                    quantity9.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM9.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname9_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname9.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname9.Text == "")
                    { }
                    else
                    {
                        itemname9.Text = "";
                    }
                }
            }
            else { itemname9.Text = ""; }
            itm = itemname9.Text;
        }
        private void Itemname9_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname9.Text.Trim() != "")
            {
                aaid = itemname9.Text;
                pos.likea = itemname9.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM9.Visibility = Visibility.Visible;
                    itemname9.Text = aaid;
                    if (TOTITM9.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM9.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM9.ItemsSource = dt.DefaultView;
                    TOTITM9.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM9.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname10_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname10.Text = "";
                TOTITM10.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM10.Focus();
                TOTITM10.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity10.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM10.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname10.Text == "")
                {
                    quantity10.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM10.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname10_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname10.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname10.Text == "")
                    { }
                    else
                    {
                        itemname10.Text = "";
                    }
                }
            }
            else { itemname10.Text = ""; }
            itm = itemname10.Text;
        }
        private void Itemname10_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname10.Text.Trim() != "")
            {
                aaid = itemname10.Text;
                pos.likea = itemname10.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM10.Visibility = Visibility.Visible;
                    itemname10.Text = aaid;
                    if (TOTITM10.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM10.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM10.ItemsSource = dt.DefaultView;
                    TOTITM10.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM10.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname11_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname11.Text = "";
                TOTITM11.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM11.Focus();
                TOTITM11.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity11.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM11.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname11.Text == "")
                {
                    quantity11.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM11.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname11_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname11.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname11.Text == "")
                    { }
                    else
                    {
                        itemname11.Text = "";
                    }
                }
            }
            else { itemname11.Text = ""; }
            itm = itemname11.Text;
        }
        private void Itemname11_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname11.Text.Trim() != "")
            {
                aaid = itemname11.Text;
                pos.likea = itemname11.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM11.Visibility = Visibility.Visible;
                    itemname11.Text = aaid;
                    if (TOTITM11.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM11.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM11.ItemsSource = dt.DefaultView;
                    TOTITM11.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM11.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname12_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname12.Text = "";
                TOTITM12.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM12.Focus();
                TOTITM12.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity12.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM12.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname12.Text == "")
                {
                    quantity12.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM12.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname12_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname12.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname12.Text == "")
                    { }
                    else
                    {
                        itemname12.Text = "";
                    }
                }
            }
            else { itemname12.Text = ""; }
            itm = itemname12.Text;
        }
        private void Itemname12_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname12.Text.Trim() != "")
            {
                aaid = itemname12.Text;
                pos.likea = itemname12.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM12.Visibility = Visibility.Visible;
                    itemname12.Text = aaid;
                    if (TOTITM12.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM12.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM12.ItemsSource = dt.DefaultView;
                    TOTITM12.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM12.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname13_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname13.Text = "";
                TOTITM13.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM13.Focus();
                TOTITM13.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity13.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM13.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname13.Text == "")
                {
                    quantity13.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM13.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname13_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname13.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname13.Text == "")
                    { }
                    else
                    {
                        itemname13.Text = "";
                    }
                }
            }
            else { itemname13.Text = ""; }
            itm = itemname13.Text;
        }
        private void Itemname13_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname13.Text.Trim() != "")
            {
                aaid = itemname13.Text;
                pos.likea = itemname13.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM13.Visibility = Visibility.Visible;
                    itemname13.Text = aaid;
                    if (TOTITM13.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM13.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM13.ItemsSource = dt.DefaultView;
                    TOTITM13.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM13.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname14_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname14.Text = "";
                TOTITM14.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM14.Focus();
                TOTITM14.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity14.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM14.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname14.Text == "")
                {
                    quantity14.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM14.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname14_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname14.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname14.Text == "")
                    { }
                    else
                    {
                        itemname14.Text = "";
                    }
                }
            }
            else { itemname14.Text = ""; }
            itm = itemname14.Text;
        }
        private void Itemname14_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname14.Text.Trim() != "")
            {
                aaid = itemname14.Text;
                pos.likea = itemname14.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM14.Visibility = Visibility.Visible;
                    itemname14.Text = aaid;
                    if (TOTITM14.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM14.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM14.ItemsSource = dt.DefaultView;
                    TOTITM14.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM14.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname15_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname15.Text = "";
                TOTITM15.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM15.Focus();
                TOTITM15.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity15.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM15.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname15.Text == "")
                {
                    quantity15.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM15.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname15_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname15.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname15.Text == "")
                    { }
                    else
                    {
                        itemname15.Text = "";
                    }
                }
            }
            else { itemname15.Text = ""; }
            itm = itemname15.Text;
        }
        private void Itemname15_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname15.Text.Trim() != "")
            {
                aaid = itemname15.Text;
                pos.likea = itemname15.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM15.Visibility = Visibility.Visible;
                    itemname15.Text = aaid;
                    if (TOTITM15.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM15.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM15.ItemsSource = dt.DefaultView;
                    TOTITM15.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM15.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname16_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname16.Text = "";
                TOTITM16.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM16.Focus();
                TOTITM16.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity16.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM16.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname16.Text == "")
                {
                    quantity16.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM16.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname16_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname16.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname16.Text == "")
                    { }
                    else
                    {
                        itemname16.Text = "";
                    }
                }
            }
            else { itemname16.Text = ""; }
            itm = itemname16.Text;
        }
        private void Itemname16_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname16.Text.Trim() != "")
            {
                aaid = itemname16.Text;
                pos.likea = itemname16.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM16.Visibility = Visibility.Visible;
                    itemname16.Text = aaid;
                    if (TOTITM16.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM16.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM16.ItemsSource = dt.DefaultView;
                    TOTITM16.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM16.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname17_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname17.Text = "";
                TOTITM17.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM17.Focus();
                TOTITM17.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity17.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM17.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname17.Text == "")
                {
                    quantity17.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM17.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname17_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname17.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname17.Text == "")
                    { }
                    else
                    {
                        itemname17.Text = "";
                    }
                }
            }
            else { itemname17.Text = ""; }
            itm = itemname17.Text;
        }
        private void Itemname17_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname17.Text.Trim() != "")
            {
                aaid = itemname17.Text;
                pos.likea = itemname17.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM17.Visibility = Visibility.Visible;
                    itemname17.Text = aaid;
                    if (TOTITM17.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM17.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM17.ItemsSource = dt.DefaultView;
                    TOTITM17.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM17.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname18_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname18.Text = "";
                TOTITM18.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM18.Focus();
                TOTITM18.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity18.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM18.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname18.Text == "")
                {
                    quantity18.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM18.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname18_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname18.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname18.Text == "")
                    { }
                    else
                    {
                        itemname18.Text = "";
                    }
                }
            }
            else { itemname18.Text = ""; }
            itm = itemname18.Text;
        }
        private void Itemname18_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname18.Text.Trim() != "")
            {
                aaid = itemname18.Text;
                pos.likea = itemname18.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM18.Visibility = Visibility.Visible;
                    itemname18.Text = aaid;
                    if (TOTITM18.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM18.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM18.ItemsSource = dt.DefaultView;
                    TOTITM18.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM18.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname19_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname19.Text = "";
                TOTITM19.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM19.Focus();
                TOTITM19.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                quantity19.Text = "";
                DataTable dt = pos.itemslist();
                TOTITM19.ItemsSource = dt.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname19.Text == "")
                {
                    quantity19.Text = "";
                    DataTable dt = pos.itemslist();
                    TOTITM19.ItemsSource = dt.DefaultView;
                }
            }
        }
        private void Itemname19_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname19.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname19.Text == "")
                    { }
                    else
                    {
                        itemname19.Text = "";
                    }
                }
            }
            else { itemname19.Text = ""; }
            itm = itemname19.Text;
        }
        private void Itemname19_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = pos.itemslist();
            if (itemname19.Text.Trim() != "")
            {
                aaid = itemname19.Text;
                pos.likea = itemname19.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM19.Visibility = Visibility.Visible;
                    itemname19.Text = aaid;
                    if (TOTITM19.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM19.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM19.ItemsSource = dt.DefaultView;
                    TOTITM19.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM19.Visibility = Visibility.Collapsed;
            }
        }
        private void TOTITM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM.SelectedItem;
                itemname.Text = drv.Row["NAM_Name"].ToString();
                TOTITM.Visibility = Visibility.Collapsed;
                if (itemname.Text == "")
                {
                    itemname.Focus();
                }
                else
                {
                    id = itemname.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname.Text == "")
                            {
                            }
                            else
                            {
                                itemname.Text = "";
                            }
                        }
                        else
                        {
                            itemname.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname.Text;
                    quantity.Focus();
                }
            }
        }
        private void TOTITM1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM1.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM1.SelectedItem;
                itemname1.Text = drv.Row["NAM_Name"].ToString();
                TOTITM1.Visibility = Visibility.Collapsed;
                if (itemname1.Text == "")
                {
                    itemname1.Focus();
                }
                else
                {
                    id = itemname1.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname1.Text == "")
                            {
                            }
                            else
                            {
                                itemname1.Text = "";
                            }
                        }
                        else
                        {
                            itemname1.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate1.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname1.Text;
                    quantity1.Focus();
                }
            }
        }
        private void TOTITM2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM2.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM2.SelectedItem;
                itemname2.Text = drv.Row["NAM_Name"].ToString();
                TOTITM2.Visibility = Visibility.Collapsed;
                if (itemname2.Text == "")
                {
                    itemname2.Focus();
                }
                else
                {
                    id = itemname2.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname2.Text == "")
                            {
                            }
                            else
                            {
                                itemname2.Text = "";
                            }
                        }
                        else
                        {
                            itemname2.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate2.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname2.Text;
                    quantity2.Focus();
                }
            }
        }
        private void TOTITM3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM3.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM3.SelectedItem;
                itemname3.Text = drv.Row["NAM_Name"].ToString();
                TOTITM3.Visibility = Visibility.Collapsed;
                if (itemname3.Text == "")
                {
                    itemname3.Focus();
                }
                else
                {
                    id = itemname3.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname3.Text == "")
                            {
                            }
                            else
                            {
                                itemname3.Text = "";
                            }
                        }
                        else
                        {
                            itemname3.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate3.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname3.Text;
                    quantity3.Focus();
                }
            }
        }
        private void TOTITM4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM4.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM4.SelectedItem;
                itemname4.Text = drv.Row["NAM_Name"].ToString();
                TOTITM4.Visibility = Visibility.Collapsed;
                if (itemname4.Text == "")
                {
                    itemname4.Focus();
                }
                else
                {
                    id = itemname4.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname4.Text == "")
                            {
                            }
                            else
                            {
                                itemname4.Text = "";
                            }
                        }
                        else
                        {
                            itemname4.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate4.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname4.Text;
                    quantity4.Focus();
                }
            }
        }
        private void TOTITM5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM5.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM5.SelectedItem;
                itemname5.Text = drv.Row["NAM_Name"].ToString();
                TOTITM5.Visibility = Visibility.Collapsed;
                if (itemname5.Text == "")
                {
                    itemname5.Focus();
                }
                else
                {
                    id = itemname5.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname5.Text == "")
                            {
                            }
                            else
                            {
                                itemname5.Text = "";
                            }
                        }
                        else
                        {
                            itemname5.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate5.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname5.Text;
                    quantity5.Focus();
                }
            }
        }
        private void TOTITM6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM6.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM6.SelectedItem;
                itemname6.Text = drv.Row["NAM_Name"].ToString();
                TOTITM6.Visibility = Visibility.Collapsed;
                if (itemname6.Text == "")
                {
                    itemname6.Focus();
                }
                else
                {
                    id = itemname6.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname6.Text == "")
                            {
                            }
                            else
                            {
                                itemname6.Text = "";
                            }
                        }
                        else
                        {
                            itemname6.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate6.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname6.Text;
                    quantity6.Focus();
                }
            }
        }
        private void TOTITM7_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM7.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM7.SelectedItem;
                itemname7.Text = drv.Row["NAM_Name"].ToString();
                TOTITM7.Visibility = Visibility.Collapsed;
                if (itemname7.Text == "")
                {
                    itemname7.Focus();
                }
                else
                {
                    id = itemname7.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname7.Text == "")
                            {
                            }
                            else
                            {
                                itemname7.Text = "";
                            }
                        }
                        else
                        {
                            itemname7.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate7.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname7.Text;
                    quantity7.Focus();
                }
            }
        }

        

        private void TOTITM8_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM8.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM8.SelectedItem;
                itemname8.Text = drv.Row["NAM_Name"].ToString();
                TOTITM8.Visibility = Visibility.Collapsed;
                if (itemname8.Text == "")
                {
                    itemname8.Focus();
                }
                else
                {
                    id = itemname8.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname8.Text == "")
                            {
                            }
                            else
                            {
                                itemname8.Text = "";
                            }
                        }
                        else
                        {
                            itemname8.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate8.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname8.Text;
                    quantity8.Focus();
                }
            }
        }
        private void TOTITM9_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM9.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM9.SelectedItem;
                itemname9.Text = drv.Row["NAM_Name"].ToString();
                TOTITM9.Visibility = Visibility.Collapsed;
                if (itemname9.Text == "")
                {
                    itemname9.Focus();
                }
                else
                {
                    id = itemname9.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname9.Text == "")
                            {
                            }
                            else
                            {
                                itemname9.Text = "";
                            }
                        }
                        else
                        {
                            itemname9.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate9.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname9.Text;
                    quantity9.Focus();
                }
            }
        }
        private void TOTITM10_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM10.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM10.SelectedItem;
                itemname10.Text = drv.Row["NAM_Name"].ToString();
                TOTITM10.Visibility = Visibility.Collapsed;
                if (itemname10.Text == "")
                {
                    itemname10.Focus();
                }
                else
                {
                    id = itemname10.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname10.Text == "")
                            {
                            }
                            else
                            {
                                itemname10.Text = "";
                            }
                        }
                        else
                        {
                            itemname10.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate10.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname10.Text;
                    quantity10.Focus();
                }
            }
        }
        private void TOTITM11_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM11.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM11.SelectedItem;
                itemname11.Text = drv.Row["NAM_Name"].ToString();
                TOTITM11.Visibility = Visibility.Collapsed;
                if (itemname11.Text == "")
                {
                    itemname11.Focus();
                }
                else
                {
                    id = itemname11.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname11.Text == "")
                            {
                            }
                            else
                            {
                                itemname11.Text = "";
                            }
                        }
                        else
                        {
                            itemname11.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate11.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname11.Text;
                    quantity11.Focus();
                }
            }
        }
        private void TOTITM12_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM12.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM12.SelectedItem;
                itemname12.Text = drv.Row["NAM_Name"].ToString();
                TOTITM12.Visibility = Visibility.Collapsed;
                if (itemname12.Text == "")
                {
                    itemname12.Focus();
                }
                else
                {
                    id = itemname12.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname12.Text == "")
                            {
                            }
                            else
                            {
                                itemname12.Text = "";
                            }
                        }
                        else
                        {
                            itemname12.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate12.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname12.Text;
                    quantity12.Focus();
                }
            }
        }
        private void TOTITM13_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM13.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM13.SelectedItem;
                itemname13.Text = drv.Row["NAM_Name"].ToString();
                TOTITM13.Visibility = Visibility.Collapsed;
                if (itemname13.Text == "")
                {
                    itemname13.Focus();
                }
                else
                {
                    id = itemname13.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname13.Text == "")
                            {
                            }
                            else
                            {
                                itemname13.Text = "";
                            }
                        }
                        else
                        {
                            itemname13.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate13.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname13.Text;
                    quantity13.Focus();
                }
            }
        }
        private void TOTITM14_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM14.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM14.SelectedItem;
                itemname14.Text = drv.Row["NAM_Name"].ToString();
                TOTITM14.Visibility = Visibility.Collapsed;
                if (itemname14.Text == "")
                {
                    itemname14.Focus();
                }
                else
                {
                    id = itemname14.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname14.Text == "")
                            {
                            }
                            else
                            {
                                itemname14.Text = "";
                            }
                        }
                        else
                        {
                            itemname14.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate14.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname14.Text;
                    quantity14.Focus();
                }
            }
        }
        private void TOTITM15_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM15.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM15.SelectedItem;
                itemname15.Text = drv.Row["NAM_Name"].ToString();
                TOTITM15.Visibility = Visibility.Collapsed;
                if (itemname15.Text == "")
                {
                    itemname15.Focus();
                }
                else
                {
                    id = itemname15.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname15.Text == "")
                            {
                            }
                            else
                            {
                                itemname15.Text = "";
                            }
                        }
                        else
                        {
                            itemname15.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate15.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname15.Text;
                    quantity15.Focus();
                }
            }
        }
        private void TOTITM16_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM16.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM16.SelectedItem;
                itemname16.Text = drv.Row["NAM_Name"].ToString();
                TOTITM16.Visibility = Visibility.Collapsed;
                if (itemname16.Text == "")
                {
                    itemname16.Focus();
                }
                else
                {
                    id = itemname16.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname16.Text == "")
                            {
                            }
                            else
                            {
                                itemname16.Text = "";
                            }
                        }
                        else
                        {
                            itemname16.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate16.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname16.Text;
                    quantity16.Focus();
                }
            }
        }
        private void TOTITM17_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM17.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM17.SelectedItem;
                itemname17.Text = drv.Row["NAM_Name"].ToString();
                TOTITM17.Visibility = Visibility.Collapsed;
                if (itemname17.Text == "")
                {
                    itemname17.Focus();
                }
                else
                {
                    id = itemname17.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname17.Text == "")
                            {
                            }
                            else
                            {
                                itemname17.Text = "";
                            }
                        }
                        else
                        {
                            itemname17.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate17.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname17.Text;
                    quantity17.Focus();
                }
            }
        }
        private void TOTITM18_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM18.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM18.SelectedItem;
                itemname18.Text = drv.Row["NAM_Name"].ToString();
                TOTITM18.Visibility = Visibility.Collapsed;
                if (itemname18.Text == "")
                {
                    itemname18.Focus();
                }
                else
                {
                    id = itemname18.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname18.Text == "")
                            {
                            }
                            else
                            {
                                itemname18.Text = "";
                            }
                        }
                        else
                        {
                            itemname18.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate18.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname18.Text;
                    quantity18.Focus();
                }
            }
        }
        private void TOTITM19_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM19.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM19.SelectedItem;
                itemname19.Text = drv.Row["NAM_Name"].ToString();
                TOTITM19.Visibility = Visibility.Collapsed;
                if (itemname19.Text == "")
                {
                    itemname19.Focus();
                }
                else
                {
                    id = itemname19.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname19.Text == "")
                            {
                            }
                            else
                            {
                                itemname19.Text = "";
                            }
                        }
                        else
                        {
                            itemname19.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate19.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname19.Text;
                    quantity19.Focus();
                }
            }
        }
        private void Quantity_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname.Text == "")
            { itemname.Focus(); }
        }
        private void Quantity1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname1.Text == "")
            { itemname1.Focus(); }
        }
        private void Quantity2_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname2.Text == "")
            { itemname2.Focus(); }
        }
        private void Quantity3_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname3.Text == "")
            { itemname3.Focus(); }
        }
        private void Quantity4_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname4.Text == "")
            { itemname4.Focus(); }
        }
        private void Quantity5_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname5.Text == "")
            { itemname5.Focus(); }
        }
        private void Quantity6_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname6.Text == "")
            { itemname6.Focus(); }
        }
        private void Quantity7_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname7.Text == "")
            { itemname7.Focus(); }
        }
        private void Quantity8_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname8.Text == "")
            { itemname8.Focus(); }
        }
        private void Quantity9_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname9.Text == "")
            { itemname9.Focus(); }
        }
        private void Quantity10_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname10.Text == "")
            { itemname10.Focus(); }
        }
        private void Quantity11_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname11.Text == "")
            { itemname11.Focus(); }
        }
        private void Quantity12_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname12.Text == "")
            { itemname12.Focus(); }
        }
        private void Quantity13_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname13.Text == "")
            { itemname13.Focus(); }
        }
        private void Quantity14_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname14.Text == "")
            { itemname14.Focus(); }
        }
        private void Quantity15_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname15.Text == "")
            { itemname15.Focus(); }
        }
        private void Quantity16_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname16.Text == "")
            { itemname16.Focus(); }
        }
        private void Quantity17_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname17.Text == "")
            { itemname17.Focus(); }
        }
        private void Quantity18_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname18.Text == "")
            { itemname18.Focus(); }
        }
        private void Quantity19_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname19.Text == "")
            { itemname19.Focus(); }
        }
        private void Total_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity.Text == "")
            { quantity.Focus(); }
            else
            { itemname1.Focus(); }
        }
        private void Total1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity1.Text == "")
            { quantity1.Focus(); }
            else
            { itemname2.Focus(); }
        }
        private void Total2_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity2.Text == "")
            { quantity2.Focus(); }
            else
            { itemname3.Focus(); }
        }
        private void Total3_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity3.Text == "")
            { quantity3.Focus(); }
            else
            { itemname4.Focus(); }
        }
        private void Total4_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity4.Text == "")
            { quantity4.Focus(); }
            else
            { itemname5.Focus(); }
        }
        private void Total5_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity5.Text == "")
            { quantity5.Focus(); }
            else
            { itemname6.Focus(); }
        }
        private void Total6_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity6.Text == "")
            { quantity6.Focus(); }
            else
            { itemname7.Focus(); }
        }
        private void Total7_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity7.Text == "")
            { quantity7.Focus(); }
            else
            { itemname8.Focus(); }
        }
        private void Total8_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity8.Text == "")
            { quantity8.Focus(); }
            else
            { itemname9.Focus(); }
        }
        private void Total9_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity9.Text == "")
            { quantity9.Focus(); }
            else
            { itemname10.Focus(); }
        }
        private void Total10_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity10.Text == "")
            { quantity10.Focus(); }
            else
            { itemname11.Focus(); }
        }
        private void Total11_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity11.Text == "")
            { quantity11.Focus(); }
            else
            { itemname12.Focus(); }
        }
        private void Total12_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity12.Text == "")
            { quantity12.Focus(); }
            else
            { itemname13.Focus(); }
        }
        private void Total13_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity13.Text == "")
            { quantity13.Focus(); }
            else
            { itemname14.Focus(); }
        }
        private void Total14_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity14.Text == "")
            { quantity14.Focus(); }
            else
            { itemname15.Focus(); }
        }
        private void Total15_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity15.Text == "")
            { quantity15.Focus(); }
            else
            { itemname16.Focus(); }
        }
        private void Total16_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity16.Text == "")
            { quantity16.Focus(); }
            else
            { itemname17.Focus(); }
        }
        private void Total17_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity17.Text == "")
            { quantity17.Focus(); }
            else
            { itemname18.Focus(); }
        }
        private void Total18_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity18.Text == "")
            { quantity18.Focus(); }
            else
            { itemname19.Focus(); }
        }
        private void Total19_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity19.Text == "")
            { quantity19.Focus(); }
        }
        public decimal tax118, tax218, tax318, tax418, tax518, tax618, tax718, tax818, tax918, tax1018, tax1118, tax1218, tax1318, tax1418, tax1518, tax1618, tax1718, tax1818, tax1918, tax2018;
        private void Txtstall_DropDownClosed(object sender, EventArgs e)
        {
            offername = txtstall.Text;
            DataTable DTT = pos.getoffer();
            if (DTT.Rows.Count == 0)
            { }
            else
            {
                offid = Convert.ToInt32(DTT.Rows[0]["OFF_ID"]);
                if (DTT.Rows[0]["OFF_Percentage"].ToString() == "0" || DTT.Rows[0]["OFF_Percentage"].ToString() == null)
                { disper = 0; }
                else { disper = Convert.ToDecimal(DTT.Rows[0]["OFF_Percentage"]); }
                txtpercentage.Text = disper.ToString();
                amdis = (ttotal * disper) / 100;
                if (DTT.Rows[0]["OFF_MaxAmount"].ToString() == "0" || DTT.Rows[0]["OFF_MaxAmount"].ToString() == null)
                { maxamount = 0; }
                else
                { maxamount = Convert.ToDecimal(DTT.Rows[0]["OFF_MaxAmount"]); }
                if(maxamount == 0)
                {
                    txtdisAmount.Text = amdis.ToString();
                }
                else if (maxamount <= amdis)
                {
                    txtdisAmount.Text = maxamount.ToString();
                }
                else if (maxamount > amdis)
                {
                    txtdisAmount.Text = amdis.ToString();
                }
            }
        }

        public static string offername;
        public static decimal tttt;
        private void OfferOk_Click(object sender, RoutedEventArgs e)
        {
            OfferPage.IsOpen = false;
            if (error != 0)
            {
                MessageBox.Show("Please Fill All Fields");
            }
            else
            {
                pos.bill = Convert.ToInt32(txtbillno.Text);
                pos.BILL_Amount = Convert.ToDecimal(txtttl.Text);
                pos.BILL_Tax = Convert.ToDecimal(txtgst.Text);
                pos.BILL_Discount = Convert.ToDecimal(txtdisAmount.Text);
                pos.Bill_OfferId = offid;
                pos.Bill_InstantDis = Convert.ToDecimal(txtInsdis.Text);
                tttt = Convert.ToDecimal(txtgttl.Text) - (pos.BILL_Discount + pos.Bill_InstantDis) ;
                pos.BILL_Total = (int)Math.Round(tttt,2);
                pos.insertbill();
                pos.A = Convert.ToInt32(txtbillno.Text);
                for (int i = 0; i < cou; i++)
                {
                    if (i == 0)
                    {
                        if (itemname.Text != "" || itemrate.Text != "")
                        {
                            int A = Convert.ToInt32(quantity.Text); decimal B = A * Convert.ToDecimal(itemrate.Text);
                            decimal gst = 0; check1 = itemname.Text; itemnamestlid = itemname.Text; DataTable dts0 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts0.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax15 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax118 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 1)
                    {
                        if (itemname1.Text != "" || itemrate1.Text != "")
                        {
                            int A = Convert.ToInt32(quantity1.Text); decimal B = A * Convert.ToDecimal(itemrate1.Text);
                            decimal gst = 0; check1 = itemname1.Text; itemnamestlid = itemname1.Text; DataTable dts1 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts1.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname1.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate1.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax25 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax218 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 2)
                    {
                        if (itemname2.Text != "" || itemrate2.Text != "")
                        {
                            int A = Convert.ToInt32(quantity2.Text); decimal B = A * Convert.ToDecimal(itemrate2.Text);
                            decimal gst = 0; check1 = itemname2.Text; itemnamestlid = itemname2.Text; DataTable dts2 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts2.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname2.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate2.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax35 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax318 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 3)
                    {

                        if (itemname3.Text != "" || itemrate3.Text != "")
                        {
                            int A = Convert.ToInt32(quantity3.Text); decimal B = A * Convert.ToDecimal(itemrate3.Text);
                            decimal gst = 0; check1 = itemname3.Text; itemnamestlid = itemname3.Text; DataTable dts3 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts3.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname3.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate3.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax45 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax418 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 4)
                    {
                        if (itemname4.Text != "" || itemrate4.Text != "")
                        {
                            int A = Convert.ToInt32(quantity4.Text); decimal B = A * Convert.ToDecimal(itemrate4.Text);
                            decimal gst = 0; check1 = itemname4.Text; itemnamestlid = itemname4.Text; DataTable dts4 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts4.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname4.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate4.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax55 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax518 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 5)
                    {
                        if (itemname5.Text != "" || itemrate5.Text != "")
                        {
                            int A = Convert.ToInt32(quantity5.Text); decimal B = A * Convert.ToDecimal(itemrate5.Text);
                            decimal gst = 0; check1 = itemname5.Text; itemnamestlid = itemname5.Text; DataTable dts5 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts5.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname5.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate5.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax65 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax618 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 6)
                    {
                        if (itemname6.Text != "" || itemrate6.Text != "")
                        {
                            int A = Convert.ToInt32(quantity6.Text); decimal B = A * Convert.ToDecimal(itemrate6.Text);
                            decimal gst = 0; check1 = itemname6.Text; itemnamestlid = itemname6.Text; DataTable dts6 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts6.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname6.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate6.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax75 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax718 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 7)
                    {
                        if (itemname7.Text != "" || itemrate7.Text != "")
                        {
                            int A = Convert.ToInt32(quantity7.Text); decimal B = A * Convert.ToDecimal(itemrate7.Text);
                            decimal gst = 0; check1 = itemname7.Text; itemnamestlid = itemname7.Text; DataTable dts7 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts7.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname7.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate7.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax85 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax818 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 8)
                    {
                        if (itemname8.Text != "" || itemrate8.Text != "")
                        {
                            int A = Convert.ToInt32(quantity8.Text); decimal B = A * Convert.ToDecimal(itemrate8.Text);
                            decimal gst = 0; check1 = itemname8.Text; itemnamestlid = itemname8.Text; DataTable dts8 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts8.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname8.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate8.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax95 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax918 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 9)
                    {
                        if (itemname9.Text != "" || itemrate9.Text != "")
                        {
                            int A = Convert.ToInt32(quantity9.Text); decimal B = A * Convert.ToDecimal(itemrate9.Text);
                            decimal gst = 0; check1 = itemname9.Text; itemnamestlid = itemname9.Text; DataTable dts9 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts9.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname9.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate9.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax105 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1018 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 10)
                    {
                        if (itemname10.Text != "" || itemrate10.Text != "")
                        {
                            int A = Convert.ToInt32(quantity10.Text); decimal B = A * Convert.ToDecimal(itemrate10.Text);
                            decimal gst = 0; check1 = itemname10.Text; itemnamestlid = itemname10.Text; DataTable dts10 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts10.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname10.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate10.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax115 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1118 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 11)
                    {
                        if (itemname11.Text != "" || itemrate11.Text != "")
                        {
                            int A = Convert.ToInt32(quantity11.Text); decimal B = A * Convert.ToDecimal(itemrate11.Text);
                            decimal gst = 0; check1 = itemname11.Text; itemnamestlid = itemname11.Text; DataTable dts11 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts11.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname11.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate11.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax125 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1218 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 12)
                    {
                        if (itemname12.Text != "" || itemrate12.Text != "")
                        {
                            int A = Convert.ToInt32(quantity12.Text); decimal B = A * Convert.ToDecimal(itemrate12.Text);
                            decimal gst = 0; check1 = itemname12.Text; itemnamestlid = itemname12.Text; DataTable dts12 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts12.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname12.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate12.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax135 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1318 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 13)
                    {
                        if (itemname13.Text != "" || itemrate13.Text != "")
                        {
                            int A = Convert.ToInt32(quantity13.Text); decimal B = A * Convert.ToDecimal(itemrate13.Text);
                            decimal gst = 0; check1 = itemname13.Text; itemnamestlid = itemname13.Text; DataTable dts13 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts13.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname13.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate13.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax145 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1418 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 14)
                    {
                        if (itemname14.Text != "" || itemrate14.Text != "")
                        {
                            int A = Convert.ToInt32(quantity14.Text); decimal B = A * Convert.ToDecimal(itemrate14.Text);
                            decimal gst = 0; check1 = itemname14.Text; itemnamestlid = itemname14.Text; DataTable dts14 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts14.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname14.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate14.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax155 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1518 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 15)
                    {
                        if (itemname15.Text != "" || itemrate15.Text != "")
                        {
                            int A = Convert.ToInt32(quantity15.Text); decimal B = A * Convert.ToDecimal(itemrate15.Text);
                            decimal gst = 0; check1 = itemname15.Text; itemnamestlid = itemname15.Text; DataTable dts15 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts15.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname15.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate15.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax165 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1618 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 16)
                    {
                        if (itemname16.Text != "" || itemrate16.Text != "")
                        {
                            int A = Convert.ToInt32(quantity16.Text); decimal B = A * Convert.ToDecimal(itemrate16.Text);
                            decimal gst = 0; check1 = itemname16.Text; itemnamestlid = itemname16.Text; DataTable dts16 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts16.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname16.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate16.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax175 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1718 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 17)
                    {
                        if (itemname17.Text != "" || itemrate17.Text != "")
                        {
                            int A = Convert.ToInt32(quantity17.Text); decimal B = A * Convert.ToDecimal(itemrate17.Text);
                            decimal gst = 0; check1 = itemname17.Text; itemnamestlid = itemname17.Text; DataTable dts17 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts17.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname17.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate17.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax185 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1818 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 18)
                    {
                        if (itemname18.Text != "" || itemrate18.Text != "")
                        {
                            int A = Convert.ToInt32(quantity18.Text); decimal B = A * Convert.ToDecimal(itemrate18.Text);
                            decimal gst = 0; check1 = itemname18.Text; itemnamestlid = itemname18.Text; DataTable dts18 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts18.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname18.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate18.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax195 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1918 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 19)
                    {
                        if (itemname19.Text != "" || itemrate19.Text != "")
                        {
                            int A = Convert.ToInt32(quantity19.Text); decimal B = A * Convert.ToDecimal(itemrate19.Text);
                            decimal gst = 0; check1 = itemname19.Text; itemnamestlid = itemname19.Text; DataTable dts19 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts19.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname19.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate19.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax205 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax2018 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                }
                tax5sum = tax15 + tax25 + tax35 + tax45 + tax55 + tax65 + tax75 + tax85 + tax95 + tax105 + tax115 + tax125 + tax135 + tax145 + tax155 + tax165 + tax175 + tax185 + tax195 + tax205;
                tax18sum = tax118 + tax218 + tax318 + tax418 + tax518 + tax618 + tax718 + tax818 + tax918 + tax1018 + tax1118 + tax1218 + tax1318 + tax1418 + tax1518 + tax1618 + tax1718 + tax1818 + tax1918 + tax2018;

                MessageBox.Show("Inserted successfully");
                ReportDocument re = new ReportDocument();
                DataTable d1 = Billprint1();
                pos1 = d1;
                re.Load("../../REPORTS/PRINTBILL1.rpt");
                DataTable ds = Billprint();
                pos11 = ds;
                re.Load("../../REPORTS/Printbill.rpt");
                re.SetDataSource(pos11);
                re.Subreports[0].SetDataSource(pos1);
                re.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;

                ReportDocument res = new ReportDocument();

                res.Load("../../REPORTS/kotprint.rpt");

                DataTable dstlss = pos.stlidsss();
                for (int i = 1; i <= dstlss.Rows.Count; i++)
                {
                    DataTable d3 = kot();
                    a11 = d3;
                    DataTable d2 = kot1();
                    a1 = d2;
                    res.Load("../../REPORTS/kotprint1.rpt");
                    res.SetDataSource(a1);
                    res.Subreports[0].SetDataSource(a11);
                    res.PrintToPrinter(1, false, 0, 0);
                    res.Refresh();
                }
                res.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;

                re.PrintToPrinter(1, false, 0, 0);
                re.Refresh();
                //ReportDocument re1 = new ReportDocument();
                //DataTable d21 = Dprint1();
                //a11 = d21;
                //re1.Load("../../REPORTS/Dprint1.rpt");
                //DataTable ds1 = Dprint();
                //a1 = ds1;
                //re1.Load("../../REPORTS/DuplicatePrint.rpt");
                //re1.SetDataSource(a1);
                //re1.Subreports[0].SetDataSource(a11);
                //re1.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                //re1.PrintToPrinter(1, false, 0, 0);
                //re1.Refresh();
                //re.PrintToPrinter(1, false, 0, 0);
                //re.Refresh();
                clear();
                this.NavigationService.Refresh();
                j = 0;
                cou = 0;
            }
        }
        public int offid;
        private void OffNo_Click(object sender, RoutedEventArgs e)
        {

            OfferConfirmation.IsOpen = false;
            if (error != 0)
            {
                MessageBox.Show("Please Fill All Fields");
            }
            else
            {
                pos.bill = Convert.ToInt32(txtbillno.Text);
                pos.BILL_Amount = Convert.ToDecimal(txtttl.Text);
                pos.BILL_Tax = Convert.ToDecimal(txtgst.Text);
                pos.BILL_Total = Convert.ToDecimal(txtgttl.Text);
                pos.BILL_Discount = Convert.ToDecimal("0.00");
                pos.Bill_OfferId = 0;
                pos.Bill_InstantDis = Convert.ToDecimal("0.00");
                pos.insertbill();
                pos.A = Convert.ToInt32(txtbillno.Text);
                for (int i = 0; i < cou; i++)
                {
                    if (i == 0)
                    {
                        if (itemname.Text != "" || itemrate.Text != "")
                        {
                            int A = Convert.ToInt32(quantity.Text); decimal B = A * Convert.ToDecimal(itemrate.Text);
                            decimal gst = 0; check1 = itemname.Text; itemnamestlid = itemname.Text; DataTable dts0 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts0.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax15 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax118 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 1)
                    {
                        if (itemname1.Text != "" || itemrate1.Text != "")
                        {
                            int A = Convert.ToInt32(quantity1.Text); decimal B = A * Convert.ToDecimal(itemrate1.Text);
                            decimal gst = 0; check1 = itemname1.Text; itemnamestlid = itemname1.Text; DataTable dts1 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts1.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname1.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate1.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax25 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax218 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 2)
                    {
                        if (itemname2.Text != "" || itemrate2.Text != "")
                        {
                            int A = Convert.ToInt32(quantity2.Text); decimal B = A * Convert.ToDecimal(itemrate2.Text);
                            decimal gst = 0; check1 = itemname2.Text; itemnamestlid = itemname2.Text; DataTable dts2 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts2.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname2.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate2.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax35 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax318 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 3)
                    {

                        if (itemname3.Text != "" || itemrate3.Text != "")
                        {
                            int A = Convert.ToInt32(quantity3.Text); decimal B = A * Convert.ToDecimal(itemrate3.Text);
                            decimal gst = 0; check1 = itemname3.Text; itemnamestlid = itemname3.Text; DataTable dts3 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts3.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname3.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate3.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax45 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax418 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 4)
                    {
                        if (itemname4.Text != "" || itemrate4.Text != "")
                        {
                            int A = Convert.ToInt32(quantity4.Text); decimal B = A * Convert.ToDecimal(itemrate4.Text);
                            decimal gst = 0; check1 = itemname4.Text; itemnamestlid = itemname4.Text; DataTable dts4 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts4.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname4.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate4.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax55 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax518 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 5)
                    {
                        if (itemname5.Text != "" || itemrate5.Text != "")
                        {
                            int A = Convert.ToInt32(quantity5.Text); decimal B = A * Convert.ToDecimal(itemrate5.Text);
                            decimal gst = 0; check1 = itemname5.Text; itemnamestlid = itemname5.Text; DataTable dts5 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts5.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname5.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate5.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax65 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax618 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 6)
                    {
                        if (itemname6.Text != "" || itemrate6.Text != "")
                        {
                            int A = Convert.ToInt32(quantity6.Text); decimal B = A * Convert.ToDecimal(itemrate6.Text);
                            decimal gst = 0; check1 = itemname6.Text; itemnamestlid = itemname6.Text; DataTable dts6 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts6.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname6.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate6.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax75 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax718 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 7)
                    {
                        if (itemname7.Text != "" || itemrate7.Text != "")
                        {
                            int A = Convert.ToInt32(quantity7.Text); decimal B = A * Convert.ToDecimal(itemrate7.Text);
                            decimal gst = 0; check1 = itemname7.Text; itemnamestlid = itemname7.Text; DataTable dts7 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts7.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname7.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate7.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax85 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax818 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 8)
                    {
                        if (itemname8.Text != "" || itemrate8.Text != "")
                        {
                            int A = Convert.ToInt32(quantity8.Text); decimal B = A * Convert.ToDecimal(itemrate8.Text);
                            decimal gst = 0; check1 = itemname8.Text; itemnamestlid = itemname8.Text; DataTable dts8 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts8.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname8.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate8.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax95 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax918 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 9)
                    {
                        if (itemname9.Text != "" || itemrate9.Text != "")
                        {
                            int A = Convert.ToInt32(quantity9.Text); decimal B = A * Convert.ToDecimal(itemrate9.Text);
                            decimal gst = 0; check1 = itemname9.Text; itemnamestlid = itemname9.Text; DataTable dts9 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts9.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname9.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate9.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax105 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1018 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 10)
                    {
                        if (itemname10.Text != "" || itemrate10.Text != "")
                        {
                            int A = Convert.ToInt32(quantity10.Text); decimal B = A * Convert.ToDecimal(itemrate10.Text);
                            decimal gst = 0; check1 = itemname10.Text; itemnamestlid = itemname10.Text; DataTable dts10 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts10.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname10.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate10.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax115 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1118 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 11)
                    {
                        if (itemname11.Text != "" || itemrate11.Text != "")
                        {
                            int A = Convert.ToInt32(quantity11.Text); decimal B = A * Convert.ToDecimal(itemrate11.Text);
                            decimal gst = 0; check1 = itemname11.Text; itemnamestlid = itemname11.Text; DataTable dts11 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts11.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname11.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate11.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax125 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1218 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 12)
                    {
                        if (itemname12.Text != "" || itemrate12.Text != "")
                        {
                            int A = Convert.ToInt32(quantity12.Text); decimal B = A * Convert.ToDecimal(itemrate12.Text);
                            decimal gst = 0; check1 = itemname12.Text; itemnamestlid = itemname12.Text; DataTable dts12 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts12.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname12.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate12.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax135 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1318 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 13)
                    {
                        if (itemname13.Text != "" || itemrate13.Text != "")
                        {
                            int A = Convert.ToInt32(quantity13.Text); decimal B = A * Convert.ToDecimal(itemrate13.Text);
                            decimal gst = 0; check1 = itemname13.Text; itemnamestlid = itemname13.Text; DataTable dts13 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts13.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname13.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate13.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax145 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1418 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 14)
                    {
                        if (itemname14.Text != "" || itemrate14.Text != "")
                        {
                            int A = Convert.ToInt32(quantity14.Text); decimal B = A * Convert.ToDecimal(itemrate14.Text);
                            decimal gst = 0; check1 = itemname14.Text; itemnamestlid = itemname14.Text; DataTable dts14 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts14.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname14.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate14.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax155 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1518 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 15)
                    {
                        if (itemname15.Text != "" || itemrate15.Text != "")
                        {
                            int A = Convert.ToInt32(quantity15.Text); decimal B = A * Convert.ToDecimal(itemrate15.Text);
                            decimal gst = 0; check1 = itemname15.Text; itemnamestlid = itemname15.Text; DataTable dts15 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts15.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname15.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate15.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax165 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1618 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 16)
                    {
                        if (itemname16.Text != "" || itemrate16.Text != "")
                        {
                            int A = Convert.ToInt32(quantity16.Text); decimal B = A * Convert.ToDecimal(itemrate16.Text);
                            decimal gst = 0; check1 = itemname16.Text; itemnamestlid = itemname16.Text; DataTable dts16 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts16.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname16.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate16.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax175 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1718 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 17)
                    {
                        if (itemname17.Text != "" || itemrate17.Text != "")
                        {
                            int A = Convert.ToInt32(quantity17.Text); decimal B = A * Convert.ToDecimal(itemrate17.Text);
                            decimal gst = 0; check1 = itemname17.Text; itemnamestlid = itemname17.Text; DataTable dts17 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts17.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname17.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate17.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax185 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1818 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 18)
                    {
                        if (itemname18.Text != "" || itemrate18.Text != "")
                        {
                            int A = Convert.ToInt32(quantity18.Text); decimal B = A * Convert.ToDecimal(itemrate18.Text);
                            decimal gst = 0; check1 = itemname18.Text; itemnamestlid = itemname18.Text; DataTable dts18 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts18.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname18.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate18.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax195 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1918 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                    if (i == 19)
                    {
                        if (itemname19.Text != "" || itemrate19.Text != "")
                        {
                            int A = Convert.ToInt32(quantity19.Text); decimal B = A * Convert.ToDecimal(itemrate19.Text);
                            decimal gst = 0; check1 = itemname19.Text; itemnamestlid = itemname19.Text; DataTable dts19 = pos.getstlid();
                            DataTable dd = pos.gsttax(); stlid = dts19.Rows[0]["STL_ID"].ToString();
                            if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                            DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                            pos.BILLITM_Name = itemname19.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate19.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                            if (gst == Convert.ToDecimal(5.00))
                            {
                                tax205 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax2018 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                            }
                        }
                    }
                }
                tax5sum = tax15 + tax25 + tax35 + tax45 + tax55 + tax65 + tax75 + tax85 + tax95 + tax105 + tax115 + tax125 + tax135 + tax145 + tax155 + tax165 + tax175 + tax185 + tax195 + tax205;
                tax18sum = tax118 + tax218 + tax318 + tax418 + tax518 + tax618 + tax718 + tax818 + tax918 + tax1018 + tax1118 + tax1218 + tax1318 + tax1418 + tax1518 + tax1618 + tax1718 + tax1818 + tax1918 + tax2018;

                MessageBox.Show("Inserted successfully");
                ReportDocument re = new ReportDocument();
                DataTable d1 = Billprint1();
                pos1 = d1;
                re.Load("../../REPORTS/PRINTBILL1.rpt");
                DataTable ds = Billprint();
                pos11 = ds;
                re.Load("../../REPORTS/Printbill.rpt");
                re.SetDataSource(pos11);
                re.Subreports[0].SetDataSource(pos1);
                re.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;


                ReportDocument res = new ReportDocument();
                
                res.Load("../../REPORTS/kotprint.rpt");
                
                DataTable dstlss = pos.stlidsss();
                for (int i = 1; i <= dstlss.Rows.Count; i++)
                {
                    DataTable d3 = kot();
                    a11 = d3;
                    DataTable d2 = kot1();
                    a1 = d2;
                    res.Load("../../REPORTS/kotprint1.rpt");
                    res.SetDataSource(a1);
                    res.Subreports[0].SetDataSource(a11);
                    res.PrintToPrinter(1, false, 0, 0);
                    res.Refresh();
                }
                res.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                
                re.PrintToPrinter(1, false, 0, 0);
                re.Refresh();
                //ReportDocument re1 = new ReportDocument();
                //DataTable d21 = Dprint1();
                //a11 = d21;
                //re1.Load("../../REPORTS/Dprint1.rpt");
                //DataTable ds1 = Dprint();
                //a1 = ds1;
                //re1.Load("../../REPORTS/DuplicatePrint.rpt");
                //re1.SetDataSource(a1);
                //re1.Subreports[0].SetDataSource(a11);
                //re1.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                //re1.PrintToPrinter(1, false, 0, 0);
                //re1.Refresh();
                //re.PrintToPrinter(1, false, 0, 0);
                //re.Refresh();
                clear();
                j = 0;
                this.NavigationService.Refresh();
                cou = 0;
            }
        }
        public static string STALLID;
        private void OffYes_Click(object sender, RoutedEventArgs e)
        {
            OfferConfirmation.IsOpen = false;
            DataTable dt = pos.getofferlist();
            txtstall.ItemsSource = dt.DefaultView;
            OfferPage.IsOpen = true;
            txtTotal.Text = txtgttl.Text;
            ttotal = Convert.ToDecimal(txtTotal.Text);
        }

        
        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            PrintConfirmation.IsOpen = false;
        }
        public static string itemnamestlid;
        private void No_Click(object sender, RoutedEventArgs e)
        {
            
        }
        public static string st;
        private void Button_Click(object sender, RoutedEventArgs e)
        {       for (int i = 0; i < cou; i++)
                {
                    if (i == 0)
                    {
                        if (itemname.Text != "" && itemrate.Text != "" && quantity.Text!= "")
                        {}
                        else { quantity.Focus(); }
                    }
                    if (i == 1)
                    {
                        if (itemname1.Text != "" && itemrate1.Text != "" && quantity1.Text != "")
                        {}
                        else { quantity1.Focus(); }
                    }
                    if (i == 2)
                    {
                        if (itemname2.Text != "" && itemrate2.Text != "" && quantity2.Text != "")
                        {}
                        else { quantity2.Focus(); }
                    }
                    if (i == 3)
                    {

                        if (itemname3.Text != "" && itemrate3.Text != "" && quantity3.Text != "")
                        {}
                        else { quantity3.Focus(); }
                    }
                    if (i == 4)
                    {
                        if (itemname4.Text != "" && itemrate4.Text != "" && quantity4.Text != "")
                        {}
                        else { quantity4.Focus(); }
                    }
                    if (i == 5)
                    {
                        if (itemname5.Text != "" && itemrate5.Text != "" && quantity5.Text != "")
                        {}
                        else { quantity5.Focus(); }
                    }
                    if (i == 6)
                    {
                        if (itemname6.Text != "" && itemrate6.Text != "" && quantity6.Text != "")
                        {}
                        else { quantity6.Focus(); }
                    }
                    if (i == 7)
                    {
                        if (itemname7.Text != "" && itemrate7.Text != "" && quantity7.Text != "")
                        {}
                        else { quantity7.Focus(); }
                    }
                    if (i == 8)
                    {
                        if (itemname8.Text != "" && itemrate8.Text != "" && quantity8.Text != "")
                        {}
                        else { quantity8.Focus(); }
                    }
                    if (i == 9)
                    {
                        if (itemname9.Text != "" && itemrate9.Text != "" && quantity9.Text != "")
                        {}
                        else { quantity9.Focus(); }
                    }
                    if (i == 10)
                    {
                        if (itemname10.Text != "" && itemrate10.Text != "" && quantity10.Text != "")
                        {}
                        else { quantity10.Focus(); }
                    }
                    if (i == 11)
                    {
                        if (itemname11.Text != "" && itemrate11.Text != "" && quantity11.Text != "")
                        {}
                        else { quantity11.Focus(); }
                    }
                    if (i == 12)
                    {
                        if (itemname12.Text != "" && itemrate12.Text != "" && quantity12.Text != "")
                        {}
                        else { quantity12.Focus(); }
                    }
                    if (i == 13)
                    {
                        if (itemname13.Text != "" && itemrate13.Text != "" && quantity13.Text != "")
                        {}
                        else { quantity13.Focus(); }
                    }
                    if (i == 14)
                    {
                        if (itemname14.Text != "" && itemrate14.Text != "" && quantity14.Text != "")
                        {}
                        else { quantity14.Focus(); }
                    }
                    if (i == 15)
                    {
                        if (itemname15.Text != "" && itemrate15.Text != "" && quantity15.Text != "")
                        {}
                        else { quantity15.Focus(); }
                    }
                    if (i == 16)
                    {
                        if (itemname16.Text != "" && itemrate16.Text != "" && quantity16.Text != "")
                        {}
                        else { quantity16.Focus(); }
                    }
                    if (i == 17)
                    {
                        if (itemname17.Text != "" && itemrate17.Text != "" && quantity17.Text != "")
                        {}
                        else { quantity17.Focus(); }
                    }
                    if (i == 18)
                    {
                        if (itemname18.Text != "" && itemrate18.Text != "" && quantity18.Text != "")
                        {}
                        else { quantity18.Focus(); }
                    }
                    if (i == 19)
                    {
                        if (itemname19.Text != "" && itemrate19.Text != "" && quantity19.Text != "")
                        {}
                        else { quantity19.Focus(); }
                    }
               }
                if (cou == 1) { if (quantity.Text != "") { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 2) { if (quantity.Text != "" && quantity1.Text!="") { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 3) { if (quantity.Text != "" && quantity1.Text != "" && quantity2.Text != "") { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 4) { if (quantity.Text != "" && quantity1.Text != "" && quantity2.Text != "" && quantity3.Text!="") { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 5) { if (quantity.Text != "" && quantity1.Text != "" && quantity2.Text != "" && quantity3.Text != "" && quantity4.Text!= "") { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 6) { if (quantity.Text != "" && quantity1.Text != "" && quantity2.Text != "" && quantity3.Text != "" && quantity4.Text != "" && quantity5.Text != "") { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 7) { if (quantity.Text != "" && quantity1.Text != "" && quantity2.Text != "" && quantity3.Text != "" && quantity4.Text != "" && quantity5.Text != "" && quantity6.Text != "") { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 8) { if (quantity.Text != "" && quantity1.Text != "" && quantity2.Text != "" && quantity3.Text != "" && quantity4.Text != "" && quantity5.Text != "" && quantity6.Text != "" && quantity7.Text != "") { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 9)
                {
                if (quantity.Text!=""&&quantity1.Text!=""&&quantity2.Text!=""&&quantity3.Text!=""&&quantity4.Text!=""&&quantity5.Text!=""&&quantity6.Text!=""&&quantity7.Text!=""&&quantity8.Text!="")
                { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ }
            }
                if (cou == 10)
                {
                if (quantity.Text!=""&&quantity1.Text!=""&&quantity2.Text!=""&&quantity3.Text!=""&&quantity4.Text!=""&&quantity5.Text!=""&&quantity6.Text!=""&&quantity7.Text!=""&&quantity8.Text!=""&&quantity9.Text!="")
                { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 11)
                { if (quantity.Text!=""&&quantity1.Text!=""&&quantity2.Text!=""&&quantity3.Text!=""&&quantity4.Text!=""&&quantity5.Text!=""&&quantity6.Text!=""&&quantity7.Text!=""&&quantity8.Text!=""&&quantity9.Text!=""&&quantity10.Text!="")
                { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 12)
                { if (quantity.Text!=""&&quantity1.Text!=""&&quantity2.Text!=""&&quantity3.Text!=""&&quantity4.Text!=""&&quantity5.Text!=""&&quantity6.Text!=""&&quantity7.Text!=""&&quantity8.Text!=""&&quantity9.Text!=""&&quantity10.Text!=""
                    &&quantity11.Text!="")
                { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 13)
                { if (quantity.Text!=""&&quantity1.Text!=""&&quantity2.Text!=""&&quantity3.Text!=""&&quantity4.Text!=""&&quantity5.Text!=""&&quantity6.Text!=""&&quantity7.Text!=""&&quantity8.Text!=""&&quantity9.Text!=""&&quantity10.Text!=""
                    &&quantity11.Text!=""&&quantity12.Text!="")
                { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 14)
                { if (quantity.Text!=""&&quantity1.Text!=""&&quantity2.Text!=""&&quantity3.Text!=""&&quantity4.Text!=""&&quantity5.Text!=""&&quantity6.Text!=""&&quantity7.Text!=""&&quantity8.Text!=""&&quantity9.Text!=""&&quantity10.Text!=""
                    &&quantity11.Text!=""&&quantity12.Text!=""&&quantity13.Text!="")
                { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 15)
                { if (quantity.Text!=""&&quantity1.Text!=""&&quantity2.Text!=""&&quantity3.Text!=""&&quantity4.Text!=""&&quantity5.Text!=""&&quantity6.Text!=""&&quantity7.Text!=""&&quantity8.Text!=""&&quantity9.Text!=""&&quantity10.Text!=""
                    &&quantity11.Text!=""&&quantity12.Text!=""&&quantity13.Text!=""&&quantity14.Text!="")
                { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 16)
                { if (quantity.Text!=""&&quantity1.Text!=""&&quantity2.Text!=""&&quantity3.Text!=""&&quantity4.Text!=""&&quantity5.Text!=""&&quantity6.Text!=""&&quantity7.Text!=""&&quantity8.Text!=""&&quantity9.Text!=""&&quantity10.Text!=""
                    &&quantity11.Text!=""&&quantity12.Text!=""&&quantity13.Text!=""&&quantity14.Text!=""&&quantity15.Text!="")
                { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 17)
                { if (quantity.Text!=""&&quantity1.Text!=""&&quantity2.Text!=""&&quantity3.Text!=""&&quantity4.Text!=""&&quantity5.Text!=""&&quantity6.Text!=""&&quantity7.Text!=""&&quantity8.Text!=""&&quantity9.Text!=""&&quantity10.Text!=""
                    &&quantity11.Text!=""&&quantity12.Text!=""&&quantity13.Text!=""&&quantity14.Text!=""&&quantity15.Text!=""&&quantity16.Text!="")
                { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 18)
                { if (quantity.Text!=""&&quantity1.Text!=""&&quantity2.Text!=""&&quantity3.Text!=""&&quantity4.Text!=""&&quantity5.Text!=""&&quantity6.Text!=""&&quantity7.Text!=""&&quantity8.Text!=""&&quantity9.Text!=""&&quantity10.Text!=""
                    &&quantity11.Text!=""&&quantity12.Text!=""&&quantity13.Text!=""&&quantity14.Text!=""&&quantity15.Text!=""&&quantity16.Text!=""&&quantity17.Text!="")
                { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 19)
                { if (quantity.Text!=""&&quantity1.Text!=""&&quantity2.Text!=""&&quantity3.Text!=""&&quantity4.Text!=""&&quantity5.Text!=""&&quantity6.Text!=""&&quantity7.Text!=""&&quantity8.Text!=""&&quantity9.Text!=""&&quantity10.Text!=""
                    &&quantity11.Text!=""&&quantity12.Text!=""&&quantity13.Text!=""&&quantity14.Text!=""&&quantity15.Text!=""&&quantity16.Text!=""&&quantity17.Text!="" && quantity18.Text != "")
                { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
                if (cou == 20)
                { if (quantity.Text!=""&&quantity1.Text!=""&&quantity2.Text!=""&&quantity3.Text!=""&&quantity4.Text!=""&&quantity5.Text!=""&&quantity6.Text!=""&&quantity7.Text!=""&&quantity8.Text!=""&&quantity9.Text!=""&&quantity10.Text!=""
                    &&quantity11.Text!=""&&quantity12.Text!=""&&quantity13.Text!=""&&quantity14.Text!=""&&quantity15.Text!=""&&quantity16.Text!=""&&quantity17.Text!=""&&quantity18.Text!=""&&quantity19.Text!="")
                { OfferConfirmation.IsOpen = true; /*PrintConfirmation.IsOpen = true;*/ } }
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            clear();
            //WRAPC.Children.Clear();
            DataTable DT = pos.itmnames();
            DT.Rows.Clear();
            //ITMNAM.Visibility = Visibility.Hidden;
            //WRAPN.Children.Clear();
            checkbox_checks = null;
            //ITMCTG.Visibility = Visibility.Collapsed;
            this.NavigationService.Refresh();
        }

        //public DataTable Dprint()
        //{
        //    DataTable d = new DataTable();
        //    d.Columns.Add("Name", typeof(string));
        //    d.Columns.Add("Address", typeof(string));
        //    d.Columns.Add("GstNo", typeof(string));
        //    d.Columns.Add("BillNo", typeof(string));
        //    d.Columns.Add("BillDate", typeof(DateTime));
        //    d.Columns.Add("Total", typeof(decimal));
        //    d.Columns.Add("Cgst", typeof(decimal));
        //    d.Columns.Add("Sgst", typeof(decimal));
        //    d.Columns.Add("GrandTotal", typeof(decimal));
        //    DataRow row = d.NewRow();
        //    DataTable adres = pos.Address();
        //    row["Name"] = adres.Rows[0]["PRPT_Name"].ToString();
        //    row["Address"] = adres.Rows[0]["PRPT_Address"].ToString();
        //    row["GstNo"] = adres.Rows[0]["PRPT_GST"].ToString();
        //    row["BillNo"] = txtbillno.Text;
        //    row["BillDate"] = DateTime.Now.Date;
        //    DataTable dt2 = pos.itemstot();
        //    row["Total"] = dt2.Rows[0]["BILL_Amount"].ToString();
        //    decimal tax = Convert.ToDecimal(dt2.Rows[0]["BILL_Tax"].ToString());
        //    row["Cgst"] = tax / 2;
        //    row["Sgst"] = tax / 2;
        //    row["GrandTotal"] = dt2.Rows[0]["BILL_Total"].ToString();

        //    d.Rows.Add(row);
        //    return d;
        //}

        //public DataTable Dprint1()
        //{
        //    DataTable dd = new DataTable();
        //    dd.Columns.Add("Item", typeof(string));
        //    dd.Columns.Add("Rate", typeof(decimal));
        //    dd.Columns.Add("Quantity", typeof(int));
        //    dd.Columns.Add("Amount", typeof(decimal));
        //    DataTable DD = pos.itms();
        //    for (int i = 0; i < DD.Rows.Count; i++)
        //    {
        //        DataRow r = dd.NewRow();
        //        r["Item"] = DD.Rows[i]["BILITM_Name"].ToString();
        //        r["Rate"] = DD.Rows[i]["BILITM_Rate"].ToString();
        //        r["Quantity"] = DD.Rows[i]["BILLITM_Quanty"].ToString();
        //        decimal d = Convert.ToDecimal(DD.Rows[i]["BILITM_Rate"].ToString());
        //        int s = Convert.ToInt32(DD.Rows[i]["BILLITM_Quanty"].ToString());
        //        r["Amount"] = d * s;
        //        dd.Rows.Add(r);
        //    }
        //    return dd;
        //}

        public static int j = 0,h=0;
        public static DataTable a11, a1;
        public static string stallname;
        public DataTable kot()
        {
            DataTable d = new DataTable();
            d.Columns.Add("Item", typeof(string));
            d.Columns.Add("Qty", typeof(int));
            DataTable dstlss = pos.stlidsss();
            if(j<dstlss.Rows.Count)
            {
                STALLID = dstlss.Rows[j]["STL_ID"].ToString();
                DataTable dsn = pos.STALLNAME();
                stallname = dsn.Rows[0]["STL_Name"].ToString();
                DataTable s = pos.STLIDITEMNAMES();
                for (int i = 0; i < s.Rows.Count; i++)
                {
                    DataRow r = d.NewRow();
                    r["Item"] = s.Rows[i]["BILITM_Name"].ToString();
                    r["Qty"] = s.Rows[i]["BILLITM_Quanty"].ToString();
                    d.Rows.Add(r);
                }
                j = j + 1;
            }
            return d;
        }
        public DataTable kot1()
        {
            DataTable ds = new DataTable();
            ds.Columns.Add("BillNo", typeof(string));
            ds.Columns.Add("BillDate", typeof(DateTime));
            ds.Columns.Add("BillTime", typeof(DateTime));
            ds.Columns.Add("Stallname", typeof(string));
            DataTable dstlss = pos.stlidsss();
            DataRow r = ds.NewRow();
            r["BillNO"] = txtbillno.Text;
            r["BillDate"] = DateTime.Now.Date;
            r["BillTime"] = DateTime.Now;
            r["Stallname"] = stallname;
            //if (h < dstlss.Rows.Count)
            //{
            //    STALLID = dstlss.Rows[h]["STL_ID"].ToString();
            //    DataTable dsn = pos.STALLNAME();
            //    r["Stallname"] = dsn.Rows[0]["STL_Name"].ToString();
            ds.Rows.Add(r);
            //    h = h + 1;
            //}
            return ds;
        }
        public static DataTable pos1, pos11;
        public void clear()
        {
            itemname.Text = "";itemrate.Text = "";quantity.Text = "";total.Text = ""; itemname1.Text = ""; itemrate1.Text = ""; quantity1.Text = ""; total1.Text = "";
            itemname2.Text = ""; itemrate2.Text = ""; quantity2.Text = ""; total2.Text = ""; itemname3.Text = ""; itemrate3.Text = ""; quantity3.Text = ""; total3.Text = "";
            itemname4.Text = ""; itemrate4.Text = ""; quantity4.Text = ""; total4.Text = ""; itemname5.Text = ""; itemrate5.Text = ""; quantity5.Text = ""; total5.Text = "";
            itemname6.Text = ""; itemrate6.Text = ""; quantity6.Text = ""; total6.Text = ""; itemname7.Text = ""; itemrate7.Text = ""; quantity7.Text = ""; total7.Text = "";
            itemname8.Text = ""; itemrate8.Text = ""; quantity8.Text = ""; total8.Text = ""; itemname9.Text = ""; itemrate9.Text = ""; quantity9.Text = ""; total9.Text = "";
            itemname10.Text = ""; itemrate10.Text = ""; quantity10.Text = ""; total10.Text = ""; itemname11.Text = ""; itemrate11.Text = ""; quantity11.Text = ""; total11.Text = "";
            itemname12.Text = ""; itemrate12.Text = ""; quantity12.Text = ""; total12.Text = ""; itemname13.Text = ""; itemrate13.Text = ""; quantity13.Text = ""; total13.Text = "";
            itemname14.Text = ""; itemrate14.Text = ""; quantity14.Text = ""; total14.Text = ""; itemname15.Text = ""; itemrate15.Text = ""; quantity15.Text = ""; total15.Text = "";
            itemname16.Text = ""; itemrate16.Text = ""; quantity16.Text = ""; total16.Text = ""; itemname17.Text = ""; itemrate17.Text = ""; quantity17.Text = ""; total17.Text = "";
            itemname18.Text = ""; itemrate18.Text = ""; quantity18.Text = ""; total18.Text = ""; itemname19.Text = ""; itemrate19.Text = ""; quantity19.Text = ""; total19.Text = "";
            txtttl.Text = ""; txtgst.Text = ""; txtgttl.Text = "";
        }
            public void checkede()
        {
            if (cou == 1)
            {
               // sp1.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname.Text = dt.Rows[0]["NAM_Name"].ToString();
                sa = dt.Rows[0]["NAM_Name"].ToString();
                rate1 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate.Text = Convert.ToString(rate1);
                itemname.IsReadOnly = true;
                itemrate.IsReadOnly = true;
                total.IsReadOnly = true;
            }
            if (cou == 2)
            {
              //  sp2.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname1.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate2 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate1.Text = Convert.ToString(rate2);
                itemname1.IsReadOnly = true;
                itemrate1.IsReadOnly = true;
                total1.IsReadOnly = true;
            }
            if (cou == 3)
            {
              //  sp3.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname2.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate3 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate2.Text = Convert.ToString(rate3);
                itemname2.IsReadOnly = true;
                itemrate2.IsReadOnly = true;
                total2.IsReadOnly = true;
            }
            if (cou == 4)
            {
              //  sp4.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname3.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate4 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate3.Text = Convert.ToString(rate4);
                itemname3.IsReadOnly = true;
                itemrate3.IsReadOnly = true;
                total3.IsReadOnly = true;
            }
            if (cou == 5)
            {
              //  sp5.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname4.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate5 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate4.Text = Convert.ToString(rate5);
                itemname4.IsReadOnly = true;
                itemrate4.IsReadOnly = true;
                total4.IsReadOnly = true;
            }
            if (cou == 6)
            {
              //  sp6.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname5.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate6 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate5.Text = Convert.ToString(rate6);
                itemname5.IsReadOnly = true;
                itemrate5.IsReadOnly = true;
                total5.IsReadOnly = true;
            }
            if (cou == 7)
            {
             //   sp7.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname6.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate7 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate6.Text = Convert.ToString(rate7);
                itemname6.IsReadOnly = true;
                itemrate6.IsReadOnly = true;
                total6.IsReadOnly = true;
            }
            if (cou == 8)
            {
               // sp8.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname7.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate8 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate7.Text = Convert.ToString(rate8);
                itemname7.IsReadOnly = true;
                itemrate7.IsReadOnly = true;
                total7.IsReadOnly = true;
            }
            if (cou == 9)
            {
              //  sp9.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname8.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate9 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate8.Text = Convert.ToString(rate9);
                itemname8.IsReadOnly = true;
                itemrate8.IsReadOnly = true;
                total8.IsReadOnly = true;
            }
            if (cou == 10)
            {
             //   sp10.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname9.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate10 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate9.Text = Convert.ToString(rate10);
                itemname9.IsReadOnly = true;
                itemrate9.IsReadOnly = true;
                total9.IsReadOnly = true;
            }
            if (cou == 11)
            {
              //  sp11.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname10.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate11 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate10.Text = Convert.ToString(rate11);
                itemname10.IsReadOnly = true;
                itemrate10.IsReadOnly = true;
                total10.IsReadOnly = true;
            }
            if (cou == 12)
            {
              //  sp12.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname11.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate12 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate11.Text = Convert.ToString(rate12);
                itemname11.IsReadOnly = true;
                itemrate11.IsReadOnly = true;
                total11.IsReadOnly = true;
            }
            if (cou == 13)
            {
              //  sp13.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname12.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate13 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate12.Text = Convert.ToString(rate13);
                itemname12.IsReadOnly = true;
                itemrate12.IsReadOnly = true;
                total12.IsReadOnly = true;
            }
            if (cou == 14)
            {
              //  sp14.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname13.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate14 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate13.Text = Convert.ToString(rate14);
                itemname13.IsReadOnly = true;
                itemrate13.IsReadOnly = true;
                total13.IsReadOnly = true;
            }
            if (cou == 15)
            {
             //   sp15.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname14.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate15 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate14.Text = Convert.ToString(rate15);
                itemname14.IsReadOnly = true;
                itemrate14.IsReadOnly = true;
                total14.IsReadOnly = true;
            }
            if (cou == 16)
            {
             //   sp16.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname15.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate16 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate15.Text = Convert.ToString(rate16);
                itemname15.IsReadOnly = true;
                itemrate15.IsReadOnly = true;
                total15.IsReadOnly = true;
            }
            if (cou == 17)
            {
             //   sp17.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname16.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate17 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate16.Text = Convert.ToString(rate17);
                itemname16.IsReadOnly = true;
                itemrate16.IsReadOnly = true;
                total16.IsReadOnly = true;
            }
            if (cou == 18)
            {
              //  sp18.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname17.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate18 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate17.Text = Convert.ToString(rate18);
                itemname17.IsReadOnly = true;
                itemrate17.IsReadOnly = true;
                total17.IsReadOnly = true;
            }
            if (cou == 19)
            {
              //  sp19.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname18.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate19 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate18.Text = Convert.ToString(rate19);
                itemname18.IsReadOnly = true;
                itemrate18.IsReadOnly = true;
                total18.IsReadOnly = true;
            }
            if (cou == 20)
            {
              //  sp20.Visibility = Visibility.Visible;
                dt = pos.GETRATE();
                itemname19.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate20 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate19.Text = Convert.ToString(rate20);
                itemname19.IsReadOnly = true;
                itemrate19.IsReadOnly = true;
                total19.IsReadOnly = true;
            }

        }
        public void checkede1()
        {
            if (cou == 1)
            {
                // var S = sender as CheckBox;
                 check1 = z1;
                dt = pos.GETRATE();
                itemname.Text = dt.Rows[0]["NAM_Name"].ToString();
                sa = dt.Rows[0]["NAM_Name"].ToString();
                rate1 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate.Text = Convert.ToString(rate1);
                itemname.IsReadOnly = true;
                itemrate.IsReadOnly = true;
                total.IsReadOnly = true;
            }
            if (cou == 2)
            {
                // var S = sender as CheckBox;
                 check1 = z2;
                dt = pos.GETRATE();
                itemname1.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate2 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate1.Text = Convert.ToString(rate2);
                itemname1.IsReadOnly = true;
                itemrate1.IsReadOnly = true;
                total1.IsReadOnly = true;
            }
            if (cou == 3)
            {
                // var S = sender as CheckBox;
                // check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname2.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate3 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate2.Text = Convert.ToString(rate3);
                itemname2.IsReadOnly = true;
                itemrate2.IsReadOnly = true;
                total2.IsReadOnly = true;
            }
            if (cou == 4)
            {
                // var S = sender as CheckBox;
                // check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname3.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate4 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate3.Text = Convert.ToString(rate4);
                itemname3.IsReadOnly = true;
                itemrate3.IsReadOnly = true;
                total3.IsReadOnly = true;
            }
            if (cou == 5)
            {
                // var S = sender as CheckBox;
                // check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname4.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate5 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate4.Text = Convert.ToString(rate5);
                itemname4.IsReadOnly = true;
                itemrate4.IsReadOnly = true;
                total4.IsReadOnly = true;
            }
            if (cou == 6)
            {
                // var S = sender as CheckBox;
                // check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname5.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate6 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate5.Text = Convert.ToString(rate6);
                itemname5.IsReadOnly = true;
                itemrate5.IsReadOnly = true;
                total5.IsReadOnly = true;
            }
            if (cou == 7)
            {
                // var S = sender as CheckBox;
                // check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname6.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate7 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate6.Text = Convert.ToString(rate7);
                itemname6.IsReadOnly = true;
                itemrate6.IsReadOnly = true;
                total6.IsReadOnly = true;
            }
            if (cou == 8)
            {
                // var S = sender as CheckBox;
                // check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname7.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate8 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate7.Text = Convert.ToString(rate8);
                itemname7.IsReadOnly = true;
                itemrate7.IsReadOnly = true;
                total7.IsReadOnly = true;
            }
            if (cou == 9)
            {
                // var S = sender as CheckBox;
                // check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname8.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate9 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate8.Text = Convert.ToString(rate9);
                itemname8.IsReadOnly = true;
                itemrate8.IsReadOnly = true;
                total8.IsReadOnly = true;
            }
            if (cou == 10)
            {
                // var S = sender as CheckBox;
                // check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname9.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate10 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate9.Text = Convert.ToString(rate10);
                itemname9.IsReadOnly = true;
                itemrate9.IsReadOnly = true;
                total9.IsReadOnly = true;
            }
            if (cou == 11)
            {
                // var S = sender as CheckBox;
                // check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname10.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate11 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate10.Text = Convert.ToString(rate11);
                itemname10.IsReadOnly = true;
                itemrate10.IsReadOnly = true;
                total10.IsReadOnly = true;
            }
            if (cou == 12)
            {
                // var S = sender as CheckBox;
                // check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname11.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate12 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate11.Text = Convert.ToString(rate12);
                itemname11.IsReadOnly = true;
                itemrate11.IsReadOnly = true;
                total11.IsReadOnly = true;
            }
            if (cou == 13)
            {
                // var S = sender as CheckBox;
                //  check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname12.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate13 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate12.Text = Convert.ToString(rate13);
                itemname12.IsReadOnly = true;
                itemrate12.IsReadOnly = true;
                total12.IsReadOnly = true;
            }
            if (cou == 14)
            {
                // var S = sender as CheckBox;
                // check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname13.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate14 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate13.Text = Convert.ToString(rate14);
                itemname13.IsReadOnly = true;
                itemrate13.IsReadOnly = true;
                total13.IsReadOnly = true;
            }
            if (cou == 15)
            {
                // var S = sender as CheckBox;
                // check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname14.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate15 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate14.Text = Convert.ToString(rate15);
                itemname14.IsReadOnly = true;
                itemrate14.IsReadOnly = true;
                total14.IsReadOnly = true;
            }
            if (cou == 16)
            {
                // var S = sender as CheckBox;
                // check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname15.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate16 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate15.Text = Convert.ToString(rate16);
                itemname15.IsReadOnly = true;
                itemrate15.IsReadOnly = true;
                total15.IsReadOnly = true;
            }
            if (cou == 17)
            {
                // var S = sender as CheckBox;
                // check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname16.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate17 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate16.Text = Convert.ToString(rate17);
                itemname16.IsReadOnly = true;
                itemrate16.IsReadOnly = true;
                total16.IsReadOnly = true;
            }
            if (cou == 18)
            {
                // var S = sender as CheckBox;
                // check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname17.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate18 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate17.Text = Convert.ToString(rate18);
                itemname17.IsReadOnly = true;
                itemrate17.IsReadOnly = true;
                total17.IsReadOnly = true;
            }
            if (cou == 19)
            {
                // var S = sender as CheckBox;
                // check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname18.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate19 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate18.Text = Convert.ToString(rate19);
                itemname18.IsReadOnly = true;
                itemrate18.IsReadOnly = true;
                total18.IsReadOnly = true;
            }
            if (cou == 20)
            {
                //var S = sender as CheckBox;
                //check1 = S.Content.ToString();
                dt = pos.GETRATE();
                itemname19.Text = dt.Rows[0]["NAM_Name"].ToString();
                rate20 = Convert.ToDecimal(dt.Rows[0]["NAM_Rate"].ToString());
                itemrate19.Text = Convert.ToString(rate20);
                itemname19.IsReadOnly = true;
                itemrate19.IsReadOnly = true;
                total19.IsReadOnly = true;
            }

        }
        public static string z, z1, z2, z3, z4, z5, z6, z7, z8, z9, z10, z11, z12, z13, z14, z15, z16, z17, z18, z19, z20, z21;
        
        public decimal ta1, ta2, ta3, ta4, ta5, ta6, ta7, ta8, ta9, ta10, ta11, ta12, ta13, ta14, ta15, ta16, ta17, ta18, ta19, ta20;
        public decimal tot1, tot2, tot3, tot4, tot5, tot6, tot7, tot8, tot9, tot10, tot11, tot12, tot13, tot14, tot15, tot16, tot17, tot18, tot19, tot20;
        public decimal totbill, billtax, gtotbill;
        private void quantity_LostFocus(object sender, RoutedEventArgs e)
        {
            txtttl.Text = "";
            txtgst.Text = "";
            txtgttl.Text = "";
            if (num.IsMatch(quantity.Text))
            {
                if (rate1 != 0)
                {
                    if (quantity.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity.Text))
                    {
                        total.Text = "";
                        check1 = itemname.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity.Text);
                        rate = rate1 * s;
                        total.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {
                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta1 = 0;
                            }
                            else
                            {
                                ta1 = rate * gst / 100;
                            }
                        }
                        tot1 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate1 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity.Text = "";
                    }
                }
                else
                {
                    if (quantity.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity.Text))
                    {
                        total.Text = "";
                        check1 = itemname.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity.Text);
                        decimal drate = Convert.ToDecimal(itemrate.Text);
                        rate = drate * s;
                        total.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta1 = 0;
                            }
                            else
                            {
                                ta1 = rate * gst / 100;
                            }
                        }
                        tot1 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate1 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity.Text = "";
                    }
                }
                sp1.Visibility = Visibility.Visible;
                cou++;
            }
            else { quantity.Text = ""; }
        }
        public static string check2;
        private void quantity1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (num.IsMatch(quantity1.Text))
            {
                if (rate2 != 0)
            {
                if (quantity1.Text == "")
                {
                }
                else if (num.IsMatch(quantity1.Text))
                {
                    total1.Text = "";
                    check1 = itemname1.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity1.Text);
                    rate = rate2 * s;
                    total1.Text = Convert.ToString(rate);
                    //ta2 = rate * 5 / 100;
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {

                    }
                    else
                    {

                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta2 = 0;
                        }
                        else
                        {
                            ta2 = rate * gst / 100;
                        }
                    }
                    tot2 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = a + b; rate2 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity1.Text = "";
                }
            }
            else
            {
                if (quantity1.Text == "")
                {
                }
                else if (num.IsMatch(quantity.Text))
                {
                    total1.Text = "";
                    check1 = itemname1.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity1.Text);
                    decimal drate = Convert.ToDecimal(itemrate1.Text);
                    rate = drate * s;
                    total1.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta2 = 0;
                        }
                        else
                        {
                            ta2 = rate * gst / 100;
                        }
                    }
                    tot2 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = a + b; rate2 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity1.Text = "";
                }
            }
                sp2.Visibility = Visibility.Visible; cou++;
            }
            else { quantity1.Text = "";   }
        }
        private void quantity2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (num.IsMatch(quantity2.Text))
            {
                if (rate3 != 0)
                {
                    if (quantity2.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity2.Text))
                    {
                        total2.Text = "";
                        check1 = itemname2.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity2.Text);
                        rate = rate3 * s;
                        total2.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta3 = 0;
                            }
                            else
                            {
                                ta3 = rate * gst / 100;
                            }
                        }
                        tot3 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate3 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity2.Text = "";
                    }
                }
                else
                {
                    if (quantity2.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity2.Text))
                    {
                        total2.Text = "";
                        check1 = itemname2.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity2.Text);
                        decimal drate = Convert.ToDecimal(itemrate2.Text);
                        rate = drate * s;
                        total2.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta3 = 0;
                            }
                            else
                            {
                                ta3 = rate * gst / 100;
                            }
                        }
                        tot3 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate3 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity2.Text = "";
                    }
                }
                sp3.Visibility = Visibility.Visible; cou++;
            }
            else { quantity2.Text = ""; }
        }
        private void quantity3_LostFocus(object sender, RoutedEventArgs e)
        {
            txtttl.Text = "";
            txtgst.Text = "";
            txtgttl.Text = "";
            if (num.IsMatch(quantity3.Text))
            {
                if (rate4 != 0)
                {
                    if (quantity3.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity3.Text))
                    {
                        total3.Text = "";
                        check1 = itemname3.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity3.Text);
                        rate = rate4 * s;
                        total3.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {
                        }
                        else
                        {
                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta4 = 0;
                            }
                            else
                            {
                                ta4 = rate * gst / 100;
                            }
                        }
                        tot4 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate4 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity3.Text = "";
                    }
                }
                else
                {
                    if (quantity3.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity3.Text))
                    {
                        total3.Text = "";
                        check1 = itemname3.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity3.Text);
                        decimal drate = Convert.ToDecimal(itemrate3.Text);
                        rate = drate * s;
                        total3.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta4 = 0;
                            }
                            else
                            {
                                ta4 = rate * gst / 100;
                            }
                        }
                        tot4 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate4 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity3.Text = "";
                    }
                }
                sp4.Visibility = Visibility.Visible; cou++;
            }
            else { quantity3.Text = ""; }
        }
        private void quantity4_LostFocus(object sender, RoutedEventArgs e)
        {
            if (num.IsMatch(quantity4.Text))
            {
                if (rate5 != 0)
                {
                    if (quantity4.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity4.Text))
                    {
                        total4.Text = "";
                        check1 = itemname4.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity4.Text);
                        rate = rate5 * s;
                        total4.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta5 = 0;
                            }
                            else
                            {
                                ta5 = rate * gst / 100;
                            }
                        }
                        tot5 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate5 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity4.Text = "";
                    }
                }
                else
                {
                    if (quantity4.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity4.Text))
                    {
                        total4.Text = "";
                        check1 = itemname4.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity4.Text);
                        decimal drate = Convert.ToDecimal(itemrate4.Text);
                        rate = drate * s;
                        total4.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta5 = 0;
                            }
                            else
                            {
                                ta5 = rate * gst / 100;
                            }
                        }
                        tot5 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate5 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity4.Text = "";
                    }
                }
                sp5.Visibility = Visibility.Visible; cou++;
            }
            else
            { quantity4.Text = "";}
        }
        private void quantity5_LostFocus(object sender, RoutedEventArgs e)
        {
            if (num.IsMatch(quantity5.Text))
            {
                if (rate6 != 0)
                {
                    if (quantity5.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity5.Text))
                    {
                        check1 = itemname5.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity5.Text);
                        rate = rate6 * s;
                        total5.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta6 = 0;
                            }
                            else
                            {
                                ta6 = rate * gst / 100;
                            }
                        }
                        tot6 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate6 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity5.Text = "";
                    }
                }
                else
                {
                    if (quantity5.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity5.Text))
                    {
                        check1 = itemname5.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity5.Text);
                        decimal drate = Convert.ToDecimal(itemrate5.Text);
                        rate = drate * s;
                        total5.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta6 = 0;
                            }
                            else
                            {
                                ta6 = rate * gst / 100;
                            }
                        }
                        tot6 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate6 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity5.Text = "";
                    }
                }
                sp6.Visibility = Visibility.Visible; cou++;
            }
            else { quantity5.Text = ""; }
        }

        private void quantity6_LostFocus(object sender, RoutedEventArgs e)
        {
            if (num.IsMatch(quantity6.Text))
            {
                if (rate7 != 0)
                {
                    if (quantity6.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity6.Text))
                    {
                        check1 = itemname6.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity6.Text);
                        rate = rate7 * s;
                        total6.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta7 = 0;
                            }
                            else
                            {
                                ta7 = rate * gst / 100;
                            }
                        }
                        tot7 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate7 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity6.Text = "";
                    }
                }
                else
                {
                    if (quantity6.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity6.Text))
                    {
                        check1 = itemname6.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity6.Text);
                        decimal drate = Convert.ToDecimal(itemrate6.Text);
                        rate = drate * s;
                        total6.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta7 = 0;
                            }
                            else
                            {
                                ta7 = rate * gst / 100;
                            }
                        }
                        tot7 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate7 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity6.Text = "";
                    }
                }
                sp7.Visibility = Visibility.Visible; cou++;
            }
            else { quantity6.Text = ""; }
        }
        private void quantity7_LostFocus(object sender, RoutedEventArgs e)
        {
            if (num.IsMatch(quantity7.Text))
            {
                if (rate8 != 0)
                {
                    if (quantity7.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity7.Text))
                    {
                        check1 = itemname7.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity7.Text);
                        rate = rate8 * s;
                        total7.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta8 = 0;
                            }
                            else
                            {
                                ta8 = rate * gst / 100;
                            }
                        }
                        tot8 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate8 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity7.Text = "";
                    }
                }
                else
                {
                    if (quantity7.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity7.Text))
                    {
                        check1 = itemname7.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity7.Text);
                        decimal drate = Convert.ToDecimal(itemrate7.Text);
                        rate = drate * s;
                        total7.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta8 = 0;
                            }
                            else
                            {
                                ta8 = rate * gst / 100;
                            }
                        }
                        tot8 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate8 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity7.Text = "";
                    }
                }
                sp8.Visibility = Visibility.Visible; cou++;
            }
            else { quantity7.Text = ""; }
        }

        private void quantity8_LostFocus(object sender, RoutedEventArgs e)
        {
            if (num.IsMatch(quantity8.Text))
            {
                if (rate9 != 0)
                {
                    if (quantity8.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity8.Text))
                    {
                        check1 = itemname8.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity8.Text);
                        rate = rate9 * s;
                        total8.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta9 = 0;
                            }
                            else
                            {
                                ta9 = rate * gst / 100;
                            }
                        }
                        tot9 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate9 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity8.Text = "";
                    }
                }
                else
                {
                    if (quantity8.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity8.Text))
                    {
                        check1 = itemname8.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity8.Text);
                        decimal drate = Convert.ToDecimal(itemrate8.Text);
                        rate = drate * s;
                        total8.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta9 = 0;
                            }
                            else
                            {
                                ta9 = rate * gst / 100;
                            }
                        }
                        tot9 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate9 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity8.Text = "";
                    }
                }
                sp9.Visibility = Visibility.Visible; cou++;
            }
            else { quantity8.Text = ""; }
        }
        private void quantity9_LostFocus(object sender, RoutedEventArgs e)
        {
            if (num.IsMatch(quantity9.Text))
            {
                if (rate10 != 0)
                {
                    if (quantity9.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity9.Text))
                    {
                        check1 = itemname9.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity9.Text);
                        rate = rate10 * s;
                        total9.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta10 = 0;
                            }
                            else
                            {
                                ta10 = rate * gst / 100;
                            }
                        }
                        tot10 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate10 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity9.Text = "";
                    }
                }
                else
                {
                    if (quantity9.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity9.Text))
                    {
                        check1 = itemname9.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity9.Text);
                        decimal drate = Convert.ToDecimal(itemrate9.Text);
                        rate = drate * s;
                        total9.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta10 = 0;
                            }
                            else
                            {
                                ta10 = rate * gst / 100;
                            }
                        }
                        tot10 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate10 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity9.Text = "";
                    }
                }
                sp10.Visibility = Visibility.Visible; cou++;
            }
            else { quantity9.Text = ""; }
        }
        private void quantity10_LostFocus(object sender, RoutedEventArgs e)
        {
            if (num.IsMatch(quantity10.Text))
            {
                if (rate11 != 0)
                {
                    if (quantity10.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity10.Text))
                    {
                        check1 = itemname10.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity10.Text);
                        rate = rate11 * s;
                        total10.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta11 = 0;
                            }
                            else
                            {
                                ta11 = rate * gst / 100;
                            }
                        }
                        tot11 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate11 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity10.Text = "";
                    }
                }
                else
                {
                    if (quantity10.Text == "")
                    {
                    }
                    else if (num.IsMatch(quantity10.Text))
                    {
                        check1 = itemname10.Text;
                        DataTable dd = pos.gsttax();
                        if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                        s = Convert.ToInt32(quantity10.Text);
                        decimal drate = Convert.ToDecimal(itemrate10.Text);
                        rate = drate * s;
                        total10.Text = Convert.ToString(rate);
                        DataTable d = pos.GETTAX();
                        decimal gst;
                        if (d.Rows.Count == 0)
                        {

                        }
                        else
                        {

                            gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                            if (gst == 0)
                            {
                                ta11 = 0;
                            }
                            else
                            {
                                ta11 = rate * gst / 100;
                            }
                        }
                        tot11 = rate;
                        txtttl.Text = "";
                        txtgst.Text = "";
                        txtgttl.Text = "";
                        totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                        decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                        gtotbill = a + b; rate11 = 0;
                        txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        quantity10.Text = "";
                    }
                }
                sp11.Visibility = Visibility.Visible; cou++;
            }
            else { quantity10.Text = ""; }
        }
        private void quantity11_LostFocus(object sender, RoutedEventArgs e)
        {
            if (rate12 != 0)
            {
                if (quantity11.Text == "")
                {
                }
                else if (num.IsMatch(quantity11.Text))
                {
                    check1 = itemname11.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity11.Text);
                    rate = rate12 * s;
                    total11.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {

                    }
                    else
                    {

                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta12 = 0;
                        }
                        else
                        {
                            ta12 = rate * gst / 100;
                        }
                    }
                    tot12 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = a + b; rate12 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity11.Text = "";
                }
            }
            else
            {
                if (quantity11.Text == "")
                {
                }
                else if (num.IsMatch(quantity11.Text))
                {
                    check1 = itemname11.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity11.Text);
                    decimal drate = Convert.ToDecimal(itemrate11.Text);
                    rate = drate * s;
                    total11.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {

                    }
                    else
                    {

                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta12 = 0;
                        }
                        else
                        {
                            ta12 = rate * gst / 100;
                        }
                    }
                    tot12 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = a + b; rate12 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity11.Text = "";
                }
            }
                sp12.Visibility = Visibility.Visible; cou++;
        }
        private void quantity12_LostFocus(object sender, RoutedEventArgs e)
        {
            if (rate13 != 0)
            {
                if (quantity12.Text == "")
                {
                }
                else if (num.IsMatch(quantity12.Text))
                {
                    check1 = itemname12.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity12.Text);
                    rate = rate13 * s;
                    total12.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {

                    }
                    else
                    {

                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta13 = 0;
                        }
                        else
                        {
                            ta13 = rate * gst / 100;
                        }
                    }
                    tot13 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = a + b; rate13 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity12.Text = "";
                }
            }
            else
            {
                if (quantity12.Text == "")
                {
                }
                else if (num.IsMatch(quantity12.Text))
                {
                    check1 = itemname12.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity12.Text);
                    decimal drate = Convert.ToDecimal(itemrate12.Text);
                    rate = drate * s;
                    total12.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {

                    }
                    else
                    {

                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta13 = 0;
                        }
                        else
                        {
                            ta13 = rate * gst / 100;
                        }
                    }
                    tot13 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = a + b; rate13 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity12.Text = "";
                }
            }
                sp13.Visibility = Visibility.Visible; cou++;
        }
        private void quantity13_LostFocus(object sender, RoutedEventArgs e)
        {
            if (rate14 != 0)
            {
                if (quantity13.Text == "")
                {
                }
                else if (num.IsMatch(quantity13.Text))
                {
                    check1 = itemname13.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity13.Text);
                    rate = rate14 * s;
                    total13.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {

                    }
                    else
                    {

                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta14 = 0;
                        }
                        else
                        {
                            ta14 = rate * gst / 100;
                        }
                    }
                    tot14 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = a + b; rate14 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity13.Text = "";
                }
            }
            else
            {
                if (quantity13.Text == "")
                {
                }
                else if (num.IsMatch(quantity13.Text))
                {
                    check1 = itemname13.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity13.Text);
                    decimal drate = Convert.ToDecimal(itemrate13.Text);
                    rate = drate * s;
                    total13.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {

                    }
                    else
                    {

                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta14 = 0;
                        }
                        else
                        {
                            ta14 = rate * gst / 100;
                        }
                    }

                    tot14 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = a + b; rate14 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity13.Text = "";
                }
            }
                sp14.Visibility = Visibility.Visible; cou++;
        }
        private void quantity14_LostFocus(object sender, RoutedEventArgs e)
        {
            if (rate15 != 0)
            {
                if (quantity14.Text == "")
                {
                }
                else if (num.IsMatch(quantity14.Text))
                {
                    check1 = itemname14.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity14.Text);
                    rate = rate15 * s;
                    total14.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {

                    }
                    else
                    {

                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta15 = 0;
                        }
                        else
                        {
                            ta15 = rate * gst / 100;
                        }
                    }
                    tot15 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = a + b; rate15 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity14.Text = "";
                }
            }
            else
            {
                if (quantity14.Text == "")
                {
                }
                else if (num.IsMatch(quantity14.Text))
                {
                    check1 = itemname14.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity14.Text);
                    decimal drate = Convert.ToDecimal(itemrate14.Text);
                    rate = drate * s;
                    total14.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {

                    }
                    else
                    {

                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta15 = 0;
                        }
                        else
                        {
                            ta15 = rate * gst / 100;
                        }
                    }
                    tot15 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = a + b; rate15 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity14.Text = "";
                }
            }
                sp15.Visibility = Visibility.Visible; cou++;
        }
        private void quantity15_LostFocus(object sender, RoutedEventArgs e)
        {
            if (rate16 != 0)
            {
                if (quantity15.Text == "")
                {
                }
                else if (num.IsMatch(quantity15.Text))
                {
                    check1 = itemname15.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity15.Text);
                    rate = rate16 * s;
                    total15.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {

                    }
                    else
                    {

                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta16 = 0;
                        }
                        else
                        {
                            ta16 = rate * gst / 100;
                        }
                    }
                    tot16 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = a + b; rate16 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity15.Text = "";
                }
            }
            else
            {
                if (quantity15.Text == "")
                {
                }
                else if (num.IsMatch(quantity15.Text))
                {
                    check1 = itemname15.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity15.Text);
                    decimal drate = Convert.ToDecimal(itemrate15.Text);
                    rate = drate * s;
                    total15.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {

                    }
                    else
                    {

                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta16 = 0;
                        }
                        else
                        {
                            ta16 = rate * gst / 100;
                        }
                    }
                    tot16 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = a + b; rate16 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity15.Text = "";
                }
            }
                sp16.Visibility = Visibility.Visible; cou++;
        }
        private void quantity16_LostFocus(object sender, RoutedEventArgs e)
        {
            if (rate17 != 0)
            {
                if (quantity16.Text == "")
                {
                }
                else if (num.IsMatch(quantity16.Text))
                {
                    check1 = itemname16.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity16.Text);
                    rate = rate17 * s;
                    total16.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {

                    }
                    else
                    {

                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta17 = 0;
                        }
                        else
                        {
                            ta17 = rate * gst / 100;
                        }
                    }
                    tot17 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = a + b; rate17 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity16.Text = "";
                }
            }
            else
            {
                if (quantity16.Text == "")
                {
                }
                else if (num.IsMatch(quantity16.Text))
                {
                    check1 = itemname16.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity16.Text);
                    decimal drate = Convert.ToDecimal(itemrate16.Text);
                    rate = drate * s;
                    total16.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {

                    }
                    else
                    {

                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta17 = 0;
                        }
                        else
                        {
                            ta17 = rate * gst / 100;
                        }
                    }
                    tot17 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    billtax = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = a + b; rate17 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity16.Text = "";
                }
            }
                sp17.Visibility = Visibility.Visible; cou++;
        }
        private void quantity17_LostFocus(object sender, RoutedEventArgs e)
        {
            if (rate18 != 0)
            {
                if (quantity17.Text == "")
                {
                }
                else if (num.IsMatch(quantity17.Text))
                {
                    check1 = itemname17.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity17.Text);
                    rate = rate18 * s;
                    total17.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {

                    }
                    else
                    {

                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta18 = 0;
                        }
                        else
                        {
                            ta18 = rate * gst / 100;
                        }
                    }
                    tot18 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = (tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20);
                    billtax = (ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20);
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = a + b; rate18 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity17.Text = "";
                }
            }
            else
            {
                if (quantity17.Text == "")
                {
                }
                else if (num.IsMatch(quantity17.Text))
                {
                    check1 = itemname17.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity17.Text);
                    decimal drate = Convert.ToDecimal(itemrate17.Text);
                    rate = drate * s;
                    total17.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {

                    }
                    else
                    {

                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta18 = 0;
                        }
                        else
                        {
                            ta18 = rate * gst / 100;
                        }
                    }
                    tot18 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = (tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20);
                    billtax = (ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20);
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = (a + b); rate18 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity17.Text = "";
                }
            }
                sp18.Visibility = Visibility.Visible; cou++;
        }
        private void quantity18_LostFocus(object sender, RoutedEventArgs e)
        {
            if (rate19 != 0)
            {
                if (quantity18.Text == "")
                {
                }
                else if (num.IsMatch(quantity18.Text))
                {
                    check1 = itemname18.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity18.Text);
                    rate = rate19 * s;
                    total18.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    {

                    }
                    else
                    {

                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta19 = 0;
                        }
                        else
                        {
                            ta19 = rate * gst / 100;
                        }
                    }
                    tot19 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = (tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20);
                    billtax = (ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20);
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = (a + b); rate11 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity18.Text = "";
                }
            }
            else
            {
                if (quantity18.Text == "")
                {
                }
                else if (num.IsMatch(quantity18.Text))
                {
                    check1 = itemname18.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity18.Text);
                    decimal drate = Convert.ToDecimal(itemrate18.Text);
                    rate = drate * s;
                    total18.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    { }
                    else
                    {
                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta19 = 0;
                        }
                        else
                        {
                            ta19 = rate * gst / 100;
                        }
                    }
                    tot19 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = (tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20);
                    billtax = (ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20);
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = (a + b); rate11 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity18.Text = "";
                }
            }
                sp19.Visibility = Visibility.Visible; cou++;
        }
        private void quantity19_LostFocus(object sender, RoutedEventArgs e)
        {
            if (rate20 != 0)
            {
                if (quantity19.Text == "")
                {
                }
                else if (num.IsMatch(quantity19.Text))
                {
                    check1 = itemname19.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity19.Text);
                    rate = rate20 * s;
                    total19.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    { }
                    else
                    {
                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta20 = 0;
                        }
                        else
                        {
                            ta20 = rate * gst / 100;
                        }
                    }
                    tot20= rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = (tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20);
                    billtax = (ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20);
                    decimal a = tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20; 
                    gtotbill = (a + b); rate20 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity19.Text = "";
                }
            }
            else
            {
                if (quantity19.Text == "")
                {
                }
                else if (num.IsMatch(quantity19.Text))
                {
                    check1 = itemname19.Text;
                    DataTable dd = pos.gsttax();
                    if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
                    s = Convert.ToInt32(quantity19.Text);
                    decimal drate = Convert.ToDecimal(itemrate19.Text);
                    rate = drate * s;
                    total19.Text = Convert.ToString(rate);
                    DataTable d = pos.GETTAX();
                    decimal gst;
                    if (d.Rows.Count == 0)
                    { }
                    else
                    {
                        gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]);
                        if (gst == 0)
                        {
                            ta20 = 0;
                        }
                        else
                        {
                            ta20 = rate * gst / 100;
                        }
                    }
                    tot20 = rate;
                    txtttl.Text = "";
                    txtgst.Text = "";
                    txtgttl.Text = "";
                    totbill = (tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20);
                    billtax = (ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20);
                    decimal a =tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b = ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill = (a + b); rate20 = 0;
                    txtttl.Text = Math.Round(totbill, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgst.Text = Math.Round(billtax, 2, MidpointRounding.AwayFromZero).ToString();
                    txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    quantity19.Text = "";
                }
            }
        }
    }
}