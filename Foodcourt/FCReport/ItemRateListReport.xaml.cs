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

namespace Foodcourt.FCReports
{
    /// <summary>
    /// Interaction logic for ItemRateListReport.xaml
    /// </summary>
    public partial class ItemRateListReport : Page
    {
        Reports rpt = new Reports();
        public ItemRateListReport()
        {
            InitializeComponent();
            ReportDocument r = new ReportDocument();
            DataTable d1 = MainReport();
            r.Load("../../REPORTS/ItemRateReport.rpt");
            r.SetDataSource(d1);
            r.PrintToPrinter(1, false, 0, 0);
            r.Refresh();
            MessageBox.Show("Report Generated Succesfully");
        }

        public DataTable MainReport()
        {
            DataTable d = new DataTable();
            d.Columns.Add("Prptname", typeof(string));
            d.Columns.Add("Address", typeof(string));
            d.Columns.Add("GST", typeof(string));
            d.Columns.Add("ItemName", typeof(string));
            d.Columns.Add("ItemRate", typeof(decimal));
            d.Columns.Add("ItemTax", typeof(decimal));
            d.Columns.Add("Resname", typeof(string));

            DataTable dd = rpt.ItemRates();
            for (int i = 0; i < dd.Rows.Count; i++)
            {
                DataRow r = d.NewRow();
                rpt.PRPT();
                r["Prptname"] = Reports.Name;
                r["Address"] = Reports.Address;
                r["GST"] = Reports.GST;
                r["ItemName"] = dd.Rows[i]["NAM_Name"].ToString();
                r["ItemRate"] = dd.Rows[i]["NAM_Rate"].ToString();
                r["ItemTax"] = dd.Rows[i]["NAM_TaxPer"].ToString();
                r["Resname"] = cmb.Text;
                d.Rows.Add(r);
            }
            return d;
        }
    }
}
