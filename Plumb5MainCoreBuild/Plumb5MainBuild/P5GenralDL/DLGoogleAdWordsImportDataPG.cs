﻿using Dapper;
using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLGoogleAdWordsImportDataPG : CommonDataBaseInteraction, IDLGoogleAdWordsImportData
    {
        CommonInfo connection = null;
        public DLGoogleAdWordsImportDataPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLGoogleAdWordsImportDataPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<GoogleAdWordsImportData?> GetDetailsAsync(int Id)
        {
            string storeProcCommand = "select * from googleadwordsimportdata_getdetails(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<GoogleAdWordsImportData?>(storeProcCommand, param);

        }
        public async Task<Int32> Save(GoogleAdWordsImportData GoogleAdwords)
        {
            string storeProcCommand = "select * from googleadwordsimportdata_save(@UserInfoUserId, @Name, @MappingFields, @APIResponseId, @Status, @ErrorMessage, @TimeZone, @LmsGroupId, @OverrideSources, @MappingLmscustomFields )";
            object? param = new { GoogleAdwords.UserInfoUserId, GoogleAdwords.Name, GoogleAdwords.MappingFields, GoogleAdwords.APIResponseId, GoogleAdwords.Status, GoogleAdwords.ErrorMessage, GoogleAdwords.TimeZone, GoogleAdwords.LmsGroupId, GoogleAdwords.OverrideSources, GoogleAdwords.MappingLmscustomFields };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<bool> Update(GoogleAdWordsImportData GoogleAdwords)
        {
            string storeProcCommand = "select * from googleadwordsimportdata_update(@Id, @UserInfoUserId, @Name, @MappingFields, @APIResponseId, @Status, @ErrorMessage, @TimeZone, @LmsGroupId, @OverrideSources, @MappingLmscustomFields)";
            object? param = new { GoogleAdwords.Id, GoogleAdwords.UserInfoUserId, GoogleAdwords.Name, GoogleAdwords.MappingFields, GoogleAdwords.APIResponseId, GoogleAdwords.Status, GoogleAdwords.ErrorMessage, GoogleAdwords.TimeZone, GoogleAdwords.LmsGroupId, GoogleAdwords.OverrideSources, GoogleAdwords.MappingLmscustomFields };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }
        public async Task<List<GoogleAdWordsImportData>> GetDetails()
        {
            string storeProcCommand = "select * from googleadwordsimportdata_getdetails(@Id)";
            object? param = new { Id = 0 };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<GoogleAdWordsImportData>(storeProcCommand, param)).ToList();

        }
        public async Task<int> ChangeStatusadwords(int Id)
        {
            string storeProcCommand = "select * from googleadwordsimportdata_changestatus(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }
        public async Task<bool> DeleteadwordsData(int Id)
        {
            string storeProcCommand = "select * from googleadwordsimportdata_delete(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }

        #region Dispose Method
        bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    connection = null;
                }
            }
            //dispose unmanaged ressources
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion End of Dispose Method
    }
}
