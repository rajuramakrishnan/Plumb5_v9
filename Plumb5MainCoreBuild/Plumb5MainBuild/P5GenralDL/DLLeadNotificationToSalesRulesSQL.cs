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
    internal class DLLeadNotificationToSalesRulesSQL : CommonDataBaseInteraction, IDLLeadNotificationToSalesRules
    {
        CommonInfo connection;
        public DLLeadNotificationToSalesRulesSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async Task<IEnumerable<LeadNotificationToSalesRules>> GetLeadNotificationToSales(int Id = 0, bool? Status = null)
        {
            string storeProcCommand = "LeadNotification_ToSalesRules";
            object? param = new { Action="Get",Id, Status };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<LeadNotificationToSalesRules>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

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
