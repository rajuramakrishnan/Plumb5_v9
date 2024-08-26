﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLErrorUpdation
    {
        public static IDLErrorUpdation GetDLErrorUpdation(string vendor)
        {
            if (vendor.Equals("mssql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLErrorUpdationSQL();
            }
            else if (vendor.Equals("npgsql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLErrorUpdationPG();
            }
            throw new ArgumentException("Unknown sql vendor: " + vendor);
        }
    }
}
