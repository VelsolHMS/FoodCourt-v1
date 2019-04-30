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
using Foodcourt.Model;
using System.Windows.Controls.DataVisualization.Charting.Compatible;

namespace Foodcourt.View.Oprs
{
    /// <summary>
    /// Interaction logic for DashBoard.xaml
    /// </summary>
    public partial class DashBoard : Page
    {
        DASHBOARD DB = new DASHBOARD();
        public DashBoard()
        {
            InitializeComponent();
            sumd = 0;
            summ = 0;
            sumy = 0;
            DAY();
            YEAR();
            cmbmonth.SelectedIndex = DateTime.Now.Month - 1;
            month = cmbmonth.Text;
            i = DateTime.ParseExact(month, "MMMM", System.Globalization.CultureInfo.InvariantCulture).Month;
            //MONTH = int.Parse(i.ToString());
            YEAR1 = DateTime.Now.Year;
            DataTable DT1 = DB.GETMONTHBILL();
            DataTable dt1 = new DataTable();
            for (int i = 0; i < DT1.Rows.Count; i++)
            {
                BILL_Total = Convert.ToDecimal(DT1.Rows[i]["BILL_Total"]);
                summ = summ + BILL_Total;
            }
            tbmonth.Text = summ.ToString();
        }
        public static decimal sumd = 0, summ = 0, sumy = 0;
        public static decimal BILL_Total,i;
        public static string month;
        public static int MONTH,YEAR1;
        //To get Day wise sale
        public void DAY()
        {
            DataTable DT = DB.GETDAYBILL();
            DataTable dt = new DataTable();
            dt.Columns.Add("BILL_Total", typeof(int));
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                BILL_Total = Convert.ToDecimal(DT.Rows[i]["BILL_Total"]);
                sumd = sumd + BILL_Total;
            }
            tbday.Text = sumd.ToString();
        }
        //To get Month wise sale
        private void cmbmonth_DropDownClosed(object sender, EventArgs e)
        {
            summ = 0;
            month = cmbmonth.Text;
            i = DateTime.ParseExact(month, "MMMM", System.Globalization.CultureInfo.InvariantCulture).Month;
            DataTable DT = DB.GETMONTHBILL();
            DataTable dt = new DataTable();
            dt.Columns.Add("BILL_Total", typeof(int));
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                BILL_Total = Convert.ToDecimal(DT.Rows[i]["BILL_Total"]);
                summ = summ + BILL_Total;
            }
            tbmonth.Text = summ.ToString();
        }
        public static DataTable DS;
        //To get Year wise sale
        public void YEAR()
        {
            DataTable DT = DB.GETYEARBILL();
            DataTable dt = new DataTable();
            dt.Columns.Add("BILL_Total", typeof(int));
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                BILL_Total = Convert.ToDecimal(DT.Rows[i]["BILL_Total"]);
                sumy = sumy + BILL_Total;
            }
            tbyear.Text = sumy.ToString();
        }
        public int ii = 1,VAL;
        public string VALS, s;
        public static int i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12, i13, i14, i15, i16, i17, i18, i19, i20, i21, i22, i23, i24, i25, i26, i27, i28, i29, i30, i31;
        private void cmbsales_DropDownClosed(object sender, EventArgs e)
        {
            List<KeyValuePair<string, int>> ValueList = new List<KeyValuePair<string, int>>();
            if (cmbsales.Text == "January")
            {
                MONTH = 01;
                METHOD();
                showColumnChart();
                i1 = 0; i2 = 0; i3 = 0; i4 = 0; i5 = 0; i6 = 0; i7 = 0; i8 = 0; i9 = 0; i10 = 0; i11 = 0; i12 = 0; i13 = 0; i14 = 0; i15 = 0; i16 = 0; i17 = 0; i18 = 0; i19 = 0; i20 = 0; i21 = 0; i22 = 0; i23 = 0; i24 = 0; i25 = 0; i26 = 0; i27 = 0; i28 = 0; i29 = 0; i30 = 0; i31 = 0;
            }
            if (cmbsales.Text == "February")
            {
                MONTH = 02;
                METHOD();
                showColumnChart28();
                i1 = 0; i2 = 0; i3 = 0; i4 = 0; i5 = 0; i6 = 0; i7 = 0; i8 = 0; i9 = 0; i10 = 0; i11 = 0; i12 = 0; i13 = 0; i14 = 0; i15 = 0; i16 = 0; i17 = 0; i18 = 0; i19 = 0; i20 = 0; i21 = 0; i22 = 0; i23 = 0; i24 = 0; i25 = 0; i26 = 0; i27 = 0; i28 = 0; i29 = 0; i30 = 0; i31 = 0;
            }
            if (cmbsales.Text == "March")
            {
                MONTH = 03;
                METHOD();
                showColumnChart();
                i1 = 0; i2 = 0; i3 = 0; i4 = 0; i5 = 0; i6 = 0; i7 = 0; i8 = 0; i9 = 0; i10 = 0; i11 = 0; i12 = 0; i13 = 0; i14 = 0; i15 = 0; i16 = 0; i17 = 0; i18 = 0; i19 = 0; i20 = 0; i21 = 0; i22 = 0; i23 = 0; i24 = 0; i25 = 0; i26 = 0; i27 = 0; i28 = 0; i29 = 0; i30 = 0; i31 = 0;
            }
            if (cmbsales.Text == "April")
            {
                MONTH = 04;
                METHOD();
                showColumnChart30();
                i1 = 0; i2 = 0; i3 = 0; i4 = 0; i5 = 0; i6 = 0; i7 = 0; i8 = 0; i9 = 0; i10 = 0; i11 = 0; i12 = 0; i13 = 0; i14 = 0; i15 = 0; i16 = 0; i17 = 0; i18 = 0; i19 = 0; i20 = 0; i21 = 0; i22 = 0; i23 = 0; i24 = 0; i25 = 0; i26 = 0; i27 = 0; i28 = 0; i29 = 0; i30 = 0; i31 = 0;
            }
            if (cmbsales.Text == "May")
            {
                MONTH = 05;
                METHOD();
                showColumnChart();
                i1 = 0; i2 = 0; i3 = 0; i4 = 0; i5 = 0; i6 = 0; i7 = 0; i8 = 0; i9 = 0; i10 = 0; i11 = 0; i12 = 0; i13 = 0; i14 = 0; i15 = 0; i16 = 0; i17 = 0; i18 = 0; i19 = 0; i20 = 0; i21 = 0; i22 = 0; i23 = 0; i24 = 0; i25 = 0; i26 = 0; i27 = 0; i28 = 0; i29 = 0; i30 = 0; i31 = 0;
            }
            if (cmbsales.Text == "June")
            {
                MONTH = 06;
                METHOD();
                showColumnChart30();
                i1 = 0; i2 = 0; i3 = 0; i4 = 0; i5 = 0; i6 = 0; i7 = 0; i8 = 0; i9 = 0; i10 = 0; i11 = 0; i12 = 0; i13 = 0; i14 = 0; i15 = 0; i16 = 0; i17 = 0; i18 = 0; i19 = 0; i20 = 0; i21 = 0; i22 = 0; i23 = 0; i24 = 0; i25 = 0; i26 = 0; i27 = 0; i28 = 0; i29 = 0; i30 = 0; i31 = 0;
            }
            if (cmbsales.Text == "July")
            {
                MONTH = 07;
                METHOD();
                showColumnChart();
                i1 = 0; i2 = 0; i3 = 0; i4 = 0; i5 = 0; i6 = 0; i7 = 0; i8 = 0; i9 = 0; i10 = 0; i11 = 0; i12 = 0; i13 = 0; i14 = 0; i15 = 0; i16 = 0; i17 = 0; i18 = 0; i19 = 0; i20 = 0; i21 = 0; i22 = 0; i23 = 0; i24 = 0; i25 = 0; i26 = 0; i27 = 0; i28 = 0; i29 = 0; i30 = 0; i31 = 0;
            }
            if (cmbsales.Text == "August")
            {
                MONTH = 08;
                METHOD();
                showColumnChart();
                i1 = 0; i2 = 0; i3 = 0; i4 = 0; i5 = 0; i6 = 0; i7 = 0; i8 = 0; i9 = 0; i10 = 0; i11 = 0; i12 = 0; i13 = 0; i14 = 0; i15 = 0; i16 = 0; i17 = 0; i18 = 0; i19 = 0; i20 = 0; i21 = 0; i22 = 0; i23 = 0; i24 = 0; i25 = 0; i26 = 0; i27 = 0; i28 = 0; i29 = 0; i30 = 0; i31 = 0;
            }
            if (cmbsales.Text == "September")
            {
                MONTH = 09;
                METHOD();
                showColumnChart30();
                i1 = 0; i2 = 0; i3 = 0; i4 = 0; i5 = 0; i6 = 0; i7 = 0; i8 = 0; i9 = 0; i10 = 0; i11 = 0; i12 = 0; i13 = 0; i14 = 0; i15 = 0; i16 = 0; i17 = 0; i18 = 0; i19 = 0; i20 = 0; i21 = 0; i22 = 0; i23 = 0; i24 = 0; i25 = 0; i26 = 0; i27 = 0; i28 = 0; i29 = 0; i30 = 0;i31 = 0;
            }
            if (cmbsales.Text == "October")
            {
                MONTH = 10;
                METHOD();
                showColumnChart();
                i1 = 0; i2 = 0; i3 = 0; i4 = 0; i5 = 0; i6 = 0; i7 = 0; i8 = 0; i9 = 0; i10 = 0; i11 = 0; i12 = 0; i13 = 0; i14 = 0; i15 = 0; i16 = 0; i17 = 0; i18 = 0; i19 = 0; i20 = 0; i21 = 0; i22 = 0; i23 = 0; i24 = 0; i25 = 0; i26 = 0; i27 = 0; i28 = 0; i29 = 0; i30 = 0; i31 = 0;
            }
            if (cmbsales.Text == "November")
            {
                MONTH = 11;
                METHOD();
                showColumnChart30();
                i1 = 0; i2 = 0; i3 = 0; i4 = 0; i5 = 0; i6 = 0; i7 = 0; i8 = 0; i9 = 0; i10 = 0; i11 = 0; i12 = 0; i13 = 0; i14 = 0; i15 = 0; i16 = 0; i17 = 0; i18 = 0; i19 = 0; i20 = 0; i21 = 0; i22 = 0; i23 = 0; i24 = 0; i25 = 0; i26 = 0; i27 = 0; i28 = 0; i29 = 0; i30 = 0; i31 = 0;
            }
            if (cmbsales.Text == "December")
            {
                MONTH = 12;
                METHOD();
                showColumnChart();
                i1 = 0; i2 = 0; i3 = 0; i4 = 0; i5 = 0; i6 = 0; i7 = 0; i8 = 0; i9 = 0; i10 = 0; i11 = 0; i12 = 0; i13 = 0; i14 = 0; i15 = 0; i16 = 0; i17 = 0; i18 = 0; i19 = 0; i20 = 0; i21 = 0; i22 = 0; i23 = 0; i24 = 0; i25 = 0; i26 = 0; i27 = 0; i28 = 0; i29 = 0; i30 = 0; i31 = 0;
            }
        }
        public static string DATE;
        public void METHOD()
        {
            List<KeyValuePair<string, int>> ValueList = new List<KeyValuePair<string, int>>();
            int days = DateTime.DaysInMonth(YEAR1, MONTH);
            for (int j = 1; j <= days; j++)
            {
                if (j==1)
                {
                    DATE = (YEAR1+"-"+MONTH+"-"+j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    { }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i1 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("01", i1));
                    }
                }
                if (j==2)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i2 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("02", i2));
                    }
                }
                if (j==3)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i3 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("03", i3));
                    }
                }
                if (j==4)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i4 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("04", i4));
                    }
                }
                if (j == 5)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i5 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("05", i5));
                    }
                }
                if (j == 6)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i6 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("06", i6));
                    }
                }
                if (j == 7)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i7 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("07", i7));
                    }
                }
                if (j == 8)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i8 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("08", i8));
                    }
                }
                if (j == 9)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i9 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("09", i9));
                    }
                }
                if (j == 10)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i10 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("10", i10));
                    }
                }
                if (j == 11)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i11 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("11", i11));
                    }
                }
                if (j == 12)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i12 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("12", i12));
                    }
                }
                if (j == 13)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i13 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("13", i13));
                    }
                }
                if (j == 14)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i14 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("14", i14));
                    }
                }
                if (j == 15)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i15 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("15", i15));
                    }
                }
                if (j == 16)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i16 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("16", i16));
                    }
                }
                if (j == 17)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i17 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("17", i17));
                    }
                }
                if (j == 18)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i18 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("18", i18));
                    }
                }
                if (j == 19)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i19 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("19", i19));
                    }
                }
                if (j == 20)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i20 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("20", i20));
                    }
                }
                if (j == 21)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i21 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("21", i21));
                    }
                }
                if (j == 22)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i22 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("22", i22));
                    }
                }
                if (j == 23)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i23 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("23", i23));
                    }
                }
                if (j == 24)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i24 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("24", i24));
                    }
                }
                if (j == 25)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i25 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("25", i25));
                    }
                }
                if (j == 26)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i26 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("26", i26));
                    }
                }
                if (j == 27)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i27 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("27", i27));
                    }
                }
                if (j == 28)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i28 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("28", i28));
                    }
                }
                if (j == 29)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i29 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("29", i29));
                    }
                }
                if (j == 30)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i30 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("30", i30));
                    }
                }
                if (j == 31)
                {
                    DATE = (YEAR1 + "-" + MONTH + "-" + j).ToString();
                    DS = DB.GETDATA();
                    if (DS.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        VALS = DS.Rows[0]["Total"].ToString();
                        i31 = int.Parse(VALS);
                        ValueList.Add(new KeyValuePair<string, int>("31", i31));
                    }
                }
            }
        }
        public static string date;
        private void showColumnChart()
        {
            List<KeyValuePair<string, int>> ValueList = new List<KeyValuePair<string, int>>();
            ValueList.Add(new KeyValuePair<string, int>("01", i1));
            ValueList.Add(new KeyValuePair<string, int>("02", i2));
            ValueList.Add(new KeyValuePair<string, int>("03", i3));
            ValueList.Add(new KeyValuePair<string, int>("04", i4));
            ValueList.Add(new KeyValuePair<string, int>("05", i5));
            ValueList.Add(new KeyValuePair<string, int>("06", i6));
            ValueList.Add(new KeyValuePair<string, int>("07", i7));
            ValueList.Add(new KeyValuePair<string, int>("08", i8));
            ValueList.Add(new KeyValuePair<string, int>("09", i9));
            ValueList.Add(new KeyValuePair<string, int>("10", i10));
            ValueList.Add(new KeyValuePair<string, int>("11", i11));
            ValueList.Add(new KeyValuePair<string, int>("12", i12));
            ValueList.Add(new KeyValuePair<string, int>("13", i13));
            ValueList.Add(new KeyValuePair<string, int>("14", i14));
            ValueList.Add(new KeyValuePair<string, int>("15", i15));
            ValueList.Add(new KeyValuePair<string, int>("16", i16));
            ValueList.Add(new KeyValuePair<string, int>("17", i17));
            ValueList.Add(new KeyValuePair<string, int>("18", i18));
            ValueList.Add(new KeyValuePair<string, int>("19", i19));
            ValueList.Add(new KeyValuePair<string, int>("20", i20));
            ValueList.Add(new KeyValuePair<string, int>("21", i21));
            ValueList.Add(new KeyValuePair<string, int>("22", i22));
            ValueList.Add(new KeyValuePair<string, int>("23", i23));
            ValueList.Add(new KeyValuePair<string, int>("24", i24));
            ValueList.Add(new KeyValuePair<string, int>("25", i25));
            ValueList.Add(new KeyValuePair<string, int>("26", i26));
            ValueList.Add(new KeyValuePair<string, int>("27", i27));
            ValueList.Add(new KeyValuePair<string, int>("28", i28));
            ValueList.Add(new KeyValuePair<string, int>("29", i29));
            ValueList.Add(new KeyValuePair<string, int>("30", i30));
            ValueList.Add(new KeyValuePair<string, int>("31", i31));

           //Setting data for column chart
            columnChart.DataContext = ValueList;
        }
        private void showColumnChart30()
        {
            List<KeyValuePair<string, int>> ValueList = new List<KeyValuePair<string, int>>();
            ValueList.Add(new KeyValuePair<string, int>("01", i1));
            ValueList.Add(new KeyValuePair<string, int>("02", i2));
            ValueList.Add(new KeyValuePair<string, int>("03", i3));
            ValueList.Add(new KeyValuePair<string, int>("04", i4));
            ValueList.Add(new KeyValuePair<string, int>("05", i5));
            ValueList.Add(new KeyValuePair<string, int>("06", i6));
            ValueList.Add(new KeyValuePair<string, int>("07", i7));
            ValueList.Add(new KeyValuePair<string, int>("08", i8));
            ValueList.Add(new KeyValuePair<string, int>("09", i9));
            ValueList.Add(new KeyValuePair<string, int>("10", i10));
            ValueList.Add(new KeyValuePair<string, int>("11", i11));
            ValueList.Add(new KeyValuePair<string, int>("12", i12));
            ValueList.Add(new KeyValuePair<string, int>("13", i13));
            ValueList.Add(new KeyValuePair<string, int>("14", i14));
            ValueList.Add(new KeyValuePair<string, int>("15", i15));
            ValueList.Add(new KeyValuePair<string, int>("16", i16));
            ValueList.Add(new KeyValuePair<string, int>("17", i17));
            ValueList.Add(new KeyValuePair<string, int>("18", i18));
            ValueList.Add(new KeyValuePair<string, int>("19", i19));
            ValueList.Add(new KeyValuePair<string, int>("20", i20));
            ValueList.Add(new KeyValuePair<string, int>("21", i21));
            ValueList.Add(new KeyValuePair<string, int>("22", i22));
            ValueList.Add(new KeyValuePair<string, int>("23", i23));
            ValueList.Add(new KeyValuePair<string, int>("24", i24));
            ValueList.Add(new KeyValuePair<string, int>("25", i25));
            ValueList.Add(new KeyValuePair<string, int>("26", i26));
            ValueList.Add(new KeyValuePair<string, int>("27", i27));
            ValueList.Add(new KeyValuePair<string, int>("28", i28));
            ValueList.Add(new KeyValuePair<string, int>("29", i29));
            ValueList.Add(new KeyValuePair<string, int>("30", i30));
            //ValueList.Add(new KeyValuePair<string, int>("31", i31));

            //Setting data for column chart
            columnChart.DataContext = ValueList;
        }
        private void showColumnChart28()
        {
            List<KeyValuePair<string, int>> ValueList = new List<KeyValuePair<string, int>>();
            ValueList.Add(new KeyValuePair<string, int>("01", i1));
            ValueList.Add(new KeyValuePair<string, int>("02", i2));
            ValueList.Add(new KeyValuePair<string, int>("03", i3));
            ValueList.Add(new KeyValuePair<string, int>("04", i4));
            ValueList.Add(new KeyValuePair<string, int>("05", i5));
            ValueList.Add(new KeyValuePair<string, int>("06", i6));
            ValueList.Add(new KeyValuePair<string, int>("07", i7));
            ValueList.Add(new KeyValuePair<string, int>("08", i8));
            ValueList.Add(new KeyValuePair<string, int>("09", i9));
            ValueList.Add(new KeyValuePair<string, int>("10", i10));
            ValueList.Add(new KeyValuePair<string, int>("11", i11));
            ValueList.Add(new KeyValuePair<string, int>("12", i12));
            ValueList.Add(new KeyValuePair<string, int>("13", i13));
            ValueList.Add(new KeyValuePair<string, int>("14", i14));
            ValueList.Add(new KeyValuePair<string, int>("15", i15));
            ValueList.Add(new KeyValuePair<string, int>("16", i16));
            ValueList.Add(new KeyValuePair<string, int>("17", i17));
            ValueList.Add(new KeyValuePair<string, int>("18", i18));
            ValueList.Add(new KeyValuePair<string, int>("19", i19));
            ValueList.Add(new KeyValuePair<string, int>("20", i20));
            ValueList.Add(new KeyValuePair<string, int>("21", i21));
            ValueList.Add(new KeyValuePair<string, int>("22", i22));
            ValueList.Add(new KeyValuePair<string, int>("23", i23));
            ValueList.Add(new KeyValuePair<string, int>("24", i24));
            ValueList.Add(new KeyValuePair<string, int>("25", i25));
            ValueList.Add(new KeyValuePair<string, int>("26", i26));
            ValueList.Add(new KeyValuePair<string, int>("27", i27));
            ValueList.Add(new KeyValuePair<string, int>("28", i28));
            //ValueList.Add(new KeyValuePair<string, int>("29", i29));
            //ValueList.Add(new KeyValuePair<string, int>("30", i30));
            //ValueList.Add(new KeyValuePair<string, int>("31", i31));

            //Setting data for column chart
            columnChart.DataContext = ValueList;
        }
    }
}
