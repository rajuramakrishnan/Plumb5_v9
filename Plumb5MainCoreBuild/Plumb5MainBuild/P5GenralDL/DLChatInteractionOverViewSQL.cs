﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLChatInteractionOverViewSQL : CommonDataBaseInteraction, IDLChatInteractionOverView
    {
        CommonInfo connection = null;
        public DLChatInteractionOverViewSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLChatInteractionOverViewSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(ChatInteractionOverView chatOverView)
        {
            string storeProcCommand = "ChatInteraction_OverView";
            object? param = new { @Action = "Save", chatOverView.ChatUserId, chatOverView.LastAgentServedBy, chatOverView.InitiatedByUser, chatOverView.IsCompleted, chatOverView.FeedBack, chatOverView.FeedBackForAgentId, chatOverView.IsTransferd, chatOverView.IsConvertedToLeadOrCustomer, chatOverView.ChatInitiatedOnPageUrl, chatOverView.ResponseCount, chatOverView.IsMissed };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Update(ChatInteractionOverView chatOverView)
        {
            string storeProcCommand = "ChatInteraction_OverView";
            object? param = new { @Action = "Update", chatOverView.ChatUserId, chatOverView.LastAgentServedBy, chatOverView.InitiatedByUser, chatOverView.IsCompleted, chatOverView.FeedBack, chatOverView.FeedBackForAgentId, chatOverView.IsTransferd, chatOverView.IsConvertedToLeadOrCustomer, chatOverView.ChatInitiatedOnPageUrl, chatOverView.ResponseCount, chatOverView.IsMissed, chatOverView.IsFormFilled };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }
        public async Task<List<ChatInteractionOverView>> GetList(ChatInteractionOverView chatOverView, DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "ChatInteraction_OverView";
            object? param = new { @Action = "GetList", chatOverView.ChatUserId, chatOverView.LastAgentServedBy, chatOverView.InitiatedByUser, chatOverView.IsCompleted, chatOverView.FeedBack, chatOverView.FeedBackForAgentId, chatOverView.IsTransferd, chatOverView.IsConvertedToLeadOrCustomer, chatOverView.ChatInitiatedOnPageUrl, chatOverView.ResponseCount, chatOverView.IsMissed, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ChatInteractionOverView>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<MLChatInteractionOverView>> GetImpressionList(ChatInteractionOverView chatOverView, DateTime FromDateTime, DateTime ToDateTime, int OffSet, int FetchNext)
        {
            string storeProcCommand = "ChatInteraction_OverView";
            object? param = new { @Action = "GetImpressionList", chatOverView.ChatInitiatedOnPageUrl, FromDateTime, ToDateTime, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLChatInteractionOverView>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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