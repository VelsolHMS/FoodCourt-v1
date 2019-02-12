using CrystalDecisions.CrystalReports.Engine;
using Foodcourt.Model;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for Daywiserpt.xaml
    /// </summary>
    public partial class Daywiserpt : Page
    {
        reportClass1 r = new reportClass1();
        public Daywiserpt()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            DWSRPT.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            r.dateday = dt.Text;
            ReportDocument re = new ReportDocument();
            DataTable dd1 = report1();
            re.Load("../../View/DatewiseReport.rpt");
            DataTable dd = report();
            re.Load("../../View/DatewiseReport.rpt");
            re.SetDataSource(dd);
            re.Subreports[0].SetDataSource(dd1);
            CrystalReportViewer1.Visibility = Visibility.Visible;
            DWSRPT.Visibility = Visibility.Hidden;
            CrystalReportViewer1.ShowRefreshButton = false;
            re.Refresh();
            CrystalReportViewer1.ViewerCore.ReportSource = re;
        }
        public int a, b, c;
        public DataTable report()
        {
            DataTable d = new DataTable();
            d.Columns.Add("BillNo", typeof(int));
            d.Columns.Add("NetAmount", typeof(decimal));
            d.Columns.Add("CGST", typeof(decimal));
            d.Columns.Add("SGST", typeof(decimal));
            d.Columns.Add("GRANDTOTAL", typeof(decimal));
            d.Columns.Add("USER", typeof(string));
            d.Columns.Add("TOTALNETAMOUNT", typeof(decimal));
            d.Columns.Add("TOTALCGST", typeof(decimal));
            d.Columns.Add("TOTALSGST", typeof(decimal));
            d.Columns.Add("GRANDTOTALL", typeof(decimal));
            d.Columns.Add("Name", typeof(string));
            d.Columns.Add("Address", typeof(string));
            d.Columns.Add("Gst", typeof(string));
            DataTable dd = r.daywise();
            DataRow row = d.NewRow();
            DataTable d1 = r.Address();
            row["Name"] = d1.Rows[0]["PRPT_Name"].ToString();
            row["Address"] = d1.Rows[0]["PRPT_Address"].ToString();
            row["Gst"] = d1.Rows[0]["PRPT_GST"].ToString();
            for (int i = 0; i < dd.Rows.Count; i++)
            {
                
                row["BillNo"] = dd.Rows[i]["BILL_Id"].ToString();
                row["NetAmount"] = dd.Rows[i]["BILL_Amount"].ToString();
                a=int.Parse(dd.Rows[i]["BILL_Tax"].ToString());
                row["CGST"] = a / 2;
                row["SGST"] = a / 2;
                row["GRANDTOTAL"] = dd.Rows[i]["BILL_Total"].ToString();
                row["USER"] = dd.Rows[i]["BILL_InsertBy"].ToString();
            }
            DataTable ddd = r.daywise1();
            if (ddd.Rows[0]["AMOUNT"].ToString() == "" || ddd.Rows[0]["AMOUNT"].ToString() == "0")
            {
                row["TOTALNETAMOUNT"] = 0.00;
            }
            else
            {
                row["TOTALNETAMOUNT"] = ddd.Rows[0]["AMOUNT"].ToString();
                c = int.Parse(ddd.Rows[0]["AMOUNT"].ToString());
                b = int.Parse(ddd.Rows[0]["TAX"].ToString());
                row["TOTALCGST"] = b / 2;
                row["TOTALSGST"] = b / 2;
                row["GRANDTOTALL"] = b + c;
                d.Rows.Add(row);
            }
                return d;
        }
        public DataTable report1()
        {
            DataTable d = new DataTable();
            d.Columns.Add("BillNo", typeof(int));
            d.Columns.Add("NetAmount", typeof(decimal));
            d.Columns.Add("CGST", typeof(decimal));
            d.Columns.Add("SGST", typeof(decimal));
            d.Columns.Add("GRANDTOTAL", typeof(decimal));
            d.Columns.Add("USER", typeof(string));
            d.Columns.Add("TOTALNETAMOUNT", typeof(decimal));
            d.Columns.Add("TOTALCGST", typeof(decimal));
            d.Columns.Add("TOTALSGST", typeof(decimal));
            d.Columns.Add("GRANDTOTALL", typeof(decimal));
            d.Columns.Add("Name", typeof(string));
            d.Columns.Add("Address", typeof(string));
            d.Columns.Add("Gst", typeof(string));
            DataTable dd = r.daywise();
          
            //DataTable d1 = r.Address();
            //row["Name"] = d1.Rows[0]["PRPT_Name"].ToString();
            //row["Address"] = d1.Rows[0]["PRPT_Address"].ToString();
            //row["Gst"] = d1.Rows[0]["PRPT_GST"].ToString();
            for (int i = 0; i < dd.Rows.Count; i++)
            {
                DataRow row = d.NewRow();
                row["BillNo"] = dd.Rows[i]["BILL_Id"].ToString();
                row["NetAmount"] = dd.Rows[i]["BILL_Amount"].ToString();
                a = int.Parse(dd.Rows[i]["BILL_Tax"].ToString());
                row["CGST"] = a / 2;
                row["SGST"] = a / 2;
                row["GRANDTOTAL"] = dd.Rows[i]["BILL_Total"].ToString();
                row["USER"] = dd.Rows[i]["BILL_InsertBy"].ToString();
                d.Rows.Add(row);
            }
            //DataTable ddd = r.daywise1();
            //if (ddd.Rows[0]["AMOUNT"].ToString() == "" || ddd.Rows[0]["AMOUNT"].ToString() == "0")
            //{
            //    row["TOTALNETAMOUNT"] = 0.00;
            //}
            //else
            //{
            //    row["TOTALNETAMOUNT"] = ddd.Rows[0]["AMOUNT"].ToString();
            //    c = int.Parse(ddd.Rows[0]["AMOUNT"].ToString());
            //    b = int.Parse(ddd.Rows[0]["TAX"].ToString());
            //    row["TOTALCGST"] = b / 2;
            //    row["TOTALSGST"] = b / 2;
            //    row["GRANDTOTALL"] = b + c;
             
            return d;
        }
    }
}
