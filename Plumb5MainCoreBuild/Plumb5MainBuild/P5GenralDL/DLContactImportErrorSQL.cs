﻿using Dapper;
using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    internal class DLContactImportErrorSQL: CommonDataBaseInteraction, IDLContactImportError
    {
        CommonInfo connection;
        public DLContactImportErrorSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLContactImportErrorSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(ContactImportError contactError)
        {
            string storeProcCommand = "ContactImport_Error";
            object? param = new { Action= "Save", contactError.EmailId, contactError.PhoneNumber, contactError.ContactInfoInString, contactError.ContactImportOverviewId, contactError.RejectReason };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<int> GetMaxCount(int ContactImportOverviewId)
        {
            string storeProcCommand = "ContactImport_Error";
            object? param = new { Action = "GetMaxCount", ContactImportOverviewId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<ContactImportError>> GetList(int ContactImportOverviewId, int OffSet, int FetchNext)
        {
            string storeProcCommand = "ContactImport_Error";
            object? param = new { Action= "GetList", OffSet, FetchNext, ContactImportOverviewId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ContactImportError>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

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
