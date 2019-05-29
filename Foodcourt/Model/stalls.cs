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
    class stalls
    {
        public string STL_ID { get; set; }
        public string STL_Name { get; set; }
        public string STL_ReportingName { get; set; }
        public string STL_Description { get; set; }
        public string STL_Status { get; set; }
        public string STL_InsertBy { get; set; }
        public DateTime STL_InsertDate { get; set; }

        public int GetItemId()
        {
            int a = 0;
            string S = "SELECT MAX(STL_ID) FROM FCSTALLS";
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
        public void INSERT()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@STL_Name", STL_Name);
            list.AddSqlParameter("@STL_ReportingName", STL_ReportingName);
            list.AddSqlParameter("@STL_Description", STL_Description);
            list.AddSqlParameter("@STL_Status", STL_Status);
            list.AddSqlParameter("@STL_InsertBy", login.u);
            list.AddSqlParameter("@STL_InsertDate", DateTime.Today.ToShortDateString());
            string s = "INSERT INTO FCSTALLS (STL_Name,STL_ReportingName,STL_Description,STL_Status,STL_InsertBy,STL_InsertDate) "+
                " VALUES (@STL_Name,@STL_ReportingName,@STL_Description,@STL_Status,@STL_InsertBy,@STL_InsertDate)";
            DbFunctions.ExecuteCommand<DataTable>(s,list);
        }
        public void UPDATE()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@STL_Name", STL_Name);
            list.AddSqlParameter("@STL_ReportingName", STL_ReportingName);
            list.AddSqlParameter("@STL_Description", STL_Description);
            list.AddSqlParameter("@STL_Status", STL_Status);
            string s = "UPDATE FCSTALLS SET STL_Name=@STL_Name,STL_ReportingName=@STL_ReportingName,STL_Description=@STL_Description,STL_Status=@STL_Status WHERE STL_ID = '" + STL_ID + "'";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public DataTable FillGrid()
        {
            var list = new List<SqlParameter>();
            string d = "SELECT STL_ID,STL_Name,STL_ReportingName,STL_Status FROM FCSTALLS";
            DataTable S = DbFunctions.ExecuteCommand<DataTable>(d, list);
            return S;
        }
        public DataTable GetAllDetails()
        {
            var list = new List<SqlParameter>();
            string d = "SELECT * FROM FCSTALLS WHERE STL_ID = '" + STL_ID + "'";
            DataTable S = DbFunctions.ExecuteCommand<DataTable>(d, list);
            return S;
        }
    }
}
