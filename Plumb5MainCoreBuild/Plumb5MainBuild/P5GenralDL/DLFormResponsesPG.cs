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
    public class DLFormResponsesPG : CommonDataBaseInteraction, IDLFormResponses
    {
        CommonInfo connection = null;
        public DLFormResponsesPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLFormResponsesPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32>  Save(FormResponses formResponses)
        {
            string storeProcCommand = "select form_responses_save( @FormId, @UserInfoUserId, @UserGroupId, @TrackIp, @MachineId, @SessionRefer, @ContactId, @SearchKeyword, @Referrer, @IsAdSenseOrAdWord, @Country, @StateName, @City, @PageUrl, @Field1, @Field2, @Field3, @Field4, @Field5, @Field6, @Field7, @Field8, @Field9, @Field10, @Field11, @Field12, @Field13, @Field14, @Field15, @Field16, @Field17, @Field18, @Field19, @Field20, @UtmTagSource, @MailSubscribe, @MailOverallSubscribe, @SmsSubscribe, @SmsOverallSubscribe, @UtmMedium, @UtmCampaign, @UtmTerm, @UtmContent, @Project, @ProjectDate, @Field21, @Field22, @Field23, @Field24, @Field25, @Field26, @Field27, @Field28, @Field29, @Field30, @Field31, @Field32, @Field33, @Field34, @Field35, @Field36, @Field37, @Field38, @Field39, @Field40, @ResponsedDevice, @ResponsedDeviceType, @ResponsedUserAgent )";
            object? param = new { formResponses.FormId, formResponses.UserInfoUserId, formResponses.UserGroupId, formResponses.TrackIp, formResponses.MachineId, formResponses.SessionRefer, formResponses.ContactId, formResponses.SearchKeyword, formResponses.Referrer, formResponses.IsAdSenseOrAdWord, formResponses.Country, formResponses.StateName, formResponses.City, formResponses.PageUrl, formResponses.Field1, formResponses.Field2, formResponses.Field3, formResponses.Field4, formResponses.Field5, formResponses.Field6, formResponses.Field7, formResponses.Field8, formResponses.Field9, formResponses.Field10, formResponses.Field11, formResponses.Field12, formResponses.Field13, formResponses.Field14, formResponses.Field15, formResponses.Field16, formResponses.Field17, formResponses.Field18, formResponses.Field19, formResponses.Field20, formResponses.UtmTagSource, formResponses.MailSubscribe, formResponses.MailOverallSubscribe, formResponses.SmsSubscribe, formResponses.SmsOverallSubscribe, formResponses.UtmMedium, formResponses.UtmCampaign, formResponses.UtmTerm, formResponses.UtmContent, formResponses.Project, formResponses.ProjectDate, formResponses.Field21, formResponses.Field22, formResponses.Field23, formResponses.Field24, formResponses.Field25, formResponses.Field26, formResponses.Field27, formResponses.Field28, formResponses.Field29, formResponses.Field30, formResponses.Field31, formResponses.Field32, formResponses.Field33, formResponses.Field34, formResponses.Field35, formResponses.Field36, formResponses.Field37, formResponses.Field38, formResponses.Field39, formResponses.Field40, formResponses.ResponsedDevice, formResponses.ResponsedDeviceType, formResponses.ResponsedUserAgent };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool>  Update(int Id)
        {
            string storeProcCommand = "select form_responses_updateleadseen(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param)>0;
        }

        public async Task<IEnumerable<FormResponses>> GetDetails(FormResponses formResponses, int OFFSET, int FETCH, DateTime? TrackFromDate, DateTime? TrackToDate, List<string> fieldName = null, List<int> UserInfoUserIdList = null, int? IsSuperAdmin = null)
        {
            string fieldNames = fieldName != null ? string.Join(",", fieldName.ToArray()) : null;
            string storeProcCommand = "select * from form_responses_get(@FormId, @UserInfoUserId, @UserGroupId, @TrackIp, @MachineId, @SessionRefer, @ContactId, @SearchKeyword, @Referrer, @IsAdSenseOrAdWord, @Country, @StateName, @City, @PageUrl, @Field1, @Field2, @Field3, @Field4, @Field5, @Field6, @Field7, @Field8, @Field9, @Field10, @OFFSET, @FETCH, @TrackFromDate, @TrackToDate, @fieldNames, @Field11, @Field12, @Field13, @Field14, @Field15, @Field16, @Field17, @Field18, @Field19, @Field20, @UtmTagSource, @UtmMedium, @UtmCampaign, @UtmTerm, @UtmContent, @MailSubscribe, @MailOverallSubscribe, @SmsSubscribe, @SmsOverallSubscribe, @Field21, @Field22, @Field23, @Field24, @Field25, @Field26, @Field27, @Field28, @Field29, @Field30, @Field31, @Field32, @Field33, @Field34, @Field35, @Field36, @Field37, @Field38, @Field39, @Field40, @UserInfoUserIdList, @IsSuperAdmin)";
            object? param = new { formResponses.FormId, formResponses.UserInfoUserId, formResponses.UserGroupId, formResponses.TrackIp, formResponses.MachineId, formResponses.SessionRefer, formResponses.ContactId, formResponses.SearchKeyword, formResponses.Referrer, formResponses.IsAdSenseOrAdWord, formResponses.Country, formResponses.StateName, formResponses.City, formResponses.PageUrl, formResponses.Field1, formResponses.Field2, formResponses.Field3, formResponses.Field4, formResponses.Field5, formResponses.Field6, formResponses.Field7, formResponses.Field8, formResponses.Field9, formResponses.Field10, OFFSET, FETCH, TrackFromDate, TrackToDate, fieldNames, formResponses.Field11, formResponses.Field12, formResponses.Field13, formResponses.Field14, formResponses.Field15, formResponses.Field16, formResponses.Field17, formResponses.Field18, formResponses.Field19, formResponses.Field20, formResponses.UtmTagSource, formResponses.UtmMedium, formResponses.UtmCampaign, formResponses.UtmTerm, formResponses.UtmContent, formResponses.MailSubscribe, formResponses.MailOverallSubscribe, formResponses.SmsSubscribe, formResponses.SmsOverallSubscribe, formResponses.Field21, formResponses.Field22, formResponses.Field23, formResponses.Field24, formResponses.Field25, formResponses.Field26, formResponses.Field27, formResponses.Field28, formResponses.Field29, formResponses.Field30, formResponses.Field31, formResponses.Field32, formResponses.Field33, formResponses.Field34, formResponses.Field35, formResponses.Field36, formResponses.Field37, formResponses.Field38, formResponses.Field39, formResponses.Field40, UserInfoUserIdList, IsSuperAdmin };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<FormResponses>(storeProcCommand);
        }

        public async Task<IEnumerable<string>> GetIpAddress()
        {
            string storeProcCommand = "select * from form_responses_getipaddress()"; 
             
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<string>(storeProcCommand); 
        }


         
        public async Task<Int32> MaxCount(DateTime FromDateTime, DateTime ToDateTime, string EmbeddedFormOrPopUpFormOrTaggedForm, int UserInfoUserId = 0, List<int> UserInfoUserIdList = null, int? IsSuperAdmin = null, int? VisitorType = null)
        {
            string UserInfoUserIdLists = UserInfoUserIdList != null ? string.Join(",", UserInfoUserIdList) : null;
            string storeProcCommand = "select form_responses_maxcount(@FromDateTime, @ToDateTime, @EmbeddedFormOrPopUpFormOrTaggedForm, @UserInfoUserId, @UserInfoUserIdLists, @IsSuperAdmin, @VisitorType)";
            object? param = new { FromDateTime, ToDateTime, EmbeddedFormOrPopUpFormOrTaggedForm, UserInfoUserId, UserInfoUserIdLists, IsSuperAdmin, VisitorType };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<IEnumerable<MLFormResponseWithFormDetails>>  FormResponseDetails(int FormId, int OffSet, int FetchNext, DateTime FromDateTime, DateTime ToDateTime, string EmbeddedFormOrPopUpFormOrTaggedForm, int UserInfoUserId = 0, List<int> UserInfoUserIdList = null, int? IsSuperAdmin = null, int? VisitorType = null)
        {
            string UserInfoUserIdLists = UserInfoUserIdList != null ? string.Join(",", UserInfoUserIdList) : null;
            string storeProcCommand = "select * from form_responses_allresponsedata( @FormId, @OffSet, @FetchNext, @FromDateTime, @ToDateTime, @EmbeddedFormOrPopUpFormOrTaggedForm, @UserInfoUserId, @UserInfoUserIdLists, @IsSuperAdmin, @VisitorType)";
            object? param = new { FormId, OffSet, FetchNext, FromDateTime, ToDateTime, EmbeddedFormOrPopUpFormOrTaggedForm, UserInfoUserId, UserInfoUserIdLists, IsSuperAdmin, VisitorType };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLFormResponseWithFormDetails>(storeProcCommand);
        }


        public async Task<Int32> GetCustomMaxCount(FormResponses formResponses, string StartDate, string EndDate, string EmbeddedFormOrPopUpFormOrTaggedForm, List<int> UserInfoUserIdList = null, int? IsSuperAdmin = null)
        {
            string storeProcCommand = "select forms_pg_responsescustomreport_getdatamaxcount(@UserInfoUserId, @FormId, @TrackIp, @Field1, @PageUrl, @SearchKeyword, @Referrer, @IsAdSenseOrAdWord, @StartDate, @EndDate, @UtmTagSource, @UtmMedium, @UtmCampaign, @UtmTerm, @UtmContent, @MailSubscribe, @MailOverallSubscribe, @SmsSubscribe, @SmsOverallSubscribe, @UserInfoUserIdList, @IsSuperAdmin, @EmbeddedFormOrPopUpFormOrTaggedForm)";
            object? param = new { formResponses.UserInfoUserId, formResponses.FormId, formResponses.TrackIp, formResponses.Field1, formResponses.PageUrl, formResponses.SearchKeyword, formResponses.Referrer, formResponses.IsAdSenseOrAdWord, StartDate, EndDate, formResponses.UtmTagSource, formResponses.UtmMedium, formResponses.UtmCampaign, formResponses.UtmTerm, formResponses.UtmContent, formResponses.MailSubscribe, formResponses.MailOverallSubscribe, formResponses.SmsSubscribe, formResponses.SmsOverallSubscribe, UserInfoUserIdList, IsSuperAdmin, EmbeddedFormOrPopUpFormOrTaggedForm };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }


        public async Task<IEnumerable<MLFormResponseWithFormDetails>>  GetCustomResponses(FormResponses formResponses, int OffSet, int FetchNext, string StartDate, string EndDate, string EmbeddedFormOrPopUpFormOrTaggedForm, List<int> UserInfoUserIdList = null, int? IsSuperAdmin = null)
        {
            string storeProcCommand = "select * from forms_pg_responsescustomreport_getsearchdata(@UserInfoUserId, @FormId, @TrackIp, @Field1, @PageUrl, @SearchKeyword, @Referrer, @IsAdSenseOrAdWord, @OffSet, @FetchNext, @StartDate, @EndDate, @UtmTagSource, @UtmMedium, @UtmCampaign, @UtmTerm, @UtmContent, @MailSubscribe, @MailOverallSubscribe, @SmsSubscribe, @SmsOverallSubscribe, @UserInfoUserIdList, @IsSuperAdmin, @EmbeddedFormOrPopUpFormOrTaggedForm)";
            object? param = new { formResponses.UserInfoUserId, formResponses.FormId, formResponses.TrackIp, formResponses.Field1, formResponses.PageUrl, formResponses.SearchKeyword, formResponses.Referrer, formResponses.IsAdSenseOrAdWord, OffSet, FetchNext, StartDate, EndDate, formResponses.UtmTagSource, formResponses.UtmMedium, formResponses.UtmCampaign, formResponses.UtmTerm, formResponses.UtmContent, formResponses.MailSubscribe, formResponses.MailOverallSubscribe, formResponses.SmsSubscribe, formResponses.SmsOverallSubscribe, UserInfoUserIdList, IsSuperAdmin, EmbeddedFormOrPopUpFormOrTaggedForm };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLFormResponseWithFormDetails>(storeProcCommand,param);
        }

        public async Task<DataSet> GetCustomisedFormReponseDetails(FormResponses formResponses, string StartDate, string EndDate)
        {
            string storeProcCommand = "select * from forms_pg_responsescustomreport_getcustomisedformresponsedata(@FormId, @TrackIp, @Field1, @PageUrl, @SearchKeyword, @Referrer, @IsAdSenseOrAdWord, @StartDate, @EndDate, @UtmTagSource, @UtmMedium, @UtmCampaign, @UtmTerm, @UtmContent, @MailSubscribe, @MailOverallSubscribe, @SmsSubscribe, @SmsOverallSubscribe )";
            object? param = new { formResponses.FormId, formResponses.TrackIp, formResponses.Field1, formResponses.PageUrl, formResponses.SearchKeyword, formResponses.Referrer, formResponses.IsAdSenseOrAdWord, StartDate, EndDate, formResponses.UtmTagSource, formResponses.UtmMedium, formResponses.UtmCampaign, formResponses.UtmTerm, formResponses.UtmContent, formResponses.MailSubscribe, formResponses.MailOverallSubscribe, formResponses.SmsSubscribe, formResponses.SmsOverallSubscribe };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<DataSet> FilterDataBySourceCount(DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "select * from form_responses_filterdatabysourcecount( @FromDateTime, @ToDateTime)";
            object? param = new { FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<DataSet> FilterDataByCityCount(DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "select * from form_responses_filterdatabycitycount(@FromDateTime, @ToDateTime)"; 
            object? param = new { FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<DataSet> GetFormRespondedNameByContactId(int? ContactId = null)
        {
            string storeProcCommand = "select * from form_responses_formrespondednamesbycontactid(@ContactId)"; 
            object? param = new { ContactId };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }
        public async Task<Int32> LeadsResponseCount(string Duration, DateTime FromDate, DateTime ToDate)
        {
            string storeProcCommand = "select * from forms_pg_responsescustomreport_leadsresponsecount(@Duration, @FromDate, @ToDate)"; 
            object? param = new { Duration, FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
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
