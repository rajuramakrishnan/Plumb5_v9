﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;
using System.Data;
using System.Reflection.PortableExecutable;

namespace P5GenralDL
{
    public class DLCustomDetailsFormTrackingPG : CommonDataBaseInteraction, IDLCustomDetailsFormTracking
    {
        CommonInfo connection = null;
        public DLCustomDetailsFormTrackingPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLCustomDetailsFormTrackingPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<int> GetLeadType(string MachineId)
        {
            string storeProcCommand = "select forms_sp_allformsloading_getleadtype(@MachineId)";
            object? param = new { MachineId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<int> GetContactDetailsByMachineId(string MachineId)
        {
            string storeProcCommand = "select forms_sp_allformsloading_getemailidbymachineid(@MachineId)";
            object? param = new { MachineId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<string> GroupsByMachineId(string MachineId)
        {
            string storeProcCommand = "select forms_sp_allformsloading_groupidmachineid(@MachineId)";
            object? param = new { MachineId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<string>(storeProcCommand, param);
        }

        public async Task<string> GroupsByMachineIdForDynamicGroup(string MachineId)
        {
            string storeProcCommand = "select forms_sp_allformsloading_groupidmachineidfordynamicgroup(@MachineId)";
            object? param = new { MachineId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<string>(storeProcCommand, param);
        }

        public async Task<string> GetSession(string MachineId)
        {
            string returnvalue = "";
            string storeProcCommand = "select selectvisitordetails_aggregate(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = reader["Session"].ToString();

            return returnvalue;
        }

        public async Task<short> GetBehavioralScore(string MachineId)
        {
            short returnvalue = 0;
            string storeProcCommand = "select selectvisitordetails_aggregate(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = Convert.ToByte(reader["Score"]);

            return returnvalue;
        }

        public async Task<short> GetPageDepeth(string MachineId)
        {
            short returnvalue = 0;
            string storeProcCommand = "select selectvisitordetails_recent(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = Convert.ToByte(reader["PageDepth"]);

            return returnvalue;
        }

        public async Task<Int32> GetPageviews(string MachineId)
        {
            int returnvalue = 0;
            string storeProcCommand = "select selectvisitordetails_aggregate(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = Convert.ToByte(reader["PageViews"]);

            return returnvalue;
        }

        public async Task<short> GetFrequency(string MachineId)
        {
            short returnvalue = 0;
            string storeProcCommand = "select selectvisitordetails_aggregate(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = Convert.ToByte(reader["Frequency"]);

            return returnvalue;
        }
        public async Task<string> GetSource(string MachineId)
        {
            string returnvalue = "";
            string storeProcCommand = "select selectvisitordetails_recentsource(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = reader["RecentSource"].ToString();

            return returnvalue;
        }

        public async Task<string> GetSourcekey(string MachineId)
        {
            string returnvalue = "";
            string storeProcCommand = "select selectvisitordetails_recentsourcekey(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = reader["RecentSourceKey"].ToString();

            return returnvalue;
        }

        public async Task<string> GetSourceType(string MachineId)
        {
            string returnvalue = "";
            string storeProcCommand = "select selectvisitordetails_recentsourcetype(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = reader["RecentSourceType"].ToString();

            return returnvalue;
        }

        public async Task<string> GetUtmTagSource(string MachineId)
        {
            string returnvalue = "";
            string storeProcCommand = "select selectvisitordetails_recent(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = reader["UtmTagSource"].ToString();

            return returnvalue;
        }

        public async Task<bool> IsMailRespondent(string EmailId, string MailTemplateIds, bool IsMailRespondentClickCondition)
        {
            if (!String.IsNullOrEmpty(EmailId))
            {
                string returnvalue = "";
                string storeProcCommand = "select forms_sp_allformsloading_ismailrespondent(@EmailId, @IsMailRespondentClickCondition, @MailTemplateIds)";
                object? param = new { EmailId, IsMailRespondentClickCondition, MailTemplateIds };
                using var db = GetDbConnection(connection.Connection);
                var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

                if (reader.Read())
                    returnvalue = reader["EmailId"].ToString();

                if (!String.IsNullOrEmpty(returnvalue))
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> IsSmsRespondent(string PhoneNumber, string SmsTemplateIds)
        {
            if (!String.IsNullOrEmpty(PhoneNumber))
            {
                string returnvalue = "";
                string storeProcCommand = "select forms_sp_allformsloading_issmsrespondent(@PhoneNumber, @SmsTemplateIds)";
                object? param = new { PhoneNumber, SmsTemplateIds };
                using var db = GetDbConnection(connection.Connection);
                var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

                if (reader.Read())
                    returnvalue = reader["PhoneNumber"].ToString();

                if (!String.IsNullOrEmpty(returnvalue))
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<string> SearchKeyword(string MachineId)
        {
            string returnvalue = "";
            string storeProcCommand = "select selectvisitordetails_recent(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = reader["RecentSearchKey"].ToString();

            return returnvalue;
        }


        public async Task<string> GetStateDetails(string MachineId)
        {
            string returnvalue = "";
            string storeProcCommand = "select selectvisitordetails_getstatedetails(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = reader["State"].ToString();

            return returnvalue;
        }

        public async Task<string[]> GetCityCountry(string MachineId)
        {
            string[] CountryAndCity = { "", "" };
            string storeProcCommand = "select selectvisitordetails_recent(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
            {
                CountryAndCity[0] = reader["RecentCountry"].ToString();
                CountryAndCity[1] = reader["RecentCity"].ToString();
            }
            return CountryAndCity;
        }

        public async Task<bool> AlreadyVisitedPages(string MachineId, string SessionRefeer, string PageUrls, bool VisitedPagesOverAllOrSessionWise, bool IsVisitedPagesContainsCondition)
        {
            string storeProcCommand = "select forms_sp_allformsloading_alreadyvisitedpages(@IsVisitedPagesContainsCondition, @VisitedPagesOverAllOrSessionWise, @SessionRefeer, @MachineId, @PageUrls)";
            object? param = new { IsVisitedPagesContainsCondition, VisitedPagesOverAllOrSessionWise, SessionRefeer, MachineId, PageUrls };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }

        public async Task<bool> AlreadyNotVisitedPages(string MachineId, string SessionRefeer, string PageUrls, bool VisitedPagesOverAllOrSessionWise, bool IsNotVisitedPagesContainsCondition)
        {
            string storeProcCommand = "select forms_sp_allformsloading_alreadynotvisitedpages(@IsNotVisitedPagesContainsCondition, @VisitedPagesOverAllOrSessionWise, @SessionRefeer, @MachineId, @PageUrls)";
            object? param = new { IsNotVisitedPagesContainsCondition, VisitedPagesOverAllOrSessionWise, SessionRefeer, MachineId, PageUrls };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }

        public async Task<int> OverAllTimeSpentInSite(string MachineId)
        {
            int returnvalue = 0;
            string storeProcCommand = "select selectvisitordetails_aggregate(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = Convert.ToInt32(reader["AvgTime"]);

            return returnvalue;
        }

        public async Task<bool> isMobileBrowser(string MachineId)
        {
            string returnvalue = "";
            string storeProcCommand = "select selectvisitordetails_recent(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = reader["IsDevice"].ToString();

            if (!String.IsNullOrEmpty(returnvalue))
                return true;
            else
                return false; ;
        }

        public async Task<string> GetClickedButton(string MachineId)
        {
            string returnvalue = "";
            string storeProcCommand = "select selectvisitordetails_aggregateofeventproduct(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = reader["Events"].ToString();

            return returnvalue;
        }
        public async Task<string> GetRecentClickedButton(string MachineId)
        {
            string returnvalue = "";
            string storeProcCommand = "select selectvisitordetails_recent(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = reader["RecentEvent"].ToString();

            return returnvalue;

        }

        public async Task<bool> RespondedChatAgent(string MachineId)
        {
            string storeProcCommand = "select forms_sp_allformsloading_respondedchatagent(@MachineId)";
            object? param = new { MachineId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }


        public async Task<byte[]> MailCampignResponsiveStage(string EmailId)
        {
            byte[] MailResposiveStage = { 0, 0 };
            string storeProcCommand = "select forms_sp_allformsloading_mailcampaignsstage(@EmailId)";
            object? param = new { EmailId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
            {
                if (!String.IsNullOrEmpty(reader["Score"].ToString()))
                    MailResposiveStage[0] = Convert.ToByte(reader["Score"]);
                if (!String.IsNullOrEmpty(reader["ScoreType"].ToString()))
                    MailResposiveStage[1] = Convert.ToByte(reader["ScoreType"]);
            }
            return MailResposiveStage;
        }

        public async Task<List<int>> ResponseFormList(string MachineId)
        {
            List<int> FormIdList = new List<int>();
            string storeProcCommand = "select forms_sp_allformsloading_responseformlist(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            while (reader.Read())
            {
                FormIdList.Add(Convert.ToInt32(reader["FormId"]));
            }
            return FormIdList;
        }


        public async Task<DataSet> FormLeadDetailsAnswerDependency(string MachineId, int FormId)
        {
            string storeProcCommand = "select * from forms_sp_allformsloading_responseleaddata(@MachineId, @FormId)";
            object? param = new { MachineId, FormId };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<short> ClosedFormNthTime(string MachineId, int FormId)
        {
            short returnvalue = 0;
            string storeProcCommand = "select forms_sp_allformsloading_closedformnthtime(@MachineId, @FormId)";
            object? param = new { MachineId, FormId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = Convert.ToByte(reader["ClosedCount"]);

            return returnvalue;
        }

        public async Task<short> ChatClosedFormNthTime(string MachineId, int ChatId)
        {
            short returnvalue = 0;
            string storeProcCommand = "select forms_sp_allformsloading_closedchatnthtime(@MachineId, @ChatId)";
            object? param = new { MachineId, ChatId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = Convert.ToByte(reader["ClosedCount"]);

            return returnvalue;
        }

        public async Task<short> ClosedFormSessionWise(string MachineId, string SessionRefeer, int FormId)
        {
            short returnvalue = 0;
            string storeProcCommand = "select forms_sp_allformsloading_closedformnthtimesessionwise(@MachineId,@SessionRefeer,@FormId)";
            object? param = new { MachineId, SessionRefeer, FormId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                returnvalue = Convert.ToByte(reader["CloseCountSessionWise"]);

            return returnvalue;
        }

        public async Task<short> GetFormImpression(string MachineId, int FormId)
        {
            string storeProcCommand = "select forms_sp_allformsloading_formimpression(@MachineId, @FormId)";
            object? param = new { MachineId, FormId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<short>(storeProcCommand, param);
        }

        public async Task<short> GetFormCloseCount(string MachineId, int FormId)
        {
            string storeProcCommand = "select forms_sp_allformsloading_formclosecount(@MachineId, @FormId)";
            object? param = new { MachineId, FormId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<short>(storeProcCommand, param);
        }

        public async Task<short> GetFormResponseCount(string MachineId, int FormId)
        {
            string storeProcCommand = "select forms_sp_allformsloading_formresponsecount(@MachineId,@FormId)";
            object? param = new { MachineId, FormId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<short>(storeProcCommand, param);
        }

        public async Task<short> GetCountShowThisFormOnlyNthTime(string MachineId, int FormId)
        {
            string storeProcCommand = "select forms_sp_allformsloading_getcountshowthisformonlynthtime(@MachineId,@FormId)";
            object? param = new { MachineId, FormId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<short>(storeProcCommand, param);
        }

        public async Task<short> ChatGetCountShowThisFormOnlyNthTime(string MachineId, int ChatId)
        {
            string storeProcCommand = "select chat_sp_allloadingcondition_getcountshowthisformonlynthtime(@MachineId,@ChatId)";
            object? param = new { MachineId, ChatId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<short>(storeProcCommand, param);
        }

        public async Task<string> ProfessionIs(int ContactId)
        {
            string storeProcCommand = "select contact_details_getprofessiondetails(@ContactId)";
            object? param = new { ContactId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<string>(storeProcCommand, param);
        }

        public async Task<short> NurtureStatusIs(int ContactId)
        {
            string storeProcCommand = "select forms_sp_allformsloading_nurturestatusis(@ContactId)";
            object? param = new { ContactId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<short>(storeProcCommand, param);
        }

        public async Task<short> LoyaltyPoints(int ContactId)
        {
            string storeProcCommand = "select formrules_engauageandq5_loyaltypoints(@ContactId)";
            object? param = new { ContactId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<short>(storeProcCommand, param);
        }

        public async Task<Int16> PaidCampaignFlag(string MachineId)
        {
            string storeProcCommand = "select selectvisitordetails_paidcampaignflag(@MachineId)";
            object? param = new { MachineId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param);
        }

        public async Task<string> GetGenderValue(int ContactId)
        {
            string storeProcCommand = "select contact_details_getgenderdetails(@ContactId)";
            object? param = new { ContactId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<string>(storeProcCommand, param);
        }

        public async Task<short> GetMaritalStatus(int ContactId)
        {
            string MaritalStatus = "";
            short returnvalue = 0;
            string storeProcCommand = "select contact_details_getmaritaldetails(@ContactId)";
            object? param = new { ContactId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
                MaritalStatus = reader["Maritalstatus"].ToString();

            if (!String.IsNullOrEmpty(MaritalStatus))
            {
                if (MaritalStatus.ToLower().Trim() == "single")
                    return 1;
                else if (MaritalStatus.ToLower().Trim() == "married")
                    return 2;
                else if (MaritalStatus.ToLower().Trim() == "divorced")
                    return 3;
            }
            return returnvalue;
        }

        public async Task<short> ConnectedSocially(int ContactId)
        {
            string storeProcCommand = "select formrules_engauageandq5_connectedsocially(@ContactId)";
            object? param = new { ContactId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<short>(storeProcCommand, param);
        }

        public async Task<Tuple<string, string, string, string, string, string, string, Tuple<string, string, string>>> GetVisitorDetails(string MachineId)
        {
            string storeProcCommand = "select selectvisitordetails_recent(@MachineId)";
            object? param = new { MachineId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            if (reader.Read())
            {
                return new Tuple<string, string, string, string, string, string, string, Tuple<string, string, string>>(reader["RecentSourceType"].ToString(), reader["RecentSource"].ToString(), reader["RecentSearchKey"].ToString(), reader["RecentSourceKey"].ToString(), reader["RecentCountry"].ToString(), reader["RecentCity"].ToString(), reader["PaidCampaignFlag"].ToString(), new Tuple<string, string, string>(reader["UtmTagSource"].ToString(), reader["UtmMedium"].ToString(), reader["UtmCampaign"].ToString()));
            }
            return null;
        }

        public async Task<List<string>> GetPageNamesByMachineId(string MachineId)
        {
            List<string> PageList = new List<string>();
            string storeProcCommand = "select forms_sp_allformsloading_getpagenamesbymachineid(@MachineId)";
            object? param = new { MachineId };

            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            while (reader.Read())
            {
                PageList.Add(reader["PageName"].ToString());
            }

            return PageList;
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

