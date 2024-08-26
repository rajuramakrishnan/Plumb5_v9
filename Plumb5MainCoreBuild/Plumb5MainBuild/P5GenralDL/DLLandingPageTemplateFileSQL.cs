﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLLandingPageTemplateFileSQL : CommonDataBaseInteraction, IDLLandingPageTemplateFile
    {
        CommonInfo connection = null;
        public DLLandingPageTemplateFileSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLandingPageTemplateFileSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(LandingPageTemplateFile TemplateFile)
        {
            string storeProcCommand = "LandingPage_TemplateFile";
            object? param = new { @Action = "Save", TemplateFile.LandingPageId, TemplateFile.TemplateFileName, TemplateFile.TemplateFileType, TemplateFile.TemplateFileContent };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Update(LandingPageTemplateFile TemplateFile)
        {
            string storeProcCommand = "LandingPage_TemplateFile";
            object? param = new { @Action = "Update", TemplateFile.Id, TemplateFile.TemplateFileContent };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> Delete(int TemplateId)
        {
            string storeProcCommand = "LandingPage_TemplateFile";
            object? param = new { @Action = "Delete", TemplateId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<List<LandingPageTemplateFile>> GetTemplateFiles(LandingPageTemplateFile TemplateFile)
        {
            string storeProcCommand = "LandingPage_TemplateFile";
            object? param = new { @Action = "GetTemplateFiles", TemplateFile.LandingPageId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<LandingPageTemplateFile>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<LandingPageTemplateFile> GetSingleFileType(LandingPageTemplateFile TemplateFile)
        {
            string storeProcCommand = "LandingPage_TemplateFile";
            object? param = new { @Action = "GetSingleFileType", TemplateFile.LandingPageId, TemplateFile.TemplateFileType };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<LandingPageTemplateFile>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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


