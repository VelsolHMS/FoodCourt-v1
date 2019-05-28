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
    /// Interaction logic for billwiserpt.xaml
    /// </summary>
    public partial class billwiserpt : Page
    {
        reportClass1 r = new reportClass1();
        public  billwiserpt()
        {
            InitializeComponent();
        }
        
        public static string DA;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DA = dt.Text;
            //DWSRPT.Visibility = Visibility.Hidden;
            ReportDocument re = new ReportDocument();
            DataTable dd1 = report1();
            re.Load("../../View/BillWiseReport11.rpt");
            DataTable dd = report();
            re.Load("../../View/BillWiseReport1.rpt");
            re.SetDataSource(dd);
            re.Subreports[0].SetDataSource(dd1);
            re.PrintToPrinter(1, false, 0, 0);
            //CrystalReportViewer1.Visibility = Visibility.Visible;
           // DWSRPT.Visibility = Visibility.Hidden;
            //CrystalReportViewer1.ShowRefreshButton = false;
            re.Refresh();
            //CrystalReportViewer1.ViewerCore.ReportSource = re;

            //r.SetDataSource(d);
            //r.Subreports[0].SetDataSource(dd);
            //r.PrintToPrinter(1, false, 0, 0);
            //r.Refresh();
            //ReportDocument re = new ReportDocument();
            //DataTable dd = reportTA();
            //re.Load("../../REPORTS/TaxReport1.rpt");
            //re.SetDataSource(dd);
            //re.PrintToPrinter(1, false, 0, 0);
            //re.Refresh();
        }
        public DataTable report()
        {
            DataTable d = new DataTable();
            d.Columns.Add("Name", typeof(string));
            d.Columns.Add("Address", typeof(string));
            d.Columns.Add("Gst", typeof(string));
            d.Columns.Add("Total", typeof(decimal));
            d.Columns.Add("GraandTotal", typeof(decimal));
            d.Columns.Add("TotalCgst", typeof(decimal));
            d.Columns.Add("TotalSgst", typeof(decimal));
            DataRow row = d.NewRow();
            DataTable dd = r.Address();
            row["Name"] = dd.Rows[0]["PRPT_Name"].ToString();
            row["Address"] = dd.Rows[0]["PRPT_Address"].ToString();
            row["Gst"] = dd.Rows[0]["PRPT_Gst"].ToString();
            //DataTable dd1 = r.BILL1();
            if(dd.Rows[0]["AMOUNT"] == null || dd.Rows[0]["AMOUNT"].ToString() == "" &&  dd.Rows[0]["TAX"] == null || dd.Rows[0]["TAX"].ToString() == "" && dd.Rows[0]["TOTAL"] == null || dd.Rows[0]["TOTAL"].ToString() == ""  )
            {
               
            }
            else
            {
                row["Total"] = dd.Rows[0]["AMOUNT"].ToString();
                decimal a = Convert.ToDecimal(dd.Rows[0]["TAX"].ToString());
                row["TotalCgst"] = a / 2;
                row["TotalSgst"] = a / 2;
                row["GraandTotal"] = dd.Rows[0]["TOTAL"].ToString();
            }
            d.Rows.Add(row);
            return d;
        }
        public DataTable report1()
        {
            DataTable d = new DataTable();
            d.Columns.Add("Date", typeof(DateTime));
            d.Columns.Add("BillNo", typeof(int));
            d.Columns.Add("ItemName", typeof(string));
            d.Columns.Add("Rate", typeof(decimal));
            d.Columns.Add("Tax", typeof(decimal));
            d.Columns.Add("Total", typeof(decimal));
            
            DataTable D = r.BILL();
            for (int i = 0; i < D.Rows.Count; i++)
            {
                DataRow row = d.NewRow();
                row["Date"] = D.Rows[i]["BILITM_InsertDate"].ToString();
                row["BillNo"] = D.Rows[i]["BILITM_ID"].ToString();
                row["ItemName"] = D.Rows[i]["BILITM_Name"].ToString();
                row["Rate"] = D.Rows[i]["BILITM_Rate"].ToString();
                row["Tax"] = D.Rows[i]["BILITM_Tax"].ToString();
                int a = int.Parse(D.Rows[i]["BILLITM_Quanty"].ToString());
                decimal b = Convert.ToDecimal(D.Rows[i]["BILITM_Rate"].ToString());
                decimal c = Convert.ToDecimal(D.Rows[i]["BILITM_Tax"].ToString());
                decimal e = b * c / 100;
                decimal f = a * b;
                row["Total"] = f + e;
                d.Rows.Add(row);
            }
                return d;
        }
    }
}
