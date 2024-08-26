﻿using IP5GenralDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLCustomDetailsFormTracking
    {
        public static IDLCustomDetailsFormTracking GetDLCustomDetailsFormTracking(int AccountId, string vendor)
        {
            if (vendor.Equals("mssql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLCustomDetailsFormTrackingSQL(AccountId);
            }
            else if (vendor.Equals("npgsql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLCustomDetailsFormTrackingPG(AccountId);
            }
            throw new ArgumentException("Unknown sql vendor: " + vendor);
        }
    }
}
