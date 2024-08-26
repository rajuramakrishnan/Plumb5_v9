using Newtonsoft.Json;
using P5GenralDL;
using P5GenralML;
using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Plumb5GenralFunction
{
    public class WebHooksNew
    {
        int AdsId;
        string CallingSource;
        int FormorChatId;
        string SqlProvider;

        public WebHooksNew(int adsId, string callingSource, int formorchatId,string _SqlProvider)
        {
            AdsId = adsId;
            CallingSource = callingSource;
            FormorChatId = formorchatId;
            SqlProvider = _SqlProvider;
        }

        public async void Send(WebHookDetails webHookDetails, Contact contact)
        {
            try
            {
                string username = "";
                string password = "";
                string jsonfieldsdetails = "";
                string requesturl = "";
                List<WebHookParameterDetails> headerdetails = new List<WebHookParameterDetails>();
                List<WebHookMappingFieldDetails> mappingfieldsdetails = new List<WebHookMappingFieldDetails>();

                if (webHookDetails != null && webHookDetails.WebHookId > 0)
                {
                    using (var dLWebHookDetails =   DLWebHookDetails.GetDLWebHookDetails(AdsId,SqlProvider))
                    {
                        webHookDetails =  await dLWebHookDetails.GetWebHookDetails(webHookDetails.WebHookId);
                    }

                    if (!string.IsNullOrEmpty(webHookDetails.Headers))
                    {
                        if (webHookDetails.Headers.Length > 3)
                            headerdetails = JsonConvert.DeserializeObject<List<WebHookParameterDetails>>(webHookDetails.Headers);
                    }

                    if (!string.IsNullOrEmpty(webHookDetails.BasicAuthentication))
                    {
                        WebHookBasicAuthenticationDetails authdetails = new WebHookBasicAuthenticationDetails();
                        authdetails = JsonConvert.DeserializeObject<WebHookBasicAuthenticationDetails>(webHookDetails.BasicAuthentication);

                        username = authdetails.AuthenticationKey;
                        password = authdetails.AuthenticationValue;
                    }
                    requesturl = webHookDetails.RequestURL;
                    if (!string.IsNullOrEmpty(requesturl) && requesturl.Contains("[{*") && requesturl.Contains("*}]"))
                    {
                        StringBuilder Urldata = new StringBuilder();
                        Urldata.Append(requesturl);
                        HelperForSMS HelpSMS = new HelperForSMS(AdsId,SqlProvider);
                        HelpSMS.ReplaceContactDetails(Urldata, contact);
                        requesturl = (Urldata.ToString().Contains("[{*") && Urldata.ToString().Contains("*}]")) ? checkcustomfield(Urldata.ToString()) : Urldata.ToString();
                    }
                    Dictionary<string, string> CustomList = new Dictionary<string, string>();
                    if (webHookDetails.ContentType.ToLower() == "raw body")
                    {
                        if (webHookDetails.RawBody.Contains("[{*") && webHookDetails.RawBody.Contains("*}]"))
                        {
                            StringBuilder Bodydata = new StringBuilder();
                            Bodydata.Append(webHookDetails.RawBody);
                            HelperForSMS HelpSMS = new HelperForSMS(AdsId,SqlProvider);
                            HelpSMS.ReplaceContactDetails(Bodydata, contact);
                            jsonfieldsdetails = (Bodydata.ToString().Contains("[{*") && Bodydata.ToString().Contains("*}]")) ? checkcustomfield(Bodydata.ToString()) : Bodydata.ToString();
                        }
                        else
                        {
                            jsonfieldsdetails = webHookDetails.RawBody;
                        }
                    }
                    else if (webHookDetails.ContentType.ToLower() == "form")
                    {
                        mappingfieldsdetails = JsonConvert.DeserializeObject<List<WebHookMappingFieldDetails>>(webHookDetails.FieldMappingDetails);

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

                    if (CustomList != null && CustomList.Count() > 0)
                    {
                        jsonfieldsdetails = JsonConvert.SerializeObject(CustomList);
                    }
                    if (!string.IsNullOrEmpty(requesturl))
                    {
                        var client = new RestClient(requesturl);
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("content-type", "application/json");

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

                        if (response != null)
                            SaveWebHookTrack(webHookDetails, response, requesturl, jsonfieldsdetails);
                    }
                }
            }
            catch (Exception ex)
            {
                using (ErrorUpdation objError = new ErrorUpdation("Plumb5GenralFunction_WebHooksNew_Send"))
                {
                    objError.AddError(ex.Message.ToString(), "", DateTime.Now.ToString(), "WebHooksNew", ex.ToString(), true);
                }
            }

        }
        public async virtual void SaveWebHookTrack(WebHookDetails webHookDetails, IRestResponse response, string RequestURL, string RequestBody)
        {
            try
            {
                using (var objDLWebHookTracker =   DLWebHookTracker.GetDLWebHookTracker(AdsId,SqlProvider))
                {
                    WebHookTracker webHookTracker = new WebHookTracker();
                    webHookTracker.FormorChatId = FormorChatId;
                    webHookTracker.WebHookId = webHookDetails.WebHookId;
                    webHookTracker.PostedUrl = RequestURL;
                    webHookTracker.RequestBody = RequestBody;
                    webHookTracker.CallingSource = CallingSource;
                    if (response.StatusDescription != null && response.StatusDescription != "")
                        webHookTracker.Response = response.StatusDescription.ToString();
                    webHookTracker.ResponseCode = Convert.ToDouble(response.StatusCode);
                    if (response.Content != null && response.Content != "")
                        webHookTracker.ResponseFromServer = response.Content.ToString();

                    await objDLWebHookTracker.Save(webHookTracker);
                }
            }
            catch (Exception ex)
            {
                using (ErrorUpdation objError = new ErrorUpdation("Plumb5GenralFunction_WebHooksNew_SaveWebHookTrack"))
                {
                    objError.AddError(ex.Message.ToString(), "", DateTime.Now.ToString(), "SaveWebHookTrack", ex.ToString(), true);
                }
            }

        }

        public string checkcustomfield(string getRawBody)
        {
            Contact contactDetails = new Contact();
            StringBuilder data = new StringBuilder();
            StringBuilder Body = new StringBuilder();
            Body.Append(getRawBody);
            string ReplacingValue = "";
            var contactMemberList = contactDetails.GetType().GetProperties().Select(x => new { Name = x.Name }).ToList();

            for (int i = 0; i < contactMemberList.Count; i++)
            {
                var Name = contactMemberList[i].Name;

                data.Clear().Append(Regex.Replace(Body.ToString(), @"\[\{\*" + Name + @"\*\}\]", ReplacingValue, RegexOptions.IgnoreCase));
                Body.Clear().Append(data);
            }
            data = null;
            return Body.ToString();
        }
    }
}
