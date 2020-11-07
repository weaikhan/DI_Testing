using System;
using System.Collections.Generic;
using System.Data;

namespace IRepo
{
    public interface IPeopleInfo
    {
        public DataSet GetPeopleList();
    }
}
