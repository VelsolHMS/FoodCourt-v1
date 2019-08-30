using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Foodcourt.View.Oprs;
using DAL;

namespace Foodcourt.Model
{
    class BillVIEW
    {
        public string BILL_Id { get; set; }
        public string BILL_Amount { get; set; }
        public string BILL_Tax { get; set; }
        public string BILL_Total { get; set; }
        public string BILL_Discount { get; set; }
        public DateTime BILL_InsertDate { get; set; }
        public string BILL_Status { get; set; }
        public DateTime SelectedDate { get; set; } 
        public string SelectedBillNo { get; set; }
        public DataTable GetBillWithBillNo()
        {
            var LIST = new List<SqlParameter>();
            LIST.AddSqlParameter("@SelectedBillNo", SelectedBillNo);
            string S = "Select BILL_Id,CONVERT(decimal(17,2),BILL_Amount) AS BILL_Amount,BILL_Tax,CONVERT(decimal(17,2),BILL_Total) AS BILL_Total ,(BILL_Discount+Bill_InstantDis) as Dis_TOTAL,CONVERT(varchar ,BILL_InsertDate ,101) AS BILL_InsertDate from FCBILLNO WHERE BILL_Status = 'Settled' and BILL_Id = @SelectedBillNo";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(S, LIST);
            return DT;
        }
        public DataTable GetBillWithDate()
        {
            var LIST = new List<SqlParameter>();
            LIST.AddSqlParameter("@SelectedDate", SelectedDate);
            string S = "Select BILL_Id,CONVERT(decimal(17,2),BILL_Amount) AS BILL_Amount,BILL_Tax,CONVERT(decimal(17,2),BILL_Total) AS BILL_Total ,(BILL_Discount+Bill_InstantDis) as Dis_TOTAL,BILL_InsertDate from FCBILLNO WHERE BILL_Status = 'Settled' and BILL_InsertDate = @SelectedDate";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(S, LIST);
            return DT;
        }
        public DataTable GETBILL()
        {
            var LIST = new List<SqlParameter>();
            string S = "select BILL_Id,CONVERT(decimal(17,2),BILL_Amount) AS BILL_Amount,BILL_Tax,CONVERT(decimal(17,2),BILL_Total) AS BILL_Total ,BILL_Discount,BILL_InsertDate from FCBILLNO WHERE BILL_Status = 'Settled'";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(S, LIST);
            return DT;
        }
        public DataTable GETITMNAM() 
        {
            var LIST = new List<SqlParameter>();
            string SS = "SELECT BILITM_Name,BILLITM_Quanty,CONVERT(decimal(17,2),BILITM_Rate) AS BILITM_Rate ,BILITM_Tax, Discount, DiscountPer FROM FCBILLITM WHERE BILL_Id='" + BillView.B_bill_no + "'";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(SS, LIST);
            return DT;
        }
        public DataTable Address()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT PRPT_Name, PRPT_Address, PRPT_GST FROM FCPRPTY";
            DataTable D = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return D;
        }
        public void billcancel()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@BILL_Status", BILL_Status);
            string s = "UPDATE FCBILLNO SET BILL_Status = 'Canceled' WHERE BILL_Id = '"+ BillView.A+ "'";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public DataTable items()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT BILITM_Name,BILLITM_Quanty,BILITM_Rate,BILITM_Tax FROM FCBILLITM WHERE BILL_Id='" + BillView.A + "'";
            DataTable D = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return D;
        }
        public DataTable itemstot()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT CONVERT(decimal(17,2),BILL_Amount) AS BILL_Amount,BILL_Tax,CONVERT(decimal(17,2),BILL_Total) AS BILL_Total FROM FCBILLNO WHERE BILL_Id='" + BillView.A + "'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable cancel()
        {
            var list = new List<SqlParameter>();
            string s = "select BILL_Id,CONVERT(decimal(17,2),BILL_Amount) AS BILL_Amount ,BILL_Tax, CONVERT(decimal(17,2),BILL_Total) AS BILL_Total ,BILL_Discount from FCBILLNO WHERE BILL_Status = 'Canceled'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable pending()
        {
            var list = new List<SqlParameter>();
            string s = "select BILL_Id,CONVERT(decimal(17,2),BILL_Amount) AS BILL_Amount ,BILL_Tax, CONVERT(decimal(17,2),BILL_Total) AS BILL_Total ,BILL_Discount from FCBILLNO WHERE BILL_Status = 'Pending'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
    }
}
