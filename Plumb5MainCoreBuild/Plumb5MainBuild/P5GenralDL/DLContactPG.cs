﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using Microsoft.Identity.Client;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using Newtonsoft.Json;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;


namespace P5GenralDL
{
    public partial class DLContactPG : CommonDataBaseInteraction, IDLContact
    {
        CommonInfo connection;
        int AccountId;
        public DLContactPG(int adsId)
        {
            connection = GetDBConnection(adsId);
            AccountId = adsId;
        }

        public DLContactPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(Contact contact, ContactMergeConfiguration? mergeConfiguration = null)
        {

            using (var dl = DLContactMergeConfiguration.GetDLContactMergeConfiguration(AccountId, "npgsql"))
            {
                mergeConfiguration = await dl.GetSettingDetails();
            }
            //Checking EmailVerifyProviderSetting
            EmailVerifyProviderSetting? emailVerifyProviderSetting = new EmailVerifyProviderSetting();
            using (var obj = DLEmailVerifyProviderSetting.GetDLEmailVerifyProviderSetting(AccountId, "npgsql"))
            {
                emailVerifyProviderSetting = await obj.GetActiveprovider();
            }

            if (emailVerifyProviderSetting == null)
                contact.IsVerifiedMailId = 1;

            List<Contact> contactObject = new List<Contact>();
            contactObject.Add(contact);
            DataTable contacts = new DataTable();
            contacts = ToDataTables(contactObject);
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in contacts.Columns)
                column.ColumnName = ti.ToLower(column.ColumnName);
            contacts.Columns.Add("action", typeof(string));
            contacts.Columns.Add("primaryemail", typeof(bool));
            contacts.Columns.Add("primarysms", typeof(bool));
            contacts.Columns.Add("alternateemail", typeof(bool));
            contacts.Columns.Add("alternatesms", typeof(bool));
            contacts.Rows[0]["action"] = "save";
            contacts.Rows[0]["primaryemail"] = mergeConfiguration.PrimaryEmail;
            contacts.Rows[0]["primarysms"] = mergeConfiguration.PrimarySMS;
            contacts.Rows[0]["alternateemail"] = mergeConfiguration.AlternateEmail;
            contacts.Rows[0]["alternatesms"] = mergeConfiguration.AlternateSMS;

            string storeProcCommand = "select * from contact_details_save(@contacts)";
            object? param = new { contacts = new JsonParameter(JsonConvert.SerializeObject(contacts)) };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool> Update(Contact contact, ContactMergeConfiguration? mergeConfiguration = null)
        {

            using (var dl = DLContactMergeConfiguration.GetDLContactMergeConfiguration(AccountId, "npgsql"))
            {
                mergeConfiguration = await dl.GetSettingDetails();
            }
            List<Contact> contactObject = new List<Contact>();
            contactObject.Add(contact);
            DataTable contacts = new DataTable();
            contacts = ToDataTables(contactObject);
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in contacts.Columns)
                column.ColumnName = ti.ToLower(column.ColumnName);
            contacts.Columns.Add("action", typeof(string));
            contacts.Columns.Add("primaryemail", typeof(bool));
            contacts.Columns.Add("primarysms", typeof(bool));
            contacts.Columns.Add("alternateemail", typeof(bool));
            contacts.Columns.Add("alternatesms", typeof(bool));
            contacts.Rows[0]["action"] = "save";
            contacts.Rows[0]["primaryemail"] = mergeConfiguration.PrimaryEmail;
            contacts.Rows[0]["primarysms"] = mergeConfiguration.PrimarySMS;
            contacts.Rows[0]["alternateemail"] = mergeConfiguration.AlternateEmail;
            contacts.Rows[0]["alternatesms"] = mergeConfiguration.AlternateSMS;

