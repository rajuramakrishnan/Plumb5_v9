﻿using DBInteraction;
using Newtonsoft.Json;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IP5GenralDL;

namespace P5GenralDL
{
    public class DLWebPushSentPG : CommonDataBaseInteraction, IDLWebPushSent
    {
        CommonInfo connection;

        public DLWebPushSentPG(int AccountId)
        {
            connection = GetDBConnection(AccountId);
        }

        public DLWebPushSentPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(WebPushSent webpushsent)
        {
            string storeProcCommand = "select webpush_sent_save(@WebPushSendingSettingId,@WebPushTemplateId,@SessionId,@MachineId,@IsSent,@IsViewed,@IsClicked,@IsClosed,@IsUnsubscribed,@SendStatus,@WorkFlowId,@WorkFlowDataId,@CampaignJobName,@SentDate,@ViewDate,@ClickDate,@CloseDate,@UnsubscribedDate,@ContactId,@ErrorMessage,@MessageContent,@P5UniqueId,@ResponseId,@VendorName,@FCMRegId,@VapidEndpointUrl,@VapidTokenkey,@VapidAuthkey,@ClickedDevice,@ClickedDeviceType,@ClickedUserAgent,@MessageTitle,@Button1_Redirect,@Button2_Redirect,@OnClickRedirect,@BannerImage)";

            object? param = new
            {
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
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public void SaveBulkWebPushCampaignResponses(DataTable WebPushSentBulk)
        {
            CommonBulkInsert(connection.Connection, WebPushSentBulk, "webpushsent");
        }

        public async Task<bool> UpdateBulkWebPushCampaignErrorResponses(DataTable WebPushSentBulkErrorResponse)
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new LowercaseContractResolver();
            var rulesjson = JsonConvert.SerializeObject(WebPushSentBulkErrorResponse, Formatting.Indented, settings);

            string storeProcCommand = "select webpush_sent_updatewebpushbulkcampaignresponses(@rulesjson)";
            object? param = new { rulesjson };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> DeleteResponsesFromBulkCampaign(List<Int64> WebPushSentBulkids)
        {
            string storeProcCommand = "select webpush_sent_deleteresponsesfrombulkcampaign(@WebPushSentBulkids)";
            object? param = new { WebPushSentBulkids = string.Join(",", WebPushSentBulkids) };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<int> GetWebPushTestCampaignMaxCount(DateTime FromDateTime, DateTime ToDateTime, int WebPushTemplateId = 0, string MachineId = null)
        {
            try
            {
                string storeProcCommand = "select webpush_sent_gettestcampaignmaxcount(@FromDateTime, @ToDateTime, @WebPushTemplateId, @MachineId)";
                object? param = new { FromDateTime, ToDateTime, WebPushTemplateId, MachineId };

                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
            }
            catch(Exception ex)
            {
                ex.ToString();
                return  -1;
            }
        }

        public async Task<List<WebPushSent>> GetWebPushTestCampaign(int OffSet, int FetchNext, DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "select * from webpush_sent_gettestcampaign(@OffSet, @FetchNext, @FromDateTime, @ToDateTime)";
            object? param = new { OffSet, FetchNext, FromDateTime, ToDateTime };

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

            string storeProcCommand = "select * from webpush_sent_getcontactidlist(@WebPushSendingSettingIdList,@IsViewed, @IsClicked, @IsClosed, @SendStatus)";
            object? param = new { WebPushSendingSettingIdList = string.Join(",", WebPushSendingSettingIdList), IsViewed, IsClicked, IsClosed, SendStatus };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<GroupMember>(storeProcCommand, param)).ToList();
        }

        public async Task<int> GetConsumptionCount(DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "select webpush_sent_getconsumptioncount(@FromDateTime, @ToDateTime)";
            object? param = new { FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task UpdateWebPushSent(PushNotificationResponses pushNotificationResponses)
        {
            string storeProcCommand = "select webpush_sent_responseupdate(@Action, @P5UniqueId, @MachineId, @WorkFlowId, @WorkFlowDataId)";
            object? param = new { pushNotificationResponses.Action, pushNotificationResponses.P5UniqueId, pushNotificationResponses.MachineId, pushNotificationResponses.WorkFlowId, pushNotificationResponses.WorkFlowDataId };

            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool> UpdateWebPushSentAsync(PushNotificationResponses pushNotificationResponses)
        {
            string storeProcCommand = "select webpush_sent_responseupdate(@Action, @P5UniqueId, @MachineId, @WorkFlowId, @WorkFlowDataId)";
            object? param = new { pushNotificationResponses.Action, pushNotificationResponses.P5UniqueId, pushNotificationResponses.MachineId, pushNotificationResponses.WorkFlowId, pushNotificationResponses.WorkFlowDataId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }
        public async Task<DataSet> GetWebPushIndividualResponses(DateTime FromDateTime, DateTime ToDateTime, int OffSet, int FetchNext, int WebPushTemplateId = 0, string MachineId = null)
        {
            string storeProcCommand = "select * from webpush_sent_getindividualresponses(@OffSet, @FetchNext, @FromDateTime, @ToDateTime, @WebPushTemplateId, @MachineId)";
            object? param = new { OffSet, FetchNext, FromDateTime, ToDateTime, WebPushTemplateId, MachineId };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
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
