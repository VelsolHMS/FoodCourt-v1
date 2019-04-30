using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodcourt.Model
{
    class Reports
    {
        public static string Name, GST, Address;
        public DataTable PRPT()
        {
            var list = new List<SqlParameter>();
            string s = "Select PRPT_Name,PRPT_GST,CONCAT(PRPT_Address, ', ', PRPT_State, ', ', PRPT_Country, '.') AS PRPT_Address from FCPRPTY";
            DataTable d = DbFunctions.ExecuteCommand<DataTable>(s, list);
            if (d.Rows.Count == 0)
            {
            }
            else
            {
                Name = d.Rows[0]["PRPT_Name"].ToString();
                Address = d.Rows[0]["PRPT_Address"].ToString();
                GST = d.Rows[0]["PRPT_GST"].ToString();
            }
            return d;
        }
        public DataTable ItemRates()
        {
            var list = new List<SqlParameter>();
            string s = "Select NAM_Name,NAM_Rate,(select TAX_Percentage from FCTAX where TAX_Name = A.NAM_Tax) AS NAM_TaxPer from FCITMNAM A";
            DataTable d = DbFunctions.ExecuteCommand<DataTable>(s, list);
            return d;
        }
    }
}
