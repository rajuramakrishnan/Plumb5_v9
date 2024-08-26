using Microsoft.Identity.Client;
using P5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Plumb5GenralFunction
{
    public class EventSaveResponse
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public int Total { get; set; }
        public int Inserted { get; set; }
        public int Rejected { get; set; }
        public int PartialyInserted { get; set; }
        public List<ContactResponse> ResponseDetails { get; set; }
        public List<SuccessResponse> successResponseDetails { get; set; }
    }

    public class ContactSaveResponse
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public int Total { get; set; }
        public int Inserted { get; set; }
        public int Rejected { get; set; }
        public int PartialyInserted { get; set; }
        public int GroupAddSuccessCount { get; set; }
        public int GroupAddRejectCount { get; set; }
        public int LmsGroupAddSuccessCount { get; set; }
        public int LmsGroupAddRejectCount { get; set; }
        public List<ContactResponse> ResponseDetails { get; set; }
        public List<SuccessResponse> successResponseDetails { get; set; }
    }

    public class SuccessResponse
    {
        public int ContactId { get; set; }
        public Int64 GroupMemberId { get; set; }
    }

    public class ContactResponse
    {
        public int RowNumber { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public bool IsHardBounce { get; set; }
        public string Reason { get; set; }
    }

    public class LastGroupDetails
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }

    public class GenralContactImport
    {
        int AdsId, UserInfoUserId, UserGroupId;
        private readonly string? SQLProvider;

        public GenralContactImport(int adsId, int userInfoUserId, int userGroupId, bool AssociateContactsToGroup, string? SqlProvider = null)
        {
            SQLProvider = SqlProvider;
            UserInfoUserId = userInfoUserId;
            UserGroupId = userGroupId;
            AdsId = adsId;
        }

        public async Task<Tuple<int, bool, string, Int64>> SaveAddToGroup(Contact contact, int GroupId = 0)
        {
            int ContactId = 0;
            bool IsInsertedToGroup = false;
            string GroupRejectReason = string.Empty;
            Int64 GroupInsertedId = 0;
            try
            {
                try
                {
                    using var bLContact = DLContact.GetContactDetails(AdsId, SQLProvider);

                    ContactId = await bLContact.Save(contact);
                    contact.ContactId = ContactId;
                }
                catch (Exception ex)
                {
                    using (ErrorUpdation objError = new ErrorUpdation("GetContactDetails"))
                    {
                        objError.AddError(ex.Message.ToString(), "", DateTime.Now.ToString(), "GenralContactImport->SaveAddToGroup->SaveContact", "");
                    }
                }
                if (GroupId > 0 && ContactId > 0)
                {
                    try
                    {
                        int[] Groups = { GroupId };
                        int[] Contacts = { ContactId };
                        Int64 GroupMemberId;
                        using (GeneralAddToGroups generalAddToGroups = new GeneralAddToGroups(AdsId, SQLProvider))
                        {
                            Tuple<List<Int64>, List<Int64>, List<Int64>> tuple = await generalAddToGroups.AddToGroupMemberAndRespectiveModule(UserInfoUserId, UserGroupId, Groups, Contacts);
                            if (tuple.Item1 != null && tuple.Item1.Count > 0 && tuple.Item1[0] > 0)
                                GroupMemberId = tuple.Item1[0];
                            else
                                GroupMemberId = tuple.Item1[0];
                        }

                        if (GroupMemberId > 0)
                        {
                            IsInsertedToGroup = true;
                            GroupInsertedId = GroupMemberId;
                            GroupRejectReason = "";
                        }
                        else
                        {
                            IsInsertedToGroup = false;
                            switch (GroupMemberId)
                            {
                                case -1:
                                    GroupRejectReason = "Contact already present in given group.";
                                    break;
                                case -2:
                                    GroupRejectReason = "Contact already aresent in other group.";
                                    break;
                                case -3:
                                    GroupRejectReason = "Contact not present in system.";
                                    break;
                                default:
                                    GroupRejectReason = "Not Identified.";
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        IsInsertedToGroup = false;
                        GroupRejectReason = ex.Message.ToString();
                        using (ErrorUpdation objError = new ErrorUpdation("ContactsUpload"))
                        {
                            objError.AddError(ex.Message.ToString(), "", DateTime.Now.ToString(), "GenralContactImport->SaveAddToGroup->AddToGroup", "");
                        }
                    }
                }
                else
                {
                    IsInsertedToGroup = false;
                    GroupRejectReason = "ContactId and GroupId should be greater than 0.";
                }
            }
            catch (Exception ex)
            {
                IsInsertedToGroup = false;
                GroupRejectReason = ex.Message.ToString();
                using (ErrorUpdation objError = new ErrorUpdation("ContactsUpload"))
                {
                    objError.AddError(ex.Message.ToString(), "", DateTime.Now.ToString(), "GenralContactImport->SaveAddToGroup", ex.StackTrace.ToString());
                }
            }
            return Tuple.Create(ContactId, IsInsertedToGroup, GroupRejectReason, GroupInsertedId);
        }

        
        
    }
}
