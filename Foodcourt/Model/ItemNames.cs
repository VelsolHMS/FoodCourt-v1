using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL;
using Foodcourt.View;

namespace Foodcourt.Model
{
    class ItemNames
    {
        public string NAM_Id { get; set; }
        public string NAM_Name { get; set; }
        public string NAM_Details { get; set; }
        public decimal NAM_Rate { get; set; }
        public string NAM_Tax { get; set; }
        public string CTG_Id { get; set; }
        public string CTG_Name { get; set; }
        public DateTime NAM_ActiveFrom { get; set; }
        public string NAM_Status { get; set; }
        public string NAM_ReportingName { get; set; }
        public DateTime NAM_InsertDate { get; set; }
        public string NAM_InsertBY { get; set; }

        public void Insert()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@NAM_Name", NAM_Name);
            list.AddSqlParameter("@STL_ID", STL_ID);
            list.AddSqlParameter("@NAM_Details", NAM_Details);
            list.AddSqlParameter("@CTG_Name", CTG_Name);
            list.AddSqlParameter("@NAM_Rate", NAM_Rate);
            list.AddSqlParameter("@NAM_Tax", NAM_Tax);
            list.AddSqlParameter("@NAM_ReportingName", NAM_ReportingName);
            list.AddSqlParameter("@NAM_Status", NAM_Status);
            list.AddSqlParameter("@NAM_ActiveFrom", DateTime.Today.Date);
            list.AddSqlParameter("@NAM_InsertDate", DateTime.Today.ToShortDateString());
            list.AddSqlParameter("@NAM_InsertBY", login.u);
            string s = "INSERT INTO FCITMNAM(NAM_Name,STL_ID,NAM_Details,CTG_Id,NAM_Rate,NAM_Tax,NAM_ReportingName,NAM_Status,NAM_ActiveFrom,NAM_InsertDate,NAM_InsertBY)" +
                       "VALUES (@NAM_Name,@STL_ID,@NAM_Details,(select CTG_Id from FCRITMCTG where CTG_Name = @CTG_Name),@NAM_Rate,@NAM_Tax,@NAM_ReportingName,@NAM_Status,@NAM_ActiveFrom,@NAM_InsertDate,@NAM_InsertBY)";
            DbFunctions.ExecuteCommand<DataTable>(s, list);
        }
        public void Update()
        {
            var lists = new List<SqlParameter>();
            lists.AddSqlParameter("@NAM_Name", NAM_Name);
            lists.AddSqlParameter("@NAM_Details", NAM_Details);
            lists.AddSqlParameter("@CTG_Name", CTG_Name);
            lists.AddSqlParameter("@NAM_Rate", NAM_Rate);
            lists.AddSqlParameter("@NAM_Tax", NAM_Tax);
            lists.AddSqlParameter("@NAM_ReportingName", NAM_ReportingName);
            lists.AddSqlParameter("@NAM_ActiveFrom", DateTime.Today.Date);
            lists.AddSqlParameter("@NAM_Status", NAM_Status);
            lists.AddSqlParameter("@NAM_InsertDate", DateTime.Today.ToShortDateString());
            lists.AddSqlParameter("@NAM_InsertBY", login.u);
            string s = "UPDATE FCITMNAM SET NAM_Name = @NAM_Name,NAM_Details = @NAM_Details,CTG_Id = (select CTG_Id from FCRITMCTG where CTG_Name = @CTG_Name),NAM_Rate = @NAM_Rate,NAM_Tax = @NAM_Tax," +
                            "NAM_ReportingName = @NAM_ReportingName,NAM_Status = @NAM_Status,NAM_ActiveFrom = @NAM_ActiveFrom,NAM_InsertDate = @NAM_InsertDate,NAM_InsertBY = @NAM_InsertBY WHERE NAM_Id = '" + NAM_Id + "'";
            DbFunctions.ExecuteCommand<int>(s, lists);
        }
        public int GetItemId()
        {
            int a = 0;
            string S = "SELECT MAX(NAM_Id) FROM FCITMNAM";
            var L = new List<SqlParameter>();
            object c = DbFunctions.ExecuteCommand<object>(S, L);
            if (c == System.DBNull.Value)
            {
                a = 1;
            }
            else
            {
                a = Convert.ToInt32(c);
                a = a + 1;
            }
            return a;
        }
        public DataTable FillGrid()
        {
            var list = new List<SqlParameter>();
            string d = "SELECT NAM_Id,NAM_Name,(select CTG_Name from FCRITMCTG WHERE CTG_Id =A.CTG_Id ) AS CTG_Name,NAM_Tax,NAM_Rate,NAM_Status FROM FCITMNAM A";
            DataTable S = DbFunctions.ExecuteCommand<DataTable>(d, list);
            return S;
        }
        public DataTable GetAllDetails()
        {
            var list = new List<SqlParameter>();
            string d = "SELECT * FROM FCITMNAM WHERE NAM_Id = '" + NAM_Id + "'";
            DataTable S = DbFunctions.ExecuteCommand<DataTable>(d, list);
            return S;
        }
        public DataTable getstlid()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT STL_ID,CTG_Name FROM FCRITMCTG WHERE CTG_Name='" + CTG_Name + "'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public string STL_ID { get; set; }
        public string date { get; set; }
        public string date1 { get; set; }
        public string STL_Name { get; set; }
        public DataTable itemwise()
        {
            var list = new List<SqlParameter>();
            //string s = "SELECT DISTINCT A.BILITM_Name AS ITEM_NAME,A.BILLITM_Quanty AS QTY,(SELECT BILL_Amount FROM FCBILLNO WHERE BILL_Id=A.BILL_ID) AS TOTAL_AMOUNT,(SELECT BILL_Tax FROM FCBILLNO WHERE BILL_Id=A.BILL_ID) AS TAX,(SELECT BILL_Total FROM FCBILLNO WHERE BILL_Id=A.BILL_ID) AS GRANDTOTAL,A.BILITM_InsertDate AS DATE FROM FCBILLITM A WHERE  A.BILITM_InsertDate BETWEEN '" + date+"' AND '"+date1+ "' ORDER BY A.BILITM_InsertDate ASC ";
            
            string s = "SELECT DISTINCT A.BILITM_Name AS ITEM_NAME,"+
                "(SELECT  SUM(BILLITM_Quanty) FROM FCBILLITM WHERE BILITM_Name = A.BILITM_Name AND BILITM_InsertDate BETWEEN '" + date + "' AND '" + date1 + "' AND BILL_Id = A.BILL_Id) AS QTY," +
                "(SELECT SUM(Discount) FROM FCBILLITM WHERE BILITM_Name = A.BILITM_Name AND BILITM_InsertDate BETWEEN '" + date + "' AND '" + date1 + "' AND BILL_Id = A.BILL_Id) AS Discount," +
                "(SELECT NAM_Rate FROM FCITMNAM WHERE NAM_Name = A.BILITM_Name AND BILL_Id = A.BILL_Id) AS RATE,"+
                "(SELECT BILL_Status FROM FCBILLNO WHERE A.BILL_Id = BILL_Id And BILL_Status = 'Settled')as BILL_Status,"+
                "(SELECT sum(BILITM_Tax) FROM FCBILLITM WHERE BILITM_Name = A.BILITM_Name AND BILITM_InsertDate BETWEEN '" + date + "' AND '" + date1 + "' AND BILL_Id = A.BILL_Id) AS TAXRATE" +
                " FROM FCBILLITM A where(A.BILL_Id = (SELECT BILL_Id FROM FCBILLNO B WHERE A.BILL_Id = BILL_Id And BILL_Status = 'Settled') And A.STL_ID = (select STL_ID from FCSTALLS where STL_Name = '" + STL_Name + "')) AND A.BILITM_InsertDate BETWEEN '" + date + "' AND '" + date1 + "' ORDER BY A.BILITM_Name ASC";
            DataTable d = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return d;
        }
        public DataTable Address()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT PRPT_Name,PRPT_Address,PRPT_GST FROM FCPRPTY";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable NameChecking()
        {
            var list = new List<SqlParameter>();
            string s = "select * from FCITMNAM where NAM_Name = '"+ NAM_Name + "'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public string selectedItemName { get; set; }
        public void UpdateItemInBillItm()
        {
            var lists = new List<SqlParameter>();
            string s = "update FCBILLITM set BILITM_Name = '"+ NAM_Name + "' where BILITM_Name = '"+ selectedItemName + "'";
            DbFunctions.ExecuteCommand<int>(s, lists);
        }
    }
}
