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
    class paidout
    {
        public string PO_ID { get; set; }
        public string PO_Name { get; set; }
        public string PO_Amount { get; set; }
        public string PO_Description { get; set; }
        public string PO_InsertBY { get; set; }
        public DateTime PO_InsertDate { get; set; }
        public void Insert()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@PO_ID", PO_ID);
            list.AddSqlParameter("@PO_Name", PO_Name);
            list.AddSqlParameter("@PO_Amount", PO_Amount);
            list.AddSqlParameter("@PO_Description", PO_Description);
            list.AddSqlParameter("@PO_InsertBY", login.u);
            list.AddSqlParameter("@PO_InsertDate", PO_InsertDate);
            string s = "INSERT INTO FCPaidout(PO_Name,PO_Amount,PO_Description,PO_InsertBY,PO_InsertDate)VALUES(@PO_Name,@PO_Amount,@PO_Description,@PO_InsertBY,@PO_InsertDate)";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
    }
}
