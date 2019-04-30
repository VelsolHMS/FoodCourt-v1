using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Foodcourt.View;
using DAL;


namespace Foodcourt.Model
{
    class ItemsCAT
    {
        public string CTG_Id { get; set; }
        public string CTG_Name { get; set; }
        public string CTG_Details { get; set; }
        public string CTG_ActiveDate { get; set; }
        public string CTG_ReportingName { get; set; }
        public string CTG_InsertBy { get; set; }
        public DateTime CTG_InsertDate { get; set; }
        public string CTG_UpdateBY { get; set; }
        public DateTime CTG_UpdateDate { get; set; }
        public string CTG_Status { get; set; }

        public int id()
        {
            int a = 0;
            var L = new List<SqlParameter>();
            string S = "SELECT MAX(CTG_Id) FROM FCRITMCTG";
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
        public DataTable filltable()
        {
            var list = new List<SqlParameter>();
            string gd = "select CTG_ID,CTG_Name,CTG_Details,CTG_ReportingName,CTG_Status from FCRITMCTG";
            DataTable dt1 = DbFunctions.ExecuteCommand<DataTable>(gd, list);
            return dt1;
        }
        public DataTable FillDataGrid()
        {
            var list = new List<SqlParameter>();
            string g = "select CTG_Name,CTG_ActiveDate,CTG_ReportingName,CTG_Status from FCRITMCTG";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(g, list);
            return dt;
        }
        public void INSERT()
        {
            var LIST = new List<SqlParameter>();
            LIST.AddSqlParameter("@CTG_ID", CTG_Id);
            LIST.AddSqlParameter("@CTG_Name", CTG_Name);
            LIST.AddSqlParameter("@CTG_Details", CTG_Details);
            LIST.AddSqlParameter("@CTG_ActiveDate", DateTime.Today.Date);
            LIST.AddSqlParameter("@CTG_ReportingName", CTG_ReportingName);
            LIST.AddSqlParameter("@CTG_InsertBy", login.u);
            CTG_InsertDate = DateTime.Today.Date;
            LIST.AddSqlParameter("@CTG_InsertDate", CTG_InsertDate);
            LIST.AddSqlParameter("@CTG_UpdateBY", login.u);
            LIST.AddSqlParameter("@CTG_Status", CTG_Status);
            CTG_UpdateDate = DateTime.Today.Date;
            LIST.AddSqlParameter("@CTG_UpdateDate", CTG_UpdateDate);
            string I = "INSERT INTO FCRITMCTG (CTG_Name,CTG_Details,CTG_ActiveDate,CTG_ReportingName,CTG_InsertBy,CTG_InsertDate,CTG_UpdateBY,CTG_UpdateDate,CTG_Status)" +
                " VALUES (@CTG_Name,@CTG_Details,@CTG_ActiveDate,@CTG_ReportingName,@CTG_InsertBy,@CTG_InsertDate,@CTG_UpdateBY,@CTG_UpdateDate,@CTG_Status)";
            DbFunctions.ExecuteCommand<int>(I, LIST);
        }
        public void UPDATE()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@CTG_Name", CTG_Name);
            list.AddSqlParameter("@CTG_Details", CTG_Details);
            list.AddSqlParameter("@CTG_Status", CTG_Status);
            list.AddSqlParameter("@CTG_ReportingName", CTG_ReportingName);
            list.AddSqlParameter("@CTG_UpdateBY", login.u);
            CTG_UpdateDate = DateTime.Today.Date;
            list.AddSqlParameter("@CTG_UpdateDate", CTG_UpdateDate);
            string uquery = "Update FCRITMCTG set CTG_Name=@CTG_Name,CTG_Details=@CTG_Details,CTG_ReportingName=@CTG_ReportingName,CTG_Status = @CTG_Status WHERE CTG_ID='" + CTG_Id+"'";

            DbFunctions.ExecuteCommand<int>(uquery, list);
        }
        public DataTable GetCategories()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT CTG_Name FROM FCRITMCTG"; // WHERE CTG_ActiveDate<CURRENT_TIMESTAMP
            DataTable categories = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return categories;
        }
        public DataTable GetActiveDate()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@CTG_Name", CTG_Name);
            string s = "SELECT CTG_ActiveDate FROM FCRITMCTG WHERE CTG_Name = @CTG_Name";
            DataTable active_date = DbFunctions.ExecuteCommand<DataTable>(s, list);
            if(active_date.Rows.Count == 0)
            {
                CTG_ActiveDate = "";
            }
            else
            {
                CTG_ActiveDate = active_date.Rows[0]["CTG_ActiveDate"].ToString();
            }
            return active_date;
        }
        public DataTable GetCatName()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT CTG_Name FROM FCRITMCTG where CTG_Id = '" + CTG_Id + "'";
            DataTable categories = DbFunctions.ExecuteCommand<DataTable>(s, list);
            if(categories.Rows.Count == 0)
            {
                CTG_Name = "";
            }
            else
            {
                CTG_Name = categories.Rows[0]["CTG_Name"].ToString();
            }
            return categories;
        }
    }
}
