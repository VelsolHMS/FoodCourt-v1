using DAL;
using Foodcourt.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodcourt.Model
{
    class Reports
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string SelectedDate { get; set; }
        public static string Name, GST, Address;
        public DataTable PRPT()
        {
            var list = new List<SqlParameter>();
            string s = "Select PRPT_Name,PRPT_GST,CONCAT(PRPT_Address, ', ', PRPT_State, ', ', PRPT_Country, '.') AS PRPT_Address from FCPRPTY";
            DataTable d = DbFunctions.ExecuteCommand<DataTable>(s, list);
            if (d.Rows.Count == 0)
            {
            }
            else
            {
                Name = d.Rows[0]["PRPT_Name"].ToString();
                Address = d.Rows[0]["PRPT_Address"].ToString();
                GST = d.Rows[0]["PRPT_GST"].ToString();
            }
            return d;
        }
        public DataTable ItemRates()
        {
            var list = new List<SqlParameter>();
            string s = "Select NAM_Name,NAM_Rate,(select TAX_Percentage from FCTAX where TAX_Name = A.NAM_Tax) AS NAM_TaxPer from FCITMNAM A";
            DataTable d = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return d;
        }
        public DataTable DayWiseBills()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT A.BILL_Id,A.BILL_Amount,A.BILL_Tax,A.BILL_Total,A.BILL_InsertBy FROM FCBILLNO A WHERE A.BILL_InsertDate ='" + SelectedDate + "'";
            DataTable d = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return d;
        }
        public DataTable pettycash()
        {
            var list = new List<SqlParameter>();
            string s = "select PCH_Name,PCH_Amount,PCH_InsertBy,PCH_InsertDate from FCPETCSH WHERE PCH_InsertDate between '" + Pettycashrpt.date + "' And '" + Pettycashrpt.date1 + "'";
            DataTable d = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return d;
        }
        public DataTable paidoutcash()
        {
            var list = new List<SqlParameter>();
            string s = "select PO_Name,PO_Amount,PO_InsertBY,PO_InsertDate from FCPaidout where PO_InsertDate between '" + Paidoutrpt.date + "' And '" + Paidoutrpt.date1 + "'";
            DataTable d = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return d;
        }
        public DataTable miscollection()
        {
            var list = new List<SqlParameter>();
            string s = "select MIS_Name,MIS_Amount,MIS_InsertBY,MIS_InsertDate from FCMISSALES WHERE MIS_InsertDate BETWEEN '" + Miscollectiorpt.date + "' And '" + Miscollectiorpt.date1 + "'";
            DataTable d = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return d;
        }
        public DataTable CASHHAND()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT CH_Name, CH_Amount, CH_InsertBY, CH_InsertDate FROM FCCashHandover WHERE CH_InsertDate BETWEEN '" + Cashhandrpt.date + "' And '" + Cashhandrpt.date1 + "'";
            DataTable d = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return d;
        }
        public DataTable DayWiseBillsTotal()
        {
            var list = new List<SqlParameter>();
            string s = "Select Sum(BILL_Amount) As Bill_Amount,Sum(BILL_Tax) as BILL_Tax, Sum(BILL_Total) as BILL_Total from FCBILLNO where BILL_InsertDate = '" + SelectedDate + "'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public string STL_Name { get; set; }
        public DataTable DayWiseItemSale()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT DISTINCT A.BILITM_Name AS ITEM_NAME,"+
                "(SELECT SUM(BILLITM_Quanty) FROM FCBILLITM WHERE BILITM_Name = A.BILITM_Name AND BILITM_InsertDate ='" + SelectedDate + "' AND (A.BILL_Id = (SELECT BILL_Id FROM FCBILLNO B WHERE A.BILL_Id = BILL_Id And BILL_Status = 'Settled'))) AS QTY," +
                "(SELECT SUM(Discount) FROM FCBILLITM WHERE BILITM_Name = A.BILITM_Name AND BILITM_InsertDate = '" + SelectedDate + "' AND (A.BILL_Id = (SELECT BILL_Id FROM FCBILLNO B WHERE A.BILL_Id = BILL_Id And BILL_Status = 'Settled'))) AS Discount," +
                "(SELECT NAM_Rate FROM FCITMNAM WHERE NAM_Name = A.BILITM_Name AND (A.BILL_Id = (SELECT BILL_Id FROM FCBILLNO B WHERE A.BILL_Id = BILL_Id And BILL_Status = 'Settled'))) AS RATE," +
                "(SELECT Sum(BILITM_Tax) FROM FCBILLITM WHERE BILITM_Name = A.BILITM_Name AND BILITM_InsertDate = '" + SelectedDate + "' AND (A.BILL_Id = (SELECT BILL_Id FROM FCBILLNO B WHERE A.BILL_Id = BILL_Id And BILL_Status = 'Settled'))) AS TAXRATE " +
                " FROM FCBILLITM A where(A.BILL_Id = (SELECT BILL_Id FROM FCBILLNO B WHERE A.BILL_Id = BILL_Id And BILL_Status = 'Settled') And A.STL_ID = (select STL_ID from FCSTALLS where STL_Name = '"+ STL_Name + "')) AND A.BILITM_InsertDate = '" + SelectedDate + "' ORDER BY A.BILITM_Name ASC";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable MonthWiseBills()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT A.BILL_Id,A.BILL_Amount,A.BILL_Tax,A.BILL_Total,A.BILL_InsertBy,A.BILL_InsertDate,A.BILL_Discount,A.Bill_InstantDis,(Select OFF_Name from FCOFFERS where OFF_ID = A.Bill_OfferId) As Offer_Name FROM FCBILLNO A WHERE A.BILL_InsertDate Between '" + FromDate+"' AND '"+ToDate+ "' AND BILL_Status = 'Settled'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable MonthWiseBillsTotal()
        {
            var list = new List<SqlParameter>();
            string s = "Select Sum(BILL_Amount) As Bill_Amount,Sum(BILL_Tax) as BILL_Tax, Sum(BILL_Total) as BILL_Total,(SUM(BILL_Discount) + Sum(Bill_InstantDis)) as BILL_Dis from FCBILLNO where BILL_InsertDate Between '" + FromDate + "' AND '" + ToDate + "' AND BILL_Status = 'Settled'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
    }
}
