﻿using System;
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
            string S = "SELECT CTG_Name,STL_ID FROM FCRITMCTG WHERE CTG_ActiveDate <= GETDATE() AND CTG_Status='Active'";
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
            string SS = "SELECT NAM_Name FROM FCITMNAM Where CTG_Id =(SELECT CTG_Id FROM FCRITMCTG WHERE CTG_Name='" + PosNew.aa+ "') AND NAM_Status='Active'";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(SS, LIST);
            return DT;
        }
        public DataTable GETRATE()
        {
            var LIST = new List<SqlParameter>();
            string N = "SELECT NAM_Name, CONVERT(decimal(17,2),NAM_Rate) AS NAM_Rate  FROM FCITMNAM WHERE NAM_Name='" + PosNew.id + "'";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(N, LIST);
            return DT;
        }
        public DataTable GETRATE1()
        {
            var LIST = new List<SqlParameter>();
            string N = "SELECT NAM_Rate FROM FCITMNAM WHERE NAM_Name='" + PosNew.sa + "'";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(N, LIST);
            return DT;
        }
        public DataTable gsttax()
        {
            var LIST = new List<SqlParameter>();
            string dd = "SELECT TAX_Percentage FROM FCTAX WHERE TAX_Name IN ( SELECT NAM_Tax FROM FCITMNAM WHERE NAM_Name = '" + PosNew.id + "')";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(dd, LIST);
            return dt;
        }
        public DataTable gsttax1()
        {
            var LIST = new List<SqlParameter>();
            string dd = "select NAM_Tax FROM FCITMNAM WHERE NAM_Name='" + PosNew.id + "'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(dd, LIST);
            return dt;
        }
        public DataTable GETTAX()
        {
            var LIST = new List<SqlParameter>();
            string dd = "SELECT TAX_Percentage from FCTAX WHERE TAX_Name='" + POS.check2 + "'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(dd, LIST);
            return dt;
        }
        public DataTable GETSTALLS()
        {
            var LIST = new List<SqlParameter>();
            string dd = " SELECT STL_Name from FCSTALLS WHERE STL_Status = 'Active'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(dd, LIST);
            return dt;
        }
        public DataTable GetCategories()
        {
            var LIST = new List<SqlParameter>();
            string dd = "SELECT CTG_Name From FCRITMCTG WHERE STL_ID IN ( SELECT STL_ID FROM FCSTALLS WHERE STL_Name = '"+PosNew.st+"')";
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
        public int Bill_OfferId { get; set; }
        public decimal Bill_InstantDis { get; set; }
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
        public decimal Discount { get; set; }
        public decimal DiscountPer { get; set; }
        public string CusMobile { get; set; }
        public string CusName { get; set; }


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
            list.AddSqlParameter("@BILL_Status", BILL_Status);
           // BILL_Status = "Settled";
            list.AddSqlParameter("@BILL_Discount", BILL_Discount);
            list.AddSqlParameter("@Bill_OfferId", Bill_OfferId);
            list.AddSqlParameter("@Bill_InstantDis", Bill_InstantDis);
            list.AddSqlParameter("@CusName", CusName);
            list.AddSqlParameter("@CusMobile", CusMobile);
            string s = "INSERT INTO FCBILLNO(BILL_Amount,BILL_Tax,BILL_Total,BILL_InsertBy,BILL_InsertDate,BILL_Status,BILL_Discount,Bill_OfferId,Bill_InstantDis,CusName,CusMobile)" +
                " VALUES(@BILL_Amount,@BILL_Tax,@BILL_Total,@BILL_InsertBy,@BILL_InsertDate,@BILL_Status,@BILL_Discount,@Bill_OfferId,@Bill_InstantDis,@CusName,@CusMobile)";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public string BILLITM_Name { get; set; }
        public decimal BILLITM_Rate { get; set; }
        public decimal BILLITM_Tax { get; set; }
        public DateTime BILLITM_InsertDate { get; set; }
        public string BILLITM_InsertBy { get; set; }
        public int BILLITM_Quanty { get; set; }
        public string STL_ID { get; set; }


        public void Insertitm()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@BILLITM_Name", BILLITM_Name);
            list.AddSqlParameter("@STL_ID", PosNew.stlid);
            list.AddSqlParameter("@BILLITM_Rate", BILLITM_Rate);
            list.AddSqlParameter("@BILLITM_Tax", BILLITM_Tax);
            BILLITM_InsertDate = DateTime.Today.Date;
            list.AddSqlParameter("@BILLITM_InsertDate", BILLITM_InsertDate);
            BILLITM_InsertBy = login.u;
            list.AddSqlParameter("@BILLITM_InsertBy", BILLITM_InsertBy);
            list.AddSqlParameter("@BILLITM_Quanty", BILLITM_Quanty);
            list.AddSqlParameter("@Discount", Discount);
            list.AddSqlParameter("@DiscountPer", DiscountPer);
            string s = "INSERT INTO FCBILLITM(BILITM_Name,STL_ID,BILITM_Rate,BILITM_Tax,BILITM_InsertDate,BILITM_InsertBy,BILL_Id,BILLITM_Quanty,Discount,DiscountPer)" +
                " VALUES(@BILLITM_Name,@STL_ID,@BILLITM_Rate,@BILLITM_Tax,@BILLITM_InsertDate,@BILLITM_InsertBy,(SELECT MAX(BILL_Id) FROM FCBILLNO),@BILLITM_Quanty,@Discount,@DiscountPer)";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public int bill { get; set; }
        public DataTable items()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT BILL_Amount,BILL_Tax,BILL_Total,BILL_Discount,Bill_InstantDis FROM FCBILLNO WHERE BILL_Id='" + bill+"'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable DiscountAmount()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT Sum(Discount)AS Discount FROM FCBILLITM WHERE BILL_Id='" + bill + "'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable stlidsss()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT DISTINCT STL_ID FROM FCBILLITM WHERE BILL_Id='" + bill + "'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable STALLNAME()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT STL_Name FROM FCSTALLS WHERE STL_ID IN (SELECT STL_ID FROM FCBILLITM WHERE STL_ID='"+PosNew.STALLID+"')";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable STLIDITEMNAMES()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT BILITM_Name,BILLITM_Quanty FROM FCBILLITM WHERE STL_ID = '"+PosNew.STALLID + "' AND BILL_Id='" + bill + "'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable getstlid()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT STL_ID,NAM_Name FROM FCITMNAM WHERE NAM_Name='" + PosNew.itemnamestlid + "'";
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
            string s = "SELECT NAM_Name  FROM FCITMNAM WHERE CTG_Id =(SELECT CTG_Id FROM FCRITMCTG WHERE CTG_Name = '" + PosNew.aa + "')";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public int A { get; set; }
        public DataTable itemstot()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT CONVERT(decimal(17,2),BILL_Amount) AS BILL_Amount ,CONVERT(decimal(17,2),BILL_Tax) AS BILL_Tax,CONVERT(decimal(17,2),BILL_Total) AS BILL_Total FROM FCBILLNO WHERE BILL_Id='" + A + "'";
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
        public int billid;
        public void updatestatus()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@BILL_Status", BILL_Status);
            list.AddSqlParameter("@BILL_InsertDate", DateTime.Today);
            list.AddSqlParameter("@BILL_UpdateDate", DateTime.Today);
            string s = "UPDATE FCBILLNO SET BILL_Status=@BILL_Status,BILL_InsertDate = @BILL_InsertDate,BILL_UpdateDate = @BILL_UpdateDate WHERE BILL_Id = '" + billid + "'";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public void updateBillitmstatus()
        {
            //update FCBILLITM set BILITM_InsertDate = '' where Bill_id =
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@BILITM_InsertDate", DateTime.Today);
            string s = "update FCBILLITM set BILITM_InsertDate = @BILITM_InsertDate where BILL_Id = '" + billid + "'";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public DataTable getoffer()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT OFF_ID,OFF_Percentage,OFF_MaxAmount FROM FCOFFERS WHERE OFF_Name =  '" + PosNew.offername + "'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }

        public DataTable getofferlist()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT OFF_Name FROM FCOFFERS WHERE OFF_Status='Active'";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable itemslist()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT NAM_Name FROM FCITMNAM"; 
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public string likea;
        public DataTable itms1()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT NAM_Name FROM FCITMNAM WHERE NAM_Name  LIKE '" + likea + "%' and NAM_Status = 'Active'"; // OR VFS_ITMNAM_Name LIKE '%" + likea + "' OR VFS_ITMNAM_Name LIKE '%" + likea + "%' ";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
        public DataTable itmname()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT NAM_Id, NAM_Name,CONVERT(decimal(17,2),NAM_Rate) AS NAM_Rate From FCITMNAM  WHERE NAM_Name = '" + PosNew.id + "' AND CTG_Id IN(SELECT CTG_Id FROM FCRITMCTG WHERE CTG_ActiveDate <= GETDATE())";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return dt;
        }
    }
}