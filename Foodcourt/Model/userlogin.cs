using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Foodcourt.Model
{
    class Userlogin
    {
        public string REG_UserName { get; set; }
        public string REG_Password { get; set; }

        public int a;

        public void User_Name()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@REG_UserName", REG_UserName);
            string s = "Select REG_UserName from FCREG WHERE REG_UserName = @REG_UserName";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            if (dt.Rows.Count == 0)
            {
                a = 0;
            }
            else
            {
                a = 1;
            }
        }
        public void Password()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@REG_Password", REG_Password);
            list.AddSqlParameter("@REG_UserName", REG_UserName);
            string s = "SELECT REG_UserName FROM FCREG WHERE REG_Password = @REG_Password AND REG_UserName = @REG_UserName";
            DataTable dt = DbFunctions.ExecuteCommand<DataTable>(s, list);
            if (dt.Rows.Count == 0)
            {
                a = 0;
                REG_UserName = "";
            }
            else
            {
                a = 1;
                REG_UserName = dt.Rows[0]["REG_UserName"].ToString();
            }
        }
    }
}
