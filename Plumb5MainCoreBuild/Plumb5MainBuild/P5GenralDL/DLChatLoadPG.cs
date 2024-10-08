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
    public class DLChatLoadPG : CommonDataBaseInteraction, IDLChatLoad
    {
        CommonInfo connection = null;
        public DLChatLoadPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLChatLoadPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<List<ChatDetailsById>> GetAllActiveChat()
        {
            string storeProcCommand = "select *  from chat_sp_allloadingcondition_getallactivechat()";

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ChatDetailsById>(storeProcCommand)).ToList();
        }
        public async Task<bool> CheckChatStatus(int ChatId)
        {
            string storeProcCommand = "select chat_sp_allloadingcondition_chatstatus(@ChatId)";
            object? param = new { ChatId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
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

