﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLUCPSQL : CommonDataBaseInteraction, IDLUCP
    {
        readonly CommonInfo connection;
        public DLUCPSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLUCPSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<MLUCPVisitor?> GetVisitorDetail(MLUCPVisitor mLUCPVisitor)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetVisitorDetail", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLUCPVisitor?>(storeProcCommand, param);

        }

        public async Task<DataSet> GetMchineIdsByContactId(MLUCPVisitor mLUCPVisitor)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action= "GetMchineIdsByContactId", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetDevicedsByContactId(MLUCPVisitor mLUCPVisitor)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetDevicedsByContactId", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetBasicDetails(MLUCPVisitor mLUCPVisitor)
        {
            var storeProcCommand = "UCP_Details";

            object? param = new { Action = "GetBasicDetails", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetWebSummary(MLUCPVisitor mLUCPVisitor)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetWebSummary", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<DataSet> GetMobileSummary(MLUCPVisitor mLUCPVisitor)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetMobileSummary", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<DataSet> GetMailDetails(MLUCPVisitor mLUCPVisitor, string CampaignJobName = null)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetMailDetails", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId, mLUCPVisitor.FromDate, mLUCPVisitor.ToDate, CampaignJobName };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<DataSet> GetSmsDetails(MLUCPVisitor mLUCPVisitor, string CampaignJobName = null)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetSmsDetails", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId, mLUCPVisitor.FromDate, mLUCPVisitor.ToDate, CampaignJobName };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<DataSet> GetWhatsappDetails(MLUCPVisitor mLUCPVisitor, string CampaignJobName = null)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetWhatsappDetails", mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId, mLUCPVisitor.ContactId, mLUCPVisitor.FromDate, mLUCPVisitor.ToDate, CampaignJobName };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<DataSet> GetFormDetails(MLUCPVisitor mLUCPVisitor)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetFormDetails", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId, mLUCPVisitor.FromDate, mLUCPVisitor.ToDate };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<DataSet> GetCallDetails(MLUCPVisitor mLUCPVisitor)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetCallDetails", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId, mLUCPVisitor.FromDate, mLUCPVisitor.ToDate };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }
        public async Task<DataSet> GetTransactionDetails(MLUCPVisitor mLUCPVisitor)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetTransactionDetails", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId, mLUCPVisitor.FromDate, mLUCPVisitor.ToDate };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<DataSet> GetClickStreamDetails(MLUCPVisitor mLUCPVisitor)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetClickStreamDetails", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId, mLUCPVisitor.FromDate, mLUCPVisitor.ToDate };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<DataSet> GetMobileAppDetails(MLUCPVisitor mLUCPVisitor)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetMobileAppDetails", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId, mLUCPVisitor.FromDate, mLUCPVisitor.ToDate };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<IEnumerable<Notes>> GetLmsNoteList(MLUCPVisitor mLUCPVisitor)
        {
            string storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetLmsNotes", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId, mLUCPVisitor.FromDate, mLUCPVisitor.ToDate };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<Notes>(storeProcCommand, param, commandType: CommandType.StoredProcedure); 

        }

        public async Task<IEnumerable<MLUserJourney>> GetUserJourney(MLUCPVisitor mLUCPVisitor)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetUserJourney", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId, mLUCPVisitor.Domain, mLUCPVisitor.FromDate, mLUCPVisitor.ToDate };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLUserJourney>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<MLContact>> GetLmsAuditDetails(MLUCPVisitor mLUCPVisitor)
        {
            var storeProcCommand = "UCP_Details";

            object? param = new { Action = "GetLmsAuditDetails", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId, mLUCPVisitor.FromDate, mLUCPVisitor.ToDate };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLContact>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ChatFullTranscipt>> GetPastChatDetails(MLUCPVisitor mLUCPVisitor)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetPastChatDetails", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId, mLUCPVisitor.FromDate, mLUCPVisitor.ToDate };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<ChatFullTranscipt>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<DataSet> GetWebPushDetails(MLUCPVisitor mLUCPVisitor)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetWebPushDetails", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId, mLUCPVisitor.FromDate, mLUCPVisitor.ToDate };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<DataSet> GetMobilePushDetails(MLUCPVisitor mLUCPVisitor)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetMobilePushDetails", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId, mLUCPVisitor.FromDate, mLUCPVisitor.ToDate };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<DataSet> GetFromAndToDate(MLUCPVisitor mLUCPVisitor, string Module)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetFromAndToDate", mLUCPVisitor.ContactId, mLUCPVisitor.MachineId, mLUCPVisitor.DeviceId, mLUCPVisitor.FromDate, mLUCPVisitor.ToDate, Module };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<Int32> SaveContactName(int ContactId, string ContactName)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "SaveContactName", ContactId, ContactName };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure); 
        }
        public async Task<IEnumerable<EventTracker>> GetEventTrackerDetails(EventTracker eVenttracker)
        {
            string storeProcCommand = "Event_Tracker_Data";
            object? param = new { Action = "GetDetails", eVenttracker.MachineId, eVenttracker.SessionId };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<EventTracker>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }
        public async Task<IEnumerable<MailTemplate>> GetMailclcikstreamDetails(string MailP5UniqueID, string startdatetime, string enddatetime)
        {
            string storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetListMail", MailP5UniqueID };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MailTemplate>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<SmsTemplate>> clickstreamGetSmsTemplateDetails(string SMSP5UniqueID, string startdatetime, string enddatetime)
        {
            string storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetListSms", SMSP5UniqueID };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<SmsTemplate>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<MLWhatsAppTemplates>> clickstreamGetwhatsappTemplateDetails(string WhatsAppP5UniqueID, string startdatetime, string enddatetime)
        {
            string storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetListwhatsapp", WhatsAppP5UniqueID };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLWhatsAppTemplates>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<Customevents>> GetEventdetailsClickStream(string machineid, string sessionid)
        {
            string storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetListCustomEvents", machineid, sessionid };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<Customevents>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<MLWebPushTemplate>> GetGetWebPushClickStream(string P5WebPushUniqueID)
        {
            string storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetListwebpush", P5WebPushUniqueID };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLWebPushTemplate>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<DataSet> ClickStreamGetCaptureFormDetails(string machineid, string sessionid)
        {
            var storeProcCommand = "UCP_Details";
            object? param = new { Action = "GetClickStreamCaptureFormDetails", machineid, sessionid};
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }
        public async Task<IEnumerable<MLLeadScroinghistroy>> GetscroingDetails(MLUCPVisitor mLUCPVisitor)
        {
            string storeProcCommand = "UCP_Details";

            object? param = new { Action = "GetScroingHistroyDetails", mLUCPVisitor.ContactId, mLUCPVisitor.FromDate, mLUCPVisitor.ToDate };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLLeadScroinghistroy>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
