﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLCustomEventImportErrorSQL : CommonDataBaseInteraction, IDLCustomEventImportError
    {
        CommonInfo connection = null;
        public DLCustomEventImportErrorSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLCustomEventImportErrorSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(CustomEventImportError eventError)
        {
            string storeProcCommand = "CustomEventImport_Error";
            object? param = new { @Action = "Save", eventError.EmailId, eventError.PhoneNumber, eventError.EventImportOverViewId, eventError.RejectReason };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<CustomEventImportError>> GetList(int EventImportOverViewId, int OffSet = 0, int FetchNext = 0)
        {
            string storeProcCommand = "CustomEventImport_Error";
            object? param = new { @Action = "GetList", OffSet, FetchNext, EventImportOverViewId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<CustomEventImportError>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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

