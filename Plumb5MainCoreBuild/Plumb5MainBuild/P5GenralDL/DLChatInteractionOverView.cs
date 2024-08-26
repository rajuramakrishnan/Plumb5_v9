﻿using IP5GenralDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLChatInteractionOverView
    {
        public static IDLChatInteractionOverView GetDLChatInteractionOverView(int AccountId, string vendor)
        {
            if (vendor.Equals("mssql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLChatInteractionOverViewSQL(AccountId);
            }
            else if (vendor.Equals("npgsql", StringComparison.OrdinalIgnoreCase))
            {
                return new DLChatInteractionOverViewPG(AccountId);
            }
            throw new ArgumentException("Unknown sql vendor: " + vendor);
        }
    }
}
