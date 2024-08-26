﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;

namespace P5GenralDL
{
    public class DLApiImportResponseSettingLogsPG: CommonDataBaseInteraction, IDLApiImportResponseSettingLogs
    {
        CommonInfo connection = null;
        public DLApiImportResponseSettingLogsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async Task<bool> Save(string RequestContent, int ApiImportResponseId, bool? iscontactsuccess = false, string contacterrormessage = null, bool? islmssuccess = false, string lmserrormessage = null, string p5uniqueid = null, string errormessage = null, string sourcetype = null)
        {
            string storeProcCommand = "select apiimportresponsesetting_logs_save (@ApiImportResponseId, @RequestContent, @iscontactsuccess, @contacterrormessage, @islmssuccess, @lmserrormessage, @p5uniqueid, @errormessage, @sourcetype)";
            object? param = new { ApiImportResponseId, RequestContent, iscontactsuccess, contacterrormessage, islmssuccess, lmserrormessage, p5uniqueid, errormessage, sourcetype };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param)>0;
        }

        public async Task<bool> Update(int ApiImportResponseId, bool? iscontactsuccess = false, string contacterrormessage = null, bool? islmssuccess = false, string lmserrormessage = null, string p5uniqueid = null, string errormessage = null)
        {
            string storeProcCommand = "select apiimportresponsesetting_logs_update(@ApiImportResponseId, @iscontactsuccess, @contacterrormessage, @islmssuccess, @lmserrormessage, @p5uniqueid, @errormessage)";
            object? param = new { ApiImportResponseId, iscontactsuccess, contacterrormessage, islmssuccess, lmserrormessage, p5uniqueid, errormessage };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<Int32> MaxCount(int ApiImportResponseId)
        {
            string storeProcCommand = "select apiimportresponsesetting_logs_maxcount(@ApiImportResponseId)";
            object? param = new { ApiImportResponseId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<IEnumerable<ApiImportResponseSettingLogs>> GetDetails(int ApiImportResponseId, int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from apiimportresponsesetting_logs_get( @ApiImportResponseId, @OffSet, @FetchNext)";
            object? param = new { ApiImportResponseId, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<ApiImportResponseSettingLogs>(storeProcCommand, param);
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
