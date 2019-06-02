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
    class Offers
    {
        public int OFF_ID { get; set; }
        public string OFF_Name { get; set; }
        public string OFF_ReportingName { get; set; }
        public string OFF_Percentage { get; set; }
        public string OFF_Status { get; set; }
        public DateTime OFF_ExpireDate { get; set; }
        public string OFF_MaxAmount { get; set; }
        public string OFF_InsertBy { get; set; }
        public DateTime OFF_InsertDate { get; set; }
        
        public void Insert()
        {
            var LIST = new List<SqlParameter>();
            LIST.AddSqlParameter("@OFF_Name", OFF_Name);
            LIST.AddSqlParameter("@OFF_ReportingName", OFF_ReportingName);
            LIST.AddSqlParameter("@OFF_Percentage", OFF_Percentage);
            LIST.AddSqlParameter("@OFF_Status", OFF_Status);
            LIST.AddSqlParameter("@OFF_InsertDate", DateTime.Today.Date);
            LIST.AddSqlParameter("@OFF_InsertBy", login.u);
            LIST.AddSqlParameter("@OFF_MaxAmount", OFF_MaxAmount);
            string I = "INSERT INTO FCOFFERS (OFF_Name,OFF_Percentage,OFF_MaxAmount,OFF_ReportingName,OFF_Status,OFF_InsertBy,OFF_InsertDate)" +
                " VALUES (@OFF_Name,@OFF_Percentage,@OFF_MaxAmount,@OFF_ReportingName,@OFF_Status,@OFF_InsertBy,@OFF_InsertDate)";
            DbFunctions.ExecuteCommand<int>(I, LIST);
        }
        public void UPDATE()
        {
            var LIST = new List<SqlParameter>();
            LIST.AddSqlParameter("@OFF_ID", OFF_ID);
            LIST.AddSqlParameter("@OFF_Name", OFF_Name);
            LIST.AddSqlParameter("@OFF_ReportingName", OFF_ReportingName);
            LIST.AddSqlParameter("@OFF_Percentage", OFF_Percentage);
            LIST.AddSqlParameter("@OFF_Status", OFF_Status);
            LIST.AddSqlParameter("@OFF_MaxAmount", OFF_MaxAmount);
            string uquery = "Update FCOFFERS set OFF_ReportingName=@OFF_ReportingName,OFF_Name = @OFF_Name, OFF_Percentage=@OFF_Percentage,OFF_Status=@OFF_Status,OFF_MaxAmount = @OFF_MaxAmount WHERE OFF_ID='" + OFF_ID + "'";
            DbFunctions.ExecuteCommand<int>(uquery, LIST);
        }
        public int id()
        {
            int a = 0;
            var L = new List<SqlParameter>();
            string S = "Select MAX(OFF_ID) from FCOFFERS";
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
        public DataTable GetOffers()
        {
            var list = new List<SqlParameter>();
            string gd = "Select OFF_ID,OFF_Name,OFF_Percentage,OFF_ReportingName,OFF_Status,OFF_MaxAmount from FCOFFERS";
            DataTable dt1 = DbFunctions.ExecuteCommand<DataTable>(gd, list);
            return dt1;
        }
    }
}