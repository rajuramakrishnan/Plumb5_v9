 
using System.Text; 
using P5GenralML;
using P5GenralDL; 
using Newtonsoft.Json;
using System.Reflection;
using IP5GenralDL; 
using System.ComponentModel;
using System.Data;
using System.Globalization;
using Microsoft.AspNetCore.Mvc; 
using Plumb5GenralFunction; 
using System.Text.RegularExpressions;

namespace Plumb5GenralFunction
{
    public class APIResponseSettingAction
    {
        int AdsId;
        string AccountName;
        string SqlProvider;

        public APIResponseSettingAction(int adsid,string _SqlProvider)
        {
            AdsId = adsid;
            SqlProvider = _SqlProvider;
        }

        public class LmsUserAssignCondDetails
        {
            public string contactfieldproperty { get; set; }
            public string contactfieldvalue { get; set; }
            public int userassignment { get; set; }
            public int individual { get; set; }
            public bool isgroupsticky { get; set; }
        }

        public class LmsConditionalDetails
        {
            public string contactfieldproperty { get; set; }
            public string contactfieldvalue { get; set; }
            public string lmssource { get; set; }
        }

        public class LmsGroupConditionalDetails
        {
            public string Dependencyfield { get; set; }
            public string Value { get; set; }
            public string AssignGroup { get; set; }
        }

        public class LmsStageConditionalDetails
        {
            public string Dependencyfield { get; set; }
            public string Value { get; set; }
            public string AssignStage { get; set; }
        }

        public class MailSMSWhatsAppOutConditionalJson
        {
            public string Dependencyfield { get; set; }
            public string Value { get; set; }
            public int SendingSettingId { get; set; }
            public bool IsRepeatCommunication { get; set; }
        }

        public class MLLmsCustomFieldsNew
        {
            public string LmsCustomField1 { get; set; }
            public string LmsCustomField2 { get; set; }
            public string LmsCustomField3 { get; set; }
            public string LmsCustomField4 { get; set; }
            public string LmsCustomField5 { get; set; }
            public string LmsCustomField6 { get; set; }
            public string LmsCustomField7 { get; set; }
            public string LmsCustomField8 { get; set; }
            public string LmsCustomField9 { get; set; }
            public string LmsCustomField10 { get; set; }
            public string LmsCustomField11 { get; set; }
            public string LmsCustomField12 { get; set; }
            public string LmsCustomField13 { get; set; }
            public string LmsCustomField14 { get; set; }
            public string LmsCustomField15 { get; set; }
        }

