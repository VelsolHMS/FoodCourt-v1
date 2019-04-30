using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;
using Foodcourt.View;
using Foodcourt.ViewModel;
using Foodcourt.Model;
using Foodcourt.View.Oprs;

namespace Foodcourt.Model
{
    class POS1
    {
        public string CTG_Name { get; set; }
        public int Total1 { get; set; }
        public DataTable GETCTG()
        {
            var LIST = new List<SqlParameter>();
            string S = "SELECT CTG_Name FROM FCRITMCTG WHERE CTG_ActiveDate <= GETDATE() AND CTG_Status='Active'";
            DataTable dt= DbFunctions.ExecuteCommand<DataTable>(S, LIST);
            return dt;
        }
        public DataTable GETPROPERTY()
        {
            var LIST = new List<SqlParameter>();
            string S = "SELECT PRPT_Name from FCPRPTY";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(S, LIST);
            return DT;
        }
        public DataTable GETNAME()
        {
            var LIST = new List<SqlParameter>();
            string SS = "SELECT NAM_Name FROM FCITMNAM Where CTG_Id =(SELECT CTG_Id FROM FCRITMCTG WHERE CTG_Name='" + POS.aa+ "') AND NAM_Status='Active'";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(SS, LIST);
            return DT;
        }
        public DataTable GETRATE()
        {
            var LIST = new List<SqlParameter>();
            string N = "SELECT NAM_Name, CONVERT(decimal(17,2),NAM_Rate) AS NAM_Rate  FROM FCITMNAM WHERE NAM_Name='" + POS.check1 + "'";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(N, LIST);
            return DT;
        }
        public DataTable GETRATE1()
        {
            var LIST = new List<SqlParameter>();
            string N = "SELECT NAM_Rate FROM FCITMNAM WHERE NAM_Name='" + POS.sa + "'";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(N, LIST);
            return DT;
        }
        public DataTable gsttax()
        {
            var LIST = new List<SqlParameter>();
            string dd = "select NAM_Tax FROM FCITMNAM WHERE NAM_Name='" + POS.check1 + "'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(dd, LIST);
            return dt;
        }
        public DataTable GETTAX()
        {
            var LIST = new List<SqlParameter>();
            string dd = "SELECT TAX_Percentage from FCTAX WHERE TAX_Name='"+POS.check2+"'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(dd, LIST);
            return dt;
        }
        public int BILLID()
        {
            int b = 0;
            string c = "select max(BILL_Id) from FCBILLNO";
            var list = new List<SqlParameter>();
            object o = DbFunctions.ExecuteCommand<object>(c, list);
            if (o == System.DBNull.Value)
            {
                b = 1;
            }
            else
            {
                b = Convert.ToInt32(o);
                b = b + 1;
            }
            return b;
        }
        public string BILL_Id { get; set; }
        public decimal BILL_Amount { get; set; }
        public decimal BILL_Tax { get; set; }
        public decimal BILL_Total { get; set; }
        public string BILL_InsertBy { get; set; }
        public string BILL_InsertDate { get; set; }
        public string BILL_UpdateBy { get; set; }
        public DateTime BILL_UpdateDate { get; set; }
        public string BILL_Status { get; set; }
        public decimal BILL_Discount { get; set; }

        public string CUST_ID { get; set; }
        public string NAME { get; set; }
        public string MOBILE_NO { get; set; }
        public string EMAIL { get; set; }
        public string CITY { get; set; }
        public string ADDRESS { get; set; }
        public string Insert_By { get; set; }
        public DateTime Insert_Date { get; set; }
        public string Update_By { get; set; }
        public DateTime Update_Date { get; set; }
        
        public void insertbill()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@BILL_Amount", BILL_Amount);
            list.AddSqlParameter("@BILL_Tax", BILL_Tax);
            list.AddSqlParameter("@BILL_Total", BILL_Total);
            BILL_InsertBy = login.u;
            list.AddSqlParameter("@BILL_InsertBy", BILL_InsertBy);
            BILL_InsertDate = DateTime.Today.ToShortDateString();
            list.AddSqlParameter("@BILL_InsertDate", BILL_InsertDate);
            BILL_Status = "Settled";
            list.AddSqlParameter("@BILL_Status", BILL_Status);
            list.AddSqlParameter("@BILL_Discount", BILL_Discount);
            string s = "INSERT INTO FCBILLNO(BILL_Amount,BILL_Tax,BILL_Total,BILL_InsertBy,BILL_InsertDate,BILL_Status,BILL_Discount)" +
                " VALUES(@BILL_Amount,@BILL_Tax,@BILL_Total,@BILL_InsertBy,@BILL_InsertDate,@BILL_Status,@BILL_Discount)";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public string BILLITM_Name { get; set; }
        public decimal BILLITM_Rate { get; set; }
        public decimal BILLITM_Tax { get; set; }
        public DateTime BILLITM_InsertDate { get; set; }
        public string BILLITM_InsertBy { get; set; }
        public int BILLITM_Quanty { get; set; }

