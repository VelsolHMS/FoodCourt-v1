using CrystalDecisions.CrystalReports.Engine;
using Foodcourt.Model;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for Daywiserpt.xaml
    /// </summary>
    public partial class Daywiserpt : Page
    {
        ItemsCAT it = new ItemsCAT();
        reportClass1 r = new reportClass1();
        Reports rpt = new Reports();
        public Daywiserpt()
        {
            InitializeComponent();
            DataTable stalls = it.GetStalls();
            txtstall.ItemsSource = stalls.DefaultView;
            selecteddate.DisplayDateEnd = DateTime.Today.Date;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(selecteddate.Text == "" || selecteddate.Text == null)
            {
                MessageBox.Show("Please select the Date");
            }
            else
            {
                rpt.SelectedDate = selecteddate.Text;
                rpt.STL_Name = txtstall.Text;
                ReportDocument r = new ReportDocument();
                DataTable sub = SubReport();
                r.Load("../../View/dayCrystalReport1.rpt");
                DataTable main = MainReport();
                r.Load("../../View/DatewiseReport.rpt");
                r.SetDataSource(main);
                r.Subreports[0].SetDataSource(sub);
                r.PrintToPrinter(1, false, 0, 0);
                r.Refresh();
                MessageBox.Show("Report Generated Succesfully");
            }
        }
        public DataTable MainReport()
        {
            DataTable d = new DataTable();
            d.Columns.Add("TOTALNETAMOUNT", typeof(decimal));
            d.Columns.Add("TOTALCGST", typeof(decimal));
            d.Columns.Add("TOTALSGST", typeof(decimal));
            d.Columns.Add("GRANDTOTALL", typeof(decimal));
            d.Columns.Add("Name", typeof(string));
            d.Columns.Add("Address", typeof(string));
            d.Columns.Add("Gst", typeof(string));
            d.Columns.Add("SelectedDate", typeof(string));
            d.Columns.Add("Stall",typeof(string));
            rpt.PRPT();
            DataRow row = d.NewRow();
            //DataTable d1 = rpt.DayWiseBillsTotal();
            row["Name"] = Reports.Name;
            row["Address"] = Reports.Address;
            row["Gst"] = Reports.GST;
            row["TOTALNETAMOUNT"] = Math.Round(NetAmount, 2, MidpointRounding.AwayFromZero);
            row["TOTALCGST"] = Math.Round(TotalTax/2, 2,MidpointRounding.AwayFromZero);
            row["TOTALSGST"] = Math.Round(TotalTax/2, 2, MidpointRounding.AwayFromZero);
            row["GRANDTOTALL"] = Math.Round(GrandTotal, 2, MidpointRounding.AwayFromZero);
            row["SelectedDate"] = selecteddate.Text;
            row["Stall"] = txtstall.Text;
            d.Rows.Add(row);
            return d;
        }
        public decimal GrandTotal, TotalTax, NetAmount;
        public DataTable SubReport()
        {
            DataTable d = new DataTable();
            d.Columns.Add("ItemName", typeof(string));
            d.Columns.Add("NetAmount", typeof(decimal));
            d.Columns.Add("CGST", typeof(decimal));
            d.Columns.Add("SGST", typeof(decimal));
            d.Columns.Add("Qty", typeof(decimal));
            d.Columns.Add("GRANDTOTAL", typeof(decimal));
            DataTable day_bills = rpt.DayWiseItemSale();
            Decimal rate,tax;
            int qty;
            //DataTable day_bills = rpt.DayWiseBills();
            for (int i = 0; i < day_bills.Rows.Count; i++)
            {
                DataRow row = d.NewRow();
                row["ItemName"] = day_bills.Rows[i]["ITEM_NAME"].ToString();
                rate = Convert.ToDecimal(day_bills.Rows[i]["RATE"]);
                qty = Convert.ToInt32(day_bills.Rows[i]["QTY"]);
                NetAmount += rate * qty;
                row["NetAmount"] = Math.Round(rate * qty, 2, MidpointRounding.AwayFromZero);
                row["Qty"] = qty;
                tax = Convert.ToInt32(day_bills.Rows[i]["TAXRATE"]);
                row["CGST"] = Math.Round(tax / 2, 2, MidpointRounding.AwayFromZero);
                row["SGST"] = Math.Round(tax/2,2,MidpointRounding.AwayFromZero);
                row["GRANDTOTAL"] = Math.Round((rate * qty) + tax, 2, MidpointRounding.AwayFromZero);
                TotalTax += tax;
                GrandTotal += (rate * qty) + tax;
                d.Rows.Add(row);
            }
            return d;
        }
    }
}
