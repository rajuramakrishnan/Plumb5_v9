﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLWhatsAppReportDetailsSQL : CommonDataBaseInteraction, IDLWhatsAppReportDetails
    {
        CommonInfo connection;
        public DLWhatsAppReportDetailsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async Task<int> MaxCount(MLWhatsAppReportDetails sentContactDetails, DateTime? FromDateTime, DateTime? ToDateTime)
        {
            string storeProcCommand = "WhatsApp_ResponseCustomReport";
            object? param = new { Action = "GetCount", sentContactDetails.WhatsAppSendingSettingId, sentContactDetails.IsDelivered, sentContactDetails.IsClicked, sentContactDetails.IsRead, sentContactDetails.IsFailed, sentContactDetails.SendStatus, sentContactDetails.ErrorMessage, sentContactDetails.IsUnsubscribed, sentContactDetails.WorkFlowId, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<MLWhatsAppReportDetails>> GetReportDetails(MLWhatsAppReportDetails sentContactDetails, DateTime? FromDateTime, DateTime? ToDateTime, int OffSet = 0, int FetchNext = 0)
        {
            string storeProcCommand = "WhatsApp_ResponseCustomReport";
            object? param = new { Action = "GetReportData", sentContactDetails.WhatsAppSendingSettingId, sentContactDetails.IsDelivered, sentContactDetails.IsClicked, sentContactDetails.IsRead, sentContactDetails.IsFailed, sentContactDetails.SendStatus, sentContactDetails.ErrorMessage, sentContactDetails.IsUnsubscribed, sentContactDetails.WorkFlowId, FromDateTime, ToDateTime, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLWhatsAppReportDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<MLMailSmsBounced>> GetBouncedDetails(int WhatsAppSendingSettingId)
        {
            string storeProcCommand = "WhatsApp_ResponseCustomReport";
            object? param = new { Action = "GetBouncedDetails", WhatsAppSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLMailSmsBounced>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<MLWhatsAppReportDetails>> GetClickReportDetails(MLWhatsAppReportDetails sentContactDetails, int OffSet, int FetchNext, DateTime? FromDateTime, DateTime? ToDateTime)
        {
            string storeProcCommand = "WhatsApp_ResponseCustomReport";
            object? param = new { Action = "GetClickReportData", sentContactDetails.WhatsAppSendingSettingId, sentContactDetails.IsClicked, OffSet, FetchNext, sentContactDetails.WorkFlowId, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLWhatsAppReportDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<int> GetMaxClickCount(MLWhatsAppReportDetails sentContactDetails, DateTime? FromDateTime, DateTime? ToDateTime)
        {
            string storeProcCommand = "WhatsApp_ResponseCustomReport";
            object? param = new { Action = "GetClickCount", sentContactDetails.WhatsAppSendingSettingId, sentContactDetails.IsClicked, sentContactDetails.WorkFlowId, FromDateTime, ToDateTime };

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
