using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Foodcourt.View;
using Foodcourt.ViewModel;
using DAL;

namespace Foodcourt.Model
{
    class cashhand
    {
        public string CH_Id { get; set; }
        public string CH_Name { get; set; }
        public string CH_Amount { get; set; }
        public string CH_Description { get; set; }
        public string CH_InsertBY { get; set; }
        public DateTime CH_InsertDate { get; set; }

        public void Insert()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@CH_Id", CH_Id);
            list.AddSqlParameter("@CH_Name", CH_Name);
            list.AddSqlParameter("@CH_Amount", CH_Amount);
            list.AddSqlParameter("@CH_Description", CH_Description);
            list.AddSqlParameter("@CH_InsertBY", login.u);
            list.AddSqlParameter("@CH_InsertDate", DateTime.Today);

            string s = "INSERT INTO FCCashHandover(CH_Name,CH_Amount,CH_Description,CH_InsertBY,CH_InsertDate)VALUES(@CH_Name,@CH_Amount,@CH_Description,@CH_InsertBY,@CH_InsertDate)";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
    }
}
