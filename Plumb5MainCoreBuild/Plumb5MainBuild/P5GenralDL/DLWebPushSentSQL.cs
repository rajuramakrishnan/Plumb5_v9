﻿using DBInteraction;
using IP5GenralDL;
using Newtonsoft.Json;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace P5GenralDL
{
    public class DLWebPushSentSQL : CommonDataBaseInteraction, IDLWebPushSent
    {
        CommonInfo connection;

        public DLWebPushSentSQL(int AccountId)
        {
            connection = GetDBConnection(AccountId);
        }

        public DLWebPushSentSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(WebPushSent webpushsent)
        {
            string storeProcCommand = "WebPush_Sent";

            object? param = new
            {
                Action = "Save",
                webpushsent.WebPushSendingSettingId,
                webpushsent.WebPushTemplateId,
                webpushsent.SessionId,
                webpushsent.MachineId,
                webpushsent.IsSent,
                webpushsent.IsViewed,
                webpushsent.IsClicked,
                webpushsent.IsClosed,
                webpushsent.IsUnsubscribed,
                webpushsent.SendStatus,
                webpushsent.WorkFlowId,
                webpushsent.WorkFlowDataId,
                webpushsent.CampaignJobName,
                webpushsent.SentDate,
                webpushsent.ViewDate,
                webpushsent.ClickDate,
                webpushsent.CloseDate,
                webpushsent.UnsubscribedDate,
                webpushsent.ContactId,
                webpushsent.ErrorMessage,
                webpushsent.MessageContent,
                webpushsent.P5UniqueId,
                webpushsent.ResponseId,
                webpushsent.VendorName,
                webpushsent.FCMRegId,
                webpushsent.VapidEndpointUrl,
                webpushsent.VapidTokenkey,
                webpushsent.VapidAuthkey,
                webpushsent.ClickedDevice,
                webpushsent.ClickedDeviceType,
                webpushsent.ClickedUserAgent,
                webpushsent.MessageTitle,
                webpushsent.Button1_Redirect,
                webpushsent.Button2_Redirect,
                webpushsent.OnClickRedirect,
                webpushsent.BannerImage
            };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public void SaveBulkWebPushCampaignResponses(DataTable WebPushSentBulk)
        {
            string storeProcCommand = "WebPush_Sent";
            object? param = new { Action = "SaveResponsesForBulkCampaign", WebPushSentBulk };

            using var db = GetDbConnection(connection.Connection);
            db.ExecuteScalar<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateBulkWebPushCampaignErrorResponses(DataTable WebPushSentBulkErrorResponse)
        {
            string storeProcCommand = "WebPush_Sent";
            object? param = new { Action = "UpdateWebPushBulkCampaignResponses", WebPushSentBulkErrorResponse };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> DeleteResponsesFromBulkCampaign(List<Int64> WebPushSentBulkids)
        {
            string storeProcCommand = "WebPush_Sent";
            object? param = new { Action = "DeleteResponsesfrombulkcampaign", WebPushSentBulkids = string.Join(",", WebPushSentBulkids) };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<int> GetWebPushTestCampaignMaxCount(DateTime FromDateTime, DateTime ToDateTime, int WebPushTemplateId = 0, string MachineId = null)
        {
            string storeProcCommand = "WebPush_Sent";
            object? param = new { Action = "GetTestCampaignMaxCount", FromDateTime, ToDateTime, WebPushTemplateId, MachineId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<WebPushSent>> GetWebPushTestCampaign(int OffSet, int FetchNext, DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "WebPush_Sent";
            object? param = new { Action = "GetTestCampaign", OffSet, FetchNext, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WebPushSent>(storeProcCommand, param)).ToList();
        }

        public async Task<List<GroupMember>> GetContactIdList(int[] WebPushSendingSettingIdList, int[] CampaignResponseValue)
        {
            Int16 IsViewed = 0;
            Int16 IsClicked = 0;
            Int16 IsClosed = 0;
            Int16 SendStatus = 0;

            foreach (int value in CampaignResponseValue)
            {
                if (value == 1)
                    IsViewed = 1;
                else if (value == 2)
                    IsClicked = 1;
                else if (value == 3)
                    IsClosed = 1;
                else if (value == 4)
                    SendStatus = 0;
            }

            string storeProcCommand = "WebPush_Sent";
            object? param = new { Action = "GetContactIdList", WebPushSendingSettingIdList = string.Join(",", WebPushSendingSettingIdList), IsViewed, IsClicked, IsClosed, SendStatus };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<GroupMember>(storeProcCommand, param)).ToList();
        }

        public async Task<int> GetConsumptionCount(DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "WebPush_Sent";
            object? param = new { Action = "GetConsumptionCount", FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateWebPushSent(PushNotificationResponses pushNotificationResponses)
        {
            string storeProcCommand = "WebPush_Sent";
            object? param = new { pushNotificationResponses.Action, pushNotificationResponses.P5UniqueId, pushNotificationResponses.MachineId, pushNotificationResponses.WorkFlowId, pushNotificationResponses.WorkFlowDataId };

            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateWebPushSentAsync(PushNotificationResponses pushNotificationResponses)
        {
            string storeProcCommand = "WebPush_Sent";
            object? param = new { pushNotificationResponses.Action, pushNotificationResponses.P5UniqueId, pushNotificationResponses.MachineId, pushNotificationResponses.WorkFlowId, pushNotificationResponses.WorkFlowDataId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<DataSet> GetWebPushIndividualResponses(DateTime FromDateTime, DateTime ToDateTime, int OffSet, int FetchNext, int WebPushTemplateId = 0, string MachineId = null)
        {
            string storeProcCommand = "WebPush_Sent";
            object? param = new { Action = "GetIndividualResponses", OffSet, FetchNext, FromDateTime, ToDateTime, WebPushTemplateId, MachineId };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        #region Dispose Method
        bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
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
