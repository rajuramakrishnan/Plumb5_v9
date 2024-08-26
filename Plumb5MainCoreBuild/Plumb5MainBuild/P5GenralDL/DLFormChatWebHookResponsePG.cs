﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLFormChatWebHookResponsePG : CommonDataBaseInteraction, IDLFormChatWebHookResponse
    {
        CommonInfo connection = null;
        public DLFormChatWebHookResponsePG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLFormChatWebHookResponsePG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> MaxCount(int formOrChatId, string callingSource, string webhookids = null)
        {
            string storeProcCommand = "select formchat_webhookresponses_maxcount(@FormOrChatId, @CallingSource, @Webhookids)";
            object? param = new { formOrChatId, callingSource, webhookids };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<List<WebHookTracker>> GetDetails(int formOrChatId, string callingSource, int OffSet, int FetchNext, string webhookids = null)
        {
            string storeProcCommand = "select *  from formchat_webhookresponses_get(@FormOrChatId, @CallingSource, @Webhookids,@OffSet, @FetchNext)";
            object? param = new { formOrChatId, callingSource, webhookids, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WebHookTracker>(storeProcCommand, param)).ToList();
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

