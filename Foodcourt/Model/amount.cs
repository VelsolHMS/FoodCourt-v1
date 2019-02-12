using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Foodcourt.Model
{
    class amount
    {
        Pettycash pc = new Pettycash();
        public static decimal paidtotal, mistotal, cashtotal,pettotal,billamount, total, total1, tot;
        public void Amount()
        {
            cashtotal = 0; paidtotal = 0; mistotal = 0; pettotal = 0;
            DataTable dt = PAID();
            paidtotal = Convert.ToInt32(dt.Rows[0][0].ToString());
            DataTable dt1 = mis();
            mistotal = Convert.ToDecimal(dt1.Rows[0][0].ToString());
            DataTable dt2 = CASHHAND();
            cashtotal = Convert.ToDecimal(dt2.Rows[0][0].ToString());
            DataTable dt3 = PETTY();
            pettotal = Convert.ToDecimal(dt3.Rows[0][0].ToString());
            DataTable dt4 = BillTransactions();
            billamount = Convert.ToDecimal(dt4.Rows[0][0].ToString());
            total = pettotal + mistotal+billamount;
            total1 = paidtotal + cashtotal;
            tot = total - total1;
            if (tot == 0)
            {
            }
            else
            {
                pc.PCH_Name = "Pettycash";
                pc.PCH_Amount = tot.ToString();
                pc.INSERT1();
            }  
        }
        public DataTable PETTY()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT SUM(PCH_Amount)AS PCH_Amount FROM FCPETCSH WHERE DAY(PCH_InsertDate) = DAY(GETDATE()-1)";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable mis()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT SUM(MIS_Amount) AS MIS_Amount FROM FCMISSALES WHERE DAY(MIS_InsertDate) = DAY(GETDATE()-1)";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable CASHHAND()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT SUM(CH_Amount) AS CH_Amount FROM FCCashHandover WHERE DAY(CH_InsertDate) = DAY(GETDATE()-1)";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable PAID()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT SUM(PO_Amount) AS PO_Amount FROM FCPaidout WHERE DAY(PO_InsertDate) = DAY(GETDATE()-1)";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable BillTransactions()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT SUM(BILL_Total) AS BILL_Total FROM FCBILLNO WHERE DAY(BILL_InsertDate) = DAY(GETDATE()-1)";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
    }
}