        public async void ResponseAction(Contact contact, string APIResponseName, string CallingType = null, bool? lmsgrpadded = null, string lmscustomfields = null, string publisher = null, string p5uniqueid = null)
        {
            Task.Run(async () =>
            {
                int lmsid = 0;
                int lmsgrpid = 0;
                int Score = 0;

                if (!string.IsNullOrEmpty(APIResponseName))
                {
                    ApiImportResponseSetting ftpImportSettingsdetails = null;
                    using (var objDL = DLApiImportResponseSetting.GetDLApiImportResponseSetting(AdsId, SqlProvider))
                        ftpImportSettingsdetails = await objDL.Get(APIResponseName);

                    if (ftpImportSettingsdetails != null && ftpImportSettingsdetails.Id > 0)
                    {
                        Account account = new Account();
                        LmsGroupMembers lmsgrpdetails = new LmsGroupMembers();

                        List<MLLmsCustomFieldsNew> mllmscustomfields = new List<MLLmsCustomFieldsNew>();
                        MLLmsCustomFieldsNew eachlmsobject = new MLLmsCustomFieldsNew();

                        if (!string.IsNullOrEmpty(lmscustomfields))
                        {
                            mllmscustomfields = JsonConvert.DeserializeObject<List<MLLmsCustomFieldsNew>>(lmscustomfields);

                            if (mllmscustomfields != null && mllmscustomfields.Count() > 0)
                                eachlmsobject = mllmscustomfields[0];
                        }

                        using (var objBLAccount = DLAccount.GetDLAccount(SqlProvider))
                            account = await objBLAccount.GetAccountDetails(AdsId);

                        if (account != null && account.AccountId > 0)
                            AccountName = account.AccountName;

                        if (!string.IsNullOrEmpty(ftpImportSettingsdetails.ReportToDetailsByMail))
                            ReportThroughMail(ftpImportSettingsdetails.ReportToDetailsByMail, ftpImportSettingsdetails.Name, contact.Name, contact.EmailId, contact.PhoneNumber);

                        if (!string.IsNullOrEmpty(ftpImportSettingsdetails.ReportToDetailsBySMS))
                            ReportThroughSMS(ftpImportSettingsdetails.ReportToDetailsBySMS, ftpImportSettingsdetails.Name, contact.Name, contact.EmailId, contact.PhoneNumber);

                        if (!string.IsNullOrEmpty(ftpImportSettingsdetails.ReportToDetailsByWhatsApp))
                            ReportThroughWhatsApp(ftpImportSettingsdetails.ReportToDetailsByWhatsApp, ftpImportSettingsdetails.Name, contact.Name, contact.EmailId, contact.PhoneNumber);

                        if (ftpImportSettingsdetails.AssignToGroupId > 0 && string.IsNullOrEmpty(ftpImportSettingsdetails.AssignToGroupConditonalJson))
                        {
                            AutoAssignToGroup(ftpImportSettingsdetails.AssignToGroupId, contact.ContactId);
                        }
                        else if (ftpImportSettingsdetails.AssignToGroupId <= 0 && !string.IsNullOrEmpty(ftpImportSettingsdetails.AssignToGroupConditonalJson))
                        {
                            try
                            {
                                List<LmsGroupConditionalDetails> lmsgrpjsondetails = new List<LmsGroupConditionalDetails>();
                                lmsgrpjsondetails = JsonConvert.DeserializeObject<List<LmsGroupConditionalDetails>>(ftpImportSettingsdetails.AssignToGroupConditonalJson);

                                if (lmsgrpjsondetails != null && lmsgrpjsondetails.Count() > 0)
                                {
                                    for (int i = 0; i < lmsgrpjsondetails.Count(); i++)
                                    {
                                        string key = lmsgrpjsondetails[i].Dependencyfield;
                                        string value = lmsgrpjsondetails[i].Value;
                                        int AssignGroupId = Convert.ToInt32(lmsgrpjsondetails[i].AssignGroup);

                                        string answervalue = string.Empty;

                                        try
                                        {
                                            if (eachlmsobject != null && key.ToLower().IndexOf("lmscustomfield") > -1)
                                            {
                                                if (eachlmsobject.GetType().GetProperty(key, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(eachlmsobject, null) != null)
                                                    answervalue = eachlmsobject.GetType().GetProperty(key).GetValue(eachlmsobject, null).ToString();
                                            }
                                            else
                                            {
                                                if (contact.GetType().GetProperty(key, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(contact, null) != null)
                                                    answervalue = contact.GetType().GetProperty(key).GetValue(contact, null).ToString();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Conditional==>GroupChecking ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction Conditional GroupChecking", DateTime.Now, "APIResponseSettingAction-->Conditional-->GroupChecking", ex.ToString());
                                        }

                                        if (!string.IsNullOrEmpty(answervalue))
                                        {
                                            if (answervalue == value)
                                            {
                                                try
                                                {
                                                    AutoAssignToGroup(AssignGroupId, contact.ContactId);
                                                }
                                                catch (Exception ex)
                                                {
                                                    ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Conditional Assignment Of Group ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in Conditional Assignment Of Group", DateTime.Now, "APIResponseSettingAction-->Conditional Assignment Of Group", ex.ToString());
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Conditional Assignment of Group ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in Conditional Assignment Group", DateTime.Now, "APIResponseSettingAction-->Conditional Assignment Group", ex.ToString());
                            }
                        }

                        if (!string.IsNullOrEmpty(ftpImportSettingsdetails.WebHookId))
                        {
                            if (ftpImportSettingsdetails.WebHookId.Contains(","))
                            {
                                string[] webhooids = ftpImportSettingsdetails.WebHookId.Split(',');

                                if (webhooids != null && webhooids.Count() > 0)
                                {
                                    for (int i = 0; i < webhooids.Count(); i++)
                                        SendWebHook(Convert.ToInt32(webhooids[i]), contact );
                                }
                            }
                            else
                            {
                                SendWebHook(Convert.ToInt32(ftpImportSettingsdetails.WebHookId), contact );
                            }
                        }

                        //Unconditonal Assignment Json
                        if (!ftpImportSettingsdetails.IsUserAssignmentConditional)
                        {
                            if (ftpImportSettingsdetails.AssignLeadToUserInfoUserId > 0)
                            {
                                AssignLeadToUserInfoUserId(ftpImportSettingsdetails.AssignLeadToUserInfoUserId, ftpImportSettingsdetails.IsOverrideAssignment, contact );
                            }
                            else if (ftpImportSettingsdetails.AssignUserGroupId > 0 && contact != null)
                            {
                                List<int> userinfolist = null;

                                using (var objDL = DLLmsGroupMembers.GetDLLmsGroupMembers(AdsId, SqlProvider))
                                    userinfolist = (await objDL.LmsGrpGetUserInfoList(contact.ContactId)).ToList();

                                if (userinfolist.Count() <= 0)
                                {
                                    try
                                    {
                                        List<MLUserHierarchy> userHierarchy = null;
                                        var objBL = DLContact.GetContactDetails(AdsId, SqlProvider);

                                        UserGroupAssignment usergrpassigment = new UserGroupAssignment();
                                        var objusrgrpassign = DLUserGroupAssignment.GetDLUserGroupAssignment(AdsId, SqlProvider);

                                        using (var objUserGroup = DLUserGroupMembers.GetDLUserGroupMembers(SqlProvider))
                                        {
                                            userHierarchy =   (await objUserGroup.GetHisUsers(ftpImportSettingsdetails.AssignUserGroupId)).Where(x => x.ActiveStatus).OrderBy(x => x.UserInfoUserId).ToList();
                                            if (userHierarchy != null && userHierarchy.Count > 0)
                                            {
                                                usergrpassigment = await objusrgrpassign.GetDetails(ftpImportSettingsdetails.Id, "API", ftpImportSettingsdetails.AssignUserGroupId);

                                                int LastAssignUserInfoUserId = 0;

                                                if (usergrpassigment != null && usergrpassigment.Id > 0)
                                                    LastAssignUserInfoUserId = usergrpassigment.LastAssignUserInfoUserId;

                                                MLUserHierarchy individualUser = null;

                                                individualUser = userHierarchy.FirstOrDefault(x => x.UserInfoUserId > LastAssignUserInfoUserId);

                                                if (individualUser != null && individualUser.UserInfoUserId > 0 && individualUser.UserInfoUserId != contact.UserInfoUserId)
                                                {
                                                    using (var objUpdateLastAssinged =   DLApiImportResponseSetting.GetDLApiImportResponseSetting(AdsId,SqlProvider))
                                                    {
                                                        if (contact != null)
                                                        {
                                                            string userassignmentvalues = "";

                                                            if (usergrpassigment == null)
                                                                userassignmentvalues = Convert.ToString(individualUser.UserInfoUserId);
                                                            else
                                                                userassignmentvalues = usergrpassigment.UserAssignedValues + "," + individualUser.UserInfoUserId;

                                                            if (await objusrgrpassign.SaveOrUpdate(ftpImportSettingsdetails.Id, "API", ftpImportSettingsdetails.AssignUserGroupId, individualUser.UserInfoUserId, userassignmentvalues, usergrpassigment == null ? 0 : usergrpassigment.Id) > 0)
                                                                contact.UserInfoUserId = individualUser.UserInfoUserId;

                                                            if (ftpImportSettingsdetails.IsOverrideAssignment == 0)
                                                            {
                                                                if (await objUpdateLastAssinged.UpdateLastAssigUserIdNotificationToSales(ftpImportSettingsdetails.Id, individualUser.UserInfoUserId))
                                                                    contact.UserInfoUserId = individualUser.UserInfoUserId;
                                                            }
                                                            else if (ftpImportSettingsdetails.IsOverrideAssignment == 1)
                                                            {
                                                                if (await objUpdateLastAssinged.UpdateLastAssigUserIdNotificationToSales(ftpImportSettingsdetails.Id, individualUser.UserInfoUserId))
                                                                    contact.UserInfoUserId = individualUser.UserInfoUserId;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    individualUser = userHierarchy.First();
                                                    if (individualUser != null && individualUser.UserInfoUserId > 0 && individualUser.UserInfoUserId != contact.UserInfoUserId)
                                                    {
                                                        using (var objUpdateLastAssinged =   DLApiImportResponseSetting.GetDLApiImportResponseSetting(AdsId,SqlProvider))
                                                        {
                                                            if (contact != null)
                                                            {
                                                                string userassignmentvalues = "";

                                                                if (usergrpassigment == null)
                                                                    userassignmentvalues = Convert.ToString(individualUser.UserInfoUserId);
                                                                else
                                                                    userassignmentvalues = usergrpassigment.UserAssignedValues + "," + individualUser.UserInfoUserId;

                                                                if (await objusrgrpassign.SaveOrUpdate(ftpImportSettingsdetails.Id, "API", ftpImportSettingsdetails.AssignUserGroupId, individualUser.UserInfoUserId, userassignmentvalues, usergrpassigment == null ? 0 : usergrpassigment.Id) > 0)
                                                                    contact.UserInfoUserId = individualUser.UserInfoUserId;

                                                                if (ftpImportSettingsdetails.IsOverrideAssignment == 0)
                                                                {
                                                                    if (await objUpdateLastAssinged.UpdateLastAssigUserIdNotificationToSales(ftpImportSettingsdetails.Id, individualUser.UserInfoUserId))
                                                                        contact.UserInfoUserId = individualUser.UserInfoUserId;
                                                                }
                                                                else if (ftpImportSettingsdetails.IsOverrideAssignment == 1)
                                                                {
                                                                    if (await objUpdateLastAssinged.UpdateLastAssigUserIdNotificationToSales(ftpImportSettingsdetails.Id, individualUser.UserInfoUserId))
                                                                        contact.UserInfoUserId = individualUser.UserInfoUserId;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Account accountDetails = new Account();
                                                using (var objDLAccount =  DLAccount.GetDLAccount(SqlProvider))
                                                    accountDetails = await objDLAccount.GetAccountDetails(AdsId);

                                                if (accountDetails != null && accountDetails.AccountId > 0)
                                                    contact.UserInfoUserId = accountDetails.UserInfoUserId;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        using (ErrorUpdation objError = new ErrorUpdation("API_AssignUserGroupId_Unconditonal"))
                                            objError.AddError(ex.Message.ToString(), "", DateTime.Now.ToString(), "P5API->ContactController->API_AssignUserGroupId_Unconditonal", ex.ToString(), true);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        //this is for repeat contact checking
                                        //this code is for group sticky assign the contact.
                                        if (ftpImportSettingsdetails.IsUnConditionalGroupSticky)
                                                {
                                                    List<MLUserHierarchy> userHierarchy = null;
                                                    List<int> userinfohierarchylist = new List<int>();
                                                    List<int> finalist = null;

                                                    UserGroupAssignment usergrpassigment = new UserGroupAssignment();
                                                    var objusrgrpassign =   DLUserGroupAssignment.GetDLUserGroupAssignment(AdsId,SqlProvider);

                                                    usergrpassigment = await objusrgrpassign.GetDetails(ftpImportSettingsdetails.Id, "API", ftpImportSettingsdetails.AssignUserGroupId);

                                                    using (var objUserGroup =  DLUserGroupMembers.GetDLUserGroupMembers(SqlProvider))
                                                    {
                                                        userHierarchy = (await objUserGroup.GetHisUsers(ftpImportSettingsdetails.AssignUserGroupId)).Where(x => x.ActiveStatus).OrderBy(x => x.UserInfoUserId).ToList();

                                                        if (userHierarchy != null && userHierarchy.Count > 0)
                                                            userinfohierarchylist = userHierarchy.Select(x => x.UserInfoUserId).Distinct().ToList();

                                                        if (userinfohierarchylist != null && userinfohierarchylist.Count > 0 && userinfolist != null && userinfolist.Count > 0)
                                                            finalist = userinfolist.Intersect(userinfohierarchylist).ToList();

                                                        if (finalist != null && finalist.Count > 0)
                                                        {
                                                            int UserInfoUserId = finalist.FirstOrDefault();

                                                            string userassignmentvalues = "";

                                                            if (usergrpassigment == null)
                                                                userassignmentvalues = Convert.ToString(UserInfoUserId);
                                                            else
                                                                userassignmentvalues = usergrpassigment.UserAssignedValues + "," + UserInfoUserId;

                                                            if (await objusrgrpassign.SaveOrUpdate(ftpImportSettingsdetails.Id, "API", ftpImportSettingsdetails.AssignUserGroupId, UserInfoUserId, userassignmentvalues, usergrpassigment == null ? 0 : usergrpassigment.Id) > 0)
                                                                contact.UserInfoUserId = UserInfoUserId;
                                                        }
                                                        else
                                                        {
                                                            try
                                                            {
                                                                var objBL =   DLContact.GetContactDetails(AdsId,SqlProvider);

                                                                if (userHierarchy != null && userHierarchy.Count > 0)
                                                                {
                                                                    int LastAssignUserInfoUserId = 0;

                                                                    if (usergrpassigment != null && usergrpassigment.Id > 0)
                                                                        LastAssignUserInfoUserId = usergrpassigment.LastAssignUserInfoUserId;

                                                                    MLUserHierarchy individualUser = null;

                                                                    individualUser = userHierarchy.FirstOrDefault(x => x.UserInfoUserId > LastAssignUserInfoUserId);

                                                                    if (individualUser != null && individualUser.UserInfoUserId > 0 && individualUser.UserInfoUserId != contact.UserInfoUserId)
                                                                    {
                                                                        using (var objUpdateLastAssinged =   DLApiImportResponseSetting.GetDLApiImportResponseSetting(AdsId,SqlProvider))
                                                                        {
                                                                            if (contact != null)
                                                                            {
                                                                                string userassignmentvalues = "";

                                                                                if (usergrpassigment == null)
                                                                                    userassignmentvalues = Convert.ToString(individualUser.UserInfoUserId);
                                                                                else
                                                                                    userassignmentvalues = usergrpassigment.UserAssignedValues + "," + individualUser.UserInfoUserId;

                                                                                if (await objusrgrpassign.SaveOrUpdate(ftpImportSettingsdetails.Id, "API", ftpImportSettingsdetails.AssignUserGroupId, individualUser.UserInfoUserId, userassignmentvalues, usergrpassigment == null ? 0 : usergrpassigment.Id) > 0)
                                                                                    contact.UserInfoUserId = individualUser.UserInfoUserId;
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        individualUser = userHierarchy.First();
                                                                        if (individualUser != null && individualUser.UserInfoUserId > 0 && individualUser.UserInfoUserId != contact.UserInfoUserId)
                                                                        {
                                                                            using (var objUpdateLastAssinged =   DLApiImportResponseSetting.GetDLApiImportResponseSetting(AdsId,SqlProvider))
                                                                            {
                                                                                if (contact != null)
                                                                                {
                                                                                    string userassignmentvalues = "";

                                                                                    if (usergrpassigment == null)
                                                                                        userassignmentvalues = Convert.ToString(individualUser.UserInfoUserId);
                                                                                    else
                                                                                        userassignmentvalues = usergrpassigment.UserAssignedValues + "," + individualUser.UserInfoUserId;

                                                                                    if (await objusrgrpassign.SaveOrUpdate(ftpImportSettingsdetails.Id, "API", ftpImportSettingsdetails.AssignUserGroupId, individualUser.UserInfoUserId, userassignmentvalues, usergrpassigment == null ? 0 : usergrpassigment.Id) > 0)
                                                                                        contact.UserInfoUserId = individualUser.UserInfoUserId;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    Account accountDetails = new Account();
                                                                    using (var objDLAccount =   DLAccount.GetDLAccount(SqlProvider))
                                                                        accountDetails =await  objDLAccount.GetAccountDetails(AdsId);

                                                                    if (accountDetails != null && accountDetails.AccountId > 0)
                                                                        contact.UserInfoUserId = accountDetails.UserInfoUserId;
                                                                }

                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                using (ErrorUpdation objError = new ErrorUpdation("API_AssignUserGroupId_Unconditonal"))
                                                                    objError.AddError(ex.Message.ToString(), "", DateTime.Now.ToString(), "P5API->ContactController->API_AssignUserGroupId_Unconditonal", ex.ToString(), true);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    try
                                                    {
                                                        List<MLUserHierarchy> userHierarchy = null;
                                                        var objDL =   DLContact.GetContactDetails(AdsId,SqlProvider);

                                                        UserGroupAssignment usergrpassigment = new UserGroupAssignment();
                                                        var objusrgrpassign =   DLUserGroupAssignment.GetDLUserGroupAssignment(AdsId,SqlProvider);

                                                        using (var objUserGroup =    DLUserGroupMembers.GetDLUserGroupMembers(SqlProvider))
                                                        {
                                                            userHierarchy = (await objUserGroup.GetHisUsers(ftpImportSettingsdetails.AssignUserGroupId)).Where(x => x.ActiveStatus).OrderBy(x => x.UserInfoUserId).ToList();
                                                            if (userHierarchy != null && userHierarchy.Count > 0)
                                                            {
                                                                usergrpassigment = await objusrgrpassign.GetDetails(ftpImportSettingsdetails.Id, "API", ftpImportSettingsdetails.AssignUserGroupId);

                                                                int LastAssignUserInfoUserId = 0;

                                                                if (usergrpassigment != null && usergrpassigment.Id > 0)
                                                                    LastAssignUserInfoUserId = usergrpassigment.LastAssignUserInfoUserId;

                                                                MLUserHierarchy individualUser = null;

                                                                individualUser = userHierarchy.FirstOrDefault(x => x.UserInfoUserId > LastAssignUserInfoUserId);

                                                                if (individualUser != null && individualUser.UserInfoUserId > 0 && individualUser.UserInfoUserId != contact.UserInfoUserId)
                                                                {
                                                                    using (var objUpdateLastAssinged =   DLApiImportResponseSetting.GetDLApiImportResponseSetting(AdsId,SqlProvider))
                                                                    {
                                                                        if (contact != null)
                                                                        {
                                                                            string userassignmentvalues = "";

                                                                            if (usergrpassigment == null)
                                                                                userassignmentvalues = Convert.ToString(individualUser.UserInfoUserId);
                                                                            else
                                                                                userassignmentvalues = usergrpassigment.UserAssignedValues + "," + individualUser.UserInfoUserId;

                                                                            if (await objusrgrpassign.SaveOrUpdate(ftpImportSettingsdetails.Id, "API", ftpImportSettingsdetails.AssignUserGroupId, individualUser.UserInfoUserId, userassignmentvalues, usergrpassigment == null ? 0 : usergrpassigment.Id) > 0)
                                                                                contact.UserInfoUserId = individualUser.UserInfoUserId;

                                                                            if (ftpImportSettingsdetails.IsOverrideAssignment == 0)
                                                                            {
                                                                                if (await objUpdateLastAssinged.UpdateLastAssigUserIdNotificationToSales(ftpImportSettingsdetails.Id, individualUser.UserInfoUserId))
                                                                                    contact.UserInfoUserId = individualUser.UserInfoUserId;
                                                                            }
                                                                            else if (ftpImportSettingsdetails.IsOverrideAssignment == 1)
                                                                            {
                                                                                if (await objUpdateLastAssinged.UpdateLastAssigUserIdNotificationToSales(ftpImportSettingsdetails.Id, individualUser.UserInfoUserId))
                                                                                    contact.UserInfoUserId = individualUser.UserInfoUserId;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    individualUser = userHierarchy.First();
                                                                    if (individualUser != null && individualUser.UserInfoUserId > 0 && individualUser.UserInfoUserId != contact.UserInfoUserId)
                                                                    {
                                                                        using (var objUpdateLastAssinged =   DLApiImportResponseSetting.GetDLApiImportResponseSetting(AdsId,SqlProvider))
                                                                        {
                                                                            if (contact != null)
                                                                            {
                                                                                string userassignmentvalues = "";

                                                                                if (usergrpassigment == null)
                                                                                    userassignmentvalues = Convert.ToString(individualUser.UserInfoUserId);
                                                                                else
                                                                                    userassignmentvalues = usergrpassigment.UserAssignedValues + "," + individualUser.UserInfoUserId;

                                                                                if (await objusrgrpassign.SaveOrUpdate(ftpImportSettingsdetails.Id, "API", ftpImportSettingsdetails.AssignUserGroupId, individualUser.UserInfoUserId, userassignmentvalues, usergrpassigment == null ? 0 : usergrpassigment.Id) > 0)
                                                                                    contact.UserInfoUserId = individualUser.UserInfoUserId;

                                                                                if (ftpImportSettingsdetails.IsOverrideAssignment == 0)
                                                                                {
                                                                                    if (await objUpdateLastAssinged.UpdateLastAssigUserIdNotificationToSales(ftpImportSettingsdetails.Id, individualUser.UserInfoUserId))
                                                                                        contact.UserInfoUserId = individualUser.UserInfoUserId;
                                                                                }
                                                                                else if (ftpImportSettingsdetails.IsOverrideAssignment == 1)
                                                                                {
                                                                                    if (await objUpdateLastAssinged.UpdateLastAssigUserIdNotificationToSales(ftpImportSettingsdetails.Id, individualUser.UserInfoUserId))
                                                                                        contact.UserInfoUserId = individualUser.UserInfoUserId;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Account accountDetails = new Account();
                                                                using (var objDLAccount =   DLAccount.GetDLAccount(SqlProvider))
                                                                    accountDetails =await  objDLAccount.GetAccountDetails(AdsId);

                                                                if (accountDetails != null && accountDetails.AccountId > 0)
                                                                    contact.UserInfoUserId = accountDetails.UserInfoUserId;
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        using (ErrorUpdation objError = new ErrorUpdation("API_AssignUserGroupId_Unconditonal"))
                                                            objError.AddError(ex.Message.ToString(), "", DateTime.Now.ToString(), "P5API->ContactController->API_AssignUserGroupId_Unconditonal", ex.ToString(), true);
                                                    }
                                                }
                                    }
                                    catch (Exception ex)
                                    {
                                        using (ErrorUpdation objError = new ErrorUpdation("API_AssignUserGroupId_Unconditonal_IsUnConditionalGroupSticky"))
                                            objError.AddError(ex.Message.ToString(), "", DateTime.Now.ToString(), "P5API->ContactController->API_AssignUserGroupId_Unconditonal_IsUnConditionalGroupSticky", ex.ToString(), true);
                                    }
                                }
                            }
                        }
                        else if (ftpImportSettingsdetails.IsUserAssignmentConditional)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(ftpImportSettingsdetails.UserAssigmentJson))
                                {
                                    List<LmsUserAssignCondDetails> lmsjsondetails = new List<LmsUserAssignCondDetails>();
                                    lmsjsondetails = JsonConvert.DeserializeObject<List<LmsUserAssignCondDetails>>(ftpImportSettingsdetails.UserAssigmentJson);

                                    if (lmsjsondetails != null && lmsjsondetails.Count() > 0 && contact != null)
                                    {
                                        for (int i = 0; i < lmsjsondetails.Count(); i++)
                                        {
                                            string key = lmsjsondetails[i].contactfieldproperty;
                                            string value = lmsjsondetails[i].contactfieldvalue;
                                            int userassignid = lmsjsondetails[i].userassignment;
                                            int individual = lmsjsondetails[i].individual;
                                            bool isgroupsticky = lmsjsondetails[i].isgroupsticky;

                                            string answervalue = string.Empty;

                                            try
                                            {
                                                if (eachlmsobject != null && key.ToLower().IndexOf("lmscustomfield") > -1)
                                                {
                                                    if (eachlmsobject.GetType().GetProperty(key, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(eachlmsobject, null) != null)
                                                        answervalue = eachlmsobject.GetType().GetProperty(key).GetValue(eachlmsobject, null).ToString();
                                                }
                                                else
                                                {
                                                    if (contact.GetType().GetProperty(key, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(contact, null) != null)
                                                        answervalue = contact.GetType().GetProperty(key).GetValue(contact, null).ToString();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorUpdation.AddErrorLog("APIResponseSettingAction==>LmsCheckcing ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction LmsCheckcing", DateTime.Now, "APIResponseSettingAction-->LmsCheckcing", ex.ToString());
                                            }

                                            if (!string.IsNullOrEmpty(answervalue))
                                            {
                                                if (answervalue == value)
                                                {
                                                    try
                                                    {
                                                        contact.UserInfoUserId = await ConditionalUserAssignment(individual, userassignid, contact, ftpImportSettingsdetails, isgroupsticky);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Conditional Assignment Of Users ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in Conditional Assignment Of Users", DateTime.Now, "APIResponseSettingAction-->Conditional Assignment Of Users", ex.ToString());
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Conditional Assignment ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in Conditional Assignment", DateTime.Now, "APIResponseSettingAction-->Conditional Assignment", ex.ToString());
                            }
                        }

                        if (ftpImportSettingsdetails.AssignStage > -1 && string.IsNullOrEmpty(ftpImportSettingsdetails.AssignStageConditonalJson))
                        {
                            Score = ftpImportSettingsdetails.AssignStage;
                        }
                        else if (ftpImportSettingsdetails.AssignStage <= 0 && !string.IsNullOrEmpty(ftpImportSettingsdetails.AssignStageConditonalJson))
                        {
                            try
                            {
                                List<LmsStageConditionalDetails> lmsstagejsondetails = new List<LmsStageConditionalDetails>();
                                lmsstagejsondetails = JsonConvert.DeserializeObject<List<LmsStageConditionalDetails>>(ftpImportSettingsdetails.AssignStageConditonalJson);

                                if (lmsstagejsondetails != null && lmsstagejsondetails.Count() > 0)
                                {
                                    for (int i = 0; i < lmsstagejsondetails.Count(); i++)
                                    {
                                        string key = lmsstagejsondetails[i].Dependencyfield;
                                        string value = lmsstagejsondetails[i].Value;
                                        int AssignScore = Convert.ToInt32(lmsstagejsondetails[i].AssignStage);

                                        string answervalue = string.Empty;

                                        try
                                        {
                                            if (eachlmsobject != null && key.ToLower().IndexOf("lmscustomfield") > -1)
                                            {
                                                if (eachlmsobject.GetType().GetProperty(key, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(eachlmsobject, null) != null)
                                                    answervalue = eachlmsobject.GetType().GetProperty(key).GetValue(eachlmsobject, null).ToString();
                                            }
                                            else
                                            {
                                                if (contact.GetType().GetProperty(key, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(contact, null) != null)
                                                    answervalue = contact.GetType().GetProperty(key).GetValue(contact, null).ToString();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            ErrorUpdation.AddErrorLog("APIResponseSettingAction==>LmsStageChecking ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction LmsStageChecking", DateTime.Now, "APIResponseSettingAction-->LmsStageChecking", ex.ToString());
                                        }

                                        if (!string.IsNullOrEmpty(answervalue))
                                        {
                                            if (answervalue == value)
                                            {
                                                try
                                                {
                                                    Score = AssignScore;
                                                }
                                                catch (Exception ex)
                                                {
                                                    ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Conditional Assignment Of Stage ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in Conditional Assignment Of Stage", DateTime.Now, "APIResponseSettingAction-->Conditional Assignment Of Stage", ex.ToString());
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Conditional Assignment of Stage ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in Conditional Assignment Stage", DateTime.Now, "APIResponseSettingAction-->Conditional Assignment Stage", ex.ToString());
                            }
                        }

                        if (ftpImportSettingsdetails.IsVerifiedEmail)
                        {
                            try
                            {
                                EmailVerifyProviderSetting emailVerifyProviderSetting = null;
                                using (var objDL =   DLEmailVerifyProviderSetting.GetDLEmailVerifyProviderSetting(AdsId,SqlProvider))
                                    emailVerifyProviderSetting = await objDL.GetActiveprovider();

                                if (emailVerifyProviderSetting != null)
                                {
                                    Contact verifycontact = new Contact { ContactId = contact.ContactId };
                                    List<string> fieldName = new List<string> { "ContactId", "EmailId", "IsVerifiedMailId" };
                                    using (var objDL =   DLContact.GetContactDetails(AdsId,SqlProvider))
                                    {
                                        verifycontact = await objDL.GetContactDetails(verifycontact, fieldName);
                                        if (contact != null && !String.IsNullOrEmpty(verifycontact.EmailId))
                                        {
                                            List<Contact> contacts = new List<Contact>();
                                            contacts.Add(verifycontact);
                                            IBulkVerifyEmailContact EmailVerifierGeneralBaseFactory = Plumb5GenralFunction.EmailVerifyGeneralBaseFactory.GetEmailVerifierVendor(AdsId, emailVerifyProviderSetting);
                                            EmailVerifierGeneralBaseFactory.VerifyBulkContact(contacts);

                                            if (EmailVerifierGeneralBaseFactory.VendorResponses != null && EmailVerifierGeneralBaseFactory.VendorResponses.Count > 0)
                                            {
                                                if (EmailVerifierGeneralBaseFactory.VendorResponses[0].IsVerifiedMailId != null && EmailVerifierGeneralBaseFactory.VendorResponses[0].IsVerifiedMailId != -1)
                                                {
                                                    int IsVerifiedMailId = Convert.ToInt32(EmailVerifierGeneralBaseFactory.VendorResponses[0].IsVerifiedMailId);
                                                    await objDL.MakeItNotVerified(contact.ContactId, IsVerifiedMailId);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                using (ErrorUpdation objError = new ErrorUpdation("IsVerifiedEmail"))
                                    objError.AddError(ex.Message.ToString(), "", DateTime.Now.ToString(), "P5API->ContactController->IsVerifiedEmail", ex.ToString(), true);
                            }
                        }

                        if (!Convert.ToBoolean(lmsgrpadded))
                        {
                            LmsUpdateStageNotification objlmsassignemail = new LmsUpdateStageNotification();
                            if (ftpImportSettingsdetails.IsConditional && ftpImportSettingsdetails.IsOverRideSource <= 0)
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(ftpImportSettingsdetails.ConditionalJson))
                                    {
                                        List<LmsConditionalDetails> lmsjsondetails = new List<LmsConditionalDetails>();
                                        lmsjsondetails = JsonConvert.DeserializeObject<List<LmsConditionalDetails>>(ftpImportSettingsdetails.ConditionalJson);

                                        if (lmsjsondetails != null && lmsjsondetails.Count() > 0)
                                        {
                                            for (int i = 0; i < lmsjsondetails.Count(); i++)
                                            {
                                                string key = lmsjsondetails[i].contactfieldproperty;
                                                string value = lmsjsondetails[i].contactfieldvalue;
                                                int lmsgroupId = Convert.ToInt32(lmsjsondetails[i].lmssource);

                                                string answervalue = string.Empty;

                                                try
                                                {
                                                    if (eachlmsobject != null && key.ToLower().IndexOf("lmscustomfield") > -1)
                                                    {
                                                        if (eachlmsobject.GetType().GetProperty(key, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(eachlmsobject, null) != null)
                                                            answervalue = eachlmsobject.GetType().GetProperty(key).GetValue(eachlmsobject, null).ToString();
                                                    }
                                                    else
                                                    {
                                                        if (contact.GetType().GetProperty(key, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(contact, null) != null)
                                                            answervalue = contact.GetType().GetProperty(key).GetValue(contact, null).ToString();
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    ErrorUpdation.AddErrorLog("APIResponseSettingAction==>LmsCheckcingGrp ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction LmsCheckcingGrp", DateTime.Now, "APIResponseSettingAction-->LmsCheckcingGrp", ex.ToString());
                                                }

                                                if (!string.IsNullOrEmpty(answervalue))
                                                {
                                                    if (answervalue == value)
                                                    {
                                                        int olduserinfouserid;
                                                        using (var objDL =   DLLmsGroupMembers.GetDLLmsGroupMembers(AdsId,SqlProvider))
                                                            olduserinfouserid = await objDL.CheckLmsgrpUserId(contact.ContactId, lmsgroupId);

                                                        if (olduserinfouserid > 0)
                                                        {
                                                            contact.UserInfoUserId = olduserinfouserid;
                                                            using (var objUpdateLastAssinged =   DLApiImportResponseSetting.GetDLApiImportResponseSetting(AdsId,SqlProvider))
                                                               await objUpdateLastAssinged.UpdateLastAssigUserIdNotificationToSales(ftpImportSettingsdetails.Id, olduserinfouserid);
                                                        }

                                                        try
                                                        {
                                                            lmsgrpid = lmsgroupId;
                                                            using (var objDL =   DLLmsGroupMembers.GetDLLmsGroupMembers(AdsId,SqlProvider))
                                                                lmsid = await objDL.CheckAndSaveLmsGroup(contact.ContactId, contact.UserInfoUserId, lmsgroupId, ftpImportSettingsdetails.SourceType, lmscustomfields, 0, Score, null, publisher);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Conditional Lms Group Members Adding ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in Conditional To Lms Group Members", DateTime.Now, "APIResponseSettingAction-->Conditional To Lms Group Members", ex.ToString());
                                                        }

                                                        try
                                                        {
                                                            //this is for sending mail to assignee
                                                            if (contact.UserInfoUserId > 0 && lmsid > 0)
                                                                    await objlmsassignemail.AssignForForms(AdsId, contact.ContactId, contact.UserInfoUserId, lmsid);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ErrorUpdation.AddErrorLog("APIResponseSettingAction==>sending mail to assignee ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in sending mail to assignee", DateTime.Now, "APIResponseSettingAction-->sending mail to assignee", ex.ToString());
                                                        }

                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Conditional LmsGroup ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in Conditional LmsGroup", DateTime.Now, "APIResponseSettingAction-->Conditional LmsGroup", ex.ToString());
                                }
                            }
                            else if (!ftpImportSettingsdetails.IsConditional && ftpImportSettingsdetails.IsOverRideSource > 0)
                            {
                                lmsgrpid = ftpImportSettingsdetails.IsOverRideSource;
                                try
                                {
                                    using (var objDLGrpMembers =   DLLmsGroupMembers.GetDLLmsGroupMembers(AdsId,SqlProvider))
                                        lmsid = await objDLGrpMembers.CheckAndSaveLmsGroup(contact.ContactId, contact.UserInfoUserId, ftpImportSettingsdetails.IsOverRideSource, ftpImportSettingsdetails.SourceType, lmscustomfields, 0, Score, null, publisher);
                                }
                                catch (Exception ex)
                                {
                                    ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Unconditional Lms Group Assignment ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in Unconditional To Lms Group Members", DateTime.Now, "APIResponseSettingAction-->Unconditional To Lms Group Members", ex.ToString());
                                }

                                try
                                {
                                    //this is for sending mail to assignee
                                    if (contact.UserInfoUserId > 0 && lmsid > 0)
                                            await objlmsassignemail.AssignForForms(AdsId, contact.ContactId, contact.UserInfoUserId, lmsid);
                                }
                                catch (Exception ex)
                                {
                                    ErrorUpdation.AddErrorLog("APIResponseSettingAction==>sending mail to assignee ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in sending mail to assignee", DateTime.Now, "APIResponseSettingAction-->sending mail to assignee", ex.ToString());
                                }
                            }
                        }

                        using (var objlms =   DLLmsGroupMembers.GetDLLmsGroupMembers(AdsId,SqlProvider))
                            lmsgrpdetails = await objlms.GetLmsDetails(lmsid, contact.ContactId);

                        if (lmsid > 0)
                        {
                            using (var obj =   DLApiImportResponseSettingLogs.GetDLApiImportResponseSettingLogs(AdsId,SqlProvider))
                                await obj.Update(ftpImportSettingsdetails.Id, null, null, true, "Contact has been added to LMS successfully!", p5uniqueid);
                        }
                        else
                        {
                            using (var obj =   DLApiImportResponseSettingLogs.GetDLApiImportResponseSettingLogs(AdsId,SqlProvider))
                                await obj.Update(ftpImportSettingsdetails.Id, null, null, false, "Contact has not added to LMS", p5uniqueid);
                        }

                        //unconditonal mail out
                        if (ftpImportSettingsdetails.MailSendingSettingId > 0)
                        {
                            MailSendingSetting eachdetail = new MailSendingSetting();

                            int status = 0;
                            bool mailrepeatstatus = ftpImportSettingsdetails.IsMailRepeatCon;
                            if (!mailrepeatstatus)
                            {
                                using (var objDLMail =   DLMailSendingSetting.GetDLMailSendingSetting(AdsId,SqlProvider))
                                    eachdetail =await objDLMail.GetDetail(ftpImportSettingsdetails.MailSendingSettingId);

                                using (var objlms =   DLLmsContactMailSmsWhatsAppDetails.GetDLLmsContactMailSmsWhatsAppDetails(AdsId,SqlProvider))
                                    status = await objlms.CheckCommunicationSent(ftpImportSettingsdetails.Id, contact.ContactId, lmsgrpid, eachdetail.MailTemplateId, "mail", "", "");
                            }
                            else
                            {
                                status = 1;
                            }

                            if (status > 0)
                            {
                                try
                                {
                                    MailOutReponseAction(ftpImportSettingsdetails.MailSendingSettingId, contact, lmsid);
                                }
                                catch (Exception ex)
                                {
                                    ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Mail Out Conditional ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in Mail Out Conditional", DateTime.Now, "APIResponseSettingAction-->Mail Out Conditional", ex.ToString());
                                }

                                if (!mailrepeatstatus)
                                {
                                    try
                                    {
                                        using (var objlms =   DLLmsContactMailSmsWhatsAppDetails.GetDLLmsContactMailSmsWhatsAppDetails(AdsId,SqlProvider))
                                            await objlms.Save(ftpImportSettingsdetails.Id, contact.ContactId, lmsgrpid, eachdetail.MailTemplateId, "mail", "", "");
                                    }
                                    catch (Exception ex)
                                    {
                                        ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Mail UnConditional Save LmsContactMailSmsWhatsAppDetails ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception Mail UnConditional Save LmsContactMailSmsWhatsAppDetails", DateTime.Now, "APIResponseSettingAction-->Mail UnConditional Save LmsContactMailSmsWhatsAppDetails", ex.ToString());
                                    }
                                }
                            }
                        }
                        else if (ftpImportSettingsdetails.MailSendingSettingId <= 0)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(ftpImportSettingsdetails.MailSendingConditonalJson))
                                {
                                    List<MailSMSWhatsAppOutConditionalJson> lmsjsondetails = new List<MailSMSWhatsAppOutConditionalJson>();
                                    lmsjsondetails = JsonConvert.DeserializeObject<List<MailSMSWhatsAppOutConditionalJson>>(ftpImportSettingsdetails.MailSendingConditonalJson);

                                    if (lmsjsondetails != null && lmsjsondetails.Count() > 0)
                                    {
                                        for (int i = 0; i < lmsjsondetails.Count(); i++)
                                        {
                                            string key = lmsjsondetails[i].Dependencyfield;
                                            string value = lmsjsondetails[i].Value;
                                            int mailoutsendingsettingid = Convert.ToInt32(lmsjsondetails[i].SendingSettingId);
                                            bool mailsendingstatus = lmsjsondetails[i].IsRepeatCommunication;
                                            int status = 0;

                                            string answervalue = string.Empty;

                                            if (lmsgrpdetails != null && key.ToLower().IndexOf("lmscustomfield") > -1)
                                            {
                                                if (lmsgrpdetails.GetType().GetProperty(key, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(lmsgrpdetails, null) != null)
                                                    answervalue = lmsgrpdetails.GetType().GetProperty(key).GetValue(lmsgrpdetails, null).ToString();
                                            }
                                            else
                                            {
                                                if (contact.GetType().GetProperty(key, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(contact, null) != null)
                                                    answervalue = contact.GetType().GetProperty(key).GetValue(contact, null).ToString();
                                            }

                                            MailSendingSetting eachdetail = new MailSendingSetting();
                                            using (var objBLMail =   DLMailSendingSetting.GetDLMailSendingSetting(AdsId,SqlProvider))
                                                eachdetail = await objBLMail.GetDetail(mailoutsendingsettingid);

                                            if (!mailsendingstatus)
                                            {
                                                using (var objlms =   DLLmsContactMailSmsWhatsAppDetails.GetDLLmsContactMailSmsWhatsAppDetails(AdsId,SqlProvider))
                                                    status =await  objlms.CheckCommunicationSent(ftpImportSettingsdetails.Id, contact.ContactId, lmsgrpid, eachdetail.MailTemplateId, "mail", key, value);
                                            }
                                            else
                                            {
                                                status = 1;
                                            }

                                            if (status > 0)
                                            {
                                                if (!string.IsNullOrEmpty(answervalue))
                                                {
                                                    if (answervalue == value)
                                                    {
                                                        try
                                                        {
                                                            MailOutReponseAction(mailoutsendingsettingid, contact, lmsid);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Mail Out Conditional ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in Mail Out Conditional", DateTime.Now, "APIResponseSettingAction-->Mail Out Conditional", ex.ToString());
                                                        }

                                                        if (!mailsendingstatus)
                                                        {
                                                            try
                                                            {
                                                                using (var objlms = DLLmsContactMailSmsWhatsAppDetails.GetDLLmsContactMailSmsWhatsAppDetails(AdsId,SqlProvider))
                                                                    await objlms.Save(ftpImportSettingsdetails.Id, contact.ContactId, lmsgrpid, eachdetail.MailTemplateId, "mail", key, value);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Mail Condition Save LmsContactMailSmsWhatsAppDetails ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception Mail Condition Save LmsContactMailSmsWhatsAppDetails", DateTime.Now, "APIResponseSettingAction-->Mail Condition Save LmsContactMailSmsWhatsAppDetails", ex.ToString());
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Mail Out Conditional ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in Conditional LmsGroup", DateTime.Now, "APIResponseSettingAction-->Mail Out Conditional", ex.ToString());
                            }
                        }

                        if (ftpImportSettingsdetails.SmsSendingSettingId > 0)
                        {
                            SmsSendingSetting eachdetail = new SmsSendingSetting();

                            int smsstatus = 0;
                            bool smsrepeatstatus = ftpImportSettingsdetails.IsSmsRepeatCon;
                            if (!smsrepeatstatus)
                            {
                                using (var objBLsms =   DLSmsSendingSetting.GetDLSmsSendingSetting(AdsId,SqlProvider))
                                    eachdetail =await  objBLsms.Get(ftpImportSettingsdetails.SmsSendingSettingId);

                                using (var objlms =   DLLmsContactMailSmsWhatsAppDetails.GetDLLmsContactMailSmsWhatsAppDetails(AdsId,SqlProvider))
                                    smsstatus = await objlms.CheckCommunicationSent(ftpImportSettingsdetails.Id, contact.ContactId, lmsgrpid, eachdetail.SmsTemplateId, "sms", "", "");
                            }
                            else
                            {
                                smsstatus = 1;
                            }

                            if (smsstatus > 0)
                            {
                                try
                                {
                                    SmsOutResponseAction(ftpImportSettingsdetails.SmsSendingSettingId, contact, lmsid);
                                }
                                catch (Exception ex)
                                {
                                    ErrorUpdation.AddErrorLog("APIResponseSettingAction==>SMS Out Conditional ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in SMS Out Conditional", DateTime.Now, "APIResponseSettingAction-->SMS Out Conditional", ex.ToString());
                                }

                                if (!smsrepeatstatus)
                                {
                                    try
                                    {
                                        using (var objlms =   DLLmsContactMailSmsWhatsAppDetails.GetDLLmsContactMailSmsWhatsAppDetails(AdsId,SqlProvider))
                                           await objlms.Save(ftpImportSettingsdetails.Id, contact.ContactId, lmsgrpid, eachdetail.SmsTemplateId, "sms", "", "");
                                    }
                                    catch (Exception ex)
                                    {
                                        ErrorUpdation.AddErrorLog("APIResponseSettingAction==>SMS UnConditional Save LmsContactMailSmsWhatsAppDetails ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception SMS UnConditional Save LmsContactMailSmsWhatsAppDetails", DateTime.Now, "APIResponseSettingAction-->SMS Condition Save LmsContactMailSmsWhatsAppDetails", ex.ToString());
                                    }
                                }
                            }
                        }
                        else if (ftpImportSettingsdetails.SmsSendingSettingId <= 0)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(ftpImportSettingsdetails.SmsSendingConditonalJson))
                                {
                                    List<MailSMSWhatsAppOutConditionalJson> lmsjsondetails = new List<MailSMSWhatsAppOutConditionalJson>();
                                    lmsjsondetails = JsonConvert.DeserializeObject<List<MailSMSWhatsAppOutConditionalJson>>(ftpImportSettingsdetails.SmsSendingConditonalJson);

                                    if (lmsjsondetails != null && lmsjsondetails.Count() > 0)
                                    {
                                        for (int i = 0; i < lmsjsondetails.Count(); i++)
                                        {
                                            string key = lmsjsondetails[i].Dependencyfield;
                                            string value = lmsjsondetails[i].Value;
                                            int smsoutsendingsettingid = Convert.ToInt32(lmsjsondetails[i].SendingSettingId);
                                            bool smssendingstatus = lmsjsondetails[i].IsRepeatCommunication;
                                            int smsstatus = 0;

                                            string answervalue = string.Empty;

                                            if (lmsgrpdetails != null && key.ToLower().IndexOf("lmscustomfield") > -1)
                                            {
                                                if (lmsgrpdetails.GetType().GetProperty(key, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(lmsgrpdetails, null) != null)
                                                    answervalue = lmsgrpdetails.GetType().GetProperty(key).GetValue(lmsgrpdetails, null).ToString();
                                            }
                                            else
                                            {
                                                if (contact.GetType().GetProperty(key, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(contact, null) != null)
                                                    answervalue = contact.GetType().GetProperty(key).GetValue(contact, null).ToString();
                                            }

                                            SmsSendingSetting eachdetail = new SmsSendingSetting();
                                            using (var objDLsms =   DLSmsSendingSetting.GetDLSmsSendingSetting(AdsId,SqlProvider))
                                                eachdetail = await objDLsms.Get(smsoutsendingsettingid);

                                            if (!smssendingstatus)
                                            {
                                                using (var objlms =   DLLmsContactMailSmsWhatsAppDetails.GetDLLmsContactMailSmsWhatsAppDetails(AdsId,SqlProvider))
                                                    smsstatus = await objlms.CheckCommunicationSent(ftpImportSettingsdetails.Id, contact.ContactId, lmsgrpid, eachdetail.SmsTemplateId, "sms", key, value);
                                            }
                                            else
                                            {
                                                smsstatus = 1;
                                            }

                                            if (smsstatus > 0)
                                            {
                                                if (!string.IsNullOrEmpty(answervalue))
                                                {
                                                    if (answervalue == value)
                                                    {
                                                        try
                                                        {
                                                            SmsOutResponseAction(smsoutsendingsettingid, contact, lmsid);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ErrorUpdation.AddErrorLog("APIResponseSettingAction==>SMS Out Conditional ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in SMS Out Conditional", DateTime.Now, "APIResponseSettingAction-->SMS Out Conditional", ex.ToString());
                                                        }

                                                        if (!smssendingstatus)
                                                        {
                                                            try
                                                            {
                                                                using (var objlms =   DLLmsContactMailSmsWhatsAppDetails.GetDLLmsContactMailSmsWhatsAppDetails(AdsId,SqlProvider))
                                                                    await objlms.Save(ftpImportSettingsdetails.Id, contact.ContactId, lmsgrpid, eachdetail.SmsTemplateId, "sms", key, value);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                ErrorUpdation.AddErrorLog("APIResponseSettingAction==>SMS Condition Save LmsContactMailSmsWhatsAppDetails ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception SMS Condition Save LmsContactMailSmsWhatsAppDetails", DateTime.Now, "APIResponseSettingAction-->SMS Condition Save LmsContactMailSmsWhatsAppDetails", ex.ToString());
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorUpdation.AddErrorLog("APIResponseSettingAction==>SMS Out Conditional ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in Conditional LmsGroup", DateTime.Now, "APIResponseSettingAction-->SMS Out Conditional", ex.ToString());
                            }
                        }

                        if (ftpImportSettingsdetails.WhatsAppSendingSettingId > 0)
                        {
                            WhatsAppSendingSetting eachdetail = new WhatsAppSendingSetting();

                            int whatsappstatus = 0;
                            bool whatsapprepeatstatus = ftpImportSettingsdetails.IsWhatsappRepeatCon;
                            if (!whatsapprepeatstatus)
                            {
                                using (var objDL =   DLWhatsAppSendingSetting.GetDLWhatsAppSendingSetting(AdsId,SqlProvider))
                                    eachdetail = await objDL.Get(ftpImportSettingsdetails.WhatsAppSendingSettingId);

                                using (var objlms =   DLLmsContactMailSmsWhatsAppDetails.GetDLLmsContactMailSmsWhatsAppDetails(AdsId,SqlProvider))
                                    whatsappstatus =await  objlms.CheckCommunicationSent(ftpImportSettingsdetails.Id, contact.ContactId, lmsgrpid, eachdetail.WhatsAppTemplateId, "whatsapp", "", "");
                            }
                            else
                            {
                                whatsappstatus = 1;
                            }

                            if (whatsappstatus > 0)
                            {
                                try
                                {
                                    WhatsAppOutResponseAction(ftpImportSettingsdetails.WhatsAppSendingSettingId, contact, lmsid);
                                }
                                catch (Exception ex)
                                {
                                    ErrorUpdation.AddErrorLog("APIResponseSettingAction==>WhatsApp Out UnConditional ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in WhatsApp Out Conditional", DateTime.Now, "APIResponseSettingAction-->WhatsApp Out UnConditional", ex.ToString());
                                }

                                if (!whatsapprepeatstatus)
                                {
                                    try
                                    {
                                        using (var objlms =   DLLmsContactMailSmsWhatsAppDetails.GetDLLmsContactMailSmsWhatsAppDetails(AdsId,SqlProvider))
                                           await objlms.Save(ftpImportSettingsdetails.Id, contact.ContactId, lmsgrpid, eachdetail.WhatsAppTemplateId, "whatsapp", "", "");
                                    }
                                    catch (Exception ex)
                                    {
                                        ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Whatsapp Condition Save LmsContactMailSmsWhatsAppDetails ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception Whatsapp Condition Save LmsContactMailSmsWhatsAppDetails", DateTime.Now, "APIResponseSettingAction-->Whatsapp Condition Save LmsContactMailSmsWhatsAppDetails", ex.ToString());
                                    }
                                }
                            }
                        }
                        else if (ftpImportSettingsdetails.WhatsAppSendingSettingId <= 0)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(ftpImportSettingsdetails.WhatsAppSendingConditonalJson))
                                {
                                    List<MailSMSWhatsAppOutConditionalJson> lmsjsondetails = new List<MailSMSWhatsAppOutConditionalJson>();
                                    lmsjsondetails = JsonConvert.DeserializeObject<List<MailSMSWhatsAppOutConditionalJson>>(ftpImportSettingsdetails.WhatsAppSendingConditonalJson);

                                    if (lmsjsondetails != null && lmsjsondetails.Count() > 0)
                                    {
                                        for (int i = 0; i < lmsjsondetails.Count(); i++)
                                        {
                                            string key = lmsjsondetails[i].Dependencyfield;
                                            string value = lmsjsondetails[i].Value;
                                            int whatsappoutsendingsettingid = Convert.ToInt32(lmsjsondetails[i].SendingSettingId);
                                            bool sendingstatus = lmsjsondetails[i].IsRepeatCommunication;
                                            int whatsappstatus = 0;

                                            string answervalue = string.Empty;

                                            if (lmsgrpdetails != null && key.ToLower().IndexOf("lmscustomfield") > -1)
                                            {
                                                if (lmsgrpdetails.GetType().GetProperty(key, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(lmsgrpdetails, null) != null)
                                                    answervalue = lmsgrpdetails.GetType().GetProperty(key).GetValue(lmsgrpdetails, null).ToString();
                                            }
                                            else
                                            {
                                                if (contact.GetType().GetProperty(key, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(contact, null) != null)
                                                    answervalue = contact.GetType().GetProperty(key).GetValue(contact, null).ToString();
                                            }

                                            WhatsAppSendingSetting eachdetail = new WhatsAppSendingSetting();
                                            using (var objDL =   DLWhatsAppSendingSetting.GetDLWhatsAppSendingSetting(AdsId,SqlProvider))
                                                eachdetail = await objDL.Get(whatsappoutsendingsettingid);

                                            if (!sendingstatus)
                                            {
                                                using (var objlms =   DLLmsContactMailSmsWhatsAppDetails.GetDLLmsContactMailSmsWhatsAppDetails(AdsId,SqlProvider))
                                                    whatsappstatus =await objlms.CheckCommunicationSent(ftpImportSettingsdetails.Id, contact.ContactId, lmsgrpid, eachdetail.WhatsAppTemplateId, "whatsapp", key, value);
                                            }
                                            else
                                            {
                                                whatsappstatus = 1;
                                            }

                                            if (whatsappstatus > 0)
                                            {
                                                if (!string.IsNullOrEmpty(answervalue))
                                                {
                                                    if (answervalue == value)
                                                    {
                                                        try
                                                        {
                                                            WhatsAppOutResponseAction(whatsappoutsendingsettingid, contact, lmsid);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ErrorUpdation.AddErrorLog("APIResponseSettingAction==>WhatsApp Out Conditional ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in WhatsApp Out Conditional", DateTime.Now, "APIResponseSettingAction-->WhatsApp Out Conditional", ex.ToString());
                                                        }

                                                        if (!sendingstatus)
                                                        {
                                                            try
                                                            {
                                                                using (var objlms =   DLLmsContactMailSmsWhatsAppDetails.GetDLLmsContactMailSmsWhatsAppDetails(AdsId,SqlProvider))
                                                                    await objlms.Save(ftpImportSettingsdetails.Id, contact.ContactId, lmsgrpid, eachdetail.WhatsAppTemplateId, "whatsapp", key, value);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                ErrorUpdation.AddErrorLog("APIResponseSettingAction==>Whatsapp Condition Save LmsContactMailSmsWhatsAppDetails ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception Whatsapp Condition Save LmsContactMailSmsWhatsAppDetails", DateTime.Now, "APIResponseSettingAction-->Whatsapp Condition Save LmsContactMailSmsWhatsAppDetails", ex.ToString());
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorUpdation.AddErrorLog("APIResponseSettingAction==>WhatsApp Out Conditional ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in Conditional LmsGroup", DateTime.Now, "APIResponseSettingAction-->WhatsApp Out Conditional", ex.ToString());
                            }
                        }

                        try
                        {
                            if (ftpImportSettingsdetails.IsAutoClickToCall)
                            {
                                int phonecallstatus = 0;
                                bool phonecallrepeatstatus = ftpImportSettingsdetails.IsCallRepeatCon;
                                if (!phonecallrepeatstatus)
                                {
                                    using (var objlms =   DLLmsContactMailSmsWhatsAppDetails.GetDLLmsContactMailSmsWhatsAppDetails(AdsId,SqlProvider))
                                        phonecallstatus =await objlms.CheckCommunicationSent(ftpImportSettingsdetails.Id, contact.ContactId, lmsgrpid, 0, "phonecall", "", "");
                                }
                                else
                                {
                                    phonecallstatus = 1;
                                }

                                if (phonecallstatus > 0)
                                {
                                    try
                                    {
                                        UserInfo userdetails = new UserInfo();

                                        if (contact.UserInfoUserId > 0)
                                        {
                                            using (var objuserdetails =   DLUserInfo.GetDLUserInfo(SqlProvider))
                                                userdetails = await objuserdetails.GetDetail(contact.UserInfoUserId);
                                        }

                                        if (userdetails != null && !string.IsNullOrEmpty(userdetails.MobilePhone))
                                        {
                                            if (string.IsNullOrEmpty(contact.CountryCode))
                                                contact.CountryCode = "91";

                                            if (userdetails.MobilePhone.Length == 10)
                                                userdetails.MobilePhone = "91" + userdetails.MobilePhone;

                                            string campjobname = "API";

                                            if (lmsid > 0)
                                                campjobname = "lms";

                                            PhonecallCalling phoneCal = new PhonecallCalling(AdsId, contact.CountryCode + contact.PhoneNumber, userdetails.MobilePhone, contact.ContactId, lmsgrpid, lmsgrpdetails.Score, lmsgrpdetails.LeadLabel, null, contact.UserInfoUserId, campjobname);
                                            bool CallStatus = await phoneCal.Call();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ErrorUpdation.AddErrorLog("APIResponseSettingAction==>ClickToCall ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception in ClickToCall", DateTime.Now, "APIResponseSettingAction-->ClickToCall", ex.ToString());
                                    }

                                    if (!phonecallrepeatstatus)
                                    {
                                        try
                                        {
                                            using (var objlms =   DLLmsContactMailSmsWhatsAppDetails.GetDLLmsContactMailSmsWhatsAppDetails(AdsId,SqlProvider))
                                               await objlms.Save(ftpImportSettingsdetails.Id, contact.ContactId, lmsgrpid, 0, "phonecall", "", "");
                                        }
                                        catch (Exception ex)
                                        {
                                            ErrorUpdation.AddErrorLog("APIResponseSettingAction==>ClickToCall Save LmsContactMailSmsWhatsAppDetails ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction exception ClickToCall Save LmsContactMailSmsWhatsAppDetails", DateTime.Now, "APIResponseSettingAction-->ClickToCall Save LmsContactMailSmsWhatsAppDetails", ex.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorUpdation.AddErrorLog("API-->ClickToCall==> Account " + AdsId.ToString() + "==> Id==> " + ftpImportSettingsdetails.Id.ToString() + " ", ex.Message.ToString(), "ClickToCall-->API ResponseAction method", DateTime.Now, "ClickToCall_API", ex.ToString());
                        }
                    }
                }
            });
        }

        public async Task<int> ConditionalUserAssignment(int individual, int value, Contact contact, ApiImportResponseSetting ftpImportSettingsdetails, bool isgroupsticky )
        {
            if (individual <= 0)
            {
                  AssignLeadToUserInfoUserId(value, 0, contact);
            }
            else if (individual > 0)
            {
                bool isuserinfouserid = true;
                List<int> userinfolist = null;

                using (var objDL =   DLLmsGroupMembers.GetDLLmsGroupMembers(AdsId,SqlProvider))
                    userinfolist = (await objDL.LmsGrpGetUserInfoList(contact.ContactId)).ToList();

                if (userinfolist.Count() <= 0)
                {
                    isuserinfouserid = true;
                }
                else
                {
                    //here checking for repeat contact and if there is sticky enabled for that condition check and assign the userinfouserid
                    if (isgroupsticky)
                                {
                                    try
                                    {
                                        List<MLUserHierarchy> userHierarchy = null;
                                        var objBL =   DLContact.GetContactDetails(AdsId,SqlProvider);
                                        List<int> userinfohierarchylist = null;
                                        List<int> finalist = null;

                                        UserGroupAssignment usergrpassigment = new UserGroupAssignment();
                                        var objusrgrpassign =   DLUserGroupAssignment.GetDLUserGroupAssignment(AdsId,SqlProvider);

                                        using (var objUserGroup =   DLUserGroupMembers.GetDLUserGroupMembers(SqlProvider))
                                        {
                                            userHierarchy = (await objUserGroup.GetHisUsers(value)).Where(x => x.ActiveStatus).OrderBy(x => x.UserInfoUserId).ToList();

                                            usergrpassigment = await objusrgrpassign.GetDetails(ftpImportSettingsdetails.Id, "API", value);

                                            if (userHierarchy != null && userHierarchy.Count > 0)
                                            {
                                                userinfohierarchylist = userHierarchy.Select(x => x.UserInfoUserId).Distinct().ToList();

                                                if (userinfohierarchylist != null && userinfohierarchylist.Count > 0 && userinfolist != null && userinfolist.Count > 0)
                                                    finalist = userinfolist.Intersect(userinfohierarchylist).ToList();

                                                if (finalist != null && finalist.Count > 0)
                                                {
                                                    int UserInfoUserId = finalist.FirstOrDefault();

                                                    string userassignmentvalues = "";

                                                    if (usergrpassigment == null)
                                                        userassignmentvalues = Convert.ToString(UserInfoUserId);
                                                    else
                                                        userassignmentvalues = usergrpassigment.UserAssignedValues + "," + UserInfoUserId;

                                                    if (await objusrgrpassign.SaveOrUpdate(ftpImportSettingsdetails.Id, "API", value, UserInfoUserId, userassignmentvalues, usergrpassigment == null ? 0 : usergrpassigment.Id) > 0)
                                                        contact.UserInfoUserId = UserInfoUserId;

                                                    isuserinfouserid = false;
                                                }
                                                else
                                                {
                                                    isuserinfouserid = true;
                                                }
                                            }
                                            else
                                            {
                                                isuserinfouserid = true;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        using (ErrorUpdation objError = new ErrorUpdation("API_AssignUserGroupId_ConditonalAssignment_isgroupsticky"))
                                            objError.AddError(ex.Message.ToString(), "", DateTime.Now.ToString(), "P5API->ContactController->API_AssignUserGroupId_ConditonalAssignment_isgroupsticky", ex.ToString(), true);
                                    }
                                }
                                else
                                {
                                    isuserinfouserid = true;
                                }
                }

                if (isuserinfouserid)
                {
                    try
                    {
                        List<MLUserHierarchy> userHierarchy = null;
                        var objDL =   DLContact.GetContactDetails(AdsId,SqlProvider);

                        UserGroupAssignment usergrpassigment = new UserGroupAssignment();
                        var objusrgrpassign =   DLUserGroupAssignment.GetDLUserGroupAssignment(AdsId,SqlProvider);

                        using (var objUserGroup =   DLUserGroupMembers.GetDLUserGroupMembers(SqlProvider))
                        {
                            userHierarchy = (await objUserGroup.GetHisUsers(value)).Where(x => x.ActiveStatus).OrderBy(x => x.UserInfoUserId).ToList();
                            if (userHierarchy != null && userHierarchy.Count > 0)
                            {
                                usergrpassigment =await  objusrgrpassign.GetDetails(ftpImportSettingsdetails.Id, "API", value);

                                int LastAssignUserInfoUserId = 0;

                                if (usergrpassigment != null && usergrpassigment.Id > 0)
                                    LastAssignUserInfoUserId = usergrpassigment.LastAssignUserInfoUserId;

                                MLUserHierarchy individualUser = null;

                                individualUser = userHierarchy.FirstOrDefault(x => x.UserInfoUserId > LastAssignUserInfoUserId);

                                if (individualUser != null && individualUser.UserInfoUserId > 0 && individualUser.UserInfoUserId != contact.UserInfoUserId)
                                {
                                    using (var objUpdateLastAssinged =   DLApiImportResponseSetting.GetDLApiImportResponseSetting(AdsId,SqlProvider))
                                    {
                                        if (contact != null)
                                        {
                                            string userassignmentvalues = "";

                                            if (usergrpassigment == null)
                                                userassignmentvalues = Convert.ToString(individualUser.UserInfoUserId);
                                            else
                                                userassignmentvalues = usergrpassigment.UserAssignedValues + "," + individualUser.UserInfoUserId;

                                            if (await objusrgrpassign.SaveOrUpdate(ftpImportSettingsdetails.Id, "API", value, individualUser.UserInfoUserId, userassignmentvalues, usergrpassigment == null ? 0 : usergrpassigment.Id) > 0)
                                                contact.UserInfoUserId = individualUser.UserInfoUserId;
                                        }
                                    }
                                }
                                else
                                {
                                    individualUser = userHierarchy.First();
                                    if (individualUser != null && individualUser.UserInfoUserId > 0 && individualUser.UserInfoUserId != contact.UserInfoUserId)
                                    {
                                        using (var objUpdateLastAssinged =   DLApiImportResponseSetting.GetDLApiImportResponseSetting(AdsId,SqlProvider))
                                        {
                                            if (contact != null)
                                            {
                                                string userassignmentvalues = "";

                                                if (usergrpassigment == null && usergrpassigment.Id <= 0)
                                                    userassignmentvalues = Convert.ToString(individualUser.UserInfoUserId);
                                                else
                                                    userassignmentvalues = usergrpassigment.UserAssignedValues + "," + individualUser.UserInfoUserId;

                                                if (await objusrgrpassign.SaveOrUpdate(ftpImportSettingsdetails.Id, "API", value, individualUser.UserInfoUserId, userassignmentvalues, usergrpassigment == null ? 0 : usergrpassigment.Id) > 0)
                                                    contact.UserInfoUserId = individualUser.UserInfoUserId;
                                            }

                                            if (contact != null)
                                            {
                                                if (ftpImportSettingsdetails.IsOverrideAssignment == 0)
                                                {
                                                    if (await objUpdateLastAssinged.UpdateLastAssigUserIdNotificationToSales(ftpImportSettingsdetails.Id, individualUser.UserInfoUserId))
                                                        contact.UserInfoUserId = individualUser.UserInfoUserId;
                                                }
                                                else if (ftpImportSettingsdetails.IsOverrideAssignment == 1)
                                                {
                                                    if (await objUpdateLastAssinged.UpdateLastAssigUserIdNotificationToSales(ftpImportSettingsdetails.Id, individualUser.UserInfoUserId))
                                                        contact.UserInfoUserId = individualUser.UserInfoUserId;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Account accountDetails = new Account();
                                using (var objBLAccount =   DLAccount.GetDLAccount(SqlProvider))
                                    accountDetails =await  objBLAccount.GetAccountDetails(AdsId);

                                if (accountDetails != null && accountDetails.AccountId > 0)
                                    contact.UserInfoUserId = accountDetails.UserInfoUserId;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        using (ErrorUpdation objError = new ErrorUpdation("API_AssignUserGroupId_ConditonalAssignment"))
                            objError.AddError(ex.Message.ToString(), "", DateTime.Now.ToString(), "P5API->ContactController->API_AssignUserGroupId_ConditonalAssignment", ex.ToString(), true);
                    }
                }
            }
            return contact.UserInfoUserId;
        }

        public async void ReportThroughMail(string reportToMailId, string APIresponseName, string Name = null, string EmailId = null, string PhoneNumber = null )
        {
            List<string> EmailIds = new List<string>();

            foreach (string MailId in reportToMailId.Split(','))
            {
                string emailId = MailId.Trim();
                if (!String.IsNullOrEmpty(emailId) && Helper.IsValidEmailAddress(emailId))
                    EmailIds.Add(emailId);
            }

            DateTime? preferreddatetime =await Helper.ConvertFromUTCToPreferredTimeZone(AdsId, DateTime.Now,SqlProvider);

            MailConfiguration mailconfiguration = new MailConfiguration();
            LeadAssignmentAgentNotification leadAssignmentAgentNotification = new LeadAssignmentAgentNotification();

            using (var objBLConfig =   DLMailConfiguration.GetDLMailConfiguration(AdsId,SqlProvider))
                mailconfiguration =await objBLConfig.GetConfigurationDetailsForSending(true, IsDefaultProvider: true);

            string FromEmailId = "";
            string FromName = AllConfigURLDetails.KeyValueForConfig["FROM_NAME_EMAIL"].ToString();
            using (var objDL =  DLMailConfigForSending.GetDLMailConfigForSending(AdsId,SqlProvider))
            {
                MailConfigForSending mailConfig =await objDL.GetActiveFromEmailId();
                if (mailConfig != null && !string.IsNullOrWhiteSpace(mailConfig.FromEmailId))
                    FromEmailId = mailConfig.FromEmailId;
            }

            using (var objDLleadassignment =   DLLeadAssignmentAgentNotification.GetDLLeadAssignmentAgentNotification(AdsId,SqlProvider))
                leadAssignmentAgentNotification = await objDLleadassignment.GetLeadAssignmentAgentNotification();

            try
            {
                if ((mailconfiguration != null && mailconfiguration.Id > 0) && (leadAssignmentAgentNotification != null && leadAssignmentAgentNotification.Mail) && !string.IsNullOrWhiteSpace(FromEmailId))
                {
                    try
                    {
                        string filePath = AllConfigURLDetails.KeyValueForConfig["MAINPATH"].ToString() + "\\Template\\ReportThroughMailContactAPILeads.html";
                        string MailBody = "";

                        if (System.IO.File.Exists(filePath))
                        {
                            using (StreamReader rd = new StreamReader(filePath))
                            {
                                MailBody = rd.ReadToEnd();
                                rd.Close();
                            }

                            string MailContent = "";

                            if (!string.IsNullOrEmpty(APIresponseName))
                                MailBody = MailBody.Replace("<!--Title-->", APIresponseName);

                            if (!string.IsNullOrEmpty(Name))
                                MailContent += "<div class='p5accntdet' style='padding:10px; border-bottom: solid 1px #cccccc;background-color: #f8f9fa;'><label for='' style=\"font-family: 'Open Sans', sans-serif; font-size: 13px; color: #222222; font-weight: 700;\">Name</label><p style=\"font-family: 'Open Sans', sans-serif; font-size: 13px; color: #222222;margin: 0;\">" + Name + "</p></div>";

                            if (!string.IsNullOrEmpty(EmailId))
                                MailContent += "<div class='p5accntdet' style='padding:10px; border-bottom: solid 1px #cccccc;background-color: #f8f9fa;'><label for='' style=\"font-family: 'Open Sans', sans-serif; font-size: 13px; color: #222222; font-weight: 700;\">EmailId</label><p style=\"font-family: 'Open Sans', sans-serif; font-size: 13px; color: #222222;margin: 0;\">" + EmailId + "</p></div>";

                            if (!string.IsNullOrEmpty(PhoneNumber))
                                MailContent += "<div class='p5accntdet' style='padding:10px; border-bottom: solid 1px #cccccc;background-color: #f8f9fa;'><label for='' style=\"font-family: 'Open Sans', sans-serif; font-size: 13px; color: #222222; font-weight: 700;\">PhoneNumber</label><p style=\"font-family: 'Open Sans', sans-serif; font-size: 13px; color: #222222;margin: 0;\">" + PhoneNumber + "</p></div>";

                            MailBody = MailBody.Replace("<!--MailContent-->", MailContent.ToString());

                            MailBody = MailBody.Replace("<!--DateTime-->", (preferreddatetime != null ? Convert.ToDateTime(preferreddatetime).ToString() : DateTime.Now.ToString()));
                            MailBody = MailBody.Replace("<!--AccountName-->", AccountName);
                            MailBody = MailBody.Replace("<!--AppLink-->", AllConfigURLDetails.KeyValueForConfig["ONLINEURL"].ToString());
                            MailBody = MailBody.Replace("<!--CLIENTLOGO_ONLINEURL-->", AllConfigURLDetails.KeyValueForConfig["CLIENTLOGO_ONLINEURL"].ToString());

                            for (int a = 0; a < EmailIds.Count(); a++)
                            {
                                if (Helper.IsValidEmailAddress(EmailIds[a].Trim()))
                                {
                                    Plumb5GenralFunction.MailSetting mailSetting = new Plumb5GenralFunction.MailSetting()
                                    {
                                        Forward = false,
                                        FromEmailId = FromEmailId,
                                        FromName = FromName,
                                        MailTemplateId = 0,
                                        ReplyTo = FromEmailId,
                                        Subject = "Plumb5 API Contact Leads (" + AccountName + ")",
                                        Subscribe = true,
                                        MessageBodyText = MailBody,
                                        IsTransaction = false
                                    };

                                    MLMailSent mailSent = new MLMailSent()
                                    {
                                        MailCampaignId = 0,
                                        MailSendingSettingId = 0,
                                        GroupId = 0,
                                        ContactId = 0,
                                        EmailId = EmailIds[a],
                                        P5MailUniqueID = Guid.NewGuid().ToString()
                                    };

                                    Plumb5GenralFunction.MailSentSavingDetials mailSentSavingDetials = new Plumb5GenralFunction.MailSentSavingDetials()
                                    {
                                        ConfigurationId = 0,
                                        GroupId = 0
                                    };

                                    IBulkMailSending MailGeneralBaseFactory = Plumb5GenralFunction.MailGeneralBaseFactory.GetMailVendor(AdsId, mailSetting, mailSentSavingDetials, mailconfiguration, "MailTrack", "WebForm");
                                    bool SentStatus = MailGeneralBaseFactory.SendSingleMail(mailSent);

                                    if (MailGeneralBaseFactory.VendorResponses != null && MailGeneralBaseFactory.VendorResponses.Count > 0)
                                    {
                                        MailSent getmailSent = new MailSent()
                                        {
                                            FromEmailId = mailSetting.FromEmailId,
                                            FromName = mailSetting.FromName,
                                            MailTemplateId = 0,
                                            Subject = mailSetting.Subject,
                                            MailCampaignId = 0,
                                            MailSendingSettingId = 0,
                                            GroupId = 0,
                                            ContactId = 0,
                                            EmailId = MailGeneralBaseFactory.VendorResponses[0].EmailId,
                                            P5MailUniqueID = MailGeneralBaseFactory.VendorResponses[0].P5MailUniqueID,
                                            CampaignJobName = MailGeneralBaseFactory.VendorResponses[0].CampaignJobName,
                                            ErrorMessage = MailGeneralBaseFactory.VendorResponses[0].ErrorMessage,
                                            ProductIds = MailGeneralBaseFactory.VendorResponses[0].ProductIds,
                                            ResponseId = MailGeneralBaseFactory.VendorResponses[0].ResponseId,
                                            SendStatus = (byte)MailGeneralBaseFactory.VendorResponses[0].SendStatus,
                                            WorkFlowDataId = MailGeneralBaseFactory.VendorResponses[0].WorkFlowDataId,
                                            WorkFlowId = MailGeneralBaseFactory.VendorResponses[0].WorkFlowId,
                                            SentDate = DateTime.Now,
                                            MailConfigurationNameId = mailconfiguration.MailConfigurationNameId
                                        };

                                        using (var objDLMailSent =   DLMailSent.GetDLMailSent(AdsId,SqlProvider))
                                            await objDLMailSent.Send(getmailSent);
                                    }
                                }
                            }
                        }
                        else
                        {
                            ErrorUpdation.AddErrorLog("APIResponseSettingAction_ReportThroughMail", "Lead notification alert", "APIResponseSettingAction ReportThroughMail alert template not exists", DateTime.Now, "APIResponseSettingAction-->ReportThroughMail", "Mail Cannot be sent");
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorUpdation.AddErrorLog("APIResponseSettingAction_ReportThroughMail_Inner", ex.Message.ToString(), "ReportThroughMail notification alert Mail exception in FormChatDetails-->ReportThroughMail-->Mail Inner method", DateTime.Now, "APIResponseSettingAction-->ReportThroughMail", ex.ToString());
                    }
                }
                else
                {
                    ErrorUpdation.AddErrorLog("APIResponseSettingAction_ReportThroughMail", "Please check Mail Configuration Settings", "Unable to send ReportThroughMail as mail configuration settings is not done", DateTime.Now, "APIResponseSettingAction-->ReportThroughMail", "Mail Cannot be sent");
                }
            }
            catch (Exception ex)
            {
                ErrorUpdation.AddErrorLog("APIResponseSettingAction_ReportThroughMail", ex.Message.ToString(), "APIResponseSettingAction notification alert exception in APIResponseSettingAction-->ReportThroughMail method", DateTime.Now, "APIResponseSettingAction-->ReportThroughMail", ex.ToString());
            }
        }

        public async void ReportThroughSMS(string ReportToNumber, string APIresponseName, string Name = null, string EmailId = null, string PhoneNumber = null )
        {
            try
            {
                StringBuilder SMSContent = new StringBuilder("Domain:" + AccountName + "");
                LeadAssignmentAgentNotification leadAssignmentAgentNotification = new LeadAssignmentAgentNotification();

                #region DLT Notification SMS
                SmsNotificationTemplate smsNotificationTemplate;
                using (var obj =   DLSmsNotificationTemplate.GetDLSmsNotificationTemplate(AdsId,SqlProvider))
                    smsNotificationTemplate =await obj.GetByIdentifier("captureformlead");

                #endregion DLT Notification SMS

                using (var objDLleadassignment =   DLLeadAssignmentAgentNotification.GetDLLeadAssignmentAgentNotification(AdsId,SqlProvider))
                    leadAssignmentAgentNotification =await objDLleadassignment.GetLeadAssignmentAgentNotification();

                if (smsNotificationTemplate != null)
                {
                    if (!string.IsNullOrEmpty(APIresponseName))
                        SMSContent.Append("Title -" + APIresponseName);

                    if (!string.IsNullOrEmpty(Name))
                        SMSContent.Append("Name -" + Name);

                    if (!string.IsNullOrEmpty(EmailId))
                        SMSContent.Append("EmailId -" + EmailId);

                    if (!string.IsNullOrEmpty(PhoneNumber))
                        SMSContent.Append("PhoneNumber -" + PhoneNumber);

                    string LeadDetailsMessageContent = smsNotificationTemplate.MessageContent.Replace("[{*SMSContent*}]", SMSContent.ToString());
                    SmsConfiguration smsConfiguration = new SmsConfiguration();

                    using (var objGetSMSConfigration =   DLSmsConfiguration.GetDLSmsConfiguration(AdsId,SqlProvider))
                        smsConfiguration = await objGetSMSConfigration.GetConfigurationDetailsForSending(true, IsDefaultProvider: true);

                    if (smsConfiguration != null && smsConfiguration.Id > 0 && smsNotificationTemplate.IsSmsNotificationEnabled && (leadAssignmentAgentNotification != null && leadAssignmentAgentNotification.Sms))
                    {
                        foreach (string contactNumber in ReportToNumber.Split(','))
                        {
                            SmsSent smsSent = new SmsSent()
                            {
                                CampaignJobName = "campaign",
                                ContactId = 0,
                                GroupId = 0,
                                MessageContent = LeadDetailsMessageContent,
                                PhoneNumber = contactNumber.Trim(),
                                SmsSendingSettingId = 0,
                                SmsTemplateId = 0,
                                VendorName = smsConfiguration.ProviderName,
                                IsUnicodeMessage = false,
                                VendorTemplateId = smsNotificationTemplate.VendorTemplateId,
                                P5SMSUniqueID = Guid.NewGuid().ToString()
                            };

                            IBulkSmsSending SmsGeneralBaseFactory =  Plumb5GenralFunction.SmsGeneralBaseFactory.GetSMSVendor(AdsId, smsConfiguration, "APIResponseSettingAction",SqlProvider);
                            bool status = SmsGeneralBaseFactory.SendSingleSms(smsSent);
                            string Message = SmsGeneralBaseFactory.ErrorMessage;

                            if (SmsGeneralBaseFactory.VendorResponses != null && SmsGeneralBaseFactory.VendorResponses.Count > 0)
                            {
                                if (status)
                                {
                                    Helper.Copy(SmsGeneralBaseFactory.VendorResponses[0], smsSent);

                                    smsSent.SentDate = DateTime.Now;
                                    smsSent.IsDelivered = 0;
                                    smsSent.IsClicked = 0;
                                    smsSent.Operator = null;
                                    smsSent.Circle = null;
                                    smsSent.DeliveryTime = null;
                                    smsSent.SmsConfigurationNameId = smsConfiguration.SmsConfigurationNameId;
                                    using (var objDLSMS =   DLSmsSent.GetDLSmsSent(AdsId,SqlProvider))
                                        await objDLSMS.Save(smsSent);
                                }
                                else
                                {
                                    Helper.Copy(SmsGeneralBaseFactory.VendorResponses[0], smsSent);

                                    smsSent.SentDate = DateTime.Now;
                                    smsSent.IsDelivered = 0;
                                    smsSent.IsClicked = 0;
                                    smsSent.Operator = null;
                                    smsSent.Circle = null;
                                    smsSent.DeliveryTime = null;
                                    smsSent.SendStatus = 0;
                                    smsSent.ReasonForNotDelivery = Message;
                                    smsSent.SmsConfigurationNameId = smsConfiguration.SmsConfigurationNameId;
                                    using (var objBLSMS =   DLSmsSent.GetDLSmsSent(AdsId,SqlProvider))
                                        await objBLSMS.Save(smsSent);
                                }
                            }
                        }
                    }
                    else
                    {
                        ErrorUpdation.AddErrorLog("APIResponseSettingAction_ReportThroughSMS", "Please check SMS Configuration Settings", "Unable to send ReportThroughSMS as SMS configuration settings is not done", DateTime.Now, "APIResponseSettingAction-->ReportThroughSMS", "SMS Cannot be sent");
                    }
                }
                else
                {
                    ErrorUpdation.AddErrorLog("APIResponseSettingAction_ReportThroughSMS", "Please check smsNotificationTemplate table as the data for identifer 'captureformlead' does not exist", "Unable to send ReportThroughSMS as the data not exist", DateTime.Now, "APIResponseSettingAction-->ReportThroughSMS", "SMS Cannot be sent");
                }
            }
            catch (Exception ex)
            {
                ErrorUpdation.AddErrorLog("APIResponseSettingAction_ReportThroughSMS", ex.Message.ToString(), "ReportThroughSMS notification alert exception in APIResponseSettingAction-->ReportThroughSMS method", DateTime.Now, "APIResponseSettingAction-->ReportThroughSMS", ex.ToString());
            }
        }

        public async void ReportThroughWhatsApp(string ReportToNumber, string APIresponseName, string Name = null, string EmailId = null, string PhoneNumber = null )
        {
            try
            {
                StringBuilder WhatsAppContent = new StringBuilder("Domain:" + AccountName + "");
                WhatsAppConfiguration whatsappConfiguration = new WhatsAppConfiguration();
                LeadAssignmentAgentNotification leadAssignmentAgentNotification = new LeadAssignmentAgentNotification();

                bool SentStatus = false;
                string UserAttributeMessageDetails = "";
                string UserButtonOneDynamicURLDetails = "";
                string UserButtonTwoDynamicURLDetails = "";
                string MediaURLDetails = "";
                string langcode = "";
                WhatsappSent watsAppSent = new WhatsappSent();

                using (var objDL =   DLWhatsAppConfiguration.GetDLWhatsAppConfiguration(AdsId,SqlProvider))
                {
                    whatsappConfiguration = await objDL.GetConfigurationDetailsForSending(IsDefaultProvider: true);
                }

                using (var objDLleadassignment =   DLLeadAssignmentAgentNotification.GetDLLeadAssignmentAgentNotification(AdsId,SqlProvider))
                {
                    leadAssignmentAgentNotification = await objDLleadassignment.GetLeadAssignmentAgentNotification();
                }

                if (whatsappConfiguration != null && whatsappConfiguration.Id > 0 && (leadAssignmentAgentNotification != null && leadAssignmentAgentNotification.WhatsApp))
                {
                    #region DLT Notification WhatsApp
                    WhatsAppNotificationTemplate whatsappNotificationTemplate;
                    using (var obj =   DLWhatsAppNotificationTemplate.GetDLWhatsAppNotificationTemplate(AdsId,SqlProvider))
                    {
                        whatsappNotificationTemplate = await obj.GetByIdentifier("captureformlead");
                    }

                    WhatsAppLanguageCodes whatsapplanguagecodes;

                    using (var objBLTemplate =   DLWhatsAppLanguageCodes.GetDLWhatsAppLanguageCodes(AdsId,SqlProvider))
                    {
                        whatsapplanguagecodes = await objBLTemplate.GetWhatsAppShortenLanguageCode(whatsappConfiguration.ProviderName, whatsappNotificationTemplate.TemplateLanguage);
                    }

                    #endregion DLT Notification WhatsApp

                    if (whatsappNotificationTemplate != null && whatsappNotificationTemplate.IsWhatsAppNotificationEnabled)
                    {
                        if (!string.IsNullOrEmpty(APIresponseName))
                            WhatsAppContent.Append("Title -" + APIresponseName);

                        if (!string.IsNullOrEmpty(Name))
                            WhatsAppContent.Append("Name -" + Name);

                        if (!string.IsNullOrEmpty(EmailId))
                            WhatsAppContent.Append("EmailId -" + EmailId);

                        if (!string.IsNullOrEmpty(PhoneNumber))
                            WhatsAppContent.Append("PhoneNumber -" + PhoneNumber);

                        foreach (string contactNumber in ReportToNumber.Split(','))
                        {
                            UserAttributeMessageDetails = string.Empty;
                            UserButtonOneDynamicURLDetails = string.Empty;
                            UserButtonTwoDynamicURLDetails = string.Empty;
                            MediaURLDetails = string.Empty;
                            langcode = string.Empty;

                            Contact contactDetails = new Contact() { PhoneNumber = contactNumber };

                            using (var objDL =   DLContact.GetContactDetails(AdsId,SqlProvider))
                            {
                                contactDetails = await objDL.GetDetails(contactDetails, null, true);
                            }

                            if (contactDetails == null)
                                contactDetails = new Contact() { PhoneNumber = contactNumber, CountryCode = whatsappConfiguration.CountryCode };

                            if (contactDetails != null && string.IsNullOrEmpty(contactDetails.CountryCode))
                                contactDetails.CountryCode = whatsappConfiguration.CountryCode;

                            HelperForSMS HelpSMS = new HelperForSMS(AdsId,SqlProvider);
                            StringBuilder UserAttrBodydata = new StringBuilder();
                            StringBuilder UserButtonOneBodydata = new StringBuilder();
                            StringBuilder UserButtonTwoBodydata = new StringBuilder();
                            StringBuilder MediaUrlBodyData = new StringBuilder();

                            if (!string.IsNullOrEmpty(whatsappNotificationTemplate.UserAttributes))
                            {
                                UserAttributeMessageDetails = whatsappNotificationTemplate.UserAttributes.Replace("[{*WhatsAppContent*}]", WhatsAppContent.ToString());
                                whatsappNotificationTemplate.TemplateContent = whatsappNotificationTemplate.TemplateContent.Replace("[{*WhatsAppContent*}]", WhatsAppContent.ToString());
                            }

                            if (!string.IsNullOrEmpty(whatsappNotificationTemplate.ButtonOneDynamicURLSuffix))
                            {
                                UserButtonOneBodydata.Append(whatsappNotificationTemplate.ButtonOneDynamicURLSuffix);
                                await HelpSMS.ReplaceContactDetails(UserButtonOneBodydata, contactDetails);
                                UserButtonOneDynamicURLDetails = UserButtonOneBodydata.ToString();
                            }

                            if (!string.IsNullOrEmpty(whatsappNotificationTemplate.ButtonTwoDynamicURLSuffix))
                            {
                                UserButtonTwoBodydata.Append(whatsappNotificationTemplate.ButtonTwoDynamicURLSuffix);
                                await HelpSMS.ReplaceContactDetails(UserButtonTwoBodydata, contactDetails);
                                UserButtonTwoDynamicURLDetails = UserButtonTwoBodydata.ToString();
                            }

                            if (!string.IsNullOrEmpty(whatsappNotificationTemplate.MediaFileURL))
                            {
                                MediaUrlBodyData.Append(whatsappNotificationTemplate.MediaFileURL);
                                await HelpSMS.ReplaceContactDetails(MediaUrlBodyData, contactDetails);
                                MediaURLDetails = MediaUrlBodyData.ToString();
                            }

                            if (whatsapplanguagecodes != null && !string.IsNullOrEmpty(whatsapplanguagecodes.ShortenLanguageCode))
                                langcode = whatsapplanguagecodes.ShortenLanguageCode;

                            if (UserAttributeMessageDetails.Contains("[{*") && UserAttributeMessageDetails.Contains("*}]"))
                            {
                                WhatsappSent watsappsent = new WhatsappSent()
                                {
                                    MediaFileURL = MediaURLDetails,
                                    PhoneNumber = contactNumber,
                                    UserAttributes = UserAttributeMessageDetails,
                                    ButtonOneDynamicURLSuffix = UserButtonOneDynamicURLDetails,
                                    ButtonTwoDynamicURLSuffix = UserButtonTwoDynamicURLDetails,
                                    CampaignJobName = "campaign",
                                    ContactId = 0,
                                    GroupId = 0,
                                    MessageContent = whatsappNotificationTemplate.TemplateContent,
                                    WhatsappSendingSettingId = 0,
                                    WhatsappTemplateId = 0,
                                    VendorName = whatsappConfiguration.ProviderName,
                                    SentDate = DateTime.Now,
                                    IsDelivered = 0,
                                    IsClicked = 0,
                                    ErrorMessage = "User Attributes dynamic content not replaced",
                                    SendStatus = 0,
                                    WorkFlowDataId = 0,
                                    WorkFlowId = 0,
                                    IsFailed = 1,
                                    WhatsAppConfigurationNameId = whatsappConfiguration.WhatsAppConfigurationNameId
                                };

                                using (var objDL =   DLWhatsAppSent.GetDLWhatsAppSent(AdsId,SqlProvider))
                                    await objDL.Save(watsappsent);
                            }
                            else if (UserButtonOneDynamicURLDetails.Contains("[{*") && UserButtonOneDynamicURLDetails.Contains("*}]"))
                            {
                                WhatsappSent watsappsent = new WhatsappSent()
                                {
                                    MediaFileURL = MediaURLDetails,
                                    PhoneNumber = contactNumber,
                                    UserAttributes = UserAttributeMessageDetails,
                                    ButtonOneDynamicURLSuffix = UserButtonOneDynamicURLDetails,
                                    ButtonTwoDynamicURLSuffix = UserButtonTwoDynamicURLDetails,
                                    CampaignJobName = "campaign",
                                    ContactId = 0,
                                    GroupId = 0,
                                    MessageContent = whatsappNotificationTemplate.TemplateContent,
                                    WhatsappSendingSettingId = 0,
                                    WhatsappTemplateId = 0,
                                    VendorName = whatsappConfiguration.ProviderName,
                                    SentDate = DateTime.Now,
                                    IsDelivered = 0,
                                    IsClicked = 0,
                                    ErrorMessage = "Template button one dynamic url content not replaced",
                                    SendStatus = 0,
                                    WorkFlowDataId = 0,
                                    WorkFlowId = 0,
                                    IsFailed = 1,
                                    WhatsAppConfigurationNameId = whatsappConfiguration.WhatsAppConfigurationNameId
                                };

                                using (var objDL =   DLWhatsAppSent.GetDLWhatsAppSent(AdsId,SqlProvider))
                                    await objDL.Save(watsappsent);
                            }
                            else if (UserButtonTwoDynamicURLDetails.Contains("[{*") && UserButtonTwoDynamicURLDetails.Contains("*}]"))
                            {
                                WhatsappSent watsappsent = new WhatsappSent()
                                {
                                    MediaFileURL = MediaURLDetails,
                                    PhoneNumber = contactNumber,
                                    UserAttributes = UserAttributeMessageDetails,
                                    ButtonOneDynamicURLSuffix = UserButtonOneDynamicURLDetails,
                                    ButtonTwoDynamicURLSuffix = UserButtonTwoDynamicURLDetails,
                                    CampaignJobName = "campaign",
                                    ContactId = 0,
                                    GroupId = 0,
                                    MessageContent = whatsappNotificationTemplate.TemplateContent,
                                    WhatsappSendingSettingId = 0,
                                    WhatsappTemplateId = 0,
                                    VendorName = whatsappConfiguration.ProviderName,
                                    SentDate = DateTime.Now,
                                    IsDelivered = 0,
                                    IsClicked = 0,
                                    ErrorMessage = "Template button two dynamic url content not replaced",
                                    SendStatus = 0,
                                    WorkFlowDataId = 0,
                                    WorkFlowId = 0,
                                    IsFailed = 1,
                                    WhatsAppConfigurationNameId = whatsappConfiguration.WhatsAppConfigurationNameId
                                };

                                using (var objDL =   DLWhatsAppSent.GetDLWhatsAppSent(AdsId,SqlProvider))
                                    await objDL.Save(watsappsent);
                            }
                            else
                            {
                                List<MLWhatsappSent> whatsappSentList = new List<MLWhatsappSent>();

                                MLWhatsappSent mlwatsappsent = new MLWhatsappSent()
                                {
                                    MediaFileURL = MediaURLDetails,
                                    CountryCode = contactDetails.CountryCode,
                                    PhoneNumber = contactDetails.PhoneNumber,
                                    WhiteListedTemplateName = whatsappNotificationTemplate.WhiteListedTemplateName,
                                    LanguageCode = langcode,
                                    UserAttributes = UserAttributeMessageDetails,
                                    ButtonOneText = whatsappNotificationTemplate.ButtonOneText,
                                    ButtonTwoText = whatsappNotificationTemplate.ButtonTwoText,
                                    ButtonOneDynamicURLSuffix = UserButtonOneDynamicURLDetails,
                                    ButtonTwoDynamicURLSuffix = UserButtonTwoDynamicURLDetails,
                                    CampaignJobName = "campaign",
                                    ContactId = contactDetails.ContactId,
                                    GroupId = 0,
                                    MessageContent = whatsappNotificationTemplate.TemplateContent,
                                    WhatsappSendingSettingId = 0,
                                    WhatsappTemplateId = 0,
                                    VendorName = whatsappConfiguration.ProviderName
                                };
                                whatsappSentList.Add(mlwatsappsent);

                                IBulkWhatsAppSending WhatsAppGeneralBaseFactory = Plumb5GenralFunction.WhatsAppGeneralBaseFactory.GetWhatsAppVendor(AdsId, whatsappConfiguration, "campaign");
                                SentStatus = WhatsAppGeneralBaseFactory.SendWhatsApp(whatsappSentList);
                                string ErrorMessage = WhatsAppGeneralBaseFactory.ErrorMessage;

                                if (SentStatus && WhatsAppGeneralBaseFactory.VendorResponses != null && WhatsAppGeneralBaseFactory.VendorResponses.Count > 0)
                                {
                                    Helper.Copy(WhatsAppGeneralBaseFactory.VendorResponses[0], watsAppSent);
                                    watsAppSent.WhatsAppConfigurationNameId = whatsappConfiguration.WhatsAppConfigurationNameId;
                                    using (var objDL =   DLWhatsAppSent.GetDLWhatsAppSent(AdsId,SqlProvider))
                                        await objDL.Save(watsAppSent);
                                }
                                else if (!SentStatus && !string.IsNullOrEmpty(ErrorMessage))
                                {
                                    Helper.Copy(WhatsAppGeneralBaseFactory.VendorResponses[0], watsAppSent);
                                    watsAppSent.WhatsAppConfigurationNameId = whatsappConfiguration.WhatsAppConfigurationNameId;
                                    using (var objDL =   DLWhatsAppSent.GetDLWhatsAppSent(AdsId,SqlProvider))
                                        await objDL.Save(watsAppSent);
                                }
                            }
                        }
                    }
                    else
                    {
                        ErrorUpdation.AddErrorLog("APIResponseSettingAction_ReportThroughWhatsapp", "Please check whatsappNotificationTemplate table as the data for identifer 'captureformlead' does not exist", "Unable to send ReportThroughWhatsApp as the data not exist", DateTime.Now, "APIResponseSettingAction-->ReportThroughWhatsApp", "WhatsApp Cannot be sent");
                    }
                }
                else
                {
                    ErrorUpdation.AddErrorLog("APIResponseSettingAction_ReportThroughWhatsApp", "Please check WhatsApp Configuration Settings", "Unable to send ReportThroughWhatsApp as WhatsApp configuration settings is not done", DateTime.Now, "APIResponseSettingAction-->ReportThroughWhatsApp", "WhatsApp Cannot be sent");
                }
            }
            catch (Exception ex)
            {
                ErrorUpdation.AddErrorLog("APIResponseSettingAction_ReportThroughWhatsApp", ex.Message.ToString(), "ReportThroughWhatsApp notification alert exception in APIResponseSettingAction-->ReportThroughWhatsApp method", DateTime.Now, "APIResponseSettingAction-->ReportThroughWhatsApp", ex.ToString());
            }
        }

        public async void MailOutReponseAction(int MailSendingSettingId, Contact contact, int lmsgroupmemberid = 0 )
        {
            List<MailSendingSetting> mailSettingList = new List<MailSendingSetting>();

            try
            {
                using (var objDLMail =   DLMailSendingSetting.GetDLMailSendingSetting(AdsId,SqlProvider))
                     mailSettingList.Add(await objDLMail.GetDetail(MailSendingSettingId));

                if (contact != null && !String.IsNullOrEmpty(contact.EmailId) && mailSettingList.Count > 0)
                    MailOut(contact, mailSettingList[0], lmsgroupmemberid);
            }
            catch (Exception ex)
            {
                ErrorUpdation.AddErrorLog("APIResponseSettingAction --> MailOutReponseAction ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction-->MailOut method", DateTime.Now, "APIResponseSettingAction-->MailOut", ex.ToString());
            }
        }

        private async void MailOut(Contact contact, MailSendingSetting mailSendingSetting, int lmsgroupmemberid = 0 )
        {
            MailConfiguration mailConfigration = new MailConfiguration();
            UserInfo userdetails = new UserInfo();

            if (contact.UserInfoUserId > 0)
            {
                using (var objuserdetails =   DLUserInfo.GetDLUserInfo(SqlProvider))
                    userdetails =await  objuserdetails.GetDetail(contact.UserInfoUserId);
            }

            using (var objMailConfig =   DLMailConfiguration.GetDLMailConfiguration(AdsId,SqlProvider))
            {
                mailConfigration =await objMailConfig.GetConfigurationDetailsForSending(true, IsDefaultProvider: true);
            }

            if (mailConfigration != null && mailConfigration.Id > 0 && mailConfigration.IsBulkSupported.HasValue && mailConfigration.IsBulkSupported.Value)
            {
                try
                {
                    AllConfigURLDetails.Get(SqlProvider);
                }
                catch (Exception ex)
                {
                    ErrorUpdation.AddErrorLog("MailOut==>AllConfigURLDetails Error ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "MailOut method", DateTime.Now, "APIResponseSettingAction-->MailOut", ex.ToString());
                }

                Plumb5GenralFunction.MailSetting mailSetting = new Plumb5GenralFunction.MailSetting();
                Helper.Copy(mailSendingSetting, mailSetting);

                MLMailSent mailSent = new MLMailSent()
                {
                    MailTemplateId = mailSendingSetting.MailTemplateId,
                    MailSendingSettingId = mailSendingSetting.Id,
                    GroupId = mailSendingSetting.GroupId,
                    ContactId = contact.ContactId,
                    EmailId = contact.EmailId,
                    DripSequence = 0,
                    DripConditionType = 0,
                    CampaignJobName = "WebForm",
                    FromEmailId = mailSendingSetting.FromEmailId,
                    FromName = AllConfigURLDetails.KeyValueForConfig["FROM_NAME_EMAIL"].ToString(),
                    ReplayToEmailId = mailSendingSetting.ReplyTo,
                    P5MailUniqueID = Guid.NewGuid().ToString()
                };

                Plumb5GenralFunction.MailSentSavingDetials mailSentSavingDetials = new Plumb5GenralFunction.MailSentSavingDetials()
                {
                    ConfigurationId = mailSendingSetting.Id,
                    GroupId = mailSendingSetting.GroupId
                };

                List<MLMailSent> mailSentList = new List<MLMailSent>();
                mailSentList.Add(mailSent);

                mailSetting.MessageBodyText =await  GetMailTemplate(AdsId, mailSetting.MailTemplateId);

                MailTemplate templateDetails = new MailTemplate { Id = mailSetting.MailTemplateId };
                using (var objDLTemplate =   DLMailTemplate.GetDLMailTemplate(AdsId,SqlProvider))
                {
                    templateDetails =objDLTemplate.GETDetails(templateDetails);
                }

                StringBuilder mailcontent = new StringBuilder();
                mailcontent.Append(mailSetting.MessageBodyText);

                HelperForMail objMail = new HelperForMail(AdsId, "", "");
                objMail.ChangeImageToOnlineUrl(mailcontent, templateDetails);
                Helper.ChangeUrlToAnalyticTrackUrl(mailcontent);

                if (userdetails != null && userdetails.UserId > 0)
                    objMail.ReplaceCounselorDetails(mailcontent, userdetails);

                //replace the lms custom field data
                if ((mailcontent.ToString().Contains("[{*")) && (mailcontent.ToString().Contains("*}]")))
                {
                    mailcontent.LmsUserInfoFieldsReplacement(AdsId, lmsgroupmemberid, contact.ContactId,SqlProvider);
                }

                mailSetting.MailTemplateId = 0;
                mailSetting.MessageBodyText = mailcontent.ToString();

                IBulkMailSending MailGeneralBaseFactory = Plumb5GenralFunction.MailGeneralBaseFactory.GetMailVendor(AdsId, mailSetting, mailSentSavingDetials, mailConfigration, "MailTrack", "WebForm");
                bool result = MailGeneralBaseFactory.SendBulkMail(mailSentList);

                if (MailGeneralBaseFactory.VendorResponses != null && MailGeneralBaseFactory.VendorResponses.Count > 0)
                {
                    string ErrorMessage = MailGeneralBaseFactory.ErrorMessage;
                    if (result)
                    {
                        MailSent getmailSent = new MailSent()
                        {
                            FromEmailId = mailSendingSetting.FromEmailId,
                            FromName = AllConfigURLDetails.KeyValueForConfig["FROM_NAME_EMAIL"].ToString(),
                            MailTemplateId = mailSendingSetting.MailTemplateId,
                            Subject = mailSendingSetting.Subject,
                            MailCampaignId = 0,
                            MailSendingSettingId = 0,
                            GroupId = 0,
                            ContactId = contact.ContactId,
                            EmailId = contact.EmailId,
                            P5MailUniqueID = MailGeneralBaseFactory.VendorResponses[0].P5MailUniqueID,
                            CampaignJobName = MailGeneralBaseFactory.VendorResponses[0].CampaignJobName,
                            ErrorMessage = MailGeneralBaseFactory.VendorResponses[0].ErrorMessage,
                            MailContent = mailSetting.MessageBodyText,
                            ProductIds = MailGeneralBaseFactory.VendorResponses[0].ProductIds,
                            ResponseId = MailGeneralBaseFactory.VendorResponses[0].ResponseId,
                            SendStatus = (byte)MailGeneralBaseFactory.VendorResponses[0].SendStatus,
                            WorkFlowDataId = MailGeneralBaseFactory.VendorResponses[0].WorkFlowDataId,
                            WorkFlowId = MailGeneralBaseFactory.VendorResponses[0].WorkFlowId,
                            SentDate = DateTime.Now,
                            MailConfigurationNameId = mailConfigration.MailConfigurationNameId
                        };

                        using (var objDLMailSent =   DLMailSent.GetDLMailSent(AdsId,SqlProvider))
                            await objDLMailSent.Send(getmailSent);
                    }
                }
            }
        }

        private async Task<string> GetMailTemplate(int AccountId, int TemplateId )
        {
            MailTemplateFile mailTemplateFile;
            using (var objDL =   DLMailTemplateFile.GetDLMailTemplateFile(AccountId,SqlProvider))
            {
                mailTemplateFile = await objDL.GetSingleFileType(new MailTemplateFile() { TemplateId = TemplateId, TemplateFileType = ".HTML" });
            }
            SaveDownloadFilesToAws awsUpload = new SaveDownloadFilesToAws(AccountId, TemplateId);
            string fileString = await awsUpload.GetFileContentString(mailTemplateFile.TemplateFileName, awsUpload.bucketname);

            if (string.IsNullOrEmpty(fileString))
                fileString = awsUpload.GetFileContentStringAsync(mailTemplateFile.TemplateFileName, awsUpload.bucketname);

            return fileString;
        }

        public async void SmsOutResponseAction(int SmsSendingSettingId, Contact contact, int lmsgroupmemberid = 0 )
        {
            List<SmsSendingSetting> smsSettingList = new List<SmsSendingSetting>();

            try
            {
                using (var objDLsms =   DLSmsSendingSetting.GetDLSmsSendingSetting(AdsId,SqlProvider))
                    smsSettingList.Add(await objDLsms.Get(SmsSendingSettingId));

                if (contact != null && !String.IsNullOrEmpty(contact.PhoneNumber) && smsSettingList != null && smsSettingList.Count > 0)
                    SmsOut(contact, smsSettingList[0], lmsgroupmemberid);
            }
            catch (Exception ex)
            {
                ErrorUpdation.AddErrorLog("APIResponseSettingAction --> SmsOutResponseAction ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "SmsOutResponseAction method", DateTime.Now, "APIResponseSettingAction-->SmsOutResponseAction", ex.ToString());
            }
        }

        private async void SmsOut(Contact contact, SmsSendingSetting smsSetting, int lmsgroupmemberid = 0 )
        {
            bool SentStatus = false;
            string MessageContent = "";
            string Message;
            string ResponseId = "";

            UserInfo userdetails = new UserInfo();
            SmsConfiguration smsConfigration = new SmsConfiguration();

            if (contact.UserInfoUserId > 0)
            {
                using (var objuserdetails =   DLUserInfo.GetDLUserInfo(SqlProvider))
                    userdetails =await objuserdetails.GetDetail(contact.UserInfoUserId);
            }

            using (var objSmsConfig =   DLSmsConfiguration.GetDLSmsConfiguration(AdsId,SqlProvider))
            {
                smsConfigration =await objSmsConfig.GetConfigurationDetailsForSending(true, IsDefaultProvider: true);
            }

            if (smsConfigration != null && smsConfigration.Id > 0 && smsConfigration.IsBulkSupported.HasValue && smsConfigration.IsBulkSupported.Value)
            {
                SmsTemplate smsTemplate;
                using (var objDL =   DLSmsTemplate.GetDLSmsTemplate(AdsId,SqlProvider))
                {
                    smsTemplate =await objDL.GetDetails(smsSetting.SmsTemplateId);
                }

                if (smsTemplate != null)
                {
                    MessageContent = System.Web.HttpUtility.HtmlDecode(smsTemplate.MessageContent);

                    string P5UniqueID = Guid.NewGuid().ToString();
                    HelperForSMS HelpSMS = new HelperForSMS(AdsId,SqlProvider);
                    List<SmsTemplateUrl> smsUrlList;
                    using (var objsmstempUrl =   DLSmsTemplateUrl.GetDLSmsTemplateUrl(AdsId,SqlProvider))
                    {
                        smsUrlList = (await objsmstempUrl.GetDetail(smsSetting.SmsTemplateId)).ToList();
                    }

                    StringBuilder Bodydata = new StringBuilder();
                    Bodydata.Append(MessageContent);
                    HelpSMS.ReplaceMessageWithSMSUrl("campaign", Bodydata, 0, contact.ContactId, smsUrlList, P5UniqueID);
                    HelpSMS.ReplaceContactDetails(Bodydata, contact, AdsId, "", smsSetting.SmsTemplateId, smsSetting.Id, P5UniqueID, "sms", smsTemplate.ConvertLinkToShortenUrl);

                    if (userdetails != null && userdetails.UserId > 0)
                        HelpSMS.ReplaceCounselorDetails(Bodydata, userdetails);

                    //replace the lms custom field data
                    if ((Bodydata.ToString().Contains("[{*")) && (Bodydata.ToString().Contains("*}]")))
                    {
                        await Bodydata.LmsUserInfoFieldsReplacement(AdsId, lmsgroupmemberid, contact.ContactId,SqlProvider);
                    }

                    MessageContent = Bodydata.ToString();

                    if (MessageContent.Contains("[{*") && MessageContent.Contains("*}]"))
                    {
                        SmsSent smsSent = new SmsSent();
                        smsSent.SentDate = DateTime.Now;
                        smsSent.IsDelivered = 0;
                        smsSent.IsClicked = 0;
                        smsSent.Operator = null;
                        smsSent.Circle = null;
                        smsSent.DeliveryTime = null;
                        smsSent.SendStatus = 0;
                        smsSent.ReasonForNotDelivery = "Template dynamic content not replaced";
                        smsSent.SmsConfigurationNameId = smsConfigration.SmsConfigurationNameId;
                        using (var objDLSMS =   DLSmsSent.GetDLSmsSent(AdsId,SqlProvider))
                        {
                            await objDLSMS.Save(smsSent);
                        }
                    }
                    else
                    {
                        bool IsUnicodeMessage = Helper.ContainsUnicodeCharacter(MessageContent);
                        List<SmsSent> smsSentList = new List<SmsSent>();
                        SmsSent smsSent = new SmsSent()
                        {
                            CampaignJobName = "campaign",
                            ContactId = contact.ContactId,
                            GroupId = 0,
                            MessageContent = MessageContent,
                            PhoneNumber = contact.PhoneNumber,
                            SmsSendingSettingId = 0,
                            SmsTemplateId = smsSetting.SmsTemplateId,
                            VendorName = smsConfigration.ProviderName,
                            IsUnicodeMessage = IsUnicodeMessage
                        };
                        smsSentList.Add(smsSent);

                        IBulkSmsSending SmsGeneralBaseFactory = Plumb5GenralFunction.SmsGeneralBaseFactory.GetSMSVendor(AdsId, smsConfigration, "WebForm",SqlProvider);
                        SentStatus = SmsGeneralBaseFactory.SendBulkSms(smsSentList);
                        Message = SmsGeneralBaseFactory.ErrorMessage;

                        if (SmsGeneralBaseFactory.VendorResponses != null && SmsGeneralBaseFactory.VendorResponses.Count > 0)
                        {
                            ResponseId = SmsGeneralBaseFactory.VendorResponses[0].ResponseId;
                            Helper.Copy(SmsGeneralBaseFactory.VendorResponses[0], smsSent);

                            smsSent.SentDate = DateTime.Now;
                            smsSent.IsDelivered = 0;
                            smsSent.IsClicked = 0;
                            smsSent.Operator = null;
                            smsSent.Circle = null;
                            smsSent.DeliveryTime = null;
                            smsSent.ReasonForNotDelivery = Message;
                            smsSent.SmsConfigurationNameId = smsConfigration.SmsConfigurationNameId;
                            using (var objDL =  DLSmsSent.GetDLSmsSent(AdsId,SqlProvider))
                            {
                                await objDL.Save(smsSent);
                            }
                        }
                    }
                }
            }
        }

        public async void WhatsAppOutResponseAction(int WhatsAppSendingSettingId, Contact contact, int lmsgroupmemberid = 0 )
        {
            List<WhatsAppSendingSetting> whatsappSettingList = new List<WhatsAppSendingSetting>();

            try
            {
                using (var objDLwahtsapp =   DLWhatsAppSendingSetting.GetDLWhatsAppSendingSetting(AdsId,SqlProvider))
                    whatsappSettingList.Add(await objDLwahtsapp.Get(WhatsAppSendingSettingId));

                if (contact != null && !String.IsNullOrEmpty(contact.PhoneNumber) && whatsappSettingList != null && whatsappSettingList.Count > 0)
                    WhatsAppOut(contact, whatsappSettingList[0] );
            }
            catch (Exception ex)
            {
                ErrorUpdation.AddErrorLog("APIResponseSettingAction --> WhatsAppOutResponseAction ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "WhatsAppOutResponseAction method", DateTime.Now, "APIResponseSettingAction-->WhatsAppOutResponseAction", ex.ToString());
            }
        }

        private async  void WhatsAppOut(Contact contact, WhatsAppSendingSetting whatsappSetting )
        {
            try
            {
                if (!string.IsNullOrEmpty(contact.PhoneNumber))
                {
                    WhatsAppConfiguration whatsappConfiguration = new WhatsAppConfiguration();
                    bool SentStatus = false;
                    string UserAttributeMessageDetails = "";
                    string UserButtonOneDynamicURLDetails = "";
                    string UserButtonTwoDynamicURLDetails = "";
                    string MediaURLDetails = "";
                    string langcode = "";
                    WhatsappSent watsAppSent = new WhatsappSent();
                    string P5WhatsappUniqueID = Guid.NewGuid().ToString();
                    List<WhatsAppTemplateUrl> whatsappUrlList = new List<WhatsAppTemplateUrl>();

                    using (var objDL =  DLWhatsAppConfiguration.GetDLWhatsAppConfiguration(AdsId,SqlProvider))
                    {
                        whatsappConfiguration = await objDL.GetConfigurationDetailsForSending(IsDefaultProvider: true);
                    }

                    if (whatsappConfiguration != null && whatsappConfiguration.Id > 0)
                    {
                        Contact contactDetails = new Contact() { PhoneNumber = contact.PhoneNumber };

                        using (var objDL =   DLContact.GetContactDetails(AdsId,SqlProvider))
                        {
                            contactDetails =await objDL.GetDetails(contactDetails, null, true);
                        }

                        if (contactDetails == null)
                            contactDetails = new Contact() { PhoneNumber = contact.PhoneNumber, CountryCode = whatsappConfiguration.CountryCode };

                        if (contactDetails != null && string.IsNullOrEmpty(contactDetails.CountryCode))
                            contactDetails.CountryCode = whatsappConfiguration.CountryCode;

                        WhatsAppTemplates whatsapptemplateDetails;

                        using (var objDLTemplate =   DLWhatsAppTemplates.GetDLWhatsAppTemplates(AdsId,SqlProvider))
                        {
                            whatsapptemplateDetails =await objDLTemplate.GetSingle(whatsappSetting.WhatsAppTemplateId);
                        }

                        using (var objsmstempUrl =   DLWhatsappTemplateUrl.GetDLWhatsappTemplateUrl(AdsId,SqlProvider))
                        {
                            whatsappUrlList = await objsmstempUrl.GetDetail(whatsappSetting.WhatsAppTemplateId);
                        }

                        if (whatsapptemplateDetails != null)
                        {
                            WhatsAppLanguageCodes whatsapplanguagecodes;

                            using (var objDLTemplate =   DLWhatsAppLanguageCodes.GetDLWhatsAppLanguageCodes(AdsId,SqlProvider))
                            {
                                whatsapplanguagecodes = await objDLTemplate.GetWhatsAppShortenLanguageCode(whatsappConfiguration.ProviderName, whatsapptemplateDetails.TemplateLanguage);
                            }

                            HelperForSMS HelpSMS = new HelperForSMS(AdsId,SqlProvider);
                            HelperForWhatsApp HelpWhatsApp = new HelperForWhatsApp(AdsId,SqlProvider);
                            StringBuilder UserAttrBodydata = new StringBuilder();
                            StringBuilder UserButtonOneBodydata = new StringBuilder();
                            StringBuilder UserButtonTwoBodydata = new StringBuilder();
                            StringBuilder MediaUrlBodyData = new StringBuilder();

                            if (!string.IsNullOrEmpty(whatsapptemplateDetails.UserAttributes))
                            {
                                UserAttrBodydata.Append(whatsapptemplateDetails.UserAttributes);
                                HelpWhatsApp.ReplaceMessageWithWhatsAppUrl("campaign", UserAttrBodydata, whatsappSetting.Id, contactDetails.ContactId, whatsappUrlList, P5WhatsappUniqueID, 0, true);
                                HelpSMS.ReplaceContactDetails(UserAttrBodydata, contactDetails, AdsId, "", whatsappSetting.WhatsAppTemplateId, whatsappSetting.Id, P5WhatsappUniqueID, "whatsapp", whatsapptemplateDetails.ConvertLinkToShortenUrl);
                                UserAttributeMessageDetails = UserAttrBodydata.ToString();
                            }

                            if (!string.IsNullOrEmpty(whatsapptemplateDetails.ButtonOneDynamicURLSuffix))
                            {
                                string ButtonOneMessage = whatsapptemplateDetails.ButtonOneDynamicURLSuffix;
                                UserButtonOneBodydata.Clear().Append(ButtonOneMessage);
                                ConvertWhatsAppURLToShortenCode helpconvert = new ConvertWhatsAppURLToShortenCode(AdsId,SqlProvider);
                                helpconvert.GenerateShortenLinkByUrl(UserButtonOneBodydata, contactDetails, Convert.ToInt32(whatsapptemplateDetails.Id), 0, P5WhatsappUniqueID);
                                HelpWhatsApp.ReplaceMessageWithWhatsAppUrl("campaign", UserButtonOneBodydata, whatsappSetting.Id, contactDetails.ContactId, whatsappUrlList, P5WhatsappUniqueID, 0);
                                HelpSMS.ReplaceContactDetails(UserButtonOneBodydata, contactDetails, AdsId, "", whatsappSetting.WhatsAppTemplateId, whatsappSetting.Id, P5WhatsappUniqueID, "whatsapp", whatsapptemplateDetails.ConvertLinkToShortenUrl);
                                UserButtonOneDynamicURLDetails = UserButtonOneBodydata.ToString();
                            }

                            if (!string.IsNullOrEmpty(whatsapptemplateDetails.ButtonTwoDynamicURLSuffix))
                            {
                                string ButtonTwoMessage = whatsapptemplateDetails.ButtonTwoDynamicURLSuffix;
                                UserButtonTwoBodydata.Clear().Append(ButtonTwoMessage);
                                ConvertWhatsAppURLToShortenCode helpconvert = new ConvertWhatsAppURLToShortenCode(AdsId, SqlProvider);
                                helpconvert.GenerateShortenLinkByUrl(UserButtonTwoBodydata, contactDetails, Convert.ToInt32(whatsapptemplateDetails.Id), 0, P5WhatsappUniqueID);
                                HelpWhatsApp.ReplaceMessageWithWhatsAppUrl("campaign", UserButtonTwoBodydata, whatsappSetting.Id, contactDetails.ContactId, whatsappUrlList, P5WhatsappUniqueID, 0);
                                HelpSMS.ReplaceContactDetails(UserButtonTwoBodydata, contactDetails, AdsId, "", whatsappSetting.WhatsAppTemplateId, whatsappSetting.Id, P5WhatsappUniqueID, "whatsapp", whatsapptemplateDetails.ConvertLinkToShortenUrl);
                                UserButtonTwoDynamicURLDetails = UserButtonTwoBodydata.ToString();
                            }

                            if (!string.IsNullOrEmpty(whatsapptemplateDetails.MediaFileURL))
                            {
                                MediaUrlBodyData.Append(whatsapptemplateDetails.MediaFileURL);
                                HelpSMS.ReplaceContactDetails(MediaUrlBodyData, contactDetails, AdsId, "", whatsappSetting.WhatsAppTemplateId, whatsappSetting.Id, P5WhatsappUniqueID, "whatsapp", whatsapptemplateDetails.ConvertLinkToShortenUrl);
                                MediaURLDetails = MediaUrlBodyData.ToString();
                            }

                            if (whatsapplanguagecodes != null && !string.IsNullOrEmpty(whatsapplanguagecodes.ShortenLanguageCode))
                                langcode = whatsapplanguagecodes.ShortenLanguageCode;

                            if (UserAttributeMessageDetails.Contains("[{*") && UserAttributeMessageDetails.Contains("*}]"))
                            {
                                WhatsappSent watsappsent = new WhatsappSent()
                                {
                                    MediaFileURL = MediaURLDetails,
                                    PhoneNumber = contact.PhoneNumber,
                                    UserAttributes = UserAttributeMessageDetails,
                                    ButtonOneDynamicURLSuffix = UserButtonOneDynamicURLDetails,
                                    ButtonTwoDynamicURLSuffix = UserButtonTwoDynamicURLDetails,
                                    CampaignJobName = "campaign",
                                    ContactId = contact.ContactId,
                                    GroupId = whatsappSetting.GroupId,
                                    MessageContent = whatsapptemplateDetails.TemplateContent,
                                    WhatsappSendingSettingId = whatsappSetting.Id,
                                    WhatsappTemplateId = whatsapptemplateDetails.Id,
                                    VendorName = whatsappConfiguration.ProviderName,
                                    SentDate = DateTime.Now,
                                    IsDelivered = 0,
                                    IsClicked = 0,
                                    ErrorMessage = "User Attributes dynamic content not replaced",
                                    SendStatus = 0,
                                    WorkFlowDataId = 0,
                                    WorkFlowId = 0,
                                    IsFailed = 1,
                                    P5WhatsappUniqueID = P5WhatsappUniqueID,
                                    WhatsAppConfigurationNameId = whatsappConfiguration.WhatsAppConfigurationNameId
                                };

                                using (var objDL =   DLWhatsAppSent.GetDLWhatsAppSent(AdsId,SqlProvider))
                                    await objDL.Save(watsappsent);
                            }
                            else if (UserButtonOneDynamicURLDetails.Contains("[{*") && UserButtonOneDynamicURLDetails.Contains("*}]"))
                            {
                                WhatsappSent watsappsent = new WhatsappSent()
                                {
                                    MediaFileURL = MediaURLDetails,
                                    PhoneNumber = contact.PhoneNumber,
                                    UserAttributes = UserAttributeMessageDetails,
                                    ButtonOneDynamicURLSuffix = UserButtonOneDynamicURLDetails,
                                    ButtonTwoDynamicURLSuffix = UserButtonTwoDynamicURLDetails,
                                    CampaignJobName = "campaign",
                                    ContactId = contact.ContactId,
                                    GroupId = whatsappSetting.GroupId,
                                    MessageContent = whatsapptemplateDetails.TemplateContent,
                                    WhatsappSendingSettingId = whatsappSetting.Id,
                                    WhatsappTemplateId = whatsapptemplateDetails.Id,
                                    VendorName = whatsappConfiguration.ProviderName,
                                    SentDate = DateTime.Now,
                                    IsDelivered = 0,
                                    IsClicked = 0,
                                    ErrorMessage = "Template button one dynamic url content not replaced",
                                    SendStatus = 0,
                                    WorkFlowDataId = 0,
                                    WorkFlowId = 0,
                                    IsFailed = 1,
                                    P5WhatsappUniqueID = P5WhatsappUniqueID,
                                    WhatsAppConfigurationNameId = whatsappConfiguration.WhatsAppConfigurationNameId
                                };

                                using (var objDL =   DLWhatsAppSent.GetDLWhatsAppSent(AdsId,SqlProvider))
                                {
                                    await objDL.Save(watsappsent);
                                }
                            }
                            else if (UserButtonTwoDynamicURLDetails.Contains("[{*") && UserButtonTwoDynamicURLDetails.Contains("*}]"))
                            {
                                WhatsappSent watsappsent = new WhatsappSent()
                                {
                                    MediaFileURL = MediaURLDetails,
                                    PhoneNumber = contact.PhoneNumber,
                                    UserAttributes = UserAttributeMessageDetails,
                                    ButtonOneDynamicURLSuffix = UserButtonOneDynamicURLDetails,
                                    ButtonTwoDynamicURLSuffix = UserButtonTwoDynamicURLDetails,
                                    CampaignJobName = "campaign",
                                    ContactId = contact.ContactId,
                                    GroupId = whatsappSetting.GroupId,
                                    MessageContent = whatsapptemplateDetails.TemplateContent,
                                    WhatsappSendingSettingId = whatsappSetting.Id,
                                    WhatsappTemplateId = whatsapptemplateDetails.Id,
                                    VendorName = whatsappConfiguration.ProviderName,
                                    SentDate = DateTime.Now,
                                    IsDelivered = 0,
                                    IsClicked = 0,
                                    ErrorMessage = "Template button two dynamic url content not replaced",
                                    SendStatus = 0,
                                    WorkFlowDataId = 0,
                                    WorkFlowId = 0,
                                    IsFailed = 1,
                                    P5WhatsappUniqueID = P5WhatsappUniqueID,
                                    WhatsAppConfigurationNameId = whatsappConfiguration.WhatsAppConfigurationNameId
                                };

                                using (var objDL =   DLWhatsAppSent.GetDLWhatsAppSent(AdsId,SqlProvider))
                                {
                                    await objDL.Save(watsappsent);
                                }
                            }
                            else
                            {
                                List<MLWhatsappSent> whatsappSentList = new List<MLWhatsappSent>();

                                MLWhatsappSent mlwatsappsent = new MLWhatsappSent()
                                {
                                    MediaFileURL = MediaURLDetails,
                                    CountryCode = contactDetails.CountryCode,
                                    PhoneNumber = contactDetails.PhoneNumber,
                                    WhiteListedTemplateName = whatsapptemplateDetails.WhitelistedTemplateName,
                                    LanguageCode = langcode,
                                    UserAttributes = UserAttributeMessageDetails,
                                    ButtonOneText = whatsapptemplateDetails.ButtonOneText,
                                    ButtonTwoText = whatsapptemplateDetails.ButtonTwoText,
                                    ButtonOneDynamicURLSuffix = UserButtonOneDynamicURLDetails,
                                    ButtonTwoDynamicURLSuffix = UserButtonTwoDynamicURLDetails,
                                    CampaignJobName = "campaign",
                                    ContactId = contactDetails.ContactId,
                                    GroupId = 0,
                                    MessageContent = whatsapptemplateDetails.TemplateContent,
                                    WhatsappSendingSettingId = 0,
                                    WhatsappTemplateId = whatsappSetting.WhatsAppTemplateId,
                                    VendorName = whatsappConfiguration.ProviderName,
                                    P5WhatsappUniqueID = P5WhatsappUniqueID
                                };
                                whatsappSentList.Add(mlwatsappsent);

                                IBulkWhatsAppSending WhatsAppGeneralBaseFactory = Plumb5GenralFunction.WhatsAppGeneralBaseFactory.GetWhatsAppVendor(AdsId, whatsappConfiguration, "campaign");
                                SentStatus = WhatsAppGeneralBaseFactory.SendWhatsApp(whatsappSentList);
                                string ErrorMessage = WhatsAppGeneralBaseFactory.ErrorMessage;

                                if (SentStatus && WhatsAppGeneralBaseFactory.VendorResponses != null && WhatsAppGeneralBaseFactory.VendorResponses.Count > 0)
                                {
                                    Helper.Copy(WhatsAppGeneralBaseFactory.VendorResponses[0], watsAppSent);
                                    watsAppSent.P5WhatsappUniqueID = WhatsAppGeneralBaseFactory.VendorResponses[0].P5WhatsappUniqueID;
                                    watsAppSent.WhatsAppConfigurationNameId = whatsappConfiguration.WhatsAppConfigurationNameId;
                                    using (var objDL = DLWhatsAppSent.GetDLWhatsAppSent(AdsId, SqlProvider))
                                    {
                                        await objDL.Save(watsAppSent);
                                    }
                                }
                                else if (!SentStatus && !string.IsNullOrEmpty(ErrorMessage))
                                {
                                    Helper.Copy(WhatsAppGeneralBaseFactory.VendorResponses[0], watsAppSent);
                                    watsAppSent.P5WhatsappUniqueID = WhatsAppGeneralBaseFactory.VendorResponses[0].P5WhatsappUniqueID;
                                    watsAppSent.WhatsAppConfigurationNameId = whatsappConfiguration.WhatsAppConfigurationNameId;
                                    using (var objDL = DLWhatsAppSent.GetDLWhatsAppSent(AdsId, SqlProvider))
                                    {
                                        await objDL.Save(watsAppSent);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    ErrorUpdation.AddErrorLog("APIResponseSettingAction_WhatsAppOut", "Please check the phonenumber has not opted-in for whatsapp", "Unable to send WhatsAppOut as the phonenumber has not opted-in for whatsapp", DateTime.Now, "APIResponseSettingAction-->WhatsAppOut", "WhatsApp Cannot be sent");
                }
            }
            catch (Exception ex)
            {
                ErrorUpdation.AddErrorLog("APIResponseSettingAction --> WhatsAppOut ==> Account " + AdsId.ToString() + "", ex.Message.ToString(), "APIResponseSettingAction method", DateTime.Now, "APIResponseSettingAction-->WhatsAppOut", ex.ToString());
            }
        }

        public async void AutoAssignToGroup(int AssignToGroupId, int ContactId )
        {
            int[] ContactDetails = new int[1];
            ContactDetails[0] = ContactId;

            if (AssignToGroupId > 0)
            {
                int[] Groups = { AssignToGroupId };
                using (GeneralAddToGroups generalAddToGroups = new GeneralAddToGroups(AdsId, SqlProvider))
                {
                    await generalAddToGroups.AddToGroupMemberAndRespectiveModule(0, 0, Groups.ToArray(), ContactDetails);
                }
            }
        }

        public void SendWebHook(int WebHookId, Contact contact )
        {
            WebHookDetails webHookDetails = new WebHookDetails { WebHookId = WebHookId };
            Plumb5GenralFunction.WebHooksNew webHooksNew = new Plumb5GenralFunction.WebHooksNew(AdsId, "CaptureForm", 0, SqlProvider);
            webHooksNew.Send(webHookDetails, contact);
        }

        public async void AssignLeadToUserInfoUserId(int AssignLeadToUserInfoUserId, Int16 IsOverrideAssignment, Contact contact )
        {
            var objDL = DLContact.GetContactDetails(AdsId, SqlProvider);

            if (contact != null)
            {
                if (IsOverrideAssignment == 0)
                {
                    contact.UserInfoUserId = AssignLeadToUserInfoUserId;
                    await objDL.AssignSalesPerson(contact.ContactId, AssignLeadToUserInfoUserId);
                }
                else if (IsOverrideAssignment == 1)
                {
                    contact.UserInfoUserId = AssignLeadToUserInfoUserId;
                    await objDL.AssignSalesPerson(contact.ContactId, AssignLeadToUserInfoUserId);
                }
            }
        }
    }
}
