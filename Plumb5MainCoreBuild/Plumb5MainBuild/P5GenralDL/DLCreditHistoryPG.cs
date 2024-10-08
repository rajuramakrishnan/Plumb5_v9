﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;

namespace P5GenralDL
{
    public class DLCreditHistoryPG : CommonDataBaseInteraction, IDLCreditHistory
    {
        CommonInfo connection;
        public DLCreditHistoryPG()
        {
            connection = GetDBConnection();
        }
        public async Task<int> Save(CreditHistory credit)
        {
            string storeProcCommand = "select * from credit_history_save(@UserInfoUserId, @FeatureId, @TotalCredit, @Remarks,@AccountId)";
            object? param = new { credit.UserInfoUserId, credit.FeatureId, credit.TotalCredit, credit.Remarks, credit.AccountId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
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
