using Newtonsoft.Json;
using P5GenralDL;
using P5GenralML;
using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Plumb5GenralFunction
{
    public class SendWebHookDetails : IDisposable
    {
        #region Declaration
        public string ErrorMessage { get; set; }

        readonly int AccountId;

        public List<MLWebHookResponsesDetails> webHookResponses { get; set; }

        public SendWebHookDetails(int accountId)
        {
            AccountId = accountId;
            webHookResponses = new List<MLWebHookResponsesDetails>();
        }

        public async void SendWebHookBulkDetails(List<WorkFlowBulkWebHook> workFlowebHookBulk, WorkFlowWebHook workFlowWebHook,string SqlProvider)
        {
            try
            {
                List<int> ContactIdList = workFlowebHookBulk.Select(x => x.ContactId).Distinct().ToList();

                List<Contact> contactList;
                using (var objdlcontact =   DLContact.GetContactDetails(AccountId, SqlProvider))
                {
                    contactList = await objdlcontact.GetAllContactList(ContactIdList, false);
                }

                if (contactList != null && contactList.Count() > 0)
                {
                    for (int i = 0; i < workFlowebHookBulk.Count(); i++)
                    {
                        Contact contact = contactList.Where(x => x.ContactId == workFlowebHookBulk[i].ContactId).FirstOrDefault();
                        PostEachWebHook(contact, workFlowebHookBulk[i].Id, workFlowebHookBulk[i].WebHookSendingSettingId, workFlowebHookBulk[i].WorkFlowId, workFlowebHookBulk[i].WorkFlowDataId, workFlowWebHook,SqlProvider);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message.ToString();

                webHookResponses.Clear();

                for (int k = 0; k < workFlowebHookBulk.Count(); k++)
                {
                    MLWebHookResponsesDetails errorresponsedetails = new MLWebHookResponsesDetails();
                    errorresponsedetails.Id = workFlowebHookBulk[k].Id;
                    errorresponsedetails.WebHookSendingSettingId = workFlowebHookBulk[k].WebHookSendingSettingId;
                    errorresponsedetails.ContactId = workFlowebHookBulk[k].ContactId;
                    errorresponsedetails.WorkFlowId = workFlowebHookBulk[k].WorkFlowId;
                    errorresponsedetails.WorkFlowDataId = workFlowebHookBulk[k].WorkFlowDataId;
                    errorresponsedetails.ErrorMessage = ex.Message.ToString();
                    errorresponsedetails.WebHookPostContent = null;
                    errorresponsedetails.WebHookResponseContent = null;
                    errorresponsedetails.SendStatus = 4;
                    errorresponsedetails.WebHookStatusCode = "";

                    webHookResponses.Add(errorresponsedetails);
                }

                using (ErrorUpdation objError = new ErrorUpdation("Plumb5GenralFunction_Method_SendWebHookBulkDetails_WorkFlow"))
                {
                    objError.AddError(ex.Message.ToString(), "", DateTime.Now.ToString(), "AccountId==>" + AccountId + ",ConfigureWebHookId==>" + workFlowWebHook.ConfigureWebHookId + ",WorkFlowId==>" + workFlowebHookBulk[0].WorkFlowId + ",WorkFlowDataId==>" + workFlowebHookBulk[0].WorkFlowDataId + "", ex.ToString(), true);
                }
            }
        }

        private async Task<bool> PostEachWebHook(Contact contact, long Id, int WebHookSendingSettingId, int WorkFlowId, int WorkFlowDataId, WorkFlowWebHook workFlowWebHook,string SqlProvider)
        {
            bool responseStatus = false;

            try
            {
                string username = "";
                string password = "";
                string jsonfieldsdetails = "";

                List<WebHookParameterDetails> headerdetails = new List<WebHookParameterDetails>();
                List<WebHookMappingFieldDetails> mappingfieldsdetails = new List<WebHookMappingFieldDetails>();

                mappingfieldsdetails = JsonConvert.DeserializeObject<List<WebHookMappingFieldDetails>>(workFlowWebHook.FieldMappingDetails);

                if (!string.IsNullOrEmpty(workFlowWebHook.Headers))
                {
                    if (workFlowWebHook.Headers.Length > 3)
                        headerdetails = JsonConvert.DeserializeObject<List<WebHookParameterDetails>>(workFlowWebHook.Headers);
                }

                if (!string.IsNullOrEmpty(workFlowWebHook.BasicAuthentication))
                {
                    WebHookBasicAuthenticationDetails authdetails = new WebHookBasicAuthenticationDetails();
                    authdetails = JsonConvert.DeserializeObject<WebHookBasicAuthenticationDetails>(workFlowWebHook.BasicAuthentication);

                    username = authdetails.AuthenticationKey;
                    password = authdetails.AuthenticationValue;
                }

                Dictionary<string, string> CustomList = new Dictionary<string, string>();

                try
                {
                    if (mappingfieldsdetails != null && mappingfieldsdetails.Count() > 0)
                    {
                        for (int j = 0; j < mappingfieldsdetails.Count(); j++)
                        {
                            if (mappingfieldsdetails[j].IsPlumb5OrCustomField == "Plumb5Field")
                            {
                                var contactProperty = contact.GetType().GetProperty(mappingfieldsdetails[j].Value);
                                CustomList.Add(mappingfieldsdetails[j].Key, contactProperty.GetValue(contact, null) as string);
                            }
                            else if (mappingfieldsdetails[j].IsPlumb5OrCustomField == "StaticField")
                            {
                                CustomList.Add(mappingfieldsdetails[j].Key, mappingfieldsdetails[j].Value);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    CustomList.Clear();
                    MLWebHookResponsesDetails errorresponsedetails = new MLWebHookResponsesDetails();
                    errorresponsedetails.Id = Id;
                    errorresponsedetails.WebHookSendingSettingId = WebHookSendingSettingId;
                    errorresponsedetails.ContactId = contact.ContactId;
                    errorresponsedetails.WorkFlowId = WorkFlowId;
                    errorresponsedetails.WorkFlowDataId = WorkFlowDataId;
                    errorresponsedetails.ErrorMessage = ex.Message.ToString();
                    errorresponsedetails.WebHookPostContent = null;
                    errorresponsedetails.WebHookResponseContent = null;
                    errorresponsedetails.SendStatus = 4;
                    errorresponsedetails.WebHookStatusCode = "";

                    webHookResponses.Add(errorresponsedetails);

                    responseStatus = false;

                    using (ErrorUpdation objError = new ErrorUpdation("Plumb5GenralFunction_PostEachWebHook_ContactMappingError_WorkFlow"))
                    {
                        objError.AddError(ex.Message.ToString(), "", DateTime.Now.ToString(), "AccountId==>" + AccountId + ",ConfigureWebHookId==>" + workFlowWebHook.ConfigureWebHookId + ",WorkFlowId==>" + WorkFlowId + ",WorkFlowDataId==>" + WorkFlowDataId + "", ex.ToString());
                    }
                }

                if (CustomList != null && CustomList.Count() > 0)
                {
                    jsonfieldsdetails = JsonConvert.SerializeObject(CustomList);

                    var client = new RestClient(workFlowWebHook.RequestURL);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("content-type", workFlowWebHook.ContentType);

                    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                        client.Authenticator = new HttpBasicAuthenticator(username, password);

                    if (headerdetails != null && headerdetails.Count() > 0)
                    {
                        for (int i = 0; i < headerdetails.Count(); i++)
                        {
                            request.AddHeader(headerdetails[i].Key, headerdetails[i].Value);
                        }
                    }

                    request.AddParameter("application/json", jsonfieldsdetails, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.PartialContent || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        responseStatus = true;

                        MLWebHookResponsesDetails responsedetails = new MLWebHookResponsesDetails();
                        responsedetails.Id = Id;
                        responsedetails.WebHookSendingSettingId = WebHookSendingSettingId;
                        responsedetails.ContactId = contact.ContactId;
                        responsedetails.WorkFlowId = WorkFlowId;
                        responsedetails.WorkFlowDataId = WorkFlowDataId;
                        responsedetails.ErrorMessage = "";
                        responsedetails.WebHookPostContent = jsonfieldsdetails;
                        responsedetails.SendStatus = 1;
                        responsedetails.WebHookStatusCode = "StatusCode=" + response.StatusCode + "";

                        if (response.Content != null && response.Content != "")
                            responsedetails.WebHookResponseContent = response.Content.ToString();

                        webHookResponses.Add(responsedetails);
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.PaymentRequired || response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.NotAcceptable)
                    {
                        responseStatus = false;

                        MLWebHookResponsesDetails responsedetails = new MLWebHookResponsesDetails();
                        responsedetails.Id = Id;
                        responsedetails.WebHookSendingSettingId = WebHookSendingSettingId;
                        responsedetails.ContactId = contact.ContactId;
                        responsedetails.WorkFlowId = WorkFlowId;
                        responsedetails.WorkFlowDataId = WorkFlowDataId;
                        responsedetails.WebHookPostContent = jsonfieldsdetails;
                        responsedetails.SendStatus = 0;
                        responsedetails.WebHookStatusCode = "StatusCode=" + response.StatusCode + "";

                        if (response.Content != null && response.Content != "")
                            responsedetails.ErrorMessage = responsedetails.WebHookResponseContent = response.Content.ToString();

                        Notifications notifications = new Notifications()
                        {
                            UserInfoUserId = contact.UserInfoUserId,
                            Heading = "Work Flow Web Hook API Error ConfigureWebHookId=>" + workFlowWebHook.ConfigureWebHookId + ",WorkFlowId=>" + WorkFlowId + ",WorkFlowDataId=>" + WorkFlowDataId + "",
                            Details = "StatusCode=" + response.StatusCode + "" + responsedetails.WebHookResponseContent,
                            PageUrl = "Work Flow Web Hook",
                            IsThatSeen = false,
                            ContactId = contact.ContactId
                        };

                        using (var objdl =   DLNotifications.GetDLNotifications(AccountId,SqlProvider))
                        {
                           await  objdl.Save(notifications);
                        }

                        webHookResponses.Add(responsedetails);

                        using (ErrorUpdation objError = new ErrorUpdation("Plumb5GenralFunction_PostEachWebHook_400SeriesError_WorkFlow"))
                        {
                            objError.AddError("StatusCode=" + response.StatusCode + "" + responsedetails.WebHookResponseContent, "", DateTime.Now.ToString(), "AccountId==>" + AccountId + ",ConfigureWebHookId==>" + workFlowWebHook.ConfigureWebHookId + ",WorkFlowId==>" + WorkFlowId + ",WorkFlowDataId==>" + WorkFlowDataId + "", "");
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError || response.StatusCode == HttpStatusCode.NotImplemented || response.StatusCode == HttpStatusCode.ServiceUnavailable || response.StatusCode == HttpStatusCode.GatewayTimeout || response.StatusCode == HttpStatusCode.BadGateway)
                    {
                        responseStatus = false;

                        MLWebHookResponsesDetails responsedetails = new MLWebHookResponsesDetails();
                        responsedetails.Id = Id;
                        responsedetails.WebHookSendingSettingId = WebHookSendingSettingId;
                        responsedetails.ContactId = contact.ContactId;
                        responsedetails.WorkFlowId = WorkFlowId;
                        responsedetails.WorkFlowDataId = WorkFlowDataId;
                        responsedetails.WebHookPostContent = jsonfieldsdetails;
                        responsedetails.SendStatus = 0;
                        responsedetails.WebHookStatusCode = "StatusCode=" + response.StatusCode + "";

                        if (response.Content != null && response.Content != "")
                            responsedetails.ErrorMessage = responsedetails.WebHookResponseContent = response.Content.ToString();

                        webHookResponses.Add(responsedetails);

                        using (ErrorUpdation objError = new ErrorUpdation("Plumb5GenralFunction_PostEachWebHook_500SeriesError_WorkFlow"))
                        {
                            objError.AddError("StatusCode=" + response.StatusCode + "" + responsedetails.WebHookResponseContent, "", DateTime.Now.ToString(), "AccountId==>" + AccountId + ",ConfigureWebHookId==>" + workFlowWebHook.ConfigureWebHookId + ",WorkFlowId==>" + WorkFlowId + ",WorkFlowDataId==>" + WorkFlowDataId + "", "");
                        }
                    }
                    else
                    {
                        responseStatus = false;

                        MLWebHookResponsesDetails responsedetails = new MLWebHookResponsesDetails();
                        responsedetails.Id = Id;
                        responsedetails.WebHookSendingSettingId = WebHookSendingSettingId;
                        responsedetails.ContactId = contact.ContactId;
                        responsedetails.WorkFlowId = WorkFlowId;
                        responsedetails.WorkFlowDataId = WorkFlowDataId;
                        responsedetails.WebHookPostContent = jsonfieldsdetails;
                        responsedetails.SendStatus = 0;
                        responsedetails.WebHookStatusCode = "StatusCode=NotAvailable";

                        if (response.Content != null && response.Content != "")
                            responsedetails.ErrorMessage = responsedetails.WebHookResponseContent = response.Content.ToString();

                        webHookResponses.Add(responsedetails);

                        using (ErrorUpdation objError = new ErrorUpdation("Plumb5GenralFunction_PostEachWebHook_StatusCodeNotAvailable_WorkFlow"))
                        {
                            objError.AddError("StatusCode=NotAvailable", "", DateTime.Now.ToString(), "AccountId==>" + AccountId + ",ConfigureWebHookId==>" + workFlowWebHook.ConfigureWebHookId + ",WorkFlowId==>" + WorkFlowId + ",WorkFlowDataId==>" + WorkFlowDataId + "", "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MLWebHookResponsesDetails errorresponsedetails = new MLWebHookResponsesDetails();
                errorresponsedetails.Id = Id;
                errorresponsedetails.WebHookSendingSettingId = WebHookSendingSettingId;
                errorresponsedetails.ContactId = contact.ContactId;
                errorresponsedetails.WorkFlowId = WorkFlowId;
                errorresponsedetails.WorkFlowDataId = WorkFlowDataId;
                errorresponsedetails.ErrorMessage = ex.Message.ToString();
                errorresponsedetails.WebHookPostContent = null;
                errorresponsedetails.WebHookResponseContent = null;
                errorresponsedetails.SendStatus = 4;
                errorresponsedetails.WebHookStatusCode = "";

                webHookResponses.Add(errorresponsedetails);

                responseStatus = false;

                using (ErrorUpdation objError = new ErrorUpdation("Plumb5GenralFunction_PostEachWebHook_WorkFlow"))
                {
                    objError.AddError(ex.Message.ToString(), "", DateTime.Now.ToString(), "AccountId==>" + AccountId + ",ConfigureWebHookId==>" + workFlowWebHook.ConfigureWebHookId + ",WorkFlowId==>" + WorkFlowId + ",WorkFlowDataId==>" + WorkFlowDataId + "", ex.ToString());
                }
            }

            return responseStatus;
        }

        #endregion Declaration

        #region Dispose Method

        bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                }
            }
            //dispose unmanaged resources
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion End of Dispose Method
    }

    public class WebHookParameterDetails
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }

    public class WebHookBasicAuthenticationDetails
    {
        public string AuthenticationKey { get; set; }

        public string AuthenticationValue { get; set; }
    }

    public class WebHookMappingFieldDetails
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public string IsPlumb5OrCustomField { get; set; }
    }
}
