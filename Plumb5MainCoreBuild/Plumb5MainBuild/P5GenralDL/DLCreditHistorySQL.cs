﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLCreditHistorySQL : CommonDataBaseInteraction, IDLCreditHistory
    {
        CommonInfo connection;
        public DLCreditHistorySQL()
        {
            connection = GetDBConnection();
        }
        public async Task<int> Save(CreditHistory credit)
        {
            string storeProcCommand = "Credit_History";
            object? param = new { @Action = "Save", credit.UserInfoUserId, credit.FeatureId, credit.TotalCredit, credit.Remarks, credit.AccountId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
                    connection = null;
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
