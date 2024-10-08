﻿using Dapper;
using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLLmsCustomFieldsPG : CommonDataBaseInteraction, IDLLmsCustomFields
    {
        CommonInfo connection;
        public DLLmsCustomFieldsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLmsCustomFieldsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task SaveProperty(LmsCustomFields lmsCustomFields)
        {
            string storeProcCommand = "select * from lmscustomfields_saveproperty(@FieldDisplayName, @Position, @OverrideBy, @SearchBy, @FieldName, @PublisherField, @PublisherSearchBy)";
            object? param = new { lmsCustomFields.FieldDisplayName, lmsCustomFields.Position, lmsCustomFields.OverrideBy, lmsCustomFields.SearchBy, lmsCustomFields.FieldName, lmsCustomFields.PublisherField, lmsCustomFields.PublisherSearchBy };

            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<bool> Delete()
        {
            string storeProcCommand = "select * from lmscustomfields_delete()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }

        public async Task<List<LmsCustomFields>> GetPurgeSettings()
        {
            string storeProcCommand = "select * from lmscustomfields_getdetails()";
            object? param = new { };
            
            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<LmsCustomFields>(storeProcCommand, param)).ToList();

        }

        public async Task<List<LmsCustomFields>> GetDetails()
        {
            
            string storeProcCommand = "select * from lms_customfields_get()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<LmsCustomFields>(storeProcCommand, param)).ToList();

        }

        public async Task<List<MLContactFieldEditSetting>> GetMLIsSearchbyColumn()
        { 
            string storeProcCommand = "select * from lms_customfields_getsearchbycolum()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLContactFieldEditSetting>(storeProcCommand, param)).ToList();

        }

        public async Task<List<MLContactFieldEditSetting>> GetMLIsPublisher()
        {
           
            string storeProcCommand = "select * from lms_customfields_getispublishercolum()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLContactFieldEditSetting>(storeProcCommand, param)).ToList();

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
