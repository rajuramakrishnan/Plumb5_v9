﻿using IP5GenralDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLWorkFlowBulkSmsSent
    {
        public static IDLWorkFlowBulkSmsSent GetDLWorkFlowBulkSmsSent(int AccountId, string vendor)
        {
            if (vendor.Equals("mssql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLWorkFlowBulkSmsSentSQL(AccountId);
            }
            else if (vendor.Equals("npgsql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLWorkFlowBulkSmsSentPG(AccountId);
            }
            throw new ArgumentException("Unknown sql vendor: " + vendor);
        }

    }
}
