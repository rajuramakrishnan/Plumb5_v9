﻿using IP5GenralDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLUniqueVisitors
    {
        public static IDLUniqueVisitors Get_Plumb5DLUniqueVisitors(string  Connection, string vendor,int accountId=0)
        {
            if (vendor.Equals("mssql", StringComparison.OrdinalIgnoreCase))
            {
                if(accountId>0)
                    return new _Plumb5DLUniqueVisitorsSQL(accountId);
                else
                    return new _Plumb5DLUniqueVisitorsSQL(Connection);
            }
            else if (vendor.Equals("npgsql", StringComparison.OrdinalIgnoreCase))
            {
                if (accountId>0)
                    return new _Plumb5DLUniqueVisitorsPG(accountId);
                else
                    return new _Plumb5DLUniqueVisitorsPG(Connection);
            }
            throw new ArgumentException("Unknown sql vendor: " + vendor);
        }
    }
}
