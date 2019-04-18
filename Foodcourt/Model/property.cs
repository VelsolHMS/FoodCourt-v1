using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;
using Foodcourt.View;

namespace Foodcourt.Model
{
    class property
    {
        public string PRPT_ID { get; set; }
        public string PRPT_Name { get; set; }
        public string PRPT_Address { get; set; }
        public string PRPT_State { get; set; }
        public string PRPT_Country { get; set; }
        public string PRPT_Phone { get; set; }
        public string PRPT_GST { get; set; }
        public DateTime PRPT_InsertDate { get; set; }
        public string PRPT_InserBy { get; set; }
        public DateTime PRPT_UpdateDate { get; set; }
        public string PRPT_UpdateBy { get; set; }

        public void Insert()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@PRPT_ID", PRPT_ID);
            list.AddSqlParameter("@PRPT_Name", PRPT_Name);
            list.AddSqlParameter("@PRPT_Address", PRPT_Address);
            list.AddSqlParameter("@PRPT_State", PRPT_State);
            list.AddSqlParameter("@PRPT_Country", PRPT_Country);
            list.AddSqlParameter("@PRPT_Phone", PRPT_Phone);
            list.AddSqlParameter("@PRPT_GST", PRPT_GST);
            list.AddSqlParameter("@PRPT_InsertDate", PRPT_InsertDate);
            list.AddSqlParameter("@PRPT_InserBy", login.u);
            list.AddSqlParameter("@PRPT_UpdateDate", PRPT_UpdateDate);
            list.AddSqlParameter("@PRPT_UpdateBy", login.u);
            string s = "INSERT INTO FCPRPTY(PRPT_Name,PRPT_Address,PRPT_State,PRPT_Country,PRPT_Phone,PRPT_GST,PRPT_InsertDate,PRPT_InserBy,PRPT_UpdateDate,PRPT_UpdateBy)VALUES(@PRPT_Name,@PRPT_Address,@PRPT_State,@PRPT_Country,@PRPT_Phone,@PRPT_GST,@PRPT_InsertDate,@PRPT_InserBy,@PRPT_UpdateDate,@PRPT_UpdateBy)";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public void Update()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@PRPT_ID", PRPT_ID);
            list.AddSqlParameter("@PRPT_Name", PRPT_Name);
            list.AddSqlParameter("@PRPT_Address", PRPT_Address);
            list.AddSqlParameter("@PRPT_State", PRPT_State);
            list.AddSqlParameter("@PRPT_Country", PRPT_Country);
            list.AddSqlParameter("@PRPT_Phone", PRPT_Phone);
            list.AddSqlParameter("@PRPT_GST", PRPT_GST);
            list.AddSqlParameter("@PRPT_InsertDate", PRPT_InsertDate);
            list.AddSqlParameter("@PRPT_InserBy", login.u);
            list.AddSqlParameter("@PRPT_UpdateDate", PRPT_UpdateDate);
            list.AddSqlParameter("@PRPT_UpdateBy", login.u);
            string s = "UPDATE FCPRPTY SET PRPT_Address=@PRPT_Address,PRPT_State=@PRPT_State,PRPT_Country=@PRPT_Country,PRPT_Phone=@PRPT_Phone,PRPT_GST=@PRPT_GST,PRPT_UpdateDate=@PRPT_UpdateDate,PRPT_UpdateBy=@PRPT_UpdateBy WHERE PRPT_Name=@PRPT_Name";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public DataTable NAME()
        {
            var LIST = new List<SqlParameter>();
            String S1 = "SELECT * FROM FCPRPTY ";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(S1, LIST);
            //if (dt.Rows.Count != 0)
            //{
            //    PRPT_ID = dt.Rows[0]["PRPT_ID"].ToString();
            //    PRPT_Name = dt.Rows[0]["PRPT_Name"].ToString();
            //    PRPT_Address = dt.Rows[0]["PRPT_Address"].ToString();
            //    PRPT_State = dt.Rows[0]["PRPT_State"].ToString();
            //    PRPT_Country = dt.Rows[0]["PRPT_Country"].ToString();
            //    PRPT_Phone = dt.Rows[0]["PRPT_Phone"].ToString();
            //    PRPT_GST = dt.Rows[0]["PRPT_GST"].ToString();

            //}
            return dt;
        }
        public DataTable States()
        {
            var list = new List<SqlParameter>();
            string s = "Select STATE from STATES";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return DT;
        }
    }
}

