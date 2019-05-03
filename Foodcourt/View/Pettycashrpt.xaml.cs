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
    /// Interaction logic for Pettycashrpt.xaml
    /// </summary>
    public partial class Pettycashrpt : Page
    {
        public Pettycashrpt()
        {
            InitializeComponent();
        }

        public static string date, date1;
        Reports r = new Reports();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            date =dt.Text;
            date1 = dt1.Text;
            ReportDocument re = new ReportDocument();
            DataTable dd = report();
            re.Load("../../View/pettycashreport.rpt");
            re.SetDataSource(dd);
            re.PrintToPrinter(1, false, 0, 0);
            re.Refresh();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            pettycashRPT.Visibility = Visibility.Hidden;
        }
        public DataTable report()
        {
            DataTable d = new DataTable();
            d.Columns.Add("PCH_Name", typeof(string));
            d.Columns.Add("PCH_Amount", typeof(decimal));
            d.Columns.Add("PCH_InsertDate", typeof(DateTime));
            d.Columns.Add("PCH_InsertBy", typeof(string));
           
            DataTable dd = r.pettycash();
            for(int i=0; i< dd.Rows.Count; i++)
            {
                DataRow row = d.NewRow();
                row["PCH_Name"] = dd.Rows[i]["PCH_Name"];
                row["PCH_Amount"] = dd.Rows[i]["PCH_Amount"];
                row["PCH_InsertDate"] = dd.Rows[i]["PCH_InsertDate"];
                row["PCH_InsertBy"] = dd.Rows[i]["PCH_InsertBy"];
                d.Rows.Add(row);
            }
            return d;
        }
    }
}
