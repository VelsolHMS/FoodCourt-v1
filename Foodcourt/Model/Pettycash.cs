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
    class Pettycash
    {
        public string PCH_Id { get; set; }
        public string PCH_Name { get; set; }
        public string PCH_Amount { get; set; }
        public string PCH_Details { get; set; }
        public string PCH_InsertBy { get; set; }
        public DateTime PCH_InsertDate { get; set; }

        
        public void INSERT()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@PCH_Id", PCH_Id);
            list.AddSqlParameter("@PCH_Name", PCH_Name);
            list.AddSqlParameter("@PCH_Amount", PCH_Amount);
            list.AddSqlParameter("@PCH_Details", PCH_Details);
            list.AddSqlParameter("@PCH_InsertBy", login.u);
            PCH_InsertDate = DateTime.Today.Date;
            list.AddSqlParameter("@PCH_InsertDate", PCH_InsertDate);

            string s = "INSERT INTO FCPETCSH(PCH_Name,PCH_Amount,PCH_Details,PCH_InsertBy,PCH_InsertDate)" +
                       "VALUES (@PCH_Name,@PCH_Amount,@PCH_Details,@PCH_InsertBy,@PCH_InsertDate)";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        
        public void INSERT1()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@PCH_Id", PCH_Id);
            list.AddSqlParameter("@PCH_Name", PCH_Name);
            list.AddSqlParameter("@PCH_Amount", PCH_Amount);
            list.AddSqlParameter("@PCH_Details", PCH_Details);
            list.AddSqlParameter("@PCH_InsertBy", login.u);
            PCH_InsertDate = DateTime.Today.Date;
            list.AddSqlParameter("@PCH_InsertDate", PCH_InsertDate);

            string s = "INSERT INTO FCPETCSH(PCH_Name,PCH_Amount,PCH_Details,PCH_InsertBy,PCH_InsertDate)" +
                       "VALUES (@PCH_Name,@PCH_Amount,@PCH_Details,@PCH_InsertBy,@PCH_InsertDate)";
            DbFunctions.ExecuteCommand<int>(s, list);
        }   
    }
}
