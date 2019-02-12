using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Foodcourt
{
    public static class ListExtensions
    {
        public static void AddSqlParameter(this List<SqlParameter> list, string pName, object pValue)
        {
            var sqlParam = new SqlParameter()
            {
                ParameterName = pName,
                Value = pValue ?? (object)DBNull.Value
            };

            list.Add(sqlParam);
        }
    }
}
