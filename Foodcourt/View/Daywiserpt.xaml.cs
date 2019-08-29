using CrystalDecisions.CrystalReports.Engine;
using Foodcourt.Model;
using System;
using System.Data;
using System.Linq;
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
        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
        public Daywiserpt()
        {
            InitializeComponent();
            DataTable stalls = it.GetStalls();
            txtstall.ItemsSource = stalls.DefaultView;
            selecteddate.DisplayDateEnd = DateTime.Today.Date;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(selecteddate.Text == "" || selecteddate.Text == null || txtPer.Text == null)
            {
                MessageBox.Show("Please select the Date");
            }
            else
            {
                if (IsNumeric(txtPer.Text))
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
            DataTable d = new DataTable();
            d.Columns.Add("TOTALNETAMOUNT", typeof(decimal));
            d.Columns.Add("TOTALCGST", typeof(decimal));
            d.Columns.Add("TOTALSGST", typeof(decimal));
            d.Columns.Add("GRANDTOTALL", typeof(decimal));
            d.Columns.Add("DISCOUNT", typeof(decimal));
            d.Columns.Add("Name", typeof(string));
            d.Columns.Add("Address", typeof(string));
            d.Columns.Add("Gst", typeof(string));
            d.Columns.Add("SelectedDate", typeof(string));
            d.Columns.Add("Stall",typeof(string));
            d.Columns.Add("OwnerAmount", typeof(decimal));
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
            row["DISCOUNT"] = Math.Round(TotalDiscount, 2, MidpointRounding.AwayFromZero);
            row["SelectedDate"] = selecteddate.Text;
            row["Stall"] = txtstall.Text;
            row["OwnerAmount"] = (NetAmount * Convert.ToInt32(txtPer.Text)) / 100;
            d.Rows.Add(row);
            return d;
        }
        public decimal GrandTotal, TotalTax, NetAmount, TotalDiscount;
        public DataTable SubReport()
        {
            DataTable d = new DataTable();
            d.Columns.Add("ItemName", typeof(string));
            d.Columns.Add("NetAmount", typeof(decimal));
            d.Columns.Add("CGST", typeof(decimal));
            d.Columns.Add("SGST", typeof(decimal));
            d.Columns.Add("Qty", typeof(decimal));
            d.Columns.Add("GRANDTOTAL", typeof(decimal));
            d.Columns.Add("DISCOUNT", typeof(decimal));
            DataTable day_bills = rpt.DayWiseItemSale();
            Decimal rate,tax,discount;
            int qty;
            //DataTable day_bills = rpt.DayWiseBills();
            for (int i = 0; i < day_bills.Rows.Count; i++)
            {
                DataRow row = d.NewRow();
                row["ItemName"] = day_bills.Rows[i]["ITEM_NAME"].ToString();
                row["DISCOUNT"] = day_bills.Rows[i]["Discount"].ToString();
                rate = Convert.ToDecimal(day_bills.Rows[i]["RATE"]);
                qty = Convert.ToInt32(day_bills.Rows[i]["QTY"]);
                discount = Convert.ToDecimal(day_bills.Rows[i]["Discount"]);
                NetAmount += rate * qty;
                row["NetAmount"] = Math.Round(rate * qty, 2, MidpointRounding.AwayFromZero);
                row["Qty"] = qty;
                tax = Convert.ToInt32(day_bills.Rows[i]["TAXRATE"]);
                row["CGST"] = Math.Round(tax / 2, 2, MidpointRounding.AwayFromZero);
                row["SGST"] = Math.Round(tax/2,2,MidpointRounding.AwayFromZero);
                row["GRANDTOTAL"] = Math.Round(((rate * qty) + tax) - discount, 2, MidpointRounding.AwayFromZero);
                TotalTax += tax;
                GrandTotal += ((rate * qty) + tax) - discount;
                TotalDiscount += discount;
                d.Rows.Add(row);
            }
            return d;
        }
    }
}