using CrystalDecisions.CrystalReports.Engine;
using Foodcourt.Model;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for taxrpt.xaml
    /// </summary>
    public partial class taxrpt : Page
    {
        reportClass1 r = new reportClass1();
        public taxrpt()
        {
            InitializeComponent();
        }
        public static string date, date1;
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            taxRPT.Visibility = Visibility.Hidden;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            date = dt.Text;
            date1 = dt1.Text;
            ReportDocument re = new ReportDocument();
            DataTable d = report1();
            re.Load("../../View/TaxReport1.rpt");
            DataTable dd = report();
            re.Load("../../View/TaxReport.rpt");
            re.SetDataSource(dd);
            re.Subreports[0].SetDataSource(d);
            //CrystalReportViewer1.Visibility = Visibility.Visible;
            //taxRPT.Visibility = Visibility.Hidden;
            //CrystalReportViewer1.ShowRefreshButton = false;
            re.PrintToPrinter(1, false, 0, 0);
            re.Refresh();
            //CrystalReportViewer1.ViewerCore.ReportSource = re;
        }
        public DataTable report()
        {
            DataTable D = new DataTable();
            D.Columns.Add("Fromdate", typeof(DateTime));
            D.Columns.Add("Todate", typeof(DateTime));
            D.Columns.Add("Date", typeof(DateTime));
            D.Columns.Add("Cgst", typeof(decimal));
            D.Columns.Add("Sgst", typeof(decimal));
            D.Columns.Add("Total", typeof(decimal));
            D.Columns.Add("CgstTotal", typeof(decimal));
            D.Columns.Add("SgstTotal", typeof(decimal));
            D.Columns.Add("GrandTotal", typeof(decimal));
            D.Columns.Add("Name", typeof(string));
            D.Columns.Add("Address", typeof(string));
            D.Columns.Add("Gst", typeof(string));
            DataRow row = D.NewRow();
            DataTable s = r.Address();
            row["Name"] = s.Rows[0]["PRPT_Name"].ToString();
            row["Address"] = s.Rows[0]["PRPT_Address"].ToString();
            row["Gst"] = s.Rows[0]["PRPT_GST"].ToString();
            row["Fromdate"] = date;
            row["Todate"] = date1;
            //DataTable d = r.TAX();
            //for (int i = 0; i < d.Rows.Count; i++)
            //{
            //    int kk;
            //    row["Date"] = d.Rows[i]["BILL_InsertDate"].ToString();
            //    kk =int.Parse(d.Rows[i]["tax"].ToString());
            //    row["Cgst"] = kk / 2;
            //    row["Sgst"] = kk / 2;
            //    row["Total"] = kk;
            //}
            DataTable k = r.TAX1();
            if (k.Rows[0]["TOTALTAX"].ToString() == "" || k.Rows[0]["TOTALTAX"].ToString() == "0") { }
            else
            {
                decimal f = Convert.ToDecimal(k.Rows[0]["TOTALTAX"].ToString());
                row["CgstTotal"] = f / 2;
                row["SgstTotal"] = f / 2;
                row["GrandTotal"] = f;
                D.Rows.Add(row);
            }
            return D;
        }
        public DataTable report1()
        {
            DataTable D = new DataTable();
            D.Columns.Add("Date", typeof(DateTime));
            D.Columns.Add("Cgst", typeof(decimal));
            D.Columns.Add("Sgst", typeof(decimal));
            D.Columns.Add("Total", typeof(decimal));
            D.Columns.Add("CgstTotal", typeof(decimal));
            D.Columns.Add("SgstTotal", typeof(decimal));
            D.Columns.Add("GrandTotal", typeof(decimal));
            D.Columns.Add("Name", typeof(string));
            D.Columns.Add("Address", typeof(string));
            D.Columns.Add("Gst", typeof(string));
           
            DataTable s = r.Address();
            DataTable d = r.TAX();
            for (int i = 0; i < d.Rows.Count; i++)
            {
                DataRow row = D.NewRow();
                decimal kk;
                row["Date"] = d.Rows[i]["BILL_InsertDate"].ToString();
                kk =Convert.ToDecimal(d.Rows[i]["tax"].ToString());
                row["Cgst"] = kk / 2;
                row["Sgst"] = kk / 2;
                row["Total"] = kk;
                D.Rows.Add(row);
            }
            return D;
        }
    }
}
