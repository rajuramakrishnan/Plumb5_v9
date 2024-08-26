﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLCustomEventImportErrorPG : CommonDataBaseInteraction, IDLCustomEventImportError
    {
        CommonInfo connection = null;
        public DLCustomEventImportErrorPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLCustomEventImportErrorPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(CustomEventImportError eventError)
        {
            string storeProcCommand = "select customeventimport_error_save(@EmailId, @PhoneNumber, @EventImportOverViewId, @RejectReason)";
            object? param = new { eventError.EmailId, eventError.PhoneNumber, eventError.EventImportOverViewId, eventError.RejectReason };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<List<CustomEventImportError>> GetList(int EventImportOverViewId, int OffSet = 0, int FetchNext = 0)
        {
            string storeProcCommand = "select *  from customeventimport_error_getlist(@OffSet, @FetchNext, @EventImportOverViewId)";
            object? param = new { OffSet, FetchNext, EventImportOverViewId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<CustomEventImportError>(storeProcCommand, param)).ToList();
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

