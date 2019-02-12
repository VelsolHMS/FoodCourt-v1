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
    class registration
    {
        public string REG_Id { get; set; }
        public string REG_UserName { get; set; }
        public string REG_Name { get; set; }
        public string REG_Phone { get; set; }
        public string REG_Password { get; set; }
        public string REG_RePassword { get; set; }
        public string DESIGNATION { get; set; }
        public string REG_InsertBy { get; set; }
        public DateTime REG_InsertDate { get; set; }
        public string REG_UpdateBy { get; set; }
        public DateTime REG_UpdateDate { get; set; }

        public void Insert()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@REG_Id", REG_Id);
            list.AddSqlParameter("@REG_UserName", REG_UserName);
            list.AddSqlParameter("@REG_Name", REG_Name);
            list.AddSqlParameter("@REG_Password", REG_Password);
            list.AddSqlParameter("@REG_Phone", REG_Phone);
            list.AddSqlParameter("@REG_RePassword", REG_RePassword);
            list.AddSqlParameter("@DESIGNATION", DESIGNATION);
            list.AddSqlParameter("@REG_InsertDate", REG_InsertDate);
            list.AddSqlParameter("@REG_InsertBy", login.u);
            list.AddSqlParameter("@REG_UpdateDate", REG_UpdateDate);
            list.AddSqlParameter("@REG_UpdateBy", login.u);
            string s = "INSERT INTO FCREG(REG_UserName,REG_Name,REG_Password,REG_Phone,REG_RePassword,DESIGNATION,REG_InsertDate,REG_InsertBy,REG_UpdateDate,REG_UpdateBy)VALUES(@REG_UserName,@REG_Name,@REG_Password,@REG_Phone,@REG_RePassword,@DESIGNATION,@REG_InsertDate,@REG_InsertBy,@REG_UpdateDate,@REG_UpdateBy)";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public void Update()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@REG_Id", REG_Id);
            list.AddSqlParameter("@REG_UserName", REG_UserName);
            list.AddSqlParameter("@REG_Name", REG_Name);
            list.AddSqlParameter("@REG_Password", REG_Password);
            list.AddSqlParameter("@REG_Phone", REG_Phone);
            list.AddSqlParameter("@REG_RePassword", REG_RePassword);
            list.AddSqlParameter("@DESIGNATION", DESIGNATION);
            list.AddSqlParameter("@REG_UpdateDate", REG_UpdateDate);
            list.AddSqlParameter("@REG_UpdateBy", login.u);
            string s = "UPDATE FCREG SET REG_Name=@REG_Name,REG_Phone=@REG_Phone,REG_Password=@REG_Password,REG_RePassword=@REG_RePassword,DESIGNATION=@DESIGNATION WHERE REG_UserName = '" + REG_UserName + "'";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public DataTable user()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT REG_Name,REG_Phone,REG_Password,REG_RePassword,DESIGNATION FROM FCREG WHERE REG_UserName = '" + REG_UserName + "'";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return DT;
        }

        public string USER_ID { get; set; }
        public string USERNAME { get; set; }
        public string SIGNIN { get; set; }
        public string SIGNOUT { get; set; }
        public DateTime INSERT_DATE { get; set; }

        public void UserInsert()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@USER_ID", USER_ID);
            list.AddSqlParameter("@USERNAME", USERNAME);
            list.AddSqlParameter("@SIGNIN", SIGNIN);
            list.AddSqlParameter("@SIGNOUT", SIGNOUT);
            list.AddSqlParameter("@INSERT_DATE", INSERT_DATE);
            string s = "INSERT INTO USERS(USERNAME,SIGNIN,INSERT_DATE)VALUES(@USERNAME,@SIGNIN,@INSERT_DATE)";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public void UserUpdate()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@USER_ID", USER_ID);
            list.AddSqlParameter("@USERNAME", USERNAME);
            list.AddSqlParameter("@SIGNIN", SIGNIN);
            list.AddSqlParameter("@SIGNOUT", SIGNOUT);
            list.AddSqlParameter("@INSERT_DATE", DateTime.Today);

            string s = "UPDATE USERS SET SIGNOUT = @SIGNOUT WHERE USERNAME = '"+login.u+"'";
            DbFunctions.ExecuteCommand<int>(s, list);
        }

        public DataTable User()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT USERNAME,SIGNIN,INSERT_DATE FROM USERS WHERE DAY(INSERT_DATE) = DAY(GETDATE())";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return DT;
        }
    }
}