            string storeProcCommand = "select * from contact_details_save(@contacts)";
            object? param = new { contacts = new JsonParameter(JsonConvert.SerializeObject(contacts)) };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }
        public async Task<IEnumerable<Contact>> GET(Contact contact, int FetchNext, int OffSetFetchIndex, int AgeRange1, int AgeRange2, string ContactListOfId, List<string> fieldName = null, bool? IsPhoneContact = null, int GroupId = 0)
        {
            try
            {
                string action = IsPhoneContact == null ? "getallcontact" : !Convert.ToBoolean(IsPhoneContact) ? "get" : "getphonecontacts";
                string filenames = fieldName != null ? string.Join(",", fieldName.ToArray()) : null;
                string storeProcCommand = "select * from contact_details_getallcontacts( @action, @ContactId,@EmailId,@PhoneNumber,@Gender,@MaritalStatus,@Education,@Occupation,@Interests,@Location,@IsVerifiedMailId,@IsVerifiedContactNumber,@LookedupStatus,@CustomField1,@CustomField2,@CustomField3,@CustomField4,@CustomField5,@CustomField6,@CustomField7,@CustomField8,@CustomField9,@CustomField10,@CustomField11,@CustomField12,@CustomField13,@CustomField14,@CustomField15,@CustomField16,@CustomField17,@CustomField18,@CustomField19,@CustomField20, @FetchNext,  @OffSetFetchIndex,  @AgeRange1,  @AgeRange2,  @ContactListOfId , @filenames,@GroupId,@Unsubscribe,@ReferType, @USSDUnsubscribe,@IsSmsUnsubscribe,@OptInOverAllNewsLetter,@Country,@SmsOptInOverAllNewsLetter,@USSDOptInOverAllNewsLetter,@UtmTagSource,@FirstUtmMedium,@FirstUtmCampaign,@FirstUtmTerm,@FirstUtmContent)";

                object? param = new
                {
                    action,
                    contact.ContactId,
                    contact.EmailId,
                    contact.PhoneNumber,
                    contact.Gender,
                    contact.MaritalStatus,
                    contact.Education,
                    contact.Occupation,
                    contact.Interests,
                    contact.Location,
                    contact.IsVerifiedMailId,
                    contact.IsVerifiedContactNumber,
                    contact.LookedupStatus,
                    contact.CustomField1,
                    contact.CustomField2,
                    contact.CustomField3,
                    contact.CustomField4,
                    contact.CustomField5,
                    contact.CustomField6,
                    contact.CustomField7,
                    contact.CustomField8,
                    contact.CustomField9,
                    contact.CustomField10,
                    contact.CustomField11,
                    contact.CustomField12,
                    contact.CustomField13,
                    contact.CustomField14,
                    contact.CustomField15,
                    contact.CustomField16,
                    contact.CustomField17,
                    contact.CustomField18,
                    contact.CustomField19,
                    contact.CustomField20,
                    FetchNext,
                    OffSetFetchIndex,
                    AgeRange1,
                    AgeRange2,
                    ContactListOfId,
                    filenames,
                    GroupId,
                    contact.Unsubscribe,
                    contact.ReferType,
                    contact.USSDUnsubscribe,
                    contact.IsSmsUnsubscribe,
                    contact.OptInOverAllNewsLetter,
                    contact.Country,
                    contact.SmsOptInOverAllNewsLetter,
                    contact.USSDOptInOverAllNewsLetter,
                    contact.UtmTagSource,
                    contact.FirstUtmMedium,
                    contact.FirstUtmCampaign,
                    contact.FirstUtmTerm,
                    contact.FirstUtmContent
                };


                using var db = GetDbConnection(connection.Connection);
                return await db.QueryAsync<Contact>(storeProcCommand, param);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<Contact?> GetDetails(Contact contact, List<string> fieldName = null, bool IsPhoneContact = false)
        {
            string filenames = fieldName != null ? string.Join(",", fieldName.ToArray()) : null;
            string action = !IsPhoneContact ? "get" : "getphonecontacts";
            string storeProcCommand = "select * from contact_details_getcontact(@action, @ContactId, @EmailId, @PhoneNumber, @Gender, @MaritalStatus, @Education, @Occupation, @Interests, @Location, @IsVerifiedMailId, @IsVerifiedContactNumber, @LookedupStatus, @CustomField1, @CustomField2, @CustomField3, @CustomField4, @CustomField5, @CustomField6, @CustomField7, @CustomField8, @CustomField9, @CustomField10, @CustomField11, @CustomField12, @CustomField13, @CustomField14, @CustomField15, @CustomField16, @CustomField17, @CustomField18, @CustomField19, @CustomField20, @filenames, @Unsubscribe, @USSDUnsubscribe, @IsSmsUnsubscribe, @OptInOverAllNewsLetter, @SmsOptInOverAllNewsLetter, @USSDOptInOverAllNewsLetter, @UtmTagSource, @FirstUtmMedium, @FirstUtmCampaign, @FirstUtmTerm, @FirstUtmContent)";
            object? param = new { action, contact.ContactId, contact.EmailId, contact.PhoneNumber, contact.Gender, contact.MaritalStatus, contact.Education, contact.Occupation, contact.Interests, contact.Location, contact.IsVerifiedMailId, contact.IsVerifiedContactNumber, contact.LookedupStatus, contact.CustomField1, contact.CustomField2, contact.CustomField3, contact.CustomField4, contact.CustomField5, contact.CustomField6, contact.CustomField7, contact.CustomField8, contact.CustomField9, contact.CustomField10, contact.CustomField11, contact.CustomField12, contact.CustomField13, contact.CustomField14, contact.CustomField15, contact.CustomField16, contact.CustomField17, contact.CustomField18, contact.CustomField19, contact.CustomField20, filenames, contact.Unsubscribe, contact.USSDUnsubscribe, contact.IsSmsUnsubscribe, contact.OptInOverAllNewsLetter, contact.SmsOptInOverAllNewsLetter, contact.USSDOptInOverAllNewsLetter, contact.UtmTagSource, contact.FirstUtmMedium, contact.FirstUtmCampaign, contact.FirstUtmTerm, contact.FirstUtmContent };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<Contact?>(storeProcCommand, param);

        }

        public async Task<IEnumerable<Contact>> GetContactIds(List<Contact> contact, int OffSet, int FetchNext, List<string> fieldName, bool IsPhoneContact = false)
        {
            string action = !IsPhoneContact ? "get" : "getphonecontacts";
            string emailidslist = string.Join(",", contact.Where(x => x.EmailId != null).Select(x => x.EmailId));
            string phonenumberlist = string.Join(",", contact.Where(x => x.PhoneNumber != null).Select(x => x.PhoneNumber));
            string memberidlist = string.Join(",", contact.Where(x => x.PhoneNumber != null).Select(x => x.PhoneNumber));
            string fieldNames = fieldName != null ? string.Join(",", fieldName.ToArray()) : null;
            string storeProcCommand = "select * from contact_details_get(@action, @OffSet, @FetchNext, @emailidslist, @phonenumberlist, @memberidlist, @fieldNames)";
            object? param = new { action, OffSet, FetchNext, emailidslist, phonenumberlist, memberidlist, fieldNames };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<Contact>(storeProcCommand, param);
        }

        public async Task<Contact?> GetContactDetails(Contact contact, List<string> fieldName = null)
        {
            string fieldnames = fieldName != null ? string.Join(",", fieldName.ToArray()) : null;
            string storeProcCommand = "select * from contact_details_getdetails(@ContactId,@EmailId,@PhoneNumber,@fieldnames)";
            object? param = new { contact.ContactId, contact.EmailId, contact.PhoneNumber, fieldnames };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<Contact?>(storeProcCommand, param);
        }

        public async Task<List<Contact>> GetAllContactList(List<int> ContactIds, bool IsPhoneContact = false)
        {
            string action = !IsPhoneContact ? "get" : "getphonecontacts";
            string contactids = string.Join(",", ContactIds);
            string storeProcCommand = "select * from contact_details_getbyaction(@action,@contactids)";

            object? param = new { action, contactids };
            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Contact>(storeProcCommand, param)).ToList();
        }

        //Separate Method


        public async Task<IEnumerable<Groups>> BelongToWhichGroup(int ContactId)
        {
            string storeProcCommand = "select * from contact_customdetails_belongtowhichgroup(@ContactId)";
            object? param = new { ContactId };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<Groups>(storeProcCommand, param);
        }
        public async Task<bool> UpdateVerification(int ContactId, int IsVerifiedMailId)
        {
            string storeProcCommand = "select * from contact_customdetails_updateverificationstatus(@ContactId, @IsVerifiedMailId)";
            object? param = new { ContactId, IsVerifiedMailId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }


        public async Task<IEnumerable<MLContacts>> GetContactForVerification(int GroupId)
        {
            string storeProcCommand = "select * from contact_customdetails_getallemailsforverification(@GroupId)";

            object? param = new { GroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLContacts>(storeProcCommand, param);
        }

        public async Task<bool> SearchAndAddtoGroup(int UserInfoUserId, int UserGroupId, Contact contact, int StartCount, int EndCount, int GroupId, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime)
        {
            List<Contact> contactObject = new List<Contact>();
            contactObject.Add(contact);
            DataTable contacts = new DataTable();
            contacts = ToDataTables(contactObject);
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in contacts.Columns)
                column.ColumnName = ti.ToLower(column.ColumnName);

            string storeProcCommand = "select * from mail_searchandaddtogroup_inserttorequestedgroup(@UserInfoUserId, @UserGroupId, @StartCount, @EndCount, @FromDateTime, @ToDateTime, @GroupId, @contacts)";
            object? param = new { UserInfoUserId, UserGroupId, StartCount, EndCount, FromDateTime, ToDateTime, GroupId, contacts = new JsonParameter(JsonConvert.SerializeObject(contacts)) };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> AddToUnsubscribeList(int[] contact)
        {
            bool isDataInserted = false;
            foreach (var eachContact in contact)
            {
                string storeProcCommand = "select * from contact_customdetails_addtounsubscribelist(@eachContact)";

                object? param = new { eachContact };

                using var db = GetDbConnection(connection.Connection);
                isDataInserted = await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
            }
            return isDataInserted;
        }

        public async Task<bool> AddToSmsUnsubscribeList(int[] contact)
        {
            bool isDataInserted = false;
            foreach (var eachContact in contact)
            {
                string storeProcCommand = "select * from contact_customdetails_addtosmsunsubscribelist(@eachContact)";

                object? param = new { eachContact };

                using var db = GetDbConnection(connection.Connection);
                isDataInserted = await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
            }
            return isDataInserted;
        }

        public async Task<bool> AddToInvalidateList(int[] contact)
        {
            bool isDataInserted = false;
            foreach (var eachContact in contact)
            {
                string storeProcCommand = "select * from contact_customdetails_addtoinvalidatelist(@eachContact)";
                object? param = new { eachContact };

                using var db = GetDbConnection(connection.Connection);
                isDataInserted = await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
            }
            return isDataInserted;
        }

        public async Task<bool> SmsUnSubscribe(string PhoneNumber)
        {
            string storeProcCommand = "select * from contact_details_smsunsubscribe(@PhoneNumber)";
            object? param = new { PhoneNumber };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        //For LmsLeads 

        public List<Contact> GetListByContactIdTable(DataTable CampaignContactId, List<string> fieldNames)
        {
            string fieldname = fieldNames != null ? string.Join(",", fieldNames.Select(s => s.ToLowerInvariant()).ToArray()) : null;
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in CampaignContactId.Columns)
                column.ColumnName = ti.ToLower(column.ColumnName);
            string jsonData = JsonConvert.SerializeObject(CampaignContactId, Formatting.Indented);
            string storeProcCommand = fieldNames != null ? "select * from campaigncontact_details_getlistbycontactidtablebyfieldname(@CampaignContactIds,@fieldname)" : "select * from campaigncontact_details_getlistbycontactidtable(@CampaignContactId,@fieldname)";

            object? param = new
            {
                CampaignContactIds = new JsonParameter(jsonData),

                fieldname
            };
            using var db = GetDbConnection(connection.Connection);
            return (db.Query<Contact>(storeProcCommand, param)).ToList();
        }

        public async Task<Int32> MaxCount(Contact contact, Int32? GroupId = null)
        {
            string storeProcCommand = "select * from contact_new_details_contactscount(@EmailId,@PhoneNumber,@GroupId,@Name)";
            object? param = new { contact.EmailId, contact.PhoneNumber, GroupId, contact.Name };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<IEnumerable<Contact>> GetAllContact(Contact contact, int FetchNext, int OffSet, Int32? GroupId = null, List<string> fieldsName = null)
        {
            string storeProcCommand = "select * from  contact_new_details_contactsdata(@FetchNext, @OffSet, @EmailId, @PhoneNumber, @GroupId, @Name)";
            object? param = new { FetchNext, OffSet, contact.EmailId, contact.PhoneNumber, GroupId, contact.Name };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<Contact>(storeProcCommand, param);
        }

        public async Task<Contact?> GetContactDetailsByEmailIdPhoneNumber(Contact contact)
        {
            string storeProcCommand = "select * from  contact_details_getcontactdetailsbyemailidphonenumber(@EmailId, @PhoneNumber) ";
            object? param = new { contact.EmailId, contact.PhoneNumber };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<Contact?>(storeProcCommand, param);
        }

        public async Task<IEnumerable<Contact>> CheckEmailOrPhoneExistence(string EmailId, string PhoneNumber, List<string> FieldNames)
        {
            string FieldName = FieldNames != null ? string.Join(",", FieldNames.ToArray()) : null;
            string storeProcCommand = "select * from contact_details_checkemailorphoneexistence(@EmailId, @PhoneNumber,@FieldName)";
            object? param = new { EmailId, PhoneNumber, FieldName };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<Contact>(storeProcCommand, param);
        }

        public async Task<Int32> FacebookContactsMaxCount(Contact contact, Int32? GroupId = null)
        {

            string storeProcCommand = "select * from contact_new_details_facebookcontactsmaxcount(@EmailId, @PhoneNumber, @GroupId, @Name, @FacebookUrl )";
            object? param = new { contact.EmailId, contact.PhoneNumber, GroupId, contact.Name, contact.FacebookUrl };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<IEnumerable<Contact>> FacebookContactDetails(Contact contact, int FetchNext, int OffSet, Int32? GroupId = null, List<string> fieldsName = null)
        {


            string storeProcCommand = "select * from contact_new_details_facebookcontactdetails( @FetchNext,@OffSet,@EmailId, @PhoneNumber, @GroupId, @Name, @FacebookUrl)";
            object? param = new { FetchNext, OffSet, contact.EmailId, contact.PhoneNumber, GroupId, contact.Name, contact.FacebookUrl };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<Contact>(storeProcCommand, param);
        }

        public async Task<bool> MakeItNotVerified(int ContactId, int IsVerifiedMailId)
        {
            return await UpdateVerification(ContactId, IsVerifiedMailId);
        }

        public async Task<Int32> CheckEmailIdPhoeNumberExists(string EmailId, string PhoneNumber)
        {
            string storeProcCommand = "select * from contact_details_lms_checkcontactdetailsforrepeatlead(@EmailId, @PhoneNumber)";
            object? param = new { EmailId, PhoneNumber };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        private static DataTable ToDataTables<T>(IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prp = props[i];
                table.Columns.Add(prp.Name, Nullable.GetUnderlyingType(prp.PropertyType) ?? prp.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public async Task<Contact?> GetContactDetailsAsync(Contact contact, List<string> fieldName = null)
        {
            string fieldnames = fieldName != null ? string.Join(",", fieldName.ToArray()) : null;
            string storeProcCommand = "select * from contact_details_getdetails(@ContactId,@EmailId, @PhoneNumber,@fieldnames)";
            object? param = new { contact.ContactId, contact.EmailId, contact.PhoneNumber, fieldnames };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<Contact>(storeProcCommand, param);
        }

        public async Task<bool> UpdateVerificationAsync(int ContactId, int IsVerifiedMailId)
        {
            string storeProcCommand = "select * from contact_customdetails_updateverificationstatus(@ContactId, @IsVerifiedMailId)";
            object? param = new { ContactId, IsVerifiedMailId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }
        public async Task<bool> UpdateIsVerifiedMailId(int GroupId)
        {
            string storeProcCommand = "select * from contact_customdetails_updateisverifiedmailid(@GroupId)";
            object? param = new { GroupId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }
        public async Task<Int32> MailUnSubscribeMaxCount(int GroupId)
        {
            string storeProcCommand = "select * from contact_unsubscribe_maxcount(@GroupId)";
            object? param = new { GroupId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<IEnumerable<Contact>> GetMailUnSubscribeDetails(int OffSet, int FetchNext, int GroupId)
        {
            string storeProcCommand = "select * from contact_unsubscribe_getdetails( @OffSet, @FetchNext, @GroupId)";
            object? param = new { OffSet, FetchNext, GroupId };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<Contact>(storeProcCommand, param);
        }
        public async Task<bool> UpdateMailUnSubscribedContact(int contactid)
        {
            string storeProcCommand = "select * from contact_unsubscribe_delete(@contactid)";
            object? param = new { contactid };
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
