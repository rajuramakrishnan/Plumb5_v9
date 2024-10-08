﻿using IP5GenralDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLUserInValidLogin
    {
        public static IDLUserInValidLogin GetDLUserInValidLogin(string vendor)
        {
            if (vendor.Equals("mssql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLUserInValidLoginSQL();
            }
            else if (vendor.Equals("npgsql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLUserInValidLoginPG();
            }
            throw new ArgumentException("Unknown sql vendor: " + vendor);
        }
    }
}
