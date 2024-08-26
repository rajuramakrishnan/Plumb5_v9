﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLWebPushCampaignResponseReport
    {
        public static IDLWebPushCampaignResponseReport GetDLWebPushCampaignResponseReport(int AccountId, string vendor)
        {
            if (vendor.Equals("mssql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLWebPushCampaignResponseReportSQL(AccountId);
            }
            else if (vendor.Equals("npgsql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLWebPushCampaignResponseReportPG(AccountId);
            }
            throw new ArgumentException("Unknown sql vendor: " + vendor);
        }
    }
}
