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
    /// Interaction logic for Miscollectiorpt.xaml
    /// </summary>
    public partial class Miscollectiorpt : Page
    {
        public Miscollectiorpt()
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
            re.Load("../../FCReport/MiccollectionReport.rpt");
            re.SetDataSource(dd);
            //CrystalReportViewer1.Visibility = Visibility.Visible;
          //  miscollectionRPT.Visibility = Visibility.Hidden;
            //CrystalReportViewer1.ShowRefreshButton = false;
            //re.Refresh();
            //CrystalReportViewer1.ViewerCore.ReportSource = re;
            re.PrintToPrinter(1, false, 0, 0);
            re.Refresh();
        }
        public DataTable report()
        {
            DataTable d = new DataTable();
            d.Columns.Add("MIS_Name", typeof(string));
            d.Columns.Add("MIS_Amount", typeof(decimal));
            d.Columns.Add("MIS_InsertDate", typeof(DateTime));
            d.Columns.Add("MIS_InsertBY", typeof(string));

            DataTable dd = r.miscollection();
            for (int i = 0; i < dd.Rows.Count; i++)
            {
                DataRow row = d.NewRow();
                row["MIS_Name"] = dd.Rows[i]["MIS_Name"];
                row["MIS_Amount"] = dd.Rows[i]["MIS_Amount"];
                row["MIS_InsertDate"] = dd.Rows[i]["MIS_InsertDate"];
                row["MIS_InsertBY"] = dd.Rows[i]["MIS_InsertBY"];
                d.Rows.Add(row);
            }
            return d;
        }
    }
}
