﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;
using System.Data;


namespace P5GenralDL
{
    public class DLWorkFlowBulkMailSentSQL : CommonDataBaseInteraction, IDLWorkFlowBulkMailSent
    {
        CommonInfo connection;
        public DLWorkFlowBulkMailSentSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWorkFlowBulkMailSentSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<bool> DeleteAllTheDataWhichAreInQuque(int WorkFlowId)
        {
            string storeProcCommand = "WorkFlow_MailBulkInsert";
            object? param = new { @Action = "DeleteAllTheDataWhichAreInQuque", WorkFlowId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<long> GetTotalBulkMail(int MailSendingSettingId, int WorkFlowId)
        {
            string storeProcCommand = "WorkFlow_MailBulkInsert";
            object? param = new { @Action = "GetTotalBulkMail", MailSendingSettingId, WorkFlowId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<long>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
