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
    public class DLLmsCustomFieldsSQL : CommonDataBaseInteraction, IDLLmsCustomFields
    {
        CommonInfo connection;
        public DLLmsCustomFieldsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLmsCustomFieldsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task SaveProperty(LmsCustomFields lmsCustomFields)
        {

            string storeProcCommand = "Lms_CustomFields";
            object? param = new { Action = "Save", lmsCustomFields.FieldDisplayName, lmsCustomFields.Position, lmsCustomFields.OverrideBy, lmsCustomFields.SearchBy, lmsCustomFields.FieldName, lmsCustomFields.PublisherField, lmsCustomFields.PublisherSearchBy };

            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> Delete()
        {

            string storeProcCommand = "Lms_CustomFields";
            object? param = new { Action = "Delete" };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<List<LmsCustomFields>> GetPurgeSettings()
        {
            string storeProcCommand = "Lms_CustomFields";
            object? param = new { Action = "GetDetails" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<LmsCustomFields>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<List<LmsCustomFields>> GetDetails()
        {
            string storeProcCommand = "Lms_CustomFields";
            object? param = new { Action = "Get" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<LmsCustomFields>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<List<MLContactFieldEditSetting>> GetMLIsSearchbyColumn()
        {
            string storeProcCommand = "Lms_CustomFields";
            object? param = new { Action = "GetSearchByColumn" };
            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLContactFieldEditSetting>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<List<MLContactFieldEditSetting>> GetMLIsPublisher()
        {
            string storeProcCommand = "Lms_CustomFields";
            object? param = new { Action = "GetisPublisherColumn" };
            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLContactFieldEditSetting>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

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
