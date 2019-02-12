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
using System.Data;
using System.Data.SqlClient;
using Foodcourt.View;
using Foodcourt.Model;
using CrystalDecisions.CrystalReports.Engine;

namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for Itemwiserpt.xaml
    /// </summary>
    public partial class Itemwiserpt : Page
    {
        MainWindow main = new MainWindow();
        ItemNames i = new ItemNames();
        public Itemwiserpt()
        {
            InitializeComponent();

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            IWSRPT.Visibility = Visibility.Hidden;
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
             i.date=fromdate.Text;
             i.date1= todate.Text;
            ReportDocument re = new ReportDocument();
            DataTable dd = report();    
            re.Load("../../View/Itemwise.rpt");
            re.SetDataSource(dd);
            CrystalReportViewer1.Visibility = Visibility.Visible;
            IWSRPT.Visibility = Visibility.Hidden;
            CrystalReportViewer1.ShowRefreshButton = false;
            re.Refresh();
            CrystalReportViewer1.ViewerCore.ReportSource = re;
        }

        public DataTable report()
        {
            DataTable D = new DataTable();
            D.Columns.Add("NAME", typeof(string));
            D.Columns.Add("ADDRESS", typeof(string));
            D.Columns.Add("ITEM_NAME", typeof(string));
            D.Columns.Add("QTY", typeof(int));
            D.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
            D.Columns.Add("TAX", typeof(decimal));
            D.Columns.Add("GRANDTOTAL", typeof(decimal));
            D.Columns.Add("DATE", typeof(DateTime));
            D.Columns.Add("GST", typeof(string));
            D.Columns.Add("CGST", typeof(decimal));
            DataTable s = i.Address();
            DataTable S1 = i.itemwise();
            for (int i = 0; i < S1.Rows.Count; i++)
            {
                Decimal A,B,C,E;
                DataRow row = D.NewRow();
                row["NAME"] = s.Rows[0]["PRPT_Name"].ToString();
                row["ADDRESS"] = s.Rows[0]["PRPT_Address"].ToString();
                row["GST"] = s.Rows[0]["PRPT_GST"].ToString();
                row["ITEM_NAME"] = S1.Rows[i]["ITEM_NAME"].ToString();
                row["QTY"] = S1.Rows[i]["QTY"].ToString();
                A=Decimal.Parse(S1.Rows[i]["RATE"].ToString());
                B = Decimal.Parse(S1.Rows[i]["QTY"].ToString());
                C = Decimal.Parse(S1.Rows[i]["TAXRATE"].ToString());
                E = A * B;
                row["TOTAL_AMOUNT"] = E;
                row["TAX"] = C/2;
                row["CGST"] = C/2;
                row["GRANDTOTAL"] =E+C;
                row["DATE"] = S1.Rows[i]["DATE"].ToString();
                D.Rows.Add(row);
            }
            return D;
        }
    }
}
