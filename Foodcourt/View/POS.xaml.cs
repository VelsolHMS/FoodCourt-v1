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
        Regex alp = new Regex(@"^[a-zA-Z0-9 -()]+$");
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
            Button();
            cou = 0;
            clear();
            checkbox_checks = null;
            DataTable stall_names = pos.GETSTALLS();
            stalls.ItemsSource = stall_names.DefaultView;
        }
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                error++;
            else
                error--;
        }
        DataTable dtctg;
        public void Button()
        {
            WrapPanel wrap1 = new WrapPanel();
            dtctg = pos.GetCategories();
            for (int i = 0; i < dtctg.Rows.Count; i++)
            {
                Button btn = new Button();
                btn.Content = dtctg.Rows[i]["CTG_Name"].ToString();
                btn.Click += new RoutedEventHandler(btn1_click);
                btn.Height = 35;
                btn.Width = 175;
                btn.Background = Brushes.Green;
                btn.Foreground = Brushes.White;
                btn.BorderBrush = Brushes.White;
                btn.Margin = new System.Windows.Thickness(10, 10, 0, 0);
                wrap1.Children.Add(btn);
            }
            WRAPC.Children.Add(wrap1);
        }
        public static string stlid,ctgname;
        private void btn1_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            aa = btn.Content.ToString();
            //DataTable dtstlid = pos.getstlid();
            //stlid = dtstlid.Rows[0]["STL_ID"].ToString();
            ITMCTG.Visibility = Visibility.Hidden;
            ITMNAM.Visibility = Visibility.Visible;
            CHECKBOX();
        }
        public void CHECKBOX()
        {
            WrapPanel wrap1 = new WrapPanel();
            DataTable DT1 = pos.GETNAME();
            for (int i = 0; i < DT1.Rows.Count; i++)
            {
                CheckBox chk = new CheckBox();
                chk.Content = DT1.Rows[i]["NAM_Name"].ToString();
                chk.Click += new RoutedEventHandler(chk_Click);
                chk.Height = 35;
                chk.Width = 170;
                if (checkbox_checks == null)
                {
                }
                else
                {
                    if (checkbox_checks.Contains(DT1.Rows[i]["NAM_Name"].ToString()))
                    {
                        chk.IsChecked = true;
                    }
                }
                chk.Background = Brushes.Black;
                chk.Foreground = Brushes.Black;
                chk.BorderBrush = Brushes.Black;
                chk.Margin = new System.Windows.Thickness(10, 10, 0, 0);
                wrap1.Children.Add(chk);
            }
            WRAPN.Children.Add(wrap1);
        }
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
        public decimal dis, ins,gndtot;
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
            DROW["Total"] = dt2.Rows[0]["BILL_Amount"].ToString();
            decimal tax =Convert.ToDecimal(dt2.Rows[0]["BILL_Tax"].ToString());
            //DROW["Cgst"] = tax / 2;
            //DROW["Sgst"] = tax / 2;
            DROW["Cgst"] = tax5sum;
            DROW["Sgst"] = tax18sum;
            dis = Convert.ToDecimal(dt2.Rows[0]["BILL_Discount"].ToString());
            ins = Convert.ToDecimal(dt2.Rows[0]["Bill_InstantDis"].ToString());
            DROW["Discount"] = dis + ins;
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
            ITMNAM.Visibility = Visibility.Hidden;
            WRAPN.Children.Clear();
            ITMCTG.Visibility = Visibility.Visible;
        }
        public static string check1, t1,t2;
        public static string bid;
        public static DataTable subdata;
        public static int cou = 0;
        public string sri;
        public int s;
        public decimal rate,rate1,rate2,rate3, rate4, rate5, rate6, rate7, rate8, rate9, rate10, rate11, rate12, rate13, rate14, rate15,rate16,rate17,rate18,rate19,rate20,rate21;

        private void btnprfl_Click(object sender, RoutedEventArgs e)
        {
            cpmtrl.Visibility = Visibility.Visible;
        }

        private void btncls_Click(object sender, RoutedEventArgs e)
        {
            cpmtrl.Visibility = Visibility.Hidden;
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pos.NAME = txtnamee.Text;
                pos.MOBILE_NO = txtphone.Text;
                pos.EMAIL = txtemail.Text;
                pos.CITY = txtaddress.Text;
                pos.ADDRESS = txtadd.Text;
                string a = "Submit"; string b = Convert.ToString(btnsave.Content);
                if (a == b)
                {
                    pos.custinfo();
                    MessageBox.Show("Saved Successfully");
                    clear1();
                }
                else
                {
                    btnsave.Content = "Update";
                    MessageBox.Show("Updated Successfully");
                    clear1();
                    btnsave.Content = "Submit";
                }
            }
            catch(SystemException )
            {
            }
        }
        public void clear1()
        {
            txtnamee.Text = "";
            txtphone.Text = "";
            txtemail.Text = "";
            txtadd.Text = "";
            txtaddress.Text = "";
            btnsave.Content = "Submit";
        }
        private void btnclr_Click(object sender, RoutedEventArgs e)
        {
            clear1();
        }
        public static string phno;
        private void txtphone_LostFocus(object sender, RoutedEventArgs e)
        {
            if (error == 0)
            {
                phno = txtphone.Text;
                DataTable dt = pos.getdetails();
                if (dt.Rows.Count == 0)
                {
                    txtnamee.Text = "";
                    txtemail.Text = "";
                    txtadd.Text = "";
                    txtaddress.Text = "";
                }
                else
                {
                    btnsave.Content = "Update";
                    txtnamee.Text = dt.Rows[0]["NAME"].ToString();
                    txtemail.Text = dt.Rows[0]["EMAIL"].ToString();
                    txtadd.Text = dt.Rows[0]["CITY"].ToString();
                    txtaddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                }
            }
            else
            {
            }
        }
        private void quantity2_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        public static decimal tax5sum,tax18sum,ttotal, maxamount,disper,amdis;
        public decimal tax15, tax25, tax35, tax45, tax55, tax65, tax75, tax85, tax95, tax105, tax115, tax125, tax135, tax145, tax155, tax165, tax175, tax185, tax195, tax205;

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
        DataTable DITM;
        private void OffYes_Click(object sender, RoutedEventArgs e)
        {
            OfferConfirmation.IsOpen = false;
            DataTable dt = pos.getofferlist();
            txtstall.ItemsSource = dt.DefaultView;
            OfferPage.IsOpen = true;
            txtTotal.Text = txtgttl.Text;
            ttotal = Convert.ToDecimal(txtTotal.Text);
        }

        public decimal tax118, tax218, tax318, tax418, tax518, tax618, tax718, tax818, tax918, tax1018, tax1118, tax1218, tax1318, tax1418, tax1518, tax1618, tax1718, tax1818, tax1918, tax2018;
        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            PrintConfirmation.IsOpen = false;
            //if (error != 0)
            //{
            //    MessageBox.Show("Please Fill All Fields");
            //}
            //else
            //{
            //    pos.bill = Convert.ToInt32(txtbillno.Text);
            //    pos.BILL_Amount = Convert.ToDecimal(txtttl.Text);
            //    pos.BILL_Tax = Convert.ToDecimal(txtgst.Text);
            //    pos.BILL_Total = Convert.ToDecimal(txtgttl.Text);
            //    pos.insertbill();
            //    pos.A = Convert.ToInt32(txtbillno.Text);
            //    for (int i = 0; i < cou; i++)
            //    {
            //        if (i == 0)
            //        {
            //            if (itemname.Text != "" || itemrate.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity.Text); decimal B = A * Convert.ToDecimal(itemrate.Text);
            //                decimal gst = 0; check1 = itemname.Text; itemnamestlid = itemname.Text; DataTable dts0 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts0.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if(gst ==Convert.ToDecimal(5.00))
            //                {
            //                    tax15 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5/100;
            //                }
            //                else if(gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax118 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18/100;
            //                }
            //            }
            //        }
            //        if (i == 1)
            //        {
            //            if (itemname1.Text != "" || itemrate1.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity1.Text); decimal B = A * Convert.ToDecimal(itemrate1.Text);
            //                decimal gst = 0; check1 = itemname1.Text; itemnamestlid = itemname1.Text; DataTable dts1 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts1.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname1.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate1.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax25 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax218 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 2)
            //        {
            //            if (itemname2.Text != "" || itemrate2.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity2.Text); decimal B = A * Convert.ToDecimal(itemrate2.Text);
            //                decimal gst = 0; check1 = itemname2.Text; itemnamestlid = itemname2.Text; DataTable dts2 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts2.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname2.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate2.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax35 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax318 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 3)
            //        {

            //            if (itemname3.Text != "" || itemrate3.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity3.Text); decimal B = A * Convert.ToDecimal(itemrate3.Text);
            //                decimal gst = 0; check1 = itemname3.Text; itemnamestlid = itemname3.Text; DataTable dts3 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts3.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname3.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate3.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax45 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax418 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 4)
            //        {
            //            if (itemname4.Text != "" || itemrate4.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity4.Text); decimal B = A * Convert.ToDecimal(itemrate4.Text);
            //                decimal gst = 0; check1 = itemname4.Text; itemnamestlid = itemname4.Text; DataTable dts4 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts4.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname4.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate4.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax55 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax518 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 5)
            //        {
            //            if (itemname5.Text != "" || itemrate5.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity5.Text); decimal B = A * Convert.ToDecimal(itemrate5.Text);
            //                decimal gst = 0; check1 = itemname5.Text; itemnamestlid = itemname5.Text; DataTable dts5 = pos.getstlid();
            //                DataTable dd = pos.gsttax();stlid = dts5.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname5.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate5.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax65 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax618 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 6)
            //        {
            //            if (itemname6.Text != "" || itemrate6.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity6.Text); decimal B = A * Convert.ToDecimal(itemrate6.Text);
            //                decimal gst = 0; check1 = itemname6.Text; itemnamestlid = itemname6.Text; DataTable dts6 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts6.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname6.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate6.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax75 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax718 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 7)
            //        {
            //            if (itemname7.Text != "" || itemrate7.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity7.Text); decimal B = A * Convert.ToDecimal(itemrate7.Text);
            //                decimal gst = 0; check1 = itemname7.Text; itemnamestlid = itemname7.Text; DataTable dts7 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts7.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname7.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate7.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax85 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax818 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 8)
            //        {
            //            if (itemname8.Text != "" || itemrate8.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity8.Text); decimal B = A * Convert.ToDecimal(itemrate8.Text);
            //                decimal gst = 0; check1 = itemname8.Text; itemnamestlid = itemname8.Text; DataTable dts8 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts8.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname8.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate8.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax95 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax918 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 9)
            //        {
            //            if (itemname9.Text != "" || itemrate9.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity9.Text); decimal B = A * Convert.ToDecimal(itemrate9.Text);
            //                decimal gst = 0; check1 = itemname9.Text; itemnamestlid = itemname9.Text; DataTable dts9 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts9.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname9.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate9.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax105 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax1018 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 10)
            //        {
            //            if (itemname10.Text != "" || itemrate10.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity10.Text); decimal B = A * Convert.ToDecimal(itemrate10.Text);
            //                decimal gst = 0; check1 = itemname10.Text; itemnamestlid = itemname10.Text; DataTable dts10 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts10.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname10.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate10.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax115 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax1118 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 11)
            //        {
            //            if (itemname11.Text != "" || itemrate11.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity11.Text); decimal B = A * Convert.ToDecimal(itemrate11.Text);
            //                decimal gst = 0; check1 = itemname11.Text; itemnamestlid = itemname11.Text; DataTable dts11 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts11.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname11.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate11.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax125 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax1218 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 12)
            //        {
            //            if (itemname12.Text != "" || itemrate12.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity12.Text); decimal B = A * Convert.ToDecimal(itemrate12.Text);
            //                decimal gst = 0; check1 = itemname12.Text; itemnamestlid = itemname12.Text; DataTable dts12 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts12.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname12.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate12.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax135 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax1318 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 13)
            //        {
            //            if (itemname13.Text != "" || itemrate13.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity13.Text); decimal B = A * Convert.ToDecimal(itemrate13.Text);
            //                decimal gst = 0; check1 = itemname13.Text; itemnamestlid = itemname13.Text; DataTable dts13 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts13.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname13.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate13.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax145 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax1418 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 14)
            //        {
            //            if (itemname14.Text != "" || itemrate14.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity14.Text); decimal B = A * Convert.ToDecimal(itemrate14.Text);
            //                decimal gst = 0; check1 = itemname14.Text; itemnamestlid = itemname14.Text; DataTable dts14 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts14.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname14.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate14.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax155 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax1518 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 15)
            //        {
            //            if (itemname15.Text != "" || itemrate15.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity15.Text); decimal B = A * Convert.ToDecimal(itemrate15.Text);
            //                decimal gst = 0; check1 = itemname15.Text; itemnamestlid = itemname15.Text; DataTable dts15 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts15.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname15.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate15.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax165 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax1618 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 16)
            //        {
            //            if (itemname16.Text != "" || itemrate16.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity16.Text); decimal B = A * Convert.ToDecimal(itemrate16.Text);
            //                decimal gst = 0; check1 = itemname16.Text; itemnamestlid = itemname16.Text; DataTable dts16 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts16.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname16.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate16.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax175 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax1718 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 17)
            //        {
            //            if (itemname17.Text != "" || itemrate17.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity17.Text); decimal B = A * Convert.ToDecimal(itemrate17.Text);
            //                decimal gst = 0; check1 = itemname17.Text; itemnamestlid = itemname17.Text; DataTable dts17 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts17.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname17.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate17.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax185 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax1818 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 18)
            //        {
            //            if (itemname18.Text != "" || itemrate18.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity18.Text); decimal B = A * Convert.ToDecimal(itemrate18.Text);
            //                decimal gst = 0; check1 = itemname18.Text; itemnamestlid = itemname18.Text; DataTable dts18 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts18.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname18.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate18.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax195 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax1918 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //        if (i == 19)
            //        {
            //            if (itemname19.Text != "" || itemrate19.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity19.Text); decimal B = A * Convert.ToDecimal(itemrate19.Text);
            //                decimal gst = 0; check1 = itemname19.Text; itemnamestlid = itemname19.Text; DataTable dts19 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts19.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname19.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate19.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //                if (gst == Convert.ToDecimal(5.00))
            //                {
            //                    tax205 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
            //                }
            //                else if (gst == Convert.ToDecimal(18.00))
            //                {
            //                    tax2018 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
            //                }
            //            }
            //        }
            //    }
            //    tax5sum = tax15+ tax25+ tax35+ tax45+ tax55+tax65+ tax75+ tax85+ tax95+ tax105+ tax115+ tax125+ tax135+ tax145+ tax155+ tax165+ tax175+ tax185+ tax195+ tax205;
            //    tax18sum = tax118+ tax218+ tax318+ tax418+ tax518+ tax618+ tax718+ tax818+ tax918+ tax1018+ tax1118+ tax1218+ tax1318+ tax1418+ tax1518+ tax1618+ tax1718+ tax1818+ tax1918+ tax2018;

            //    MessageBox.Show("Inserted successfully");
            //    ReportDocument re = new ReportDocument();
            //    DataTable d1 = Billprint1();
            //    pos1 = d1;
            //    re.Load("../../REPORTS/PRINTBILL1.rpt");
            //    DataTable ds = Billprint();
            //    pos11 = ds;
            //    re.Load("../../REPORTS/Printbill.rpt");
            //    re.SetDataSource(pos11);
            //    re.Subreports[0].SetDataSource(pos1);
            //    re.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;


            //    ReportDocument res = new ReportDocument();
            //    DataTable d3 = kot();
            //    a11 = d3;
            //    res.Load("../../REPORTS/kotprint.rpt");
            //    DataTable d2 = kot1();
            //    a1 = d2;
            //    res.Load("../../REPORTS/kotprint1.rpt");
            //    res.SetDataSource(a1);
            //    res.Subreports[0].SetDataSource(a11);
            //    res.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
            //    res.PrintToPrinter(1, false, 0, 0);
            //    res.Refresh();
            //    re.PrintToPrinter(1, false, 0, 0);
            //    re.Refresh();
            //    //ReportDocument re1 = new ReportDocument();
            //    //DataTable d21 = Dprint1();
            //    //a11 = d21;
            //    //re1.Load("../../REPORTS/Dprint1.rpt");
            //    //DataTable ds1 = Dprint();
            //    //a1 = ds1;
            //    //re1.Load("../../REPORTS/DuplicatePrint.rpt");
            //    //re1.SetDataSource(a1);
            //    //re1.Subreports[0].SetDataSource(a11);
            //    //re1.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
            //    //re1.PrintToPrinter(1, false, 0, 0);
            //    //re1.Refresh();
            //    //re.PrintToPrinter(1, false, 0, 0);
            //    //re.Refresh();
            //    clear();
            //    this.NavigationService.Refresh();
            //    cou = 0;
            //}
        }
        public static string itemnamestlid;
        private void No_Click(object sender, RoutedEventArgs e)
        {
            //PrintConfirmation.IsOpen = false;
            //if (error != 0 || quantity.Text == "" || itemrate.Text == "")
            //{
            //    MessageBox.Show("Please Fill All Fields");
            //}
            //else
            //{
            //    pos.bill = Convert.ToInt32(txtbillno.Text);
            //    pos.BILL_Amount = Convert.ToDecimal(txtttl.Text);
            //    pos.BILL_Tax = Convert.ToDecimal(txtgst.Text);
            //    pos.BILL_Total = Convert.ToDecimal(txtgttl.Text);
            //    pos.insertbill();
            //    pos.A = Convert.ToInt32(txtbillno.Text);
            //    for (int i = 0; i < cou; i++)
            //    {
            //        if (i == 0)
            //        {
            //            if (itemname.Text != "" && itemrate.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity.Text); decimal B = A * Convert.ToDecimal(itemrate.Text);
            //                decimal gst = 0; check1 = itemname.Text; itemnamestlid = itemname.Text;DataTable dts0 = pos.getstlid();
            //                DataTable dd = pos.gsttax();stlid = dts0.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity.Focus(); }
            //        }
            //        if (i == 1)
            //        {
            //            if (itemname1.Text != "" && itemrate1.Text != "" && quantity1.Text!= "")
            //            {
            //                int A = Convert.ToInt32(quantity1.Text); decimal B = A * Convert.ToDecimal(itemrate1.Text);
            //                decimal gst = 0; check1 = itemname1.Text; itemnamestlid = itemname1.Text; DataTable dts1 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts1.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname1.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate1.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity1.Focus(); }
            //        }
            //        if (i == 2)
            //        {
            //            if (itemname2.Text != "" && itemrate2.Text != "" && quantity2.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity2.Text); decimal B = A * Convert.ToDecimal(itemrate2.Text);
            //                decimal gst = 0; check1 = itemname2.Text; itemnamestlid = itemname2.Text; DataTable dts2 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts2.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname2.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate2.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity2.Focus(); }
            //        }
            //        if (i == 3)
            //        {

            //            if (itemname3.Text != "" && itemrate3.Text != "" && quantity3.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity3.Text); decimal B = A * Convert.ToDecimal(itemrate3.Text);
            //                decimal gst = 0; check1 = itemname3.Text; itemnamestlid = itemname3.Text; DataTable dts3 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts3.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname3.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate3.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity3.Focus(); }
            //        }
            //        if (i == 4)
            //        {
            //            if (itemname4.Text != "" && itemrate4.Text != "" && quantity4.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity4.Text); decimal B = A * Convert.ToDecimal(itemrate4.Text);
            //                decimal gst = 0; check1 = itemname4.Text; itemnamestlid = itemname4.Text; DataTable dts4 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts4.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname4.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate4.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity4.Focus(); }
            //        }
            //        if (i == 5)
            //        {
            //            if (itemname5.Text != "" && itemrate5.Text != "" && quantity5.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity5.Text); decimal B = A * Convert.ToDecimal(itemrate5.Text);
            //                decimal gst = 0; check1 = itemname5.Text; itemnamestlid = itemname5.Text; DataTable dts5 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts5.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname5.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate5.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity5.Focus(); }
            //        }
            //        if (i == 6)
            //        {
            //            if (itemname6.Text != "" && itemrate6.Text != "" && quantity6.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity6.Text); decimal B = A * Convert.ToDecimal(itemrate6.Text);
            //                decimal gst = 0; check1 = itemname6.Text; itemnamestlid = itemname6.Text; DataTable dts6 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts6.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname6.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate6.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity6.Focus(); }
            //        }
            //        if (i == 7)
            //        {
            //            if (itemname7.Text != "" && itemrate7.Text != "" && quantity7.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity7.Text); decimal B = A * Convert.ToDecimal(itemrate7.Text);
            //                decimal gst = 0; check1 = itemname7.Text; itemnamestlid = itemname7.Text; DataTable dts7 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts7.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname7.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate7.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity7.Focus(); }
            //        }
            //        if (i == 8)
            //        {
            //            if (itemname8.Text != "" && itemrate8.Text != "" && quantity8.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity8.Text); decimal B = A * Convert.ToDecimal(itemrate8.Text);
            //                decimal gst = 0; check1 = itemname8.Text; itemnamestlid = itemname8.Text; DataTable dts8 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts8.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname8.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate8.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity8.Focus(); }
            //        }
            //        if (i == 9)
            //        {
            //            if (itemname9.Text != "" && itemrate9.Text != "" && quantity9.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity9.Text); decimal B = A * Convert.ToDecimal(itemrate9.Text);
            //                decimal gst = 0; check1 = itemname9.Text; itemnamestlid = itemname9.Text; DataTable dts9 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts9.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname9.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate9.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity9.Focus(); }
            //        }
            //        if (i == 10)
            //        {
            //            if (itemname10.Text != "" && itemrate10.Text != "" && quantity10.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity10.Text); decimal B = A * Convert.ToDecimal(itemrate10.Text);
            //                decimal gst = 0; check1 = itemname10.Text; itemnamestlid = itemname10.Text; DataTable dts10 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts10.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname10.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate10.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity10.Focus(); }
            //        }
            //        if (i == 11)
            //        {
            //            if (itemname11.Text != "" && itemrate11.Text != "" && quantity11.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity11.Text); decimal B = A * Convert.ToDecimal(itemrate11.Text);
            //                decimal gst = 0; check1 = itemname11.Text; itemnamestlid = itemname11.Text; DataTable dts11 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts11.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname11.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate11.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity11.Focus(); }
            //        }
            //        if (i == 12)
            //        {
            //            if (itemname12.Text != "" && itemrate12.Text != "" && quantity12.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity12.Text); decimal B = A * Convert.ToDecimal(itemrate12.Text);
            //                decimal gst = 0; check1 = itemname12.Text; itemnamestlid = itemname12.Text; DataTable dts12 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts12.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname12.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate12.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity12.Focus(); }
            //        }
            //        if (i == 13)
            //        {
            //            if (itemname13.Text != "" && itemrate13.Text != "" && quantity13.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity13.Text); decimal B = A * Convert.ToDecimal(itemrate13.Text);
            //                decimal gst = 0; check1 = itemname13.Text; itemnamestlid = itemname13.Text; DataTable dts13 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts13.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname13.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate13.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity13.Focus(); }
            //        }
            //        if (i == 14)
            //        {
            //            if (itemname14.Text != "" && itemrate14.Text != "" && quantity14.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity14.Text); decimal B = A * Convert.ToDecimal(itemrate14.Text);
            //                decimal gst = 0; check1 = itemname14.Text; itemnamestlid = itemname14.Text; DataTable dts14 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts14.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname14.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate14.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity14.Focus(); }
            //        }
            //        if (i == 15)
            //        {
            //            if (itemname15.Text != "" && itemrate15.Text != "" && quantity15.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity15.Text); decimal B = A * Convert.ToDecimal(itemrate15.Text);
            //                decimal gst = 0; check1 = itemname15.Text; itemnamestlid = itemname15.Text; DataTable dts15 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts15.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname15.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate15.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity15.Focus(); }
            //        }
            //        if (i == 16)
            //        {
            //            if (itemname16.Text != "" && itemrate16.Text != "" && quantity16.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity16.Text); decimal B = A * Convert.ToDecimal(itemrate16.Text);
            //                decimal gst = 0; check1 = itemname16.Text; itemnamestlid = itemname16.Text; DataTable dts16 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts16.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname16.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate16.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity16.Focus(); }
            //        }
            //        if (i == 17)
            //        {
            //            if (itemname17.Text != "" && itemrate17.Text != "" && quantity17.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity17.Text); decimal B = A * Convert.ToDecimal(itemrate17.Text);
            //                decimal gst = 0; check1 = itemname17.Text; itemnamestlid = itemname17.Text; DataTable dts17 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts17.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname17.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate17.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity17.Focus(); }
            //        }
            //        if (i == 18)
            //        {
            //            if (itemname18.Text != "" && itemrate18.Text != "" && quantity18.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity18.Text); decimal B = A * Convert.ToDecimal(itemrate18.Text);
            //                decimal gst = 0; check1 = itemname18.Text; itemnamestlid = itemname18.Text; DataTable dts18 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts18.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname18.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate18.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity18.Focus(); }
            //        }
            //        if (i == 19)
            //        {
            //            if (itemname19.Text != "" && itemrate19.Text != "" && quantity19.Text != "")
            //            {
            //                int A = Convert.ToInt32(quantity19.Text); decimal B = A * Convert.ToDecimal(itemrate19.Text);
            //                decimal gst = 0; check1 = itemname19.Text; itemnamestlid = itemname19.Text; DataTable dts19 = pos.getstlid();
            //                DataTable dd = pos.gsttax(); stlid = dts19.Rows[0]["STL_ID"].ToString();
            //                if (dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString(); }
            //                DataTable d = pos.GETTAX(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
            //                pos.BILLITM_Name = itemname19.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate19.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
            //            }
            //            else { quantity19.Focus(); }
            //        }
            //    }

            //    MessageBox.Show("Inserted successfully");
            //    clear();
            //    this.NavigationService.Refresh();
            //    cou = 0;
            //}
        }
        public static string st;
        private void Stalls_DropDownClosed(object sender, EventArgs e)
        {
            WRAPC.Children.Clear();
            ITMNAM.Visibility = Visibility.Hidden;
            WRAPN.Children.Clear();
            ITMCTG.Visibility = Visibility.Visible;
            st = stalls.Text;
            Button();
        }

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
            WRAPC.Children.Clear();
            DataTable DT = pos.itmnames();
            DT.Rows.Clear();
            ITMNAM.Visibility = Visibility.Hidden;
            WRAPN.Children.Clear();
            checkbox_checks = null;
            ITMCTG.Visibility = Visibility.Collapsed;
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
                sp1.Visibility = Visibility.Visible;
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
                sp2.Visibility = Visibility.Visible;
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
                sp3.Visibility = Visibility.Visible;
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
                sp4.Visibility = Visibility.Visible;
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
                sp5.Visibility = Visibility.Visible;
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
                sp6.Visibility = Visibility.Visible;
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
                sp7.Visibility = Visibility.Visible;
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
                sp8.Visibility = Visibility.Visible;
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
                sp9.Visibility = Visibility.Visible;
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
                sp10.Visibility = Visibility.Visible;
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
                sp11.Visibility = Visibility.Visible;
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
                sp12.Visibility = Visibility.Visible;
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
                sp13.Visibility = Visibility.Visible;
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
                sp14.Visibility = Visibility.Visible;
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
                sp15.Visibility = Visibility.Visible;
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
                sp16.Visibility = Visibility.Visible;
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
                sp17.Visibility = Visibility.Visible;
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
                sp18.Visibility = Visibility.Visible;
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
                sp19.Visibility = Visibility.Visible;
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
                sp20.Visibility = Visibility.Visible;
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
                sp1.Visibility = Visibility.Visible;
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
                sp2.Visibility = Visibility.Visible;
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
                sp3.Visibility = Visibility.Visible;
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
                sp4.Visibility = Visibility.Visible;
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
                sp5.Visibility = Visibility.Visible;
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
                sp6.Visibility = Visibility.Visible;
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
                sp7.Visibility = Visibility.Visible;
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
                sp8.Visibility = Visibility.Visible;
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
                sp9.Visibility = Visibility.Visible;
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
                sp10.Visibility = Visibility.Visible;
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
                sp11.Visibility = Visibility.Visible;
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
                sp12.Visibility = Visibility.Visible;
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
                sp13.Visibility = Visibility.Visible;
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
                sp14.Visibility = Visibility.Visible;
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
                sp15.Visibility = Visibility.Visible;
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
                sp16.Visibility = Visibility.Visible;
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
                sp17.Visibility = Visibility.Visible;
                sp16.Visibility = Visibility.Visible; sp17.Visibility = Visibility.Visible; sp18.Visibility = Visibility.Hidden; sp19.Visibility = Visibility.Hidden; sp20.Visibility = Visibility.Hidden;
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
                sp18.Visibility = Visibility.Visible;
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
                sp19.Visibility = Visibility.Visible;
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
                sp20.Visibility = Visibility.Visible;
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
        private void chk_Click(object sender,EventArgs e)
        {
          
            CheckBox chk = (CheckBox)sender;
            CheckBox chk1 = new CheckBox();
            if (chk.IsChecked == true)
            {
                cou++;
                if (cou >= 21)
                {
                    MessageBox.Show("Items should not be greterthen 20 ? please save the items");
                }
                else
                {
                    var S = sender as CheckBox;
                    check1 = S.Content.ToString();
                    checkede();
                    for (int i = 0; i < cou; i++)
                    {
                        if (checkbox_checks == null)
                        {
                            checkbox_checks = new List<string>();
                        }
                        checkbox_checks.Add(S.Content.ToString());
                        checkbox_checks.IndexOf(S.Content.ToString());
                        //s = checkbox_checks.ToString();
                    }
                    COUNT = 0;
                    sa = string.Join(",", checkbox_checks.ToArray());
                }
                
            }
            else
            {
                var C = sender as CheckBox;
                checkbox_checks.Remove(C.Content.ToString());
            }
            if (chk.IsChecked == false)
            {   
                 var g = sender as CheckBox;
                  sri = g.Content.ToString();
                if (sri == itemname.Text)
                {
                    itemname.Text = "";
                    itemrate.Text = "";
                    quantity.Text = "";
                    if (total.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total.Text);
                        decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta1; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); }
                        else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Math.Round(s4, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgst.Text = Math.Round(s5, 2, MidpointRounding.AwayFromZero).ToString();
                        txtgttl.Text = Math.Round((s4+s5), 2, MidpointRounding.AwayFromZero).ToString();
                        total.Text = "0";
                        tot1 = 0;
                    }
                    sp1.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp1);
                    wrap.Children.Add(sp1);
                }
                if (sri == itemname1.Text)
                {
                    itemname1.Text = "";
                    itemrate1.Text = "";
                    quantity1.Text = "";
                    if (total1.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total1.Text);
                        decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta2; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); }
                        else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        total1.Text = "0";
                        tot2 = 0;
                    }
                    sp2.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp2);
                    wrap.Children.Add(sp2);
                }
                if (sri == itemname2.Text)
                {
                    itemname2.Text = "";
                    itemrate2.Text = "";
                    quantity2.Text = "";
                    if (total2.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total2.Text);
                        decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta3; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);

                        total2.Text = "0";
                        tot3 = 0;
                    }
                    sp3.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp3);
                    wrap.Children.Add(sp3);
                }
                if (sri == itemname3.Text)
                {
                    itemname3.Text = "";
                    itemrate3.Text = "";
                    quantity3.Text = "";
                    if (total3.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total3.Text); decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta4; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);

                        total3.Text = "0";
                        tot4 = 0;
                    }
                    sp4.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp4);
                    wrap.Children.Add(sp4);
                }
                if (sri == itemname4.Text)
                {
                    itemname4.Text = "";
                    itemrate4.Text = "";
                    quantity4.Text = "";
                    if (total4.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total4.Text); decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta5; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        tot5 = 0;
                        total4.Text = "0";
                    }
                    sp5.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp5);
                    wrap.Children.Add(sp5);
                }
                if (sri == itemname5.Text)
                {
                    itemname5.Text = "";
                    itemrate5.Text = "";
                    quantity5.Text = "";
                    if (total5.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total5.Text); decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta6; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        tot6 = 0;
                        total5.Text = "0";
                    }
                    sp6.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp6);
                    wrap.Children.Add(sp6);
                }
                if (sri == itemname6.Text)
                {
                    itemname6.Text = "";
                    itemrate6.Text = "";
                    quantity6.Text = "";
                    if (total6.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total6.Text);
                        decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta7; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        tot7 = 0;
                        total6.Text = "0";
                    }
                    sp7.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp7);
                    wrap.Children.Add(sp7);
                }
                if (sri == itemname7.Text)
                {
                   // cou--;
                    itemname7.Text = "";
                    itemrate7.Text = "";
                    quantity7.Text = "";
                    if (total7.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total7.Text);
                        decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta8; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        tot8 = 0;
                        total7.Text = "0";
                    }
                    sp8.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp8);
                    wrap.Children.Add(sp8);
                }
                if (sri == itemname8.Text)
                {
                    //cou--;
                    itemname8.Text = "";
                    itemrate8.Text = "";
                    quantity8.Text = "";
                    if (total8.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total8.Text); decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta9; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        tot9 = 0;
                        total8.Text = "0";
                    }
                    sp9.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp9);
                    wrap.Children.Add(sp9);
                }
                if (sri == itemname9.Text)
                {
                   // cou--;
                    itemname9.Text = "";
                    itemrate9.Text = "";
                    quantity9.Text = "";
                    if (total9.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total9.Text); decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta10; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        total9.Text = "0";
                        tot10 = 0;
                    }
                    sp10.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp10);
                    wrap.Children.Add(sp10);
                }
                if (sri == itemname10.Text)
                {
                    itemname10.Text = "";
                    itemrate10.Text = "";
                    quantity10.Text = "";
                    if (total10.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total10.Text); decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta11; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        tot11 = 0;
                        total10.Text = "0";
                    }
                    sp11.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp11);
                    wrap.Children.Add(sp11);
                }
                if (sri == itemname11.Text)
                {
                    itemname11.Text = "";
                    itemrate11.Text = "";
                    quantity11.Text = "";
                    if (total11.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total11.Text); decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta12; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        tot12 = 0;
                        total11.Text = "0";
                    }
                    sp12.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp12);
                    wrap.Children.Add(sp12);
                }
                if (sri == itemname12.Text)
                {
                   /// cou--;
                    itemname12.Text = "";
                    itemrate12.Text = "";
                    quantity12.Text = "";
                    if (total12.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total12.Text); decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta13; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        tot13= 0;
                        total12.Text = "0";
                    }
                    sp13.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp13);
                    wrap.Children.Add(sp13);
                }
                if (sri == itemname13.Text)
                {
                    //cou--;
                    itemname13.Text = "";
                    itemrate13.Text = "";
                    quantity13.Text = "";
                    if (total13.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total13.Text); decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta14; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        tot14 = 0;
                        total13.Text = "0";
                    }
                    sp14.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp14);
                    wrap.Children.Add(sp14);
                }
                if (sri == itemname14.Text)
                {
                    //cou--;
                    itemname14.Text = "";
                    itemrate14.Text = "";
                    quantity14.Text = "";
                    if (total14.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total14.Text); decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta15; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        tot15 = 0;
                        total14.Text = "0";
                    }
                    sp15.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp15);
                    wrap.Children.Add(sp15);
                }
                if (sri == itemname15.Text)
                {
                   /// cou--;
                    itemname15.Text = "";
                    itemrate15.Text = "";
                    quantity15.Text = "";
                    if (total15.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total15.Text); decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta16; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        tot16= 0;
                        total15.Text = "0";
                    }
                    sp16.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp16);
                    wrap.Children.Add(sp16);
                }
                if (sri == itemname16.Text)
                {
                 //   cou--;
                    itemname16.Text = "";
                    itemrate16.Text = "";
                    quantity16.Text = "";
                    if (total16.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total16.Text);
                        decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta17; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        tot17 = 0;
                        total16.Text = "0";
                    }
                    sp17.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp17);
                    wrap.Children.Add(sp17);
                }
                if (sri == itemname17.Text)
                {
                  //  cou--;
                    itemname17.Text = "";
                    itemrate17.Text = "";
                    quantity17.Text = "";
                    if (total17.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total7.Text);
                        decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta18; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        total17.Text = "0"; tot18 = 0;
                    }
                    sp18.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp18);
                    wrap.Children.Add(sp18);
                }
                if (sri == itemname18.Text)
                {
                   // cou--;
                    itemname18.Text = "";
                    itemrate18.Text = "";
                    quantity18.Text = "";
                    if (total18.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total18.Text);
                        decimal s2; 
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta19; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        tot19 = 0;
                        total18.Text = "0";
                    }
                    sp19.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp19);
                    wrap.Children.Add(sp19);
                }
                if (sri == itemname19.Text)
                {
                  //  cou--;
                    itemname19.Text = "";
                    itemrate19.Text = "";
                    quantity19.Text = "";
                    if (total19.Text == "") { }
                    else
                    {
                        decimal s1 = Convert.ToDecimal(total19.Text); decimal s2;
                        decimal s5;
                        if (txtgst.Text == "" || txtgst.Text == "0.00")
                        {
                            s2 = decimal.Parse("0.00"); s5 = decimal.Parse("0.00");
                        }
                        else
                        { s2 = Convert.ToDecimal(txtgst.Text); s5 = s2 - ta20; }
                        decimal s3;
                        decimal s4;
                        if (txtttl.Text == "" || txtttl.Text == "0.00") { s3 = decimal.Parse("0.00"); s4 = decimal.Parse("0.00"); } else { s3 = Convert.ToDecimal(txtttl.Text); s4 = s3 - s1; }
                        txtttl.Text = Convert.ToString(s4);
                        txtgst.Text = Convert.ToString(s5);
                        txtgttl.Text = Convert.ToString(s4 + s5);
                        tot20 = 0;
                        total19.Text = "0";
                 }
                    sp20.Visibility = Visibility.Hidden;
                    wrap.Children.Remove(sp20);
                    wrap.Children.Add(sp20);
                }
            }
        }

        public decimal ta1, ta2, ta3, ta4, ta5, ta6, ta7, ta8, ta9, ta10, ta11, ta12, ta13, ta14, ta15, ta16, ta17, ta18, ta19, ta20;
        public decimal tot1, tot2, tot3, tot4, tot5, tot6, tot7, tot8, tot9, tot10, tot11, tot12, tot13, tot14, tot15, tot16, tot17, tot18, tot19, tot20;
        public decimal totbill, billtax, gtotbill;
        private void quantity_LostFocus(object sender, RoutedEventArgs e)
        {
            txtttl.Text = "";
            txtgst.Text = "";
            txtgttl.Text = "";
            if (rate1 !=0)
            {
                if(quantity.Text == "")
                {
                }
                else if(num.IsMatch(quantity.Text))
                {
                    total.Text = "";
                    check1 = itemname.Text;
                    DataTable dd = pos.gsttax();
                    if(dd.Rows.Count == 0) { } else { check2 = dd.Rows[0]["NAM_Tax"].ToString();  }                    
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
                        if(gst == 0)
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
                    decimal a =tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b =ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
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
                    decimal a =tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b =ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
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
        }
        public static string check2;
        private void quantity1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (rate2 != 0)
            {
                if (quantity1.Text == "")
                {
                }
                else if(num.IsMatch(quantity1.Text))
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
                else if(num.IsMatch(quantity.Text))
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
                    decimal a =tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b =ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
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
        }
        private void quantity2_LostFocus(object sender, RoutedEventArgs e)
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
        }
        private void quantity3_LostFocus(object sender, RoutedEventArgs e)
        {
            txtttl.Text = "";
            txtgst.Text = "";
            txtgttl.Text = "";
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
                    decimal b =ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
                    gtotbill =a + b; rate4 = 0;
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
                    decimal a =tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b =ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
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
        }
        private void quantity4_LostFocus(object sender, RoutedEventArgs e)
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
                    decimal a =tot1 + tot2 + tot3 + tot4 + tot5 + tot6 + tot7 + tot8 + tot9 + tot10 + tot11 + tot12 + tot13 + tot14 + tot15 + tot16 + tot17 + tot18 + tot19 + tot20;
                    decimal b =ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
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
        }
        private void quantity5_LostFocus(object sender, RoutedEventArgs e)
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
                    billtax =  ta1 + ta2 + ta3 + ta4 + ta5 + ta6 + ta7 + ta8 + ta9 + ta10 + ta11 + ta12 + ta13 + ta14 + ta15 + ta16 + ta17 + ta18 + ta19 + ta20;
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
        }

        private void quantity6_LostFocus(object sender, RoutedEventArgs e)
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
        }
        private void quantity7_LostFocus(object sender, RoutedEventArgs e)
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
        }

        private void quantity8_LostFocus(object sender, RoutedEventArgs e)
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
        }

        private void quantity9_LostFocus(object sender, RoutedEventArgs e)
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
        }

        private void quantity10_LostFocus(object sender, RoutedEventArgs e)
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
                    {}
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