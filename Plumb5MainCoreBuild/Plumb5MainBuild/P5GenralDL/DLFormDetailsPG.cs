using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLFormDetailsPG : CommonDataBaseInteraction, IDLFormDetails
    {
        CommonInfo connection = null;
        public DLFormDetailsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLFormDetailsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<Int32> Save(FormDetails formDetails)
        {
            string storeProcCommand = "select * from form_details_save(@UserInfoUserId, @UserGroupId, @FormCampaignId, @FormType, @FormPriority,@FormStatus, @Heading, @SubHeading, @PageContentForEmbed, @IsMainBackgroundDesignCustom, @MainBackgroundDesign, @TitleCss, @IsTitleCssCustom, @DescriptionCss, @IsDescriptionCustomCss, @LabelCss, @IsLabelCustomCss, @TextboxDropCss, @IsTextboxDropCustomCss, @ButtonCss, @IsButtonCustomCss, @ErrorCss, @IsErrorCustomCss, @CloseCss, @IsCloseCustomCss, @ButtonName, @OnPageOrInPage, @AppearenceEffect, @PositionAlign, @AppearenceSpeed, @TopOrBottomPadding, @RightOrLeftPadding, @TimeDelay, @AppearSound, @ThankYouMessage, @HideEffect, @BackgroundPxOPer, @PlaceHolderClass, @BackgroundFadeColor, @AppearOnLoadOnExitOnScroll, @ShowOnScrollDownHeight, @AppearEffectOfFields, @GeneralCss, @GeneralParentCss, @OuterMainStyle, @RowListStyle, @RowStyle, @OuterLabelStyle, @OuterInputStyle, @OuterButtonStyle, @RadioButtonLabelCss, @RadioButtonFieldsCss, @CheckBoxLabelCss, @CheckBoxFieldsCss, @IsNewDivOrOldTable, @ClientCampaignIdentifier,@CampaignIdentifier,@CampaignIdentifierName, @FormIdentifier, @IsMinimiseButton, @IsMinimiseCustomCss, @MinimiseCss, @EmbeddedFormOrPopUpFormOrTaggedForm, @CloseDesignType, @CloseAlignmentSetting, @ButtonPxOrPer,  @IsBackgroundUploadImageOrOnlineUrl, @BannerImageDesignCss, @IsBannerImageDesignCustom, @IsBannerImageHidden, @RadioCheckBoxFieldsCss, @IsRadioCheckBoxFieldsCssCustom, @ImageAppearanceAlignment, @AutoClose)";
            object? param = new { formDetails.UserInfoUserId, formDetails.UserGroupId, formDetails.FormCampaignId, formDetails.FormType, formDetails.FormPriority, formDetails.FormStatus, formDetails.Heading, formDetails.SubHeading, formDetails.PageContentForEmbed, formDetails.IsMainBackgroundDesignCustom, formDetails.MainBackgroundDesign, formDetails.TitleCss, formDetails.IsTitleCssCustom, formDetails.DescriptionCss, formDetails.IsDescriptionCustomCss, formDetails.LabelCss, formDetails.IsLabelCustomCss, formDetails.TextboxDropCss, formDetails.IsTextboxDropCustomCss, formDetails.ButtonCss, formDetails.IsButtonCustomCss, formDetails.ErrorCss, formDetails.IsErrorCustomCss, formDetails.CloseCss, formDetails.IsCloseCustomCss, formDetails.ButtonName, formDetails.OnPageOrInPage, formDetails.AppearenceEffect, formDetails.PositionAlign, formDetails.AppearenceSpeed, formDetails.TopOrBottomPadding, formDetails.RightOrLeftPadding, formDetails.TimeDelay, formDetails.AppearSound, formDetails.ThankYouMessage, formDetails.HideEffect, formDetails.BackgroundPxOPer, formDetails.PlaceHolderClass, formDetails.BackgroundFadeColor, formDetails.AppearOnLoadOnExitOnScroll, formDetails.ShowOnScrollDownHeight, formDetails.AppearEffectOfFields, formDetails.GeneralCss, formDetails.GeneralParentCss, formDetails.OuterMainStyle, formDetails.RowListStyle, formDetails.RowStyle, formDetails.OuterLabelStyle, formDetails.OuterInputStyle, formDetails.OuterButtonStyle, formDetails.RadioButtonLabelCss, formDetails.RadioButtonFieldsCss, formDetails.CheckBoxLabelCss, formDetails.CheckBoxFieldsCss, formDetails.IsNewDivOrOldTable, formDetails.ClientCampaignIdentifier, formDetails.CampaignIdentifier, formDetails.CampaignIdentifierName, formDetails.FormIdentifier, formDetails.IsMinimiseButton, formDetails.IsMinimiseCustomCss, formDetails.MinimiseCss, formDetails.EmbeddedFormOrPopUpFormOrTaggedForm, formDetails.CloseDesignType, formDetails.CloseAlignmentSetting, formDetails.ButtonPxOrPer, formDetails.IsBackgroundUploadImageOrOnlineUrl, formDetails.BannerImageDesignCss, formDetails.IsBannerImageDesignCustom, formDetails.IsBannerImageHidden, formDetails.RadioCheckBoxFieldsCss, formDetails.IsRadioCheckBoxFieldsCssCustom, formDetails.ImageAppearanceAlignment, formDetails.AutoClose };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);
        }
        public async Task<bool> Update(FormDetails formDetails)
        {
            string storeProcCommand = "select * from form_details_update(@Id,@UserGroupId, @UserInfoUserId,  @FormCampaignId, @FormType, @FormPriority,@FormStatus, @Heading, @SubHeading, @PageContentForEmbed, @IsMainBackgroundDesignCustom, @MainBackgroundDesign, @TitleCss, @IsTitleCssCustom, @DescriptionCss, @IsDescriptionCustomCss, @LabelCss, @IsLabelCustomCss, @TextboxDropCss, @IsTextboxDropCustomCss, @ButtonCss, @IsButtonCustomCss, @ErrorCss, @IsErrorCustomCss, @CloseCss, @IsCloseCustomCss, @ButtonName, @OnPageOrInPage, @AppearenceEffect, @PositionAlign, @AppearenceSpeed, @TopOrBottomPadding, @RightOrLeftPadding, @TimeDelay, @AppearSound, @ThankYouMessage, @HideEffect, @BackgroundPxOPer, @PlaceHolderClass, @BackgroundFadeColor, @AppearOnLoadOnExitOnScroll, @ShowOnScrollDownHeight, @AppearEffectOfFields, @GeneralCss, @GeneralParentCss, @OuterMainStyle, @RowListStyle, @RowStyle, @OuterLabelStyle, @OuterInputStyle, @OuterButtonStyle, @RadioButtonLabelCss, @RadioButtonFieldsCss, @CheckBoxLabelCss, @CheckBoxFieldsCss, @IsNewDivOrOldTable, @FormIdentifier, @IsMinimiseButton, @IsMinimiseCustomCss, @MinimiseCss, @EmbeddedFormOrPopUpFormOrTaggedForm, @CloseDesignType, @CloseAlignmentSetting, @ButtonPxOrPer,  @IsBackgroundUploadImageOrOnlineUrl, @BannerImageDesignCss, @IsBannerImageDesignCustom, @IsBannerImageHidden, @IsRadioCheckBoxFieldsCssCustom, @RadioCheckBoxFieldsCss, @ImageAppearanceAlignment, @AutoClose)";
            object? param = new { formDetails.Id, formDetails.UserGroupId, formDetails.UserInfoUserId, formDetails.FormCampaignId, formDetails.FormType, formDetails.FormPriority, formDetails.FormStatus, formDetails.Heading, formDetails.SubHeading, formDetails.PageContentForEmbed, formDetails.IsMainBackgroundDesignCustom, formDetails.MainBackgroundDesign, formDetails.TitleCss, formDetails.IsTitleCssCustom, formDetails.DescriptionCss, formDetails.IsDescriptionCustomCss, formDetails.LabelCss, formDetails.IsLabelCustomCss, formDetails.TextboxDropCss, formDetails.IsTextboxDropCustomCss, formDetails.ButtonCss, formDetails.IsButtonCustomCss, formDetails.ErrorCss, formDetails.IsErrorCustomCss, formDetails.CloseCss, formDetails.IsCloseCustomCss, formDetails.ButtonName, formDetails.OnPageOrInPage, formDetails.AppearenceEffect, formDetails.PositionAlign, formDetails.AppearenceSpeed, formDetails.TopOrBottomPadding, formDetails.RightOrLeftPadding, formDetails.TimeDelay, formDetails.AppearSound, formDetails.ThankYouMessage, formDetails.HideEffect, formDetails.BackgroundPxOPer, formDetails.PlaceHolderClass, formDetails.BackgroundFadeColor, formDetails.AppearOnLoadOnExitOnScroll, formDetails.ShowOnScrollDownHeight, formDetails.AppearEffectOfFields, formDetails.GeneralCss, formDetails.GeneralParentCss, formDetails.OuterMainStyle, formDetails.RowListStyle, formDetails.RowStyle, formDetails.OuterLabelStyle, formDetails.OuterInputStyle, formDetails.OuterButtonStyle, formDetails.RadioButtonLabelCss, formDetails.RadioButtonFieldsCss, formDetails.CheckBoxLabelCss, formDetails.CheckBoxFieldsCss, formDetails.IsNewDivOrOldTable, formDetails.FormIdentifier, formDetails.IsMinimiseButton, formDetails.IsMinimiseCustomCss, formDetails.MinimiseCss, formDetails.EmbeddedFormOrPopUpFormOrTaggedForm, formDetails.CloseDesignType, formDetails.CloseAlignmentSetting, formDetails.ButtonPxOrPer, formDetails.IsBackgroundUploadImageOrOnlineUrl, formDetails.BannerImageDesignCss, formDetails.IsBannerImageDesignCustom, formDetails.IsBannerImageHidden, formDetails.IsRadioCheckBoxFieldsCssCustom, formDetails.RadioCheckBoxFieldsCss, formDetails.ImageAppearanceAlignment, formDetails.AutoClose };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }
        public async Task<List<FormDetails>> GET(FormDetails formDetails, int OffSet, int FetchNext, string ListOfFormId, List<string> fieldName, bool IsEventForms, List<int> UserInfoUserIdList = null, int? IsSuperAdmin = null, Nullable<DateTime> FromDateTime = null, Nullable<DateTime> ToDateTime = null)
        {
            string UserInfoUserIdLists = (UserInfoUserIdList != null ? string.Join(",", UserInfoUserIdList) : null);
            short? AppearOnLoadOnExitOnScroll = formDetails.AppearOnLoadOnExitOnScroll == 0 ? null : formDetails.AppearOnLoadOnExitOnScroll;

            if (IsEventForms)
            {
                string storeProcCommand = "select *  from formdetails_geteventrulesformslist(@OffSet, @FetchNext, @Id, @AppearOnLoadOnExitOnScroll)";
                object? param = new { OffSet, FetchNext, formDetails.Id, AppearOnLoadOnExitOnScroll };

                using var db = GetDbConnection(connection.Connection);
                return (await db.QueryAsync<FormDetails>(storeProcCommand, param)).ToList();
            }
            else
            {
                string storeProcCommand = "select *  from formdetails_get(@UserInfoUserId, @Heading, @FormStatus, @FormType, @OnPageOrInPage, @OffSet, @FetchNext, @ListOfFormId, @fieldnames,  @IsWebOrMobileForm, @UserInfoUserIdLists, @IsSuperAdmin, @AppearOnLoadOnExitOnScroll , @FormIdentifier, @EmbeddedFormOrPopUpFormOrTaggedForm, @FromDateTime, @ToDateTime)";
                object? param = new { formDetails.UserInfoUserId, formDetails.Heading, formDetails.FormStatus, formDetails.FormType, formDetails.OnPageOrInPage, OffSet, FetchNext, ListOfFormId, fieldnames = "", formDetails.IsWebOrMobileForm, UserInfoUserIdLists, IsSuperAdmin, AppearOnLoadOnExitOnScroll, formDetails.FormIdentifier, formDetails.EmbeddedFormOrPopUpFormOrTaggedForm, FromDateTime, ToDateTime };

                using var db = GetDbConnection(connection.Connection);
                return (await db.QueryAsync<FormDetails>(storeProcCommand, param)).ToList();
            }
        }

        public async Task<FormDetails> GETDetails(FormDetails formDetails)
        {
            string storeProcCommand = "select *  from formdetails_getdetails(@Id, @Heading, @FormStatus, @FormType)";
            object? param = new { formDetails.Id, formDetails.Heading, formDetails.FormStatus, formDetails.FormType };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<FormDetails>(storeProcCommand, param);
        }

        public FormDetails? GETDetailss(FormDetails formDetails)
        {
            string storeProcCommand = "select *  from formdetails_getdetails(@Id, @Heading, @FormStatus, @FormType)";
            object? param = new { formDetails.Id, formDetails.Heading, formDetails.FormStatus, formDetails.FormType };

            using var db = GetDbConnection(connection.Connection);
            return db.QueryFirstOrDefault<FormDetails>(storeProcCommand, param);
        }

        public async Task<int> GetMaxCount(FormDetails formDetails, Nullable<DateTime> FromDateTime = null, Nullable<DateTime> ToDateTime = null)
        {
            string storeProcCommand = "select formdetails_maxcount(@UserInfoUserId, @Heading, @FormStatus, @FormType, @OnPageOrInPage, @IsWebOrMobileForm, @FormIdentifier, @EmbeddedFormOrPopUpFormOrTaggedForm, @FromDateTime, @ToDateTime)";
            object? param = new { formDetails.UserInfoUserId, formDetails.Heading, formDetails.FormStatus, formDetails.FormType, formDetails.OnPageOrInPage, formDetails.IsWebOrMobileForm, formDetails.FormIdentifier, formDetails.EmbeddedFormOrPopUpFormOrTaggedForm, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);
        }

        public async Task<bool> Delete(Int32 Id)
        {
            string storeProcCommand = "select * from form_details_delete(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }

        public async Task<bool> ToogleStatus(FormDetails formDetails)
        {
            string storeProcCommand = "select * from formdetails_tooglestatus(@Id,@FormStatus)";
            object? param = new { formDetails.Id, formDetails.FormStatus };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }

        public async Task<bool> GetStatus(int FormId)
        {
            string storeProcCommand = "select * from formdetails_getstatus(@FormId)";
            object? param = new { FormId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }

        public async Task<bool> ChangePriority(int Id, Int16 FormPriority)
        {
            string storeProcCommand = "select * from formdetails_updateprioritystatus(@Id, @FormPriority)";
            object? param = new { Id, FormPriority };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }

        public async Task<int> GetTopOneIdBasedOnFormType(int FormType)
        {
            string storeProcCommand = "select * from formdetails_gettoponeidbasedonformtype(@FormType)";
            object? param = new { FormType };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);
        }
        public async Task<bool> UpdateCampaignIdentifier(int Id, string ClientCampaignIdentifier, int CamapignId, string CampaignIdentiferName, bool IsOtpForm, int OTPFormId, bool IsWebOrMobileForm, int OTPGenerationLimits, bool OTPPageRestrictions, bool IsClickToCallForm, bool IsVerifiedEmail, bool IsAutoWhatsApp, string BlockEmailIds)
        {
            string storeProcCommand = "select * from formmdetails_updatecampaignidentifier(@Id, @ClientCampaignIdentifier, @CamapignId, @CampaignIdentiferName, @IsOtpForm, @OTPFormId, @IsWebOrMobileForm, @OTPGenerationLimits, @OTPPageRestrictions, @IsClickToCallForm, @IsVerifiedEmail, @IsAutoWhatsApp, @BlockEmailIds)";
            object? param = new { Id, ClientCampaignIdentifier, CamapignId, CampaignIdentiferName, IsOtpForm, OTPFormId, IsWebOrMobileForm, OTPGenerationLimits, OTPPageRestrictions, IsClickToCallForm, IsVerifiedEmail, IsAutoWhatsApp, BlockEmailIds };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }
        public async Task<List<FormDetails>> GetOTPForms(string FormType = null)
        {
            string storeProcCommand = "select * from formdetails_getotpforms(@FormType)";
            object? param = new { FormType };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<FormDetails>(storeProcCommand, param)).ToList();
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

