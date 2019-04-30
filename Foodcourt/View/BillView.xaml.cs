using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using Foodcourt.Model;
using CrystalDecisions.CrystalReports.Engine;
using System.Text.RegularExpressions;

namespace Foodcourt.View.Oprs
{
    /// <summary>
    /// Interaction logic for BillView.xaml
    /// </summary>
    public partial class BillView : Page
    {
        BillVIEW BV = new BillVIEW();
        public static int A;
        public int value;
        Regex num = new Regex(@"^[0-9]+$");
        public DataTable dt;
        public BillView()
        {
            InitializeComponent();
            bill_date.DisplayDateEnd = DateTime.Today.Date;
            //DataTable DT = BV.GETBILL();
            //dgBill.ItemsSource = DT.DefaultView;
        }
        public static decimal BILL_Tax, CGST, SGST, BILITM_Tax, ta1;
        public static int BILL_Id,  BILL_Discount;
        public static decimal BILL_Amount, BILL_Total, BILITM_Rate, BILLITM_Quanty, Total, sum1 = 0;
        private void Micancel_Click(object sender, RoutedEventArgs e)
        {
            A = Convert.ToInt32(txtbillno.Text);
            BV.billcancel();
            MessageBox.Show("Bill Cancelled");
            sum1 = 0;
            txtnetamount.Text = "0";
            txtcgst.Text = "0";
            txtsgst.Text = "0";
            txtgttl.Text = "0";
            this.NavigationService.Refresh();
            Dtlview.Visibility = Visibility.Hidden;
            ReportDocument re = new ReportDocument();
            DataTable d1 = Cprint1();
            a = d1;
            re.Load("../../REPORTS/Dprint1.rpt");
            DataTable ds = Cprint();
            a1 = ds;
            re.Load("../../REPORTS/Cancelbill.rpt");
            re.SetDataSource(a1);
            re.Subreports[0].SetDataSource(a);
            re.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
            re.PrintToPrinter(1, false, 0, 0);
            re.Refresh();
        }
        public static string billid, BILITM_Name;
        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Refresh();
            canview.Visibility = Visibility.Hidden;
        }
        private void btnbillnoS_Click(object sender, RoutedEventArgs e)
        {
            if (billwiseSearch.Text == "")
            {
                MessageBox.Show("Please Enter Bill Number and search.!");
            }
            else
            {
                string ID = billwiseSearch.Text;
                if (num.IsMatch(ID))
                {
                    if (dt != null)
                    {
                        dt.Clear();
                    }
                    BV.SelectedBillNo = billwiseSearch.Text;
                    dt = BV.GetBillWithBillNo();
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No Bill Found.!");
                    }
                    else
                    {
                        dgBill.ItemsSource = dt.DefaultView;
                    }
                    value = 0;
                    bill_date.Text = "";
                }
                else
                {
                    MessageBox.Show("Please enter Numeric values");
                    billwiseSearch.Text = "";
                    bill_date.Text = "";
                }
            }
        }
        private void btnsearch_Click(object sender, RoutedEventArgs e)
        {
            if (bill_date.Text == "")
            {
                MessageBox.Show("Please select date and search");
            }
            else
            {
                try
                {
                    if (dt != null)
                    {
                        dt.Clear();
                    }
                    BV.SelectedDate = Convert.ToDateTime(bill_date.Text).Date;
                    dt = BV.GetBillWithDate();
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No Bills Found.!");
                    }
                    else
                    {
                        dgBill.ItemsSource = dt.DefaultView;
                    }
                    value = 1;
                    billwiseSearch.Text = "";
                }
                catch (Exception)
                {
                }
            }
        }
        public static int B_bill_no;
        public Decimal B_Tax, B_Total, B_GTotal;
        private void dgBill_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int i = dgBill.SelectedIndex;
            DataTable dt1;
            if (i >= 0)
            {
                if (value == 0)
                {
                    dt1 = BV.GetBillWithBillNo();
                }
                else
                {
                    dt1 = BV.GetBillWithDate();
                }
                Dtlview.Visibility = Visibility.Visible;
                B_bill_no = Convert.ToInt32(dt1.Rows[i]["BILL_Id"]);
                B_Total = Convert.ToDecimal(dt1.Rows[i]["BILL_Amount"]);
                B_GTotal = Convert.ToDecimal(dt1.Rows[i]["BILL_Total"]);
                B_Tax = Convert.ToDecimal(dt1.Rows[i]["BILL_Tax"]);
                txtbillno.Text = B_bill_no.ToString();
                txtbilldate.Text = Convert.ToDateTime(dt1.Rows[i]["BILL_InsertDate"]).ToShortDateString();
                txtnetamount.Text = Math.Round(B_Total, 2, MidpointRounding.AwayFromZero).ToString();
                txtcgst.Text = Math.Round(B_Tax/2, 2, MidpointRounding.AwayFromZero).ToString();
                txtsgst.Text =Math.Round(B_Tax/2, 2, MidpointRounding.AwayFromZero).ToString();
                txtgttl.Text = Math.Round(B_GTotal, 2, MidpointRounding.AwayFromZero).ToString();
                DataTable db = BV.GETITMNAM();
                DataTable DD1 = new DataTable();
                DD1.Columns.Add("BILITM_Name", typeof(string));
                DD1.Columns.Add("BILLITM_Quanty", typeof(int));
                DD1.Columns.Add("BILITM_Rate", typeof(decimal));
                DD1.Columns.Add("BILITM_Tax", typeof(decimal));
                DD1.Columns.Add("Total", typeof(decimal));
                for (int j = 0; j < db.Rows.Count; j++)
                {
                    BILLITM_Quanty = Convert.ToDecimal(db.Rows[j]["BILLITM_Quanty"]);
                    BILITM_Rate = Convert.ToDecimal(db.Rows[j]["BILITM_Rate"]);
                    BILITM_Tax = Convert.ToDecimal(db.Rows[j]["BILITM_Tax"]);
                    Total = (BILITM_Rate + BILITM_Tax) * BILLITM_Quanty;
                    DataRow ROW = DD1.NewRow();
                    ROW["BILITM_Name"] = db.Rows[j]["BILITM_Name"];
                    ROW["BILLITM_Quanty"] = db.Rows[j]["BILLITM_Quanty"];
                    ROW["BILITM_Rate"] = db.Rows[j]["BILITM_Rate"];
                    ROW["BILITM_Tax"] = db.Rows[j]["BILITM_Tax"];
                    ROW["Total"] = Total;
                    DD1.Rows.Add(ROW);
                }
                dgdtlview.ItemsSource = DD1.DefaultView;
            }
            else
            {
                Dtlview.Visibility = Visibility.Hidden;
            }
        }
        public void Clear()
        {
            txtbillno.Text = "";
            txtbilldate.Text = "";
            txtnetamount.Text = "";
            txtcgst.Text = "";
            txtsgst.Text = "";
            txtgttl.Text = "";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            canview.Visibility = Visibility.Visible;
            DataTable DT = BV.cancel();
            DataTable dt = new DataTable();
            dt.Columns.Add("BILL_Id", typeof(int));
            dt.Columns.Add("BILL_Amount", typeof(decimal));
            dt.Columns.Add("CGST", typeof(decimal));
            dt.Columns.Add("SGST", typeof(decimal));
            dt.Columns.Add("BILL_Discount", typeof(int));
            dt.Columns.Add("BILL_Total", typeof(decimal));
            //dt.Columns.Add("BILL_InsertDate", typeof(DateTime));
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                BILL_Id = Convert.ToInt32(DT.Rows[i]["BILL_Id"]);
                BILL_Amount = Convert.ToDecimal(DT.Rows[i]["BILL_Amount"]);
                BILL_Tax = Convert.ToDecimal(DT.Rows[i]["BILL_Tax"]);
                CGST = BILL_Tax / 2;
               // SGST = BILL_Tax / 2;
                BILL_Discount = Convert.ToInt32(DT.Rows[i]["BILL_Discount"]);
                BILL_Total = Convert.ToDecimal(DT.Rows[i]["BILL_Total"]);
               // BILL_InsertDate = Convert.ToDateTime((DT.Rows[i]["BILL_InsertDate"])).ToShortDateString();
                DataRow row = dt.NewRow();
                row["BILL_Id"] = DT.Rows[i]["BILL_Id"];
                row["BILL_Amount"] = DT.Rows[i]["BILL_Amount"];
                row["BILL_Discount"] = DT.Rows[i]["BILL_Discount"];
                row["BILL_Total"] = DT.Rows[i]["BILL_Total"];
                row["CGST"] = CGST;
                row["SGST"] = CGST;
                //       row["BILL_InsertDate"] = BILL_InsertDate;
                dt.Rows.Add(row);
            }
            dgcancel.ItemsSource = dt.DefaultView;
            //DataTable DD1 = new DataTable();
            //DD1.Columns.Add("BILITM_Name", typeof(string));
            //DD1.Columns.Add("BILLITM_Quanty", typeof(int));
            //DD1.Columns.Add("BILITM_Rate", typeof(int));
            //DD1.Columns.Add("BILITM_Tax", typeof(int));
            //DD1.Columns.Add("Total", typeof(int));
            //for (int j = 0; j < dt.Rows.Count; j++)
            //{
            //    BILITM_Name = dt.Rows[j]["BILITM_Name"].ToString();
            //    BILLITM_Quanty = Convert.ToInt32(dt.Rows[j]["BILLITM_Quanty"]);
            //    BILITM_Rate = Convert.ToInt32(dt.Rows[j]["BILITM_Rate"]);
            //    BILITM_Tax = Convert.ToInt32(dt.Rows[j]["BILITM_Tax"]);
            //    CGST1 = (BILITM_Tax / 2) / 100;
            //    SGST1 = (BILITM_Tax / 2) / 100;
            //    Total = (BILLITM_Quanty * BILITM_Rate);
            //    DataRow ROW = DD1.NewRow();
            //    ROW["BILITM_Name"] = dt.Rows[j]["BILITM_Name"];
            //    ROW["BILLITM_Quanty"] = dt.Rows[j]["BILLITM_Quanty"];
            //    ROW["BILITM_Rate"] = dt.Rows[j]["BILITM_Rate"];
            //    ROW["Total"] = Total;
            //    DD1.Rows.Add(ROW);
            //    Total1 = Total;
            //    sum1 = sum1 + Total1;
            //}
            //dgcancel.ItemsSource = DD1.DefaultView;
            //netamounttxt.Text = sum1.ToString();
            //decimal gst = Convert.ToDecimal(BILITM_Tax);
            //if (gst == 0)
            //{
            //    ta1 = 0;
            //}
            //else
            //{
            //    ta1 = sum1 * gst / 100;
            //}
            //cgsttxt.Text = (ta1 / 2).ToString();
            //sgsttxt.Text = (ta1 / 2).ToString();
            //gttltxt.Text = Convert.ToString(ta1 + sum1);  
            //dgcancel.ItemsSource = dt.DefaultView;
        }
        public static string BILL_InsertDate;
        public void TAX()
        {
            //DataTable DT = BV.GETBILL();
            //DataTable dt = new DataTable();
            //dt.Columns.Add("BILL_Id", typeof(int));
            //dt.Columns.Add("BILL_Amount", typeof(decimal));
            //dt.Columns.Add("CGST", typeof(decimal));
            //dt.Columns.Add("SGST", typeof(decimal));
            //dt.Columns.Add("BILL_Discount", typeof(int));
            //dt.Columns.Add("BILL_Total", typeof(decimal));
            //dt.Columns.Add("BILL_InsertDate", typeof(DateTime));
            //for (int i = 0; i < DT.Rows.Count; i++)
            //{
            //    BILL_Id = Convert.ToInt32(DT.Rows[i]["BILL_Id"]);
            //    BILL_Amount = Convert.ToDecimal(DT.Rows[i]["BILL_Amount"]);
            //    BILL_Tax = Convert.ToDecimal(DT.Rows[i]["BILL_Tax"]);
            //    CGST = BILL_Tax / 2;
            //    SGST = BILL_Tax / 2;
            //    BILL_Discount = Convert.ToInt32(DT.Rows[i]["BILL_Discount"]);
            //    BILL_Total = Convert.ToDecimal(DT.Rows[i]["BILL_Total"]);
            //    BILL_InsertDate = Convert.ToDateTime((DT.Rows[i]["BILL_InsertDate"])).ToShortDateString();
            //    DataRow row = dt.NewRow();
            //    row["BILL_Id"] = DT.Rows[i]["BILL_Id"];
            //    row["BILL_Amount"] = DT.Rows[i]["BILL_Amount"];
            //    row["BILL_Discount"] = DT.Rows[i]["BILL_Discount"];
            //    row["BILL_Total"] = DT.Rows[i]["BILL_Total"];
            //    row["CGST"] = CGST;
            //    row["SGST"] = SGST;
            //    row["BILL_InsertDate"] = BILL_InsertDate;
            //    dt.Rows.Add(row);
            //}
            //dgBill.ItemsSource = dt.DefaultView;
        }
        private void btncls_Click(object sender, RoutedEventArgs e)
        {
            Dtlview.Visibility = Visibility.Hidden;
        }
        private void MIclose_Click(object sender, RoutedEventArgs e)
        {
            sum1 = 0;
            txtnetamount.Text = "0";
            txtcgst.Text = "0";
            txtsgst.Text = "0";
            txtgttl.Text = "0";
            this.NavigationService.Refresh();
            Dtlview.Visibility = Visibility.Hidden;
        }
        public DataTable Cprint1()
        {
            DataTable dd = new DataTable();
            dd.Columns.Add("Item", typeof(string));
            dd.Columns.Add("Rate", typeof(decimal));
            dd.Columns.Add("Quantity", typeof(int));
            dd.Columns.Add("Amount", typeof(decimal));
            DataTable DD = BV.items();
            for (int i = 0; i < DD.Rows.Count; i++)
            {
                DataRow r = dd.NewRow();
                r["Item"] = DD.Rows[i]["BILITM_Name"].ToString();
                r["Rate"] = DD.Rows[i]["BILITM_Rate"].ToString();
                r["Quantity"] = DD.Rows[i]["BILLITM_Quanty"].ToString();
                decimal d = Convert.ToDecimal(DD.Rows[i]["BILITM_Rate"].ToString());
                int s = Convert.ToInt32(DD.Rows[i]["BILLITM_Quanty"].ToString());
                r["Amount"] = d * s;
                dd.Rows.Add(r);
            }
            return dd;
        }
        public DataTable Cprint()
        {
            DataTable d = new DataTable();
            d.Columns.Add("Name", typeof(string));
            d.Columns.Add("Address", typeof(string));
            d.Columns.Add("GstNo", typeof(string));
            d.Columns.Add("BillNo", typeof(string));
            d.Columns.Add("BillDate", typeof(DateTime));
            d.Columns.Add("Total", typeof(decimal));
            d.Columns.Add("Cgst", typeof(decimal));
            d.Columns.Add("Sgst", typeof(decimal));
            d.Columns.Add("GrandTotal", typeof(decimal));
            DataRow row = d.NewRow();
            DataTable adres = BV.Address();
            row["Name"] = adres.Rows[0]["PRPT_Name"].ToString();
            row["Address"] = adres.Rows[0]["PRPT_Address"].ToString();
            row["GstNo"] = adres.Rows[0]["PRPT_GST"].ToString();
            row["BillNo"] = txtbillno.Text;
            row["BillDate"] = txtbilldate.Text;
            DataTable dt2 = BV.itemstot();
            row["Total"] = dt2.Rows[0]["BILL_Amount"].ToString();
            decimal tax = Convert.ToDecimal(dt2.Rows[0]["BILL_Tax"].ToString());
            row["Cgst"] = tax / 2;
            row["Sgst"] = tax / 2;
            row["GrandTotal"] = dt2.Rows[0]["BILL_Total"].ToString();
            
            d.Rows.Add(row);
            return d;
        }
        public DataTable Dprint()
        {
            DataTable d = new DataTable();
            d.Columns.Add("Name", typeof(string));
            d.Columns.Add("Address", typeof(string));
            d.Columns.Add("GstNo", typeof(string));
            d.Columns.Add("BillNo", typeof(string));
            d.Columns.Add("BillDate", typeof(DateTime));
            d.Columns.Add("Total", typeof(decimal));
            d.Columns.Add("Cgst", typeof(decimal));
            d.Columns.Add("Sgst", typeof(decimal));
            d.Columns.Add("GrandTotal", typeof(decimal));
            DataRow row = d.NewRow();
            DataTable adres = BV.Address();
            row["Name"] = adres.Rows[0]["PRPT_Name"].ToString();
            row["Address"] = adres.Rows[0]["PRPT_Address"].ToString();
            row["GstNo"] = adres.Rows[0]["PRPT_GST"].ToString();
            row["BillNo"] = txtbillno.Text;
            row["BillDate"] = txtbilldate.Text;
            row["Total"] = txtnetamount.Text;
            row["Cgst"] = txtcgst.Text;
            row["Sgst"] = txtsgst.Text;
            row["GrandTotal"] = txtgttl.Text;
            d.Rows.Add(row);
            return d;
        }
        public DataTable Dprint1()
        {
            DataTable dd = new DataTable();
            dd.Columns.Add("Item", typeof(string));
            dd.Columns.Add("Rate", typeof(decimal));
            dd.Columns.Add("Quantity", typeof(int));
            dd.Columns.Add("Amount", typeof(decimal));
            dd.Columns.Add("Tax", typeof(decimal));
            DataTable DD = BV.items();
            for (int i = 0; i < DD.Rows.Count; i++)
            {
                DataRow r = dd.NewRow();
                r["Item"] = DD.Rows[i]["BILITM_Name"].ToString();
                r["Rate"] = DD.Rows[i]["BILITM_Rate"].ToString();
                r["Quantity"] = DD.Rows[i]["BILLITM_Quanty"].ToString();
                r["Tax"] = DD.Rows[i]["BILITM_Tax"].ToString();
                decimal d = Convert.ToDecimal(DD.Rows[i]["BILITM_Rate"].ToString());
                decimal t = Convert.ToDecimal(DD.Rows[i]["BILITM_Tax"].ToString());
                int s = Convert.ToInt32(DD.Rows[i]["BILLITM_Quanty"].ToString());
                r["Amount"] = Math.Round((d + t)*s,2,MidpointRounding.AwayFromZero);
                dd.Rows.Add(r);
            }
            return dd;
        }
        public static DataTable a, a1;
        private void Miprint_Click(object sender, RoutedEventArgs e)
        {
            A = Convert.ToInt32(txtbillno.Text);
            ReportDocument re = new ReportDocument();
            DataTable d1 = Dprint1();
            a = d1;
            re.Load("../../REPORTS/Dprint1.rpt");
            DataTable ds = Dprint();
            a1 = ds;
            re.Load("../../REPORTS/DuplicatePrint.rpt");
            re.SetDataSource(a1);
            re.Subreports[0].SetDataSource(a);
            re.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
            re.PrintToPrinter(1, false, 0, 0);
            re.Refresh();
        }
    }
}
