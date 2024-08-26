﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;
using System;

namespace P5GenralDL
{
    public class DLLandingPageSQL : CommonDataBaseInteraction, IDLLandingPage
    {
        CommonInfo connection;
        public DLLandingPageSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLandingPageSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(LandingPage landingPage)
        {
            string storeProcCommand = "Landing_Page";
            object? param = new { @Action = "Save", landingPage.UserInfoUserId, landingPage.UserGroupId, landingPage.PageName, landingPage.PageDescription, landingPage.LandingPageConfigurationId, landingPage.IsBeeTemplate, landingPage.IsTemplateSaved };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);
        }

        public async Task<bool> Update(LandingPage landingPage)
        {
            string storeProcCommand = "Landing_Page";
            object? param = new { @Action = "Update", landingPage.Id, landingPage.UserInfoUserId, landingPage.UserGroupId, landingPage.PageName, landingPage.PageDescription, landingPage.LandingPageConfigurationId, landingPage.IsBeeTemplate, landingPage.IsTemplateSaved };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }
        public async Task<bool> UpdateIsTemplateSaved(LandingPage landingPage)
        {
            string storeProcCommand = "Landing_Page";
            object? param = new { @Action = "UpdateIsTemplateSaved", landingPage.Id, landingPage.IsTemplateSaved };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<int> MaxCount(LandingPage landingPage)
        {
            string storeProcCommand = "Landing_Page";
            object? param = new { @Action = "MaxCount", landingPage.PageName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);
        }

        public async Task<List<MLLandingPage>> GetDetails(MLLandingPage landingPage, int FetchNext, int OffSet)
        {
            string storeProcCommand = "Landing_Page";
            object? param = new { @Action = "GETList", landingPage.PageName, FetchNext, OffSet };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLLandingPage>(storeProcCommand, param)).ToList();
        }
        public async Task<LandingPage?> GetSingle(LandingPage landingPage)
        {
            string storeProcCommand = "Landing_Page";
            object? param = new { @Action = "GET", landingPage.Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<LandingPage?>(storeProcCommand, param);
        }

        public async Task<bool> Delete(int Id)
        {
            string storeProcCommand = "Landing_Page";
            object? param = new { @Action = "Delete", Id };

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


