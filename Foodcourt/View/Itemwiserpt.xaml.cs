using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using Foodcourt.Model;
using CrystalDecisions.CrystalReports.Engine;

namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for Itemwiserpt.xaml
    /// </summary>
    public partial class Itemwiserpt : Page
    {
        ItemsCAT it = new ItemsCAT();
        Reports rpt = new Reports();
        MainWindow main = new MainWindow();
        ItemNames i = new ItemNames();
        public Itemwiserpt()
        {
            InitializeComponent();
            fromdate.DisplayDateEnd = DateTime.Today.Date;
            todate.DisplayDateEnd = DateTime.Today.Date;
            DataTable stalls = it.GetStalls();
            txtstall.ItemsSource = stalls.DefaultView;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(fromdate.Text == "" || todate.Text == "")
            {
                MessageBox.Show("Please select the Date.!");
            }
            else
            {
                i.date = fromdate.Text;
                i.date1 = todate.Text;
                i.STL_Name = txtstall.Text;
                ReportDocument r = new ReportDocument();
                DataTable d1 = MainReport();
                r.Load("../../View/Itemwise.rpt");
                r.SetDataSource(d1);
                r.PrintToPrinter(1, false, 0, 0);
                r.Refresh();
                MessageBox.Show("Report Generated Succesfully");
                this.NavigationService.Refresh();
            }
        }
        public DataTable MainReport()
        {
            DataTable D = new DataTable();
            D.Columns.Add("NAME", typeof(string));
            D.Columns.Add("ADDRESS", typeof(string));
            D.Columns.Add("ITEM_NAME", typeof(string));
            D.Columns.Add("QTY", typeof(int));
            D.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
            D.Columns.Add("TAX", typeof(decimal));
            D.Columns.Add("GRANDTOTAL", typeof(decimal));
            D.Columns.Add("GST", typeof(string));
            D.Columns.Add("CGST", typeof(decimal));
            D.Columns.Add("Stall", typeof(string));
            D.Columns.Add("FromDate", typeof(DateTime));
            D.Columns.Add("ToDate", typeof(DateTime));
            rpt.PRPT();
            DataTable S1 = i.itemwise();
            for (int i = 0; i < S1.Rows.Count; i++)
            {
                Decimal A,B,C,E;
                DataRow row = D.NewRow();
                row["NAME"] = Reports.Name;
                row["ADDRESS"] = Reports.Address;
                row["GST"] = Reports.GST;
                row["Stall"] = txtstall.Text;
                row["ITEM_NAME"] = S1.Rows[i]["ITEM_NAME"].ToString();
                row["QTY"] = S1.Rows[i]["QTY"].ToString();
                row["FromDate"] = fromdate.Text;
                row["ToDate"] = todate.Text;
                A = Decimal.Parse(S1.Rows[i]["RATE"].ToString());
                B = Decimal.Parse(S1.Rows[i]["QTY"].ToString());
                C = Decimal.Parse(S1.Rows[i]["TAXRATE"].ToString());
                E = A * B;
                row["TOTAL_AMOUNT"] = E;
                row["TAX"] = C/2;
                row["CGST"] = C/2;
                row["GRANDTOTAL"] =E+C;
                D.Rows.Add(row);
            }
            return D;
        }
    }
}