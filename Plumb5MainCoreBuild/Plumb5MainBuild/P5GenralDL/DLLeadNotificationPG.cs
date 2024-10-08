﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLLeadNotificationPG : CommonDataBaseInteraction, IDLLeadNotification
    {
        CommonInfo connection;
        public DLLeadNotificationPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLeadNotificationPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        //Lead Notification

        public async Task<Int32> SaveNotificationForLead(LeadNotification lmsleadassigntouser)
        {
            string storeProcCommand = "select * from lead_notification_save(@Mail,@Sms,@WhatsApp)";
            object? param = new { lmsleadassigntouser.Mail, lmsleadassigntouser.Sms, lmsleadassigntouser.WhatsApp };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<LeadNotification?> GetNotificationForLead()
        {
            string storeProcCommand = "select * from lead_notification_get()";
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<LeadNotification?>(storeProcCommand);
        }

        //Lead Notification To Sales

        #region Dispose Method
        private bool _disposed = false;
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                if (disposing)
                {
                    connection = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion End of Dispose Method
    }
}
