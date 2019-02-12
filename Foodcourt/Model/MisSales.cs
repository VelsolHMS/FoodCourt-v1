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
    class MisSales
    {
        public string MIS_ID { get; set; }
        public string MIS_Name { get; set; }
        public string MIS_Amount { get; set; }
        public string MIS_Details { get; set; }
        public string MIS_InsertBY { get; set; }
        public string MIS_InsertDate { get; set; }

        public void Insert()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@MIS_Name", MIS_Name);
            list.AddSqlParameter("@MIS_Amount", MIS_Amount);
            list.AddSqlParameter("@MIS_Details", MIS_Details);
            list.AddSqlParameter("@MIS_InsertBY", login.u);
            list.AddSqlParameter("@MIS_InsertDate", DateTime.Today.ToShortDateString());
            string s = "INSERT INTO FCMISSALES(MIS_Name,MIS_Amount,MIS_Details,MIS_InsertBY,MIS_InsertDate)" +
                       "VALUES (@MIS_Name,@MIS_Amount,@MIS_Details,@MIS_InsertBY,@MIS_InsertDate)";
            DbFunctions.ExecuteCommand<DataTable>(s, list);
        }
        public int GetVocherId()
        {
            int a = 0;
            string S = "SELECT MAX(MIS_ID) FROM FCMISSALES";
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
    }
}
