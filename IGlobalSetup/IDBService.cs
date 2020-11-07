using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DB
{
    public interface IDBService
    {
        List<Dictionary<string, object>> ExecuteReader(string sqlText);


        object ExecuteScalar(string sqlText);


        int ExecuteNonQuery(string sqlText);


        int BulkInsert(string prepareText, List<object[]> BatchValues);

        List<string> SP_GetParams(string spName);

        DataSet SP_GetDataSet(string spName, SqlCommand sqlcmd);

        int SP_ExecuteNonQuery(string spName, SqlCommand sqlcmd);

    }
}
