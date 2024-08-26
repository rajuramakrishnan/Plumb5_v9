using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLFormDetailsSQL : CommonDataBaseInteraction, IDLFormDetails
    {
        CommonInfo connection = null;
        public DLFormDetailsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLFormDetailsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<Int32> Save(FormDetails formDetails)
        {
            string storeProcCommand = "Form_Details";
            object? param = new { @Action = "Save", formDetails.UserInfoUserId, formDetails.UserGroupId, formDetails.FormCampaignId, formDetails.FormType, formDetails.FormPriority, formDetails.Heading, formDetails.SubHeading, formDetails.PageContentForEmbed, formDetails.IsMainBackgroundDesignCustom, formDetails.MainBackgroundDesign, formDetails.TitleCss, formDetails.IsTitleCssCustom, formDetails.DescriptionCss, formDetails.IsDescriptionCustomCss, formDetails.LabelCss, formDetails.IsLabelCustomCss, formDetails.TextboxDropCss, formDetails.IsTextboxDropCustomCss, formDetails.ButtonCss, formDetails.IsButtonCustomCss, formDetails.ErrorCss, formDetails.IsErrorCustomCss, formDetails.CloseCss, formDetails.IsCloseCustomCss, formDetails.ButtonName, formDetails.OnPageOrInPage, formDetails.AppearenceEffect, formDetails.PositionAlign, formDetails.AppearenceSpeed, formDetails.TopOrBottomPadding, formDetails.RightOrLeftPadding, formDetails.TimeDelay, formDetails.AppearSound, formDetails.ThankYouMessage, formDetails.HideEffect, formDetails.BackgroundPxOPer, formDetails.PlaceHolderClass, formDetails.BackgroundFadeColor, formDetails.AppearOnLoadOnExitOnScroll, formDetails.ShowOnScrollDownHeight, formDetails.AppearEffectOfFields, formDetails.GeneralCss, formDetails.GeneralParentCss, formDetails.OuterMainStyle, formDetails.RowListStyle, formDetails.RowStyle, formDetails.OuterLabelStyle, formDetails.OuterInputStyle, formDetails.OuterButtonStyle, formDetails.RadioButtonLabelCss, formDetails.RadioButtonFieldsCss, formDetails.CheckBoxLabelCss, formDetails.CheckBoxFieldsCss, formDetails.IsNewDivOrOldTable, formDetails.ClientCampaignIdentifier, formDetails.FormIdentifier, formDetails.IsMinimiseButton, formDetails.IsMinimiseCustomCss, formDetails.MinimiseCss, formDetails.EmbeddedFormOrPopUpFormOrTaggedForm, formDetails.CloseDesignType, formDetails.CloseAlignmentSetting, formDetails.ButtonPxOrPer, formDetails.FormStatus, formDetails.IsBackgroundUploadImageOrOnlineUrl, formDetails.BannerImageDesignCss, formDetails.IsBannerImageDesignCustom, formDetails.IsBannerImageHidden, formDetails.IsRadioCheckBoxFieldsCssCustom, formDetails.RadioCheckBoxFieldsCss, formDetails.ImageAppearanceAlignment, formDetails.AutoClose };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<bool> Update(FormDetails formDetails)
        {
            string storeProcCommand = "Form_Details";
            object? param = new { @Action = "Update", formDetails.Id, formDetails.UserInfoUserId, formDetails.UserGroupId, formDetails.FormCampaignId, formDetails.FormType, formDetails.FormPriority, formDetails.Heading, formDetails.SubHeading, formDetails.PageContentForEmbed, formDetails.IsMainBackgroundDesignCustom, formDetails.MainBackgroundDesign, formDetails.TitleCss, formDetails.IsTitleCssCustom, formDetails.DescriptionCss, formDetails.IsDescriptionCustomCss, formDetails.LabelCss, formDetails.IsLabelCustomCss, formDetails.TextboxDropCss, formDetails.IsTextboxDropCustomCss, formDetails.ButtonCss, formDetails.IsButtonCustomCss, formDetails.ErrorCss, formDetails.IsErrorCustomCss, formDetails.CloseCss, formDetails.IsCloseCustomCss, formDetails.ButtonName, formDetails.OnPageOrInPage, formDetails.AppearenceEffect, formDetails.PositionAlign, formDetails.AppearenceSpeed, formDetails.TopOrBottomPadding, formDetails.RightOrLeftPadding, formDetails.TimeDelay, formDetails.AppearSound, formDetails.ThankYouMessage, formDetails.HideEffect, formDetails.BackgroundPxOPer, formDetails.PlaceHolderClass, formDetails.BackgroundFadeColor, formDetails.AppearOnLoadOnExitOnScroll, formDetails.ShowOnScrollDownHeight, formDetails.AppearEffectOfFields, formDetails.GeneralCss, formDetails.GeneralParentCss, formDetails.CreatedDate, formDetails.OuterMainStyle, formDetails.RowListStyle, formDetails.RowStyle, formDetails.OuterLabelStyle, formDetails.OuterInputStyle, formDetails.OuterButtonStyle, formDetails.RadioButtonLabelCss, formDetails.RadioButtonFieldsCss, formDetails.CheckBoxLabelCss, formDetails.CheckBoxFieldsCss, formDetails.IsNewDivOrOldTable, formDetails.FormIdentifier, formDetails.IsMinimiseButton, formDetails.IsMinimiseCustomCss, formDetails.MinimiseCss, formDetails.EmbeddedFormOrPopUpFormOrTaggedForm, formDetails.CloseDesignType, formDetails.CloseAlignmentSetting, formDetails.ButtonPxOrPer, formDetails.FormStatus, formDetails.IsBackgroundUploadImageOrOnlineUrl, formDetails.BannerImageDesignCss, formDetails.IsBannerImageDesignCustom, formDetails.IsBannerImageHidden, formDetails.IsRadioCheckBoxFieldsCssCustom, formDetails.RadioCheckBoxFieldsCss, formDetails.ImageAppearanceAlignment, formDetails.AutoClose };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }
        public async Task<List<FormDetails>> GET(FormDetails formDetails, int OffSet, int FetchNext, string ListOfFormId, List<string> fieldName, bool IsEventForms, List<int> UserInfoUserIdList = null, int? IsSuperAdmin = null, Nullable<DateTime> FromDateTime = null, Nullable<DateTime> ToDateTime = null)
        {
            string UserInfoUserIdLists = (UserInfoUserIdList != null ? string.Join(",", UserInfoUserIdList) : null);
            short? AppearOnLoadOnExitOnScroll = formDetails.AppearOnLoadOnExitOnScroll == 0 ? null : formDetails.AppearOnLoadOnExitOnScroll;

            if (IsEventForms)
            {
                string storeProcCommand = "Form_Details";
                object? param = new { @Action = "GetEventRulesFormsList", OffSet, FetchNext, formDetails.Id, AppearOnLoadOnExitOnScroll };

                using var db = GetDbConnection(connection.Connection);
                return (await db.QueryAsync<FormDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
            }
            else
            {
                string storeProcCommand = "Form_Details";
                object? param = new { @Action = "GET", formDetails.UserInfoUserId, formDetails.Heading, formDetails.FormStatus, formDetails.FormType, formDetails.OnPageOrInPage, OffSet, FetchNext, ListOfFormId, formDetails.IsWebOrMobileForm, UserInfoUserIdLists, IsSuperAdmin, AppearOnLoadOnExitOnScroll, formDetails.FormIdentifier, formDetails.EmbeddedFormOrPopUpFormOrTaggedForm, FromDateTime, ToDateTime };

                using var db = GetDbConnection(connection.Connection);
                return (await db.QueryAsync<FormDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<FormDetails> GETDetails(FormDetails formDetails)
        {
            string storeProcCommand = "Form_Details";
            object? param = new { @Action = "GET", formDetails.Id, formDetails.Heading, formDetails.FormStatus, formDetails.FormType };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<FormDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public FormDetails? GETDetailss(FormDetails formDetails)
        {
            string storeProcCommand = "Form_Details";
            object? param = new { @Action = "GET", formDetails.Id, formDetails.Heading, formDetails.FormStatus, formDetails.FormType };

            using var db = GetDbConnection(connection.Connection);
            return db.QueryFirstOrDefault<FormDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> GetMaxCount(FormDetails formDetails, Nullable<DateTime> FromDateTime = null, Nullable<DateTime> ToDateTime = null)
        {
            string storeProcCommand = "Form_Details";
            object? param = new { @Action = "MaxCount", formDetails.UserInfoUserId, formDetails.Heading, formDetails.FormStatus, formDetails.FormType, formDetails.OnPageOrInPage, formDetails.IsWebOrMobileForm, formDetails.FormIdentifier, formDetails.EmbeddedFormOrPopUpFormOrTaggedForm, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Delete(Int32 Id)
        {
            string storeProcCommand = "Form_Details";
            object? param = new { @Action = "Delete", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> ToogleStatus(FormDetails formDetails)
        {
            string storeProcCommand = "Form_Details";
            object? param = new { @Action = "ToogleStatus", formDetails.Id, formDetails.FormStatus };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> GetStatus(int FormId)
        {
            string storeProcCommand = "Form_Details";
            object? param = new { @Action = "GetStatus", FormId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> ChangePriority(int Id, Int16 FormPriority)
        {
            string storeProcCommand = "Form_Details";
            object? param = new { @Action = "UpdatePriorityStatus", Id, FormPriority };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<int> GetTopOneIdBasedOnFormType(int FormType)
        {
            string storeProcCommand = "Form_Details";
            object? param = new { @Action = "GetTopOneIdBasedOnFormType", FormType };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<bool> UpdateCampaignIdentifier(int Id, string ClientCampaignIdentifier, int CamapignId, string CampaignIdentiferName, bool IsOtpForm, int OTPFormId, bool IsWebOrMobileForm, int OTPGenerationLimits, bool OTPPageRestrictions, bool IsClickToCallForm, bool IsVerifiedEmail, bool IsAutoWhatsApp, string BlockEmailIds)
        {
            string storeProcCommand = "Form_Details";
            object? param = new { @Action = "UpdateCampaignIdentifier", Id, ClientCampaignIdentifier, CamapignId, CampaignIdentiferName, IsOtpForm, OTPFormId, IsWebOrMobileForm, OTPGenerationLimits, OTPPageRestrictions, IsClickToCallForm, IsVerifiedEmail, IsAutoWhatsApp, BlockEmailIds };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }
        public async Task<List<FormDetails>> GetOTPForms(string FormType = null)
        {
            string storeProcCommand = "Form_Details";
            object? param = new { @Action = "GetOTPForms", FormType };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<FormDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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


