﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLBlackListPassword
    {
        public static IDLBlackListPassword GetDLBlackListPassword(string vendor)
        {
            if (vendor.Equals("mssql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLBlackListPasswordSQL();
            }
            else if (vendor.Equals("npgsql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLBlackListPasswordPG();
            }
            throw new ArgumentException("Unknown sql vendor: " + vendor);
        }
    }
}