        public void Insertitm()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@BILLITM_Name", BILLITM_Name);
            list.AddSqlParameter("@BILLITM_Rate", BILLITM_Rate);
            list.AddSqlParameter("@BILLITM_Tax", BILLITM_Tax);
            BILLITM_InsertDate = DateTime.Today.Date;
            list.AddSqlParameter("@BILLITM_InsertDate", BILLITM_InsertDate);
            BILLITM_InsertBy = login.u;
            list.AddSqlParameter("@BILLITM_InsertBy", BILLITM_InsertBy);
            list.AddSqlParameter("@BILLITM_Quanty", BILLITM_Quanty);
            string s = "INSERT INTO FCBILLITM(BILITM_Name,BILITM_Rate,BILITM_Tax,BILITM_InsertDate,BILITM_InsertBy,BILL_Id,BILLITM_Quanty)" +
                " VALUES(@BILLITM_Name,@BILLITM_Rate,@BILLITM_Tax,@BILLITM_InsertDate,@BILLITM_InsertBy,(SELECT MAX(BILL_Id) FROM FCBILLNO),@BILLITM_Quanty)";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public int bill { get; set; }
        public DataTable items()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT BILL_Amount,BILL_Tax,BILL_Total FROM FCBILLNO WHERE BILL_Id='"+bill+"'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        //public DataTable Address()
        //{
        //    var list = new List<SqlParameter>();
        //    string s = "SELECT PRPT_Address FROM FCPRPTY";
        //    DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
        //    return dt;
        //}
        public DataTable Address()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT PRPT_Name,PRPT_Address,PRPT_GST FROM FCPRPTY";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        //public DataTable PrintBill()
        //{
        //    var list = new List<SqlParameter>();
        //    string s = "SELECT BILL_Id,BILITM_Name,BILITM_Rate,BILLITM_Quanty,(SELECT BILITM_Tax FROM FCBILLNO WHERE BILL_Id='"+POS.bid+ "')AS BILITM_Tax,(SELECT BILL_Total FROM FCBILLNO WHERE BILL_Id='" + POS.bid + "')AS BILL_Total,(SELECT BILL_Amount FROM FCBILLNO WHERE BILL_Id='" + POS.bid + "')AS BILL_Amount FROM FCBILLITM WHERE BILL_Id='" + POS.bid + "'";
        //    DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s,list);
        //    return dt;
        //}
        public DataTable PrintBill()
        {
            var list = new List<SqlParameter>();
            //string s = "SELECT BILL_Id,BILITM_Name,BILITM_Rate,BILLITM_Quanty,(SELECT BILITM_Tax FROM FCBILLNO WHERE BILL_Id='"+POS.bid+ "')AS BILITM_Tax,(SELECT BILL_Total FROM FCBILLNO WHERE BILL_Id='" + POS.bid + "')AS BILL_Total,(SELECT BILL_Amount FROM FCBILLNO WHERE BILL_Id=1)AS BILL_Amount FROM FCBILLITM WHERE BILL_Id='" + POS.bid + "'";
            string s = "SELECT distinct BILITM_Name,BILITM_Rate,BILLITM_Quanty FROM FCBILLITM  where BILL_Id='" + bill + "'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s,list);
            return dt;
        }

        public DataTable KOT()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT BILITM_Name,BILLITM_Quanty FROM FCBILLITM WHERE BILL_Id='" + bill + "'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable itmnames()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT NAM_Name  FROM FCITMNAM WHERE CTG_Id =(SELECT CTG_Id FROM FCRITMCTG WHERE CTG_Name = '" + POS.aa + "')";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public int A { get; set; }
        public DataTable itemstot()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT BILL_Amount,BILL_Tax,BILL_Total FROM FCBILLNO WHERE BILL_Id='" + A + "'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable itms()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT BILITM_Name,BILLITM_Quanty,BILITM_Rate FROM FCBILLITM WHERE BILL_Id='" + A + "'";
            DataTable D = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return D;
        }
        public void custinfo()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@CUST_ID", CUST_ID);
            list.AddSqlParameter("@NAME", NAME);
            list.AddSqlParameter("@MOBILE_NO", MOBILE_NO);
            list.AddSqlParameter("@EMAIL", EMAIL);
            list.AddSqlParameter("@CITY", CITY);
            list.AddSqlParameter("@ADDRESS", ADDRESS);
            list.AddSqlParameter("@Insert_By", login.u);
            list.AddSqlParameter("@Insert_Date", DateTime.Today);
            list.AddSqlParameter("@Update_By", login.u);
            list.AddSqlParameter("@Update_Date", DateTime.Today);

            string s = "INSERT INTO FCCUSTOMER(NAME,MOBILE_NO,EMAIL,CITY,ADDRESS,Insert_By,Insert_Date)VALUES(@NAME,@MOBILE_NO,@EMAIL,@CITY,@ADDRESS,@Insert_By,@Insert_Date)";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public void custupdate()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@CUST_ID", CUST_ID);
            list.AddSqlParameter("@NAME", NAME);
            list.AddSqlParameter("@MOBILE_NO", MOBILE_NO);
            list.AddSqlParameter("@EMAIL", EMAIL);
            list.AddSqlParameter("@CITY", CITY);
            list.AddSqlParameter("@ADDRESS", ADDRESS);
            list.AddSqlParameter("@Update_By", login.u);
            list.AddSqlParameter("@Update_Date", DateTime.Today);

            string s = "UPDATE FCCUSTOMER SET NAME=@NAME,EMAIL=@EMAIL,CITY=@CITY,ADDRESS=@ADDRESS WHERE MOBILE_NO = '" + MOBILE_NO + "'";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public DataTable getdetails()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT NAME,EMAIL,CITY,ADDRESS FROM FCCUSTOMER WHERE MOBILE_NO = '"+POS.phno+"'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
    }
}