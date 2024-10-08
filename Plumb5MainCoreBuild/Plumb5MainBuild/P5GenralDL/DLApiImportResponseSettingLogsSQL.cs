﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System.Data;

namespace P5GenralDL
{
    public class DLApiImportResponseSettingLogsSQL : CommonDataBaseInteraction, IDLApiImportResponseSettingLogs
    {
        CommonInfo connection = null;
        public DLApiImportResponseSettingLogsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async Task<bool> Save(string RequestContent, int ApiImportResponseId, bool? iscontactsuccess = false, string contacterrormessage = null, bool? islmssuccess = false, string lmserrormessage = null, string p5uniqueid = null, string errormessage = null, string sourcetype = null)
        {
            string storeProcCommand = "ApiImportResponseSetting_Logs";
            object? param = new { Action = "Save", ApiImportResponseId, RequestContent, iscontactsuccess, contacterrormessage, islmssuccess, lmserrormessage, p5uniqueid, errormessage, sourcetype };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure)>0;
        }

        public async Task<bool> Update(int ApiImportResponseId, bool? iscontactsuccess = false, string contacterrormessage = null, bool? islmssuccess = false, string lmserrormessage = null, string p5uniqueid = null, string errormessage = null)
        {
            string storeProcCommand = "ApiImportResponseSetting_Logs";
            object? param = new { Action = "Update", ApiImportResponseId, iscontactsuccess, contacterrormessage, islmssuccess, lmserrormessage, p5uniqueid, errormessage };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure)>0;
        }

        public async Task<Int32> MaxCount(int ApiImportResponseId)
        {
            string storeProcCommand = "ApiImportResponseSetting_Logs";
            object? param = new { Action = "MaxCount", ApiImportResponseId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ApiImportResponseSettingLogs>> GetDetails(int ApiImportResponseId, int OffSet, int FetchNext)
        {
            string storeProcCommand = "ApiImportResponseSetting_Logs)";
            object? param = new { Action= "Get", ApiImportResponseId, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<ApiImportResponseSettingLogs>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
