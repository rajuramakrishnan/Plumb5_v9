﻿using IP5GenralDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLAdminMasterCreditAudit
    {
        public static IDLAdminMasterCreditAudit GETDLAdminMasterCreditAudit(string vendor)
        {
            if (vendor.Equals("mssql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLAdminMasterCreditAuditSQL();
            }
            else if (vendor.Equals("npgsql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLAdminMasterCreditAuditPG();
            }
            throw new ArgumentException("Unknown sql vendor: " + vendor);
        }
    }
}
