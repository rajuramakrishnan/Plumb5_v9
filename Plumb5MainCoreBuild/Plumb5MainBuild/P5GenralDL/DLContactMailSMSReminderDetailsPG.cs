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
    public class DLContactMailSMSReminderDetailsPG : CommonDataBaseInteraction, IDLContactMailSMSReminderDetails
    {
        CommonInfo connection;

        public DLContactMailSMSReminderDetailsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLContactMailSMSReminderDetailsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        #region Report
        public async Task<int> GetScheduledMailAlertMaxCount(string FromDateTime, string ToDateTime, string UserIdList)
        {
            try
            {
                string storeProcCommand = "select contactmailsmsreminder_details_scheduledmailalertsmaxcount(@UserIdList,@FromDateTime, @ToDateTime  )";
                object? param = new { UserIdList,FromDateTime, ToDateTime  };

                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
            
        }
        public async Task<List<ContactMailSMSReminderDetails>> GetScheduledMailAlertDetails(string FromDateTime, string ToDateTime, string UserIdList, int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from contactmailsmsreminder_details_scheduledmailalertsdetails(@FromDateTime, @ToDateTime, @UserIdList, @OffSet, @FetchNext)";
            object? param = new { FromDateTime, ToDateTime, UserIdList, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ContactMailSMSReminderDetails>(storeProcCommand, param)).ToList();
        }

        public async Task<List<ContactMailSMSReminderDetails>> GetMailTemplateContent(string MailTemplateName, int Id)
        {
            string storeProcCommand = "select * from contactmailsmsreminder_details_getmailtemplatecontent(@MailTemplateName, @Id)";
            object? param = new { MailTemplateName, Id };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ContactMailSMSReminderDetails>(storeProcCommand, param)).ToList();
        }

        public async Task<bool> DeleteScheduledMailAlerts(Int16 Id)
        {
            string storeProcCommand = "select contactmailsmsreminder_details_deletescheduledmailalert(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<int> GetScheduledSmsAlertMaxCount(string FromDateTime, string ToDateTime, string UserIdList)
        {
            try
            {
                string storeProcCommand = "select contactmailsmsreminder_details_scheduledsmsalertsmaxcount(@UserIdList,@FromDateTime,@ToDateTime)";
                object? param = new { UserIdList, FromDateTime, ToDateTime };

                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
            }
           catch(Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<List<ContactMailSMSReminderDetails>> GetScheduledSmsAlertDetails(string FromDateTime, string ToDateTime, string UserIdList, int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from contactmailsmsreminder_details_scheduledsmsalertsdetails(@FromDateTime, @ToDateTime,@UserIdList, @OffSet, @FetchNext)";
            object? param = new { FromDateTime, ToDateTime, UserIdList, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ContactMailSMSReminderDetails>(storeProcCommand, param)).ToList();
        }

        public async Task<bool> DeleteScheduledSmsAlerts(Int16 Id)
        {
            string storeProcCommand = "select contactmailsmsreminder_details_deletescheduledsmsalert(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<int> GetScheduledWhatsappAlertMaxCount(string FromDateTime, string ToDateTime, string UserIdList)
        {
            try
            {
                string storeProcCommand = "select contactmailsmsreminder_details_scheduledwhatsappalertsmaxcount(@UserIdList,@FromDateTime,@ToDateTime)";
                object? param = new { UserIdList,FromDateTime, ToDateTime };

                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
            }
            catch(Exception ex)
            {
                throw new Exception();
           }
        }

        public async Task<List<ContactMailSMSReminderDetails>> GetScheduledWhatsappAlertDetails(string FromDateTime, string ToDateTime, string UserIdList, int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from contactmailsmsreminder_details_scheduledwhatsappalertsdetails(@FromDateTime, @ToDateTime, @UserIdList, @OffSet, @FetchNext)";
            object? param = new { FromDateTime, ToDateTime, UserIdList, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ContactMailSMSReminderDetails>(storeProcCommand, param)).ToList();
        }

        public async Task<bool> DeleteScheduledWhatsappAlerts(Int16 Id)
        {
            string storeProcCommand = "select contactmailsmsreminder_details_deletescheduledwhatsappalert(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }
        #endregion

        public async Task<int> SaveScheduledAlerts(ContactMailSMSReminderDetails reminderDetails)
        {
            string storeProcCommand = "select contactmailsmsreminder_details_savescheduledalerts(@ContactId,@ScheduleType,@Name,@EmailId,@PhoneNumber,@FromEmailId,@Subject,@FromName,@AlertScheduleStatus,@AlertScheduleDate,@UserInfoUserId,@TemplateName,@TemplateContent,@MailReplyEmailId,@IsPromotionalOrTransnational,@TemplateId,@CCEmailId,@LmsGroupId,@LmsGroupMemberId,@Score,@LeadLabel,@Publisher)";

            object? param = new
            {
                reminderDetails.ContactId,
                reminderDetails.ScheduleType,
                reminderDetails.Name,
                reminderDetails.EmailId,
                reminderDetails.PhoneNumber,
                reminderDetails.FromEmailId,
                reminderDetails.Subject,
                reminderDetails.FromName,
                reminderDetails.AlertScheduleStatus,
                reminderDetails.AlertScheduleDate,
                reminderDetails.UserInfoUserId,
                reminderDetails.TemplateName,
                reminderDetails.TemplateContent,
                reminderDetails.MailReplyEmailId,
                reminderDetails.IsPromotionalOrTransnational,
                reminderDetails.TemplateId,
                reminderDetails.CCEmailId,
                reminderDetails.LmsGroupId,
                reminderDetails.LmsGroupMemberId,
                reminderDetails.Score,
                reminderDetails.LeadLabel,
                reminderDetails.Publisher
            };


            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<bool> UpdateScheduledMailAlerts(ContactMailSMSReminderDetails reminderDetails)
        {
            string storeProcCommand = "select contactmailsmsreminder_details_updatescheduledmailalerts(@Id,@IsPromotionalOrTransnational,@EmailId,@Subject,@FromName,@FromEmailId,@TemplateName,@TemplateContent,@MailReplyEmailId,@AlertScheduleDate,@TemplateId,@CCEmailId,@Score,@LeadLabel,@Publisher)";

            object? param = new
            {
                reminderDetails.Id,
                reminderDetails.IsPromotionalOrTransnational,
                reminderDetails.EmailId,
                reminderDetails.Subject,
                reminderDetails.FromName,
                reminderDetails.FromEmailId,
                reminderDetails.TemplateName,
                reminderDetails.TemplateContent,
                reminderDetails.MailReplyEmailId,
                reminderDetails.AlertScheduleDate,
                reminderDetails.TemplateId,
                reminderDetails.CCEmailId,
                reminderDetails.Score,
                reminderDetails.LeadLabel,
                reminderDetails.Publisher
            };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> UpdateScheduledSmsAlerts(ContactMailSMSReminderDetails reminderDetails)
        {
            string storeProcCommand = "select contactmailsmsreminder_details_updatescheduledsmsalerts(@Id,@IsPromotionalOrTransnational,@PhoneNumber,@TemplateName,@TemplateContent,@AlertScheduleDate,@TemplateId,@Score,@LeadLabel,@Publisher)";

            object? param = new
            {
                reminderDetails.Id,
                reminderDetails.IsPromotionalOrTransnational,
                reminderDetails.PhoneNumber,
                reminderDetails.TemplateName,
                reminderDetails.TemplateContent,
                reminderDetails.AlertScheduleDate,
                reminderDetails.TemplateId,
                reminderDetails.Score,
                reminderDetails.LeadLabel,
                reminderDetails.Publisher
            };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }
        public async Task<bool> UpdateScheduledWhatsappAlerts(ContactMailSMSReminderDetails reminderDetails)
        {
            string storeProcCommand = "select contactmailsmsreminder_details_updatescheduledwhatsappalerts(@Id,@IsPromotionalOrTransnational,@PhoneNumber,@TemplateName,@TemplateContent,@AlertScheduleDate,@TemplateId,@Score,@LeadLabel,@Publisher)";
            object? param = new
            {
                reminderDetails.Id,
                reminderDetails.IsPromotionalOrTransnational,
                reminderDetails.PhoneNumber,
                reminderDetails.TemplateName,
                reminderDetails.TemplateContent,
                reminderDetails.AlertScheduleDate,
                reminderDetails.TemplateId,
                reminderDetails.Score,
                reminderDetails.LeadLabel,
                reminderDetails.Publisher
            };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
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
