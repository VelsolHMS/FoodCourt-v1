using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ListExtentions
/// </summary>
public static class ListExtentions
{
   public static void AddSqlParameter(this List<SqlParameter> list, string pName, object pValue)
   {
        var sqlParam = new SqlParameter()
        {
            ParameterName = pName,
            Value = pValue ??(object)DBNull.Value
        };
        list.Add(sqlParam);
   }
}