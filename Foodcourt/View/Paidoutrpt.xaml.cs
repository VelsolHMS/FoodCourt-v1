using CrystalDecisions.CrystalReports.Engine;
using Foodcourt.Model;
using Foodcourt.View;
using Foodcourt.View.Oprs;
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
    /// Interaction logic for Paidoutrpt.xaml
    /// </summary>
    public partial class Paidoutrpt : Page
    {
        public Paidoutrpt()
        {
            InitializeComponent();
        }
        public static string date, date1;
        Reports r = new Reports();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            date = dt.Text;
            date1 = dt1.Text;
            ReportDocument re = new ReportDocument();
            DataTable dd = report();
            re.Load("../../FCReport/PaidoutReport.rpt");
            re.SetDataSource(dd);
          //  CrystalReportViewer1.Visibility = Visibility.Visible;
            //paidoutRPT.Visibility = Visibility.Hidden;
            //CrystalReportViewer1.ShowRefreshButton = false;
            //re.Refresh();
            //CrystalReportViewer1.ViewerCore.ReportSource = re;
            re.PrintToPrinter(1, false, 0, 0);
            re.Refresh();
        }
        public DataTable report()
        {
            DataTable d = new DataTable();
            d.Columns.Add("PO_Name", typeof(string));
            d.Columns.Add("PO_Amount", typeof(decimal));
            d.Columns.Add("PO_InsertDate", typeof(DateTime));
            d.Columns.Add("PO_InsertBY", typeof(string));

            DataTable dd = r.paidoutcash();
            for (int i = 0; i < dd.Rows.Count; i++)
            {
                DataRow row = d.NewRow();
                row["PO_Name"] = dd.Rows[i]["PO_Name"];
                row["PO_Amount"] = dd.Rows[i]["PO_Amount"];
                row["PO_InsertDate"] = dd.Rows[i]["PO_InsertDate"];
                row["PO_InsertBY"] = dd.Rows[i]["PO_InsertBY"];
                d.Rows.Add(row);
            }
            return d;
        }
    }
}
