﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLFormBannerSQL : CommonDataBaseInteraction, IDLFormBanner
    {
        CommonInfo connection = null;
        public DLFormBannerSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLFormBannerSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(FormBanner formbanner)
        {
            string storeProcCommand = "Form_Banner";
            object? param = new { @Action = "Save", formbanner.FormId, formbanner.Name, formbanner.BannerContent, formbanner.RedirectUrl, formbanner.Impression, formbanner.BannerStatus, formbanner.BannerId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Update(FormBanner formbanner)
        {
            string storeProcCommand = "Form_Banner";
            object? param = new { @Action = "Update", formbanner.Id, formbanner.FormId, formbanner.Name, formbanner.BannerContent, formbanner.RedirectUrl, formbanner.BannerStatus, formbanner.BannerId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }
        public async Task<List<FormBanner>> GET(int FormId)
        {
            string storeProcCommand = "Form_Banner";
            object? param = new { @Action = "GET", FormId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<FormBanner>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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


