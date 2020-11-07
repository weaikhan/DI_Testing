using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;


namespace DB
{
    public class SqlServer : IDBService, IDisposable
    {

        public SqlServer(SqlConnection conn, string connstring)
        {
            _conn = conn;
            _connstring = connstring;

        }

        public void Dispose()
        {
            if (_conn.State != ConnectionState.Closed)
            {

            }
        }

        #region -----SQL statement-----
        public object ExecuteScalar(string sqlText)
        {
            SqlConnection myconn = null;

            try
            {
                myconn = new SqlConnection(_connstring);
                myconn.Open();

                using (var cmd = new SqlCommand(sqlText, myconn))
                {
                    return cmd.ExecuteScalar();
                }
            }
            finally
            {
                if (myconn != null)
                    myconn.Close();
            }
        }


        public int ExecuteNonQuery(string sqlText)
        {
            SqlConnection myconn = null;

            try
            {
                myconn = new SqlConnection(_connstring);
                myconn.Open();

                using (var cmd = new SqlCommand(sqlText, myconn))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (myconn != null)
                    myconn.Close();
            }

        }

        public List<Dictionary<string, object>> ExecuteReader(string sqlText)
        {
            SqlConnection myconn = null;

            try
            {
                myconn = new SqlConnection(_connstring);
                myconn.Open();

                using (var cmd = new SqlCommand(sqlText, myconn))
                using (var reader = cmd.ExecuteReader())
                {
                    var cols = reader.GetColumnSchema()
                        .Select((r, index) => (index, r.ColumnName))
                        .ToList();

                    var res = new List<Dictionary<string, object>>();

                    while (reader.Read())
                    {
                        res.Add(
                            cols.ToDictionary
                            (
                                col => col.ColumnName,
                                col => reader.IsDBNull(col.index) ? GetDefault(reader.GetFieldType(col.index)) : reader.GetValue(col.index)
                            )
                        );
                    }

                    return res;
                }
            }
            finally
            {
                if (myconn != null)
                    myconn.Close();
            }

        }
        #endregion -----SQL statement-----

        public List<string> SP_GetParams(string spName)
        {
            SqlConnection myconn = null;
            List<string> result = new List<string>();

            try
            {
                myconn = new SqlConnection(_connstring);
                myconn.Open();

                using (SqlCommand sqlCmd = new SqlCommand(spName, myconn))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlCommandBuilder.DeriveParameters(sqlCmd);
                    foreach (SqlParameter p in sqlCmd.Parameters)
                    {
                        result.Add(p.ParameterName.ToString().ToUpper());
                    }
                }
            }
            finally
            {
                if (myconn != null)
                    myconn.Close();

            }

            return result;
        }
        public int SP_ExecuteNonQuery(string spName, SqlCommand sqlcmd)
        {
            SqlConnection myconn = null;
            int reval = 0;

            try
            {
                myconn = new SqlConnection(_connstring);
                myconn.Open();

                using (sqlcmd)
                {
                    sqlcmd.Connection = myconn;
                    sqlcmd.CommandText = spName;
                    sqlcmd.CommandType = CommandType.StoredProcedure;

                    reval = sqlcmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (myconn != null)
                    myconn.Close();
            }

            return reval;
        }

        public DataSet SP_GetDataSet(string spName, SqlCommand cmd)
        {
            SqlConnection myconn = null;

            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                try
                {
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    return ds;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            finally
            {
                if (myconn != null)
                    myconn.Close();
            }

        }


        public static object GetDefault(Type type)
        {
            return DefaultVal.ContainsKey(type.FullName) ? DefaultVal[type.FullName] : null;
        }

        public int BulkInsert(string prepareText, List<object[]> BatchValues)
        {
            throw new NotImplementedException();
        }

        static Dictionary<string, object> DefaultVal = new Dictionary<string, object>
        {
            { "System.String", string.Empty },
            { "System.Int32", 0 },
            { "System.Int64", 0 },
            { "System.Decimal",  0.0 },
            { "System.Double",  0.0 },
            { "System.Money",  0.0 },
            { "System.DateTime", DateTime.MinValue },
        };

        SqlConnection _conn;
        string _connstring = string.Empty;
    }
}
