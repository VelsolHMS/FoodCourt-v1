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
    class tax
    {
        public string TAX_ID { get; set; }
        public string TAX_Name { get; set; }
        public decimal TAX_Percentage { get; set; }
        public string Tax_ReportingName { get; set; }
        public string Tax_InsertBy { get; set; }
        public DateTime Tax_InsertDate { get; set; }
        public string Tax_UpdateBy { get; set; }
        public DateTime Tax_UpdateDate { get; set; }

        public void Insert()
        {
            var list = new List<SqlParameter>();
            list.AddSqlParameter("@TAX_Name", TAX_Name);
            list.AddSqlParameter("@TAX_Percentage", TAX_Percentage);
            list.AddSqlParameter("@Tax_ReportingName", Tax_ReportingName);
            list.AddSqlParameter("@Tax_InsertBy", login.u);
            list.AddSqlParameter("@Tax_InsertDate", DateTime.Today.ToShortDateString());
            list.AddSqlParameter("@Tax_UpdateBy", login.u);
            list.AddSqlParameter("@Tax_UpdateDate", DateTime.Today.ToShortDateString());

            string s = "INSERT INTO FCTAX(TAX_Name,TAX_Percentage,Tax_ReportingName,Tax_InsertBy,Tax_InsertDate,Tax_UpdateBy,Tax_UpdateDate)" +
                       "VALUES (@TAX_Name,@TAX_Percentage,@Tax_ReportingName,@Tax_InsertBy,@Tax_InsertDate,@Tax_UpdateBy,@Tax_UpdateDate)";
            DbFunctions.ExecuteCommand<DataTable>(s, list);
        }
        public DataTable GetTaxNames()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT TAX_Name FROM FCTAX";
            DataTable tax_name = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return tax_name;
        }
        public DataTable CheckTaxName()
        {
            var list = new List<SqlParameter>();
            string s = "SELECT TAX_Name FROM FCTAX WHERE TAX_Name = '" + TAX_Name + "'";
            DataTable tax_name = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return tax_name;
        }
    }
}
