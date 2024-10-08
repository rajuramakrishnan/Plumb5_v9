﻿using Dapper;
using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLContactForCampaignPG : CommonDataBaseInteraction, IDLContactForCampaign
    {
        CommonInfo connection;
        public DLContactForCampaignPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLContactForCampaignPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<List<MLContactForCampaign>> GetEmailIdContacts(int GroupId, int MailSendingSettingId, int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from Contact_DetailsForCampaign(@Action,@GroupId,@MailSendingSettingId,@OffSet,@FetchNext)";
            object? param = new { Action = "GetEmailIdContacts", GroupId, MailSendingSettingId, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLContactForCampaign>(storeProcCommand, param)).ToList();

        }

        public async Task<List<MLContactForCampaign>> GetRemainingContacts(int GroupId, int MailSendingSettingId, int SMSSendingSettingId)
        {
            string storeProcCommand = "select * from Contact_DetailsForCampaign(@Action,@GroupId,@MailSendingSettingId,@SMSSendingSettingId)";
            object? param = new { Action = "GetRemainingContacts", GroupId, MailSendingSettingId, SMSSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLContactForCampaign>(storeProcCommand, param)).ToList();

        }

        public async Task<List<MLContactForCampaign>> GetPhoneContacts(int GroupId, int SmsSendingSettingId, Boolean IsPromotionalOrTransactionalType, int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from Contact_DetailsForCampaign(@Action,@GroupId,@MailSendingSettingId,@IsPromotionalOrTransactionalType,@OffSet,@FetchNext)";
            object? param = new { Action = "GetPhoneContacts", GroupId, SmsSendingSettingId, IsPromotionalOrTransactionalType, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLContactForCampaign>(storeProcCommand, param)).ToList();


        }

        public async Task<int> GetContactsCount(int GroupId)
        {
            string storeProcCommand = "select * from Contact_DetailsForCampaign(@Action,@GroupId)";
            object? param = new { Action = "GetContactsCount", GroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }
    }
}
