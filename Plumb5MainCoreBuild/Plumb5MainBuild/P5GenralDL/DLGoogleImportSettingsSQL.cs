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
    public class DLGoogleImportSettingsSQL : CommonDataBaseInteraction, IDLGoogleImportSettings
    {
        CommonInfo connection = null;
        public DLGoogleImportSettingsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLGoogleImportSettingsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<GoogleImportSettings?> GetRunningDetails(bool IsRecurring)
        {
            string storeProcCommand = "Google_ImportSettings";
            object? param = new { Action = "Delete", IsRecurring };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<GoogleImportSettings?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<GoogleImportSettings>> GetCount(bool IsRecurring)
        {
            string storeProcCommand = "Google_ImportSettings";
            object? param = new { Action = "GetCount", IsRecurring };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<GoogleImportSettings>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<GoogleImportSettings?> GetDetails(int googleimportsettingsid, bool IsRecurring)
        {
            string storeProcCommand = "Google_ImportSettings";
            object? param = new { Action = "Import", googleimportsettingsid, IsRecurring };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<GoogleImportSettings?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> UpdateStatus(GoogleImportSettings googleImportSettingss)
        {
            string storeProcCommand = "Google_ImportSettings";
            object? param = new { Action = "UpdateStatus", googleImportSettingss.Id, googleImportSettingss.ErrorMessage, googleImportSettingss.IsCompleted };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<bool> UpdateRecurringStatus(bool IsRecurring)
        {
            string storeProcCommand = "Google_ImportSettings";
            object? param = new { Action = "UpdateRecurring", IsRecurring };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<Int32> Save(GoogleImportSettings googleimportsettings)
        {
            string storeProcCommand = "Google_ImportSettings";
            object? param = new { Action = "Save", googleimportsettings.GroupId, googleimportsettings.GoogleAccountSettingsId, googleimportsettings.GoogleGroupId, googleimportsettings.Days, googleimportsettings.Times, googleimportsettings.IsRecurring, googleimportsettings.Status, googleimportsettings.ErrorMessage, googleimportsettings.LastExecutationTime, googleimportsettings.GoogleAudienceName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<Int32> Update(GoogleImportSettings googleimportsettings)
        {
            string storeProcCommand = "Google_ImportSettings";
            object? param = new { Action = "UpdateAll", googleimportsettings.Id, googleimportsettings.GroupId, googleimportsettings.GoogleAccountSettingsId, googleimportsettings.GoogleGroupId, googleimportsettings.Days, googleimportsettings.Times, googleimportsettings.IsRecurring, googleimportsettings.Status, googleimportsettings.ErrorMessage, googleimportsettings.LastExecutationTime, googleimportsettings.GoogleAudienceName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }


        public async Task<int> MaxCount(DateTime fromDateTime, DateTime toDateTime, string Groupname)
        {
            string storeProcCommand = "Google_ImportSettings";
            object? param = new { Action = "GetOverViewCount", fromDateTime, toDateTime, Groupname };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }
        public async Task<List<GoogleImportSettings>> GetOverviewDetails(DateTime fromDateTime, DateTime toDateTime, int OffSet, int FetchNext, string Groupname)
        {
            string storeProcCommand = "Google_ImportSettings";
            object? param = new { Action = "GetOverView", fromDateTime, toDateTime, Groupname, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<GoogleImportSettings>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }
        public async Task<int> ChangeStatusadwords(int Id)
        {
            string storeProcCommand = "Google_ImportSettings";
            object? param = new { Action = "ChangeStatus", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }
        public async Task<int> Delete(int Id)
        {
            string storeProcCommand = "Google_ImportSettings";
            object? param = new {Action= "Delete", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

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
