using DB;
using IRepo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Repo
{
    public class PeopleInfo: IPeopleInfo
    {
        IDBService m_DBService;
        public PeopleInfo(IDBService dBService)
        {
            m_DBService = dBService;
        }

        public DataSet GetPeopleList()
        {
            SqlCommand sqlcmd = new SqlCommand();
            SqlParameterCollection sqlPra = sqlcmd.Parameters;

            return m_DBService.SP_GetDataSet("GetPeople", sqlcmd);
        }







    }
}
