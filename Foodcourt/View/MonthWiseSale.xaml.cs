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
    /// Interaction logic for MonthWiseSale.xaml
    /// </summary>
    public partial class MonthWiseSale : Page
    {
        Reports rpt = new Reports();
        public MonthWiseSale()
        {
            InitializeComponent();
            fromdate.DisplayDateEnd = DateTime.Today.Date;
            todate.DisplayDateEnd = DateTime.Today.Date;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(fromdate.Text == "" || todate.Text == "")
            {
                MessageBox.Show("Please fill all fields");
            }
            else
            {
                rpt.FromDate = fromdate.Text;
                rpt.ToDate = todate.Text;
                ReportDocument r = new ReportDocument();
                DataTable sub = SubReport();
                r.Load("../../FCReport/MonthWiseSaleSubReport.rpt");
                DataTable main = MainReport();
                r.Load("../../FCReport/MonthWiseSaleMainReport.rpt");
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
            DataTable d1 = rpt.MonthWiseBillsTotal();
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
            if (d1.Rows[0]["BILL_Tax"].ToString() == "" || d1.Rows[0]["BILL_Tax"].ToString() == "null")
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
            row["TOTALCGST"] = Math.Round(b / 2, 2, MidpointRounding.AwayFromZero);
            row["TOTALSGST"] = Math.Round(b / 2, 2, MidpointRounding.AwayFromZero);
            row["GRANDTOTALL"] = Math.Round(c, 2, MidpointRounding.AwayFromZero); ;
            d.Rows.Add(row);
            return d;
        }
        public DataTable SubReport()
        {
            DataTable d = new DataTable();
            d.Columns.Add("Date", typeof(DateTime));
            d.Columns.Add("BillNo", typeof(int));
            d.Columns.Add("NetAmount", typeof(decimal));
            d.Columns.Add("GST", typeof(decimal));
            d.Columns.Add("GRANDTOTAL", typeof(decimal));
            d.Columns.Add("USER", typeof(string));
            DataTable month_bills = rpt.MonthWiseBills();
            for (int i = 0; i < month_bills.Rows.Count; i++)
            {
                DataRow row = d.NewRow();
                row["Date"] = month_bills.Rows[i]["BILL_InsertDate"].ToString();
                row["BillNo"] = month_bills.Rows[i]["BILL_Id"].ToString();
                row["NetAmount"] = month_bills.Rows[i]["BILL_Amount"].ToString();
                if (month_bills.Rows[i]["BILL_Tax"].ToString() == "" || month_bills.Rows[i]["BILL_Tax"].ToString() == null)
                {
                    a = 0;
                }
                else
                {
                    a = decimal.Parse(month_bills.Rows[i]["BILL_Tax"].ToString());
                }
                row["GST"] = Math.Round(a, 2, MidpointRounding.AwayFromZero);
                row["GRANDTOTAL"] = month_bills.Rows[i]["BILL_Total"].ToString();
                row["USER"] = month_bills.Rows[i]["BILL_InsertBy"].ToString();
                d.Rows.Add(row);
            }
            return d;
        }
    }
}
