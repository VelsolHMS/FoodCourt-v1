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
    class reportClass1
    {

        public string dateday { get; set; }
        public DataTable daywise()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT A.BILL_Id,A.BILL_Amount,A.BILL_Tax,A.BILL_Total,A.BILL_InsertBy FROM FCBILLNO A WHERE A.BILL_InsertDate ='"+ dateday +"'";
            DataTable d = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return d;
        }
        public DataTable daywise1()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT SUM(BILL_Amount) AS AMOUNT,SUM(BILL_Tax) AS TAX FROM FCBILLNO WHERE BILL_InsertDate='" + dateday + "'";
            DataTable d = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return d;
        }
        public DataTable Address()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT PRPT_Name,PRPT_Address,PRPT_GST,(SELECT SUM(BILL_Amount) FROM FCBILLNO WHERE BILL_InsertDate ='" + billwiserpt.DA + "'  )AS AMOUNT,(SELECT SUM(BILL_Tax) FROM FCBILLNO WHERE BILL_InsertDate ='" + billwiserpt.DA + "'  ) AS TAX,(SELECT SUM(BILL_Total) FROM FCBILLNO WHERE BILL_InsertDate ='" + billwiserpt.DA + "' ) AS TOTAL  FROM FCPRPTY";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable TAX()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT DISTINCT A.BILL_InsertDate,(SELECT SUM(BILL_Tax) FROM FCBILLNO WHERE BILL_InsertDate=A.BILL_InsertDate) AS tax FROM FCBILLNO A WHERE A.BILL_InsertDate BETWEEN '"+ taxrpt.date + "' AND '"+ taxrpt.date1 + "'";
            DataTable d = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return d;
        }
        public DataTable TAX1()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT SUM(BILL_Tax) AS TOTALTAX FROM FCBILLNO A WHERE A.BILL_InsertDate BETWEEN '" + taxrpt.date+"' AND '"+taxrpt.date1+"'";
            DataTable d = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return d;
        }
        public DataTable BILL()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT BILITM_ID,BILITM_InsertDate,BILITM_Rate,BILITM_Tax,BILLITM_Quanty,BILL_Id,BILITM_Name FROM FCBILLITM WHERE BILITM_InsertDate='" + billwiserpt.DA + "' ORDER BY BILITM_InsertDate ASC";
            DataTable d = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return d;
        }
        public DataTable BILL1()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT SUM(BILL_Amount) AS AMOUNT,SUM(BILL_Tax) AS TAX,SUM(BILL_Total) AS TOTAL FROM FCBILLNO WHERE BILL_InsertDate ='" + billwiserpt.DA + "' ";
            DataTable d = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return d;
        }
    }
}
