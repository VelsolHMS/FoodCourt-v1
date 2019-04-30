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
    /// Interaction logic for Cashhandrpt.xaml
    /// </summary>
    public partial class Cashhandrpt : Page
    {
        public Cashhandrpt()
        {
            InitializeComponent();
           
            //CrystalReportViewer1.Owner = Window.GetWindow(this);
            //  System.Windows.Window window = new System.Windows.Window(); CrystalReportViewer1.Owner = window; System.Windows.Interop.WindowInteropHelper helper = new System.Windows.Interop.WindowInteropHelper(window);
        }
        public static string date, date1;
        Reports r = new Reports();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            date = dt.Text;
            date1 = dt1.Text;
            ReportDocument re = new ReportDocument();
            DataTable dd = report();
            re.Load("../../FCReport/CashhandReport.rpt");
            re.SetDataSource(dd);
           // CrystalReportViewer1.Visibility = Visibility.Visible;
            //cashhandRPT.Visibility = Visibility.Hidden;
            //CrystalReportViewer1.ShowRefreshButton = false;
            //re.Refresh();
            //CrystalReportViewer1.ViewerCore.ReportSource = re;
            //dd.Rows.Clear();
            re.PrintToPrinter(1, false, 0, 0);
            re.Refresh();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            cashhandRPT.Visibility = Visibility.Hidden;
        }
        public DataTable report()
        {
            DataTable d = new DataTable();
            d.Columns.Add("CH_Name", typeof(string));
            d.Columns.Add("CH_Amount", typeof(decimal));
            d.Columns.Add("CH_InsertDate", typeof(DateTime));
            d.Columns.Add("CH_InsertBY", typeof(string));

            DataTable dd = r.CASHHAND();
            for (int i = 0; i < dd.Rows.Count; i++)
            {
                DataRow row = d.NewRow();
                row["CH_Name"] = dd.Rows[i]["CH_Name"];
                row["CH_Amount"] = dd.Rows[i]["CH_Amount"];
                row["CH_InsertDate"] = dd.Rows[i]["CH_InsertDate"];
                row["CH_InsertBY"] = dd.Rows[i]["CH_InsertBY"];
                d.Rows.Add(row);
            }
            return d;
        }
    }
}
