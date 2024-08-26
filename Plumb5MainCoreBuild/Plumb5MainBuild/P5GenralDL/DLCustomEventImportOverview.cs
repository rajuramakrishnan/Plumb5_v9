﻿using IP5GenralDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLCustomEventImportOverview
    {
        public static IDLCustomEventImportOverview GetDLCustomEventImportOverview(int AccountId, string vendor)
        {
            if (vendor.Equals("mssql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLCustomEventImportOverviewSQL(AccountId);
            }
            else if (vendor.Equals("npgsql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLCustomEventImportOverviewPG(AccountId);
            }
            throw new ArgumentException("Unknown sql vendor: " + vendor);
        }
    }
}
