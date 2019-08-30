using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL;
using Foodcourt.View.Oprs;

namespace Foodcourt.Model
{
    class DASHBOARD
    {
        public DataTable GETDAYBILL()
        {
            var LIST = new List<SqlParameter>();
            string S = "SELECT CONVERT(decimal(17,2),BILL_Total) AS BILL_Total from FCBILLNO WHERE BILL_InsertDate = GETDATE() AND BILL_Status='Settled'";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(S, LIST);
            return DT;
        }
        public DataTable GETMONTHBILL()
        {
            var LIST = new List<SqlParameter>();
            string S = "SELECT CONVERT(decimal(17,2),BILL_Total) AS BILL_Total from FCBILLNO WHERE MONTH(BILL_InsertDate)='" + DashBoard.i+ "' AND Year(BILL_InsertDate) = Year(GETDATE()) AND BILL_Status='Settled'";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(S, LIST);
            return DT;
        }
        public DataTable GETYEARBILL()
        {
            var LIST = new List<SqlParameter>();
            string S = "SELECT CONVERT(decimal(17,2),BILL_Total) AS BILL_Total from FCBILLNO WHERE YEAR(BILL_InsertDate)=YEAR(GETDATE()) AND BILL_Status='Settled'";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(S, LIST);
            return DT;
        }
        public DataTable GETDATA()
        {
            var LIST = new List<SqlParameter>();
            string S = "SELECT DISTINCT (SELECT SUM(BILL_Total) FROM FCBILLNO WHERE BILL_InsertDate=A.BILL_InsertDate AND BILL_Status='Settled') AS Total,BILL_InsertDate as Date FROM FCBILLNO A WHERE A.BILL_InsertDate  = '" + DashBoard.DATE + "' AND BILL_Status='Settled'";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(S, LIST);
            return DT;
        }
    }
}
