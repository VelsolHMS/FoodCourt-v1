using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;
using Foodcourt.View;
using Foodcourt.Model;

namespace Foodcourt.Model
{
    class userpermissions
    {
        public string MASTER_ID { get; set; }
        public string PROPERTY { get; set; }
        public string USER_REGISTRATION { get; set; }
        public string TAX { get; set; }
        public string ITEM_CATEGORY { get; set; }
        public string ITEM_NAME { get; set; }
        public string USER_PERMISSIONS { get; set; }
        public string OPERATION_ID { get; set; }
        public string BILL_VIEW { get; set; }
        public string PETTY_CASH { get; set; }
        public string PAIDOUTS { get; set; }
        public string MIS_COLLECTION { get; set; }
        public string HOME { get; set; }
        public string CAN_DELETE_BILL { get; set; }
        public string CAN_REPRINT_BILL { get; set; }
        public string DASHBOARD { get; set; }
        public string REPORT_ID { get; set; }
        public string ITEM_RATE_LIST { get; set; }
        public string ITEM_WISE_SALES { get; set; }
        public string DAY_WISE_SALES { get; set; }
        public string MONTH_WISE_SALE { get; set; }
        public string TAX_REPORT { get; set; }
        public string BILL_WISE_SALES { get; set; }
        public string USERNAME { get; set; }
        public string DESIGNATION { get; set; }
        public string INSERT_BY { get; set; }
        public DateTime INSERT_DATE { get; set; }
        public string UPDATE_BY { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string REG_UserName { get; set; }
        public void MasterInsert()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@MASTER_ID", MASTER_ID);
            list.AddSqlParameter("@PROPERTY", PROPERTY);
            list.AddSqlParameter("@USER_REGISTRATION", USER_REGISTRATION);
            list.AddSqlParameter("@TAX", TAX);
            list.AddSqlParameter("@ITEM_CATEGORY", ITEM_CATEGORY);
            list.AddSqlParameter("@ITEM_NAME", ITEM_NAME);
            list.AddSqlParameter("@USER_PERMISSIONS", USER_PERMISSIONS);
            list.AddSqlParameter("@USERNAME", USERNAME);
            list.AddSqlParameter("@DESIGNATION", DESIGNATION);
            list.AddSqlParameter("@INSERT_BY", login.u);
            INSERT_DATE = DateTime.Today.Date;
            list.AddSqlParameter("@INSERT_DATE", INSERT_DATE);
            string s = "INSERT INTO MASTERPERMISSIONS(USERNAME,DESIGNATION,PROPERTY,USER_REGISTRATION,TAX,ITEM_CATEGORY,ITEM_NAME,USER_PERMISSIONS,INSERT_BY,INSERT_DATE)VALUES(@USERNAME,@DESIGNATION,@PROPERTY,@USER_REGISTRATION,@TAX,@ITEM_CATEGORY,@ITEM_NAME,@USER_PERMISSIONS,@INSERT_BY,@INSERT_DATE)";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public void MasterUpdate()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@MASTER_ID", MASTER_ID);
            list.AddSqlParameter("@PROPERTY", PROPERTY);
            list.AddSqlParameter("@USER_REGISTRATION", USER_REGISTRATION);
            list.AddSqlParameter("@TAX", TAX);
            list.AddSqlParameter("@ITEM_CATEGORY", ITEM_CATEGORY);
            list.AddSqlParameter("@ITEM_NAME", ITEM_NAME);
            list.AddSqlParameter("@USER_PERMISSIONS", USER_PERMISSIONS);
            list.AddSqlParameter("@USERNAME", USERNAME);
            list.AddSqlParameter("@DESIGNATION", DESIGNATION);
            list.AddSqlParameter("@UPDATE_BY", login.u);
            UPDATE_DATE = DateTime.Today.Date;
            list.AddSqlParameter("@UPDATE_DATE", UPDATE_DATE);
            string s = "UPDATE MASTERPERMISSIONS SET DESIGNATION=@DESIGNATION,PROPERTY=@PROPERTY,USER_REGISTRATION=@USER_REGISTRATION,TAX=@TAX,ITEM_CATEGORY=@ITEM_CATEGORY,ITEM_NAME=@ITEM_NAME,USER_PERMISSIONS=@USER_PERMISSIONS WHERE USERNAME='" + USERNAME+"'";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public void OperationInsert()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@OPERATION_ID", OPERATION_ID);
            list.AddSqlParameter("@BILL_VIEW", BILL_VIEW);
            list.AddSqlParameter("@PETTY_CASH", PETTY_CASH);
            list.AddSqlParameter("@PAIDOUTS", PAIDOUTS);
            list.AddSqlParameter("@MIS_COLLECTION", MIS_COLLECTION);
            list.AddSqlParameter("@USERNAME", USERNAME);
            list.AddSqlParameter("@DESIGNATION", DESIGNATION);
            list.AddSqlParameter("@HOME",HOME);
            list.AddSqlParameter("@CAN_DELETE_BILL",CAN_DELETE_BILL);
            list.AddSqlParameter("@CAN_REPRINT_BILL", CAN_REPRINT_BILL);
            list.AddSqlParameter("@DASHBOARD", DASHBOARD);
            list.AddSqlParameter("@INSERT_BY", login.u);
            INSERT_DATE = DateTime.Today.Date;
            list.AddSqlParameter("@INSERT_DATE", INSERT_DATE);
            string s = "INSERT INTO OPERATIONPERMISSIONS(USERNAME,DESIGNATION,BILL_VIEW,PETTY_CASH,PAIDOUTS,MIS_COLLECTION,HOME,CAN_DELETE_BILL,CAN_REPRINT_BILL,DASHBOARD,INSERT_BY,INSERT_DATE)VALUES(@USERNAME,@DESIGNATION,@BILL_VIEW,@PETTY_CASH,@PAIDOUTS,@MIS_COLLECTION,@HOME,@CAN_DELETE_BILL,@CAN_REPRINT_BILL,@DASHBOARD,@INSERT_BY,@INSERT_DATE)";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public void OperationUpdate()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@OPERATION_ID", OPERATION_ID);
            list.AddSqlParameter("@BILL_VIEW", BILL_VIEW);
            list.AddSqlParameter("@PETTY_CASH", PETTY_CASH);
            list.AddSqlParameter("@PAIDOUTS", PAIDOUTS);
            list.AddSqlParameter("@MIS_COLLECTION", MIS_COLLECTION);
            list.AddSqlParameter("@USERNAME", USERNAME);
            list.AddSqlParameter("@DESIGNATION", DESIGNATION);
            list.AddSqlParameter("@HOME", HOME);
            list.AddSqlParameter("@CAN_DELETE_BILL", CAN_DELETE_BILL);
            list.AddSqlParameter("@CAN_REPRINT_BILL", CAN_REPRINT_BILL);
            list.AddSqlParameter("@DASHBOARD", DASHBOARD);
            list.AddSqlParameter("@UPDATE_BY", login.u);
            UPDATE_DATE = DateTime.Today.Date;
            list.AddSqlParameter("@UPDATE_DATE", UPDATE_DATE);
            string s = "UPDATE OPERATIONPERMISSIONS SET DESIGNATION=@DESIGNATION,BILL_VIEW=@BILL_VIEW,PETTY_CASH=@PETTY_CASH,PAIDOUTS=@PAIDOUTS,MIS_COLLECTION=@MIS_COLLECTION,HOME=@HOME,CAN_DELETE_BILL=@CAN_DELETE_BILL,CAN_REPRINT_BILL=@CAN_REPRINT_BILL,DASHBOARD=@DASHBOARD WHERE USERNAME='" + USERNAME+"'";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public void ReportInsert()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@REPORT_ID", REPORT_ID);
            list.AddSqlParameter("@ITEM_RATE_LIST", ITEM_RATE_LIST);
            list.AddSqlParameter("@ITEM_WISE_SALES", ITEM_WISE_SALES);
            list.AddSqlParameter("@DAY_WISE_SALES", DAY_WISE_SALES);
            list.AddSqlParameter("@MONTH_WISE_SALE", MONTH_WISE_SALE);
            list.AddSqlParameter("@TAX_REPORT", TAX_REPORT);
            list.AddSqlParameter("@BILL_WISE_SALES", BILL_WISE_SALES);
            list.AddSqlParameter("@USERNAME", USERNAME);
            list.AddSqlParameter("@DESIGNATION", DESIGNATION);
            list.AddSqlParameter("@MIS_COLLECTION", MIS_COLLECTION);
            list.AddSqlParameter("@INSERT_BY", login.u);
            INSERT_DATE = DateTime.Today.Date;
            list.AddSqlParameter("@INSERT_DATE", INSERT_DATE);
            string s = "INSERT INTO REPORTPERMISSIONS(USERNAME,DESIGNATION,ITEM_RATE_LIST,ITEM_WISE_SALES,DAY_WISE_SALES,MONTH_WISE_SALE,TAX_REPORT,BILL_WISE_SALES,MIS_COLLECTION,INSERT_BY,INSERT_DATE)VALUES(@USERNAME,@DESIGNATION,@ITEM_RATE_LIST,@ITEM_WISE_SALES,@DAY_WISE_SALES,@MONTH_WISE_SALE,@TAX_REPORT,@BILL_WISE_SALES,@MIS_COLLECTION,@INSERT_BY,@INSERT_DATE)";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public void ReportUpdate()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@REPORT_ID", REPORT_ID);
            list.AddSqlParameter("@ITEM_RATE_LIST", ITEM_RATE_LIST);
            list.AddSqlParameter("@ITEM_WISE_SALES", ITEM_WISE_SALES);
            list.AddSqlParameter("@DAY_WISE_SALES", DAY_WISE_SALES);
            list.AddSqlParameter("@MONTH_WISE_SALE", MONTH_WISE_SALE);
            list.AddSqlParameter("@TAX_REPORT", TAX_REPORT);
            list.AddSqlParameter("@BILL_WISE_SALES", BILL_WISE_SALES);
            list.AddSqlParameter("@USERNAME", USERNAME);
            list.AddSqlParameter("@DESIGNATION", DESIGNATION);
            list.AddSqlParameter("@MIS_COLLECTION", MIS_COLLECTION);
            list.AddSqlParameter("@UPDATE_BY", login.u);
            UPDATE_DATE = DateTime.Today.Date;
            list.AddSqlParameter("@UPDATE_DATE", UPDATE_DATE);
            string s = "UPDATE REPORTPERMISSIONS SET DESIGNATION=@DESIGNATION,ITEM_RATE_LIST=@ITEM_RATE_LIST,ITEM_WISE_SALES=@ITEM_WISE_SALES,DAY_WISE_SALES=@DAY_WISE_SALES,MONTH_WISE_SALE=@MONTH_WISE_SALE,TAX_REPORT=@TAX_REPORT,BILL_WISE_SALES=@BILL_WISE_SALES,MIS_COLLECTION=@MIS_COLLECTION WHERE USERNAME='" + USERNAME+"'";
            DbFunctions.ExecuteCommand<int>(s, list);
        }
        public DataTable username()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT REG_UserName FROM FCREG";
            DataTable DT = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return DT;
        }
        public DataTable MASTER()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT * FROM MASTERPERMISSIONS WHERE USERNAME ='" + USERNAME + "'";
            DataTable D = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return D;
        }
        public DataTable MASTERUSER()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT USERNAME FROM MASTERPERMISSIONS";
            DataTable D = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return D;
        }
        public DataTable MASTER1()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT * FROM MASTERPERMISSIONS WHERE USERNAME ='" + login.u + "'";
            DataTable D = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return D;
        }
        public DataTable OPERATION()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT * FROM OPERATIONPERMISSIONS WHERE USERNAME ='" + USERNAME + "'";
            DataTable D = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return D;
        }
        public DataTable OPERATION1()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT * FROM OPERATIONPERMISSIONS WHERE USERNAME ='" + login.u + "'";
            DataTable D = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return D;
        }
        public DataTable OPERATIONUSER()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT USERNAME FROM OPERATIONPERMISSIONS";
            DataTable D = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return D;
        }
        public DataTable Report()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT * FROM REPORTPERMISSIONS WHERE USERNAME ='" + USERNAME + "'";
            DataTable D = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return D;
        }
        public DataTable Report1()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT * FROM REPORTPERMISSIONS WHERE USERNAME ='" + login.u + "'";
            DataTable D = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return D;
        }
        public DataTable ReportUser()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT USERNAME FROM REPORTPERMISSIONS";
            DataTable D = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return D;
        }
        public DataTable regdes()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT DESIGNATION FROM FCREG WHERE REG_UserName ='" + REG_UserName + "'";
            DataTable D = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return D;
        }
       
    }
}
