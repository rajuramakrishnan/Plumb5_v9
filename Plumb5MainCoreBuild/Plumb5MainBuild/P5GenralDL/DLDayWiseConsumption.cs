﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLDayWiseConsumption
    {
        public static IDLDayWiseConsumption GetDLDayWiseConsumption(int AccountId = 0, string vendor = null)
        {
            if (vendor.Equals("mssql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLDayWiseConsumptionSQL(AccountId);
            }
            else if (vendor.Equals("npgsql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLDayWiseConsumptionPG();
            }
            throw new ArgumentException("Unknown sql vendor: " + vendor);
        }
    }
}
