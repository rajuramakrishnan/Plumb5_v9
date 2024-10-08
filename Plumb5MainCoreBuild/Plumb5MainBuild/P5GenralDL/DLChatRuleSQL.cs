﻿using DBInteraction;
using IP5GenralDL;
using Dapper;
using P5GenralML;
using System.Data;

namespace P5GenralDL
{
    public class DLChatRuleSQL : CommonDataBaseInteraction, IDLChatRule
    {
        CommonInfo connection = null;
        public DLChatRuleSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public DLChatRuleSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<bool> Save(ChatRule rulesData)
        {
            string storeProcCommand = "Chat_Rules";
            object? param = new
            {
                @Action = "Save",
                rulesData.ChatId,
                rulesData.IsLead,
                rulesData.IsBelong,
                rulesData.BelongsToGroup,
                rulesData.BehavioralScoreCondition,
                rulesData.BehavioralScore1,
                rulesData.BehavioralScore2,
                rulesData.SessionIs,
                rulesData.PageDepthIs,
                rulesData.NPageVisited,
                rulesData.FrequencyIs,
                rulesData.PageUrl,
                rulesData.IsPageUrlContainsCondition,
                rulesData.IsReferrer,
                rulesData.ReferrerUrl,
                rulesData.CheckSourceDomainOnly,
                rulesData.IsMailIsRespondent,
                rulesData.SearchString,
                rulesData.Country,
                rulesData.City,
                rulesData.IsClickedSpecificButtons,
                rulesData.ClickedPriceRangeProduct,
                rulesData.IsVisitorRespondedChat,
                rulesData.MailCampignResponsiveStage,
                rulesData.IsRespondedForm,
                rulesData.IsNotRespondedForm,
                rulesData.CloseCount,
                rulesData.AddedProductsToCart,
                rulesData.ViewedButNotAddedProductsToCart,
                rulesData.DroppedProductsFromCart,
                rulesData.PurchasedProducts,
                rulesData.NotPurchasedProducts,
                rulesData.TotalPurchaseQtyConditon,
                rulesData.CustomerTotalPurchase1,
                rulesData.CustomerTotalPurchase2,
                rulesData.TotalPurchaseAmtCondition,
                rulesData.CustomerCurrentValue1,
                rulesData.CustomerCurrentValue2,
                rulesData.DependencyFormId,
                rulesData.DependencyFormField,
                rulesData.DependencyFormCondition,
                rulesData.DependencyFormAnswer1,
                rulesData.DependencyFormAnswer2,
                rulesData.ImpressionEventForFormId,
                rulesData.ImpressionEventCountCondition,
                rulesData.CloseEventForFormId,
                rulesData.CloseEventCountCondition,
                rulesData.ResponsesEventForFormId,
                rulesData.ResponsesEventCountCondition,
                rulesData.OnlineSentimentIs,
                rulesData.SocialStatusIs,
                rulesData.InfluentialScoreCondition,
                rulesData.InfluentialScore1,
                rulesData.InfluentialScore2,
                rulesData.OfflineSentimentIs,
                rulesData.ProductRatingIs,
                rulesData.NurtureStatusIs,
                rulesData.GenderIs,
                rulesData.MaritalStatusIs,
                rulesData.ProfessionIs,
                rulesData.NotConnectedSocially,
                rulesData.LoyaltyPointsCondition,
                rulesData.LoyaltyPointsRange1,
                rulesData.LoyaltyPointsRange2,
                rulesData.RFMSScoreRangeCondition,
                rulesData.RFMSScoreRange1,
                rulesData.RFMSScoreRange2,
                rulesData.SessionConditionIsTrueOrIsFalse,
                rulesData.PageDepthConditionIsTrueOrIsFalse,
                rulesData.PageViewConditionIsTrueOrIsFalse,
                rulesData.FrequencyConditionIsTrueOrIsFalse,
                rulesData.MailRespondentConditionIsTrueOrIsFalse,
                rulesData.CountryConditionIsTrueOrIsFalse,
                rulesData.CityConditionIsTrueOrIsFalse,
                rulesData.AlreadyVisitedPages,
                rulesData.OverAllTimeSpentInSite,
                rulesData.CloseCountSessionWiseOrOverAll,
                rulesData.ShowFormOnlyNthTime,
                rulesData.IsMobileDevice,
                rulesData.AlreadyVisitedPagesOverAllOrSessionWise,
                rulesData.ExcludeIpList,
                rulesData.ExceptionPageUrl,
                rulesData.IsExceptionPageUrlContainsCondition,
                rulesData.AddedProductsCategoriesToCart,
                rulesData.NotAddedProductsCategoriesToCart,
                rulesData.AddedProductsSubCategoriesToCart,
                rulesData.NotAddedProductsSubCategoriesToCart,
                rulesData.MailRespondentTemplates,
                rulesData.IsSmsIsRespondent,
                rulesData.SmsRespondentConditionIsTrueOrIsFalse,
                rulesData.SmsRespondentTemplates,
                rulesData.IsMailRespondentClickCondition,
                rulesData.IsBirthDay,
                rulesData.IsDOBTodayOrMonth,
                rulesData.NotAlreadyVisitedPages,
                rulesData.NotAlreadyVisitedPagesOverAllOrSessionWise,
                rulesData.DOBFromDate,
                rulesData.DOBToDate,
                rulesData.DOBDaysDiffernce,
                rulesData.IsDOBIgnored,
                rulesData.IsDOBIgnoreCondition,
                rulesData.IsVisitedPagesContainsCondition,
                rulesData.IsNotVisitedPagesContainsCondition,
                rulesData.PageUrlParameters,
                rulesData.AlreadyVisitedWithPageUrlParameters,
                rulesData.NotAlreadyVisitedWithPageUrlParameters,
                rulesData.IsVisitorVisitedPagesWithPageUrlParameter,
                rulesData.IsVisitorsSource,
                rulesData.StateName,
                rulesData.StateConditionIsTrueOrIsFalse
            };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<ChatRule?> Get(int ChatId)
        {
            string storeProcCommand = "Chat_Rules";
            object? param = new { @Action = "GET", ChatId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<ChatRule>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<ChatRule?> GetRawRules(int ChatId)
        {
            string storeProcCommand = "Chat_Rules";
            object? param = new { @Action = "GetRawRules", ChatId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<ChatRule>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
