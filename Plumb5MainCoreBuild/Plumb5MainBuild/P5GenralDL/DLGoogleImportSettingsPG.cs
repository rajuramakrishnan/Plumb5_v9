﻿using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace P5GenralDL
{
    public class DLGoogleImportSettingsPG : CommonDataBaseInteraction, IDLGoogleImportSettings
    {
        CommonInfo connection = null;
        public DLGoogleImportSettingsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLGoogleImportSettingsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<GoogleImportSettings?> GetRunningDetails(bool IsRecurring)
        {
            string storeProcCommand = "select * from googleimportsettings_get(@IsRecurring)";
            object? param = new { IsRecurring };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<GoogleImportSettings?>(storeProcCommand, param);

        }

        public async Task<List<GoogleImportSettings>> GetCount(bool IsRecurring)
        {
            string storeProcCommand = "select * from googleimportsettings_getdetailscount(@IsRecurring)";
            object? param = new { IsRecurring };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<GoogleImportSettings>(storeProcCommand, param)).ToList();

        }

        public async Task<GoogleImportSettings?> GetDetails(int googleimportsettingsid, bool IsRecurring)
        {
            string storeProcCommand = "select * from googleimportsettings_getdetailstoimport(@googleimportsettingsid, @IsRecurring)";
            object? param = new { googleimportsettingsid, IsRecurring };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<GoogleImportSettings?>(storeProcCommand, param);

        }

        public async Task<bool> UpdateStatus(GoogleImportSettings googleImportSettingss)
        {
            string storeProcCommand = "select * from googleimportsettings_updatestatus(@Id, @ErrorMessage, @IsCompleted)";
            object? param = new { googleImportSettingss.Id, googleImportSettingss.ErrorMessage, googleImportSettingss.IsCompleted };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }

        public async Task<bool> UpdateRecurringStatus(bool IsRecurring)
        {
            string storeProcCommand = "select * from googleimportsettings_updaterecurring(@IsRecurring)";
            object? param = new { IsRecurring };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }

        public async Task<Int32> Save(GoogleImportSettings googleimportsettings)
        {
            string storeProcCommand = "select * from googleimportsettings_save(@GroupId, @GoogleAccountSettingsId, @GoogleGroupId, @Days, @Times, @IsRecurring, @Status, @ErrorMessage, @LastExecutationTime, @GoogleAudienceName)";
            object? param = new { googleimportsettings.GroupId, googleimportsettings.GoogleAccountSettingsId, googleimportsettings.GoogleGroupId, googleimportsettings.Days, googleimportsettings.Times, googleimportsettings.IsRecurring, googleimportsettings.Status, googleimportsettings.ErrorMessage, googleimportsettings.LastExecutationTime, googleimportsettings.GoogleAudienceName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<Int32> Update(GoogleImportSettings googleimportsettings)
        {
            string storeProcCommand = "select * from googleimportsettings_update(@Id, @GroupId, @GoogleAccountSettingsId, @GoogleGroupId, @Days, @Times, @IsRecurring, @Status, @ErrorMessage, @LastExecutationTime, @GoogleAudienceName)";
            object? param = new { googleimportsettings.Id, googleimportsettings.GroupId, googleimportsettings.GoogleAccountSettingsId, googleimportsettings.GoogleGroupId, googleimportsettings.Days, googleimportsettings.Times, googleimportsettings.IsRecurring, googleimportsettings.Status, googleimportsettings.ErrorMessage, googleimportsettings.LastExecutationTime, googleimportsettings.GoogleAudienceName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }


        public async Task<int> MaxCount(DateTime fromDateTime, DateTime toDateTime, string Groupname)
        {
            string storeProcCommand = "select * from googleimportsettings_maxcount(@fromDateTime, @toDateTime, @Groupname)";
            object? param = new { fromDateTime, toDateTime, Groupname };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }
        public async Task<List<GoogleImportSettings>> GetOverviewDetails(DateTime fromDateTime, DateTime toDateTime, int OffSet, int FetchNext, string Groupname)
        {
            string storeProcCommand = "select * from googleimportsettings_getoverview(@fromDateTime, @toDateTime, @Groupname, @OffSet, @FetchNext)";
            object? param = new { fromDateTime, toDateTime, Groupname, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<GoogleImportSettings>(storeProcCommand, param)).ToList();

        }
        public async Task<int> ChangeStatusadwords(int Id)
        {
            string storeProcCommand = "select * from googleimportsettings_changestatus(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }
        public async Task<int> Delete(int Id)
        {
            string storeProcCommand = "select * from googleimportsettings_delete(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

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
