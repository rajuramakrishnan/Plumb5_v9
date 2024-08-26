﻿using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IP5GenralDL;

namespace P5GenralDL
{
    public class DLChatAgentReportPG : CommonDataBaseInteraction, IDLChatAgentReport
    {
        CommonInfo connection = null;
        public DLChatAgentReportPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLChatAgentReportPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> GetCountOfSelecCamp(MLChatAgentReport agentReport)
        {
            string storeProcCommand = "select chat_reportsp_agentreport_ngetmaxcount(@ChatId, @UserId, @FromDate, @ToDate)";
            object? param = new { agentReport.ChatId, agentReport.UserId, agentReport.FromDate, agentReport.ToDate };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<List<ChatAgentReport>> GetAgentData(MLChatAgentReport agentReport, int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from chat_reportsp_agentreport_nagentdata(@ChatId, @UserId, @FromDate, @ToDate,@OffSet, @FetchNext)";
            object? param = new { agentReport.ChatId, agentReport.UserId, agentReport.FromDate, agentReport.ToDate, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ChatAgentReport>(storeProcCommand, param)).ToList();
        }

        public async Task<List<ChatAllAgentsName>> GetAllAgentsName(MLChatAgentReport agentReport)
        {
            string storeProcCommand = "select * from chat_reportsp_agentreport_getallagentname(@ChatId)";
            object? param = new { agentReport.ChatId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ChatAllAgentsName>(storeProcCommand, param)).ToList();
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
