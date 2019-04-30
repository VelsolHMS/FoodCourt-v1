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
        reportClass1 r = new reportClass1();
        Reports rpt = new Reports();
        public Daywiserpt()
        {
            InitializeComponent();
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
        public decimal a, b, c;
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
            rpt.PRPT();
            DataRow row = d.NewRow();
            DataTable d1 = rpt.DayWiseBillsTotal();
            row["Name"] = Reports.Name;
            row["Address"] = Reports.Address;
            row["Gst"] = Reports.GST;
            if (d1.Rows[0]["Bill_Amount"].ToString() == "" || d1.Rows[0]["Bill_Amount"].ToString() == "null")
            {
                row["TOTALNETAMOUNT"] = 0.00;
            }
            else
            {
                row["TOTALNETAMOUNT"] = d1.Rows[0]["Bill_Amount"].ToString();
            }
            if(d1.Rows[0]["BILL_Tax"].ToString() == "" || d1.Rows[0]["BILL_Tax"].ToString() == "null")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(d1.Rows[0]["BILL_Tax"].ToString());
            }
            if (d1.Rows[0]["BILL_Total"].ToString() == "" || d1.Rows[0]["BILL_Total"].ToString() == "null")
            {
                c = 0;
            }
            else
            {
                c = decimal.Parse(d1.Rows[0]["BILL_Total"].ToString());
            }
            row["TOTALCGST"] = Math.Round(b/2,2,MidpointRounding.AwayFromZero);
            row["TOTALSGST"] = Math.Round(b/2, 2, MidpointRounding.AwayFromZero);
            row["GRANDTOTALL"] = Math.Round(c, 2, MidpointRounding.AwayFromZero); ;
            d.Rows.Add(row);
            return d;
        }
        public DataTable SubReport()
        {
            DataTable d = new DataTable();
            d.Columns.Add("BillNo", typeof(int));
            d.Columns.Add("NetAmount", typeof(decimal));
            d.Columns.Add("CGST", typeof(decimal));
            d.Columns.Add("SGST", typeof(decimal));
            d.Columns.Add("GRANDTOTAL", typeof(decimal));
            d.Columns.Add("USER", typeof(string));
            DataTable day_bills = rpt.DayWiseBills();
            for (int i = 0; i < day_bills.Rows.Count; i++)
            {
                DataRow row = d.NewRow();
                row["BillNo"] = day_bills.Rows[i]["BILL_Id"].ToString();
                row["NetAmount"] = day_bills.Rows[i]["BILL_Amount"].ToString();
                if(day_bills.Rows[i]["BILL_Tax"].ToString() == "" || day_bills.Rows[i]["BILL_Tax"].ToString() == null)
                {
                    a = 0;
                }
                else
                {
                    a = decimal.Parse(day_bills.Rows[i]["BILL_Tax"].ToString());
                }
                row["CGST"] = Math.Round(a/2,2,MidpointRounding.AwayFromZero);
                row["SGST"] = Math.Round(a/2, 2, MidpointRounding.AwayFromZero);
                row["GRANDTOTAL"] = day_bills.Rows[i]["BILL_Total"].ToString();
                row["USER"] = day_bills.Rows[i]["BILL_InsertBy"].ToString();
                d.Rows.Add(row);
            }
            return d;
        }
    }
}
