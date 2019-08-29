using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using Foodcourt.Model;
using CrystalDecisions.CrystalReports.Engine;
using System.Linq;

namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for Itemwiserpt.xaml
    /// </summary>
    public partial class Itemwiserpt : Page
    {
        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
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
            if(fromdate.Text == "" || todate.Text == "" || txtstall.Text == "" || txtPer.Text == "")
            {
                MessageBox.Show("Please select the Date.!");
            }
            else
            {
                if (IsNumeric(txtPer.Text))
                {
                    i.date = fromdate.Text;
                    i.date1 = todate.Text;
                    i.STL_Name = txtstall.Text;
                    ReportDocument r = new ReportDocument();
                    DataTable sub = SubReport();
                    r.Load("../../View/MonthlyItemSaleSubReport.rpt");
                    DataTable main = MainReport();
                    r.Load("../../View/Itemwise.rpt");
                    r.SetDataSource(main);
                    r.Subreports[0].SetDataSource(sub);
                    r.PrintToPrinter(1, false, 0, 0);
                    r.Refresh();
                    MessageBox.Show("Report Generated Succesfully");
                    this.NavigationService.Refresh();
                }
                else
                {
                    MessageBox.Show("Please Enter the numbers in Revenue");
                }
            }
        }
        public DataTable MainReport()
        {
            DataTable D = new DataTable();
            D.Columns.Add("NAME", typeof(string));
            D.Columns.Add("ADDRESS", typeof(string));
            D.Columns.Add("GST", typeof(string));
            D.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
            D.Columns.Add("SGST", typeof(decimal));
            D.Columns.Add("GRANDTOTAL", typeof(decimal));
            D.Columns.Add("CGST", typeof(decimal));
            D.Columns.Add("Stall", typeof(string));
            D.Columns.Add("FromDate", typeof(DateTime));
            D.Columns.Add("ToDate", typeof(DateTime));
            D.Columns.Add("RevenueShare", typeof(decimal));
            D.Columns.Add("DISCOUNT", typeof(string));
            rpt.PRPT();
            DataRow row = D.NewRow();
            row["NAME"] = Reports.Name;
            row["ADDRESS"] = Reports.Address;
            row["GST"] = Reports.GST;
            row["Stall"] = txtstall.Text;
            row["FromDate"] = fromdate.Text;
            row["ToDate"] = todate.Text;
            row["TOTAL_AMOUNT"] = FinalNetAmount;
            row["SGST"] = FinalTax/2;
            row["CGST"] = FinalTax/2;
            row["DISCOUNT"] = FinalDiscountAmount;
            row["GRANDTOTAL"] = FinalGrandTotal;
            row["RevenueShare"] = (FinalNetAmount * Convert.ToInt32(txtPer.Text))/ 100;
            D.Rows.Add(row);
            return D;
        }
        Decimal FinalNetAmount, FinalTax, FinalGrandTotal, FinalDiscountAmount;
        public DataTable SubReport()
        {
            DataTable D = new DataTable();
            D.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
            D.Columns.Add("SGST", typeof(decimal));
            D.Columns.Add("GRANDTOTAL", typeof(decimal));
            D.Columns.Add("CGST", typeof(decimal));
            D.Columns.Add("ITEM_NAME", typeof(string));
            D.Columns.Add("QTY", typeof(Int32));
            D.Columns.Add("DISCOUNT", typeof(string));

            DataTable S1 = i.itemwise();
            for (int i = 0; i < S1.Rows.Count; i++)
            {
                Decimal A, B, C, E, F;
                DataRow row = D.NewRow();
                row["ITEM_NAME"] = S1.Rows[i]["ITEM_NAME"].ToString();
                row["QTY"] = S1.Rows[i]["QTY"].ToString();
                row["DISCOUNT"] = S1.Rows[i]["Discount"].ToString();
                A = Decimal.Parse(S1.Rows[i]["RATE"].ToString());
                B = Decimal.Parse(S1.Rows[i]["QTY"].ToString());
                C = Decimal.Parse(S1.Rows[i]["TAXRATE"].ToString());
                E = A * B;
                F = Decimal.Parse(S1.Rows[i]["Discount"].ToString());
                FinalNetAmount += E;
                FinalTax += C;
                FinalGrandTotal += (E + C) - F;
                FinalDiscountAmount += F;
                row["TOTAL_AMOUNT"] = E;
                row["SGST"] = C / 2;
                row["CGST"] = C / 2;
                row["GRANDTOTAL"] = (E + C) - F;
                D.Rows.Add(row);
            }
            return D;
        }
    }
}