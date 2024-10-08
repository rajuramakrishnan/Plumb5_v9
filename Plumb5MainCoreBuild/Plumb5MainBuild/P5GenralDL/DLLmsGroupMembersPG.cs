﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using Newtonsoft.Json;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace P5GenralDL
{
    internal class DLLmsGroupMembersPG : CommonDataBaseInteraction, IDLLmsGroupMembers
    {
        CommonInfo connection;
        public DLLmsGroupMembersPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLmsGroupMembersPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(LmsGroupMembers groupMember, bool OverrideSources = false)
        {
            string storeProcCommand = "select contact_lms_groupmembers_addtogroup(@LmsGroupId,@ContactId,@OverrideSources)";

            object? param = new { groupMember.LmsGroupId, groupMember.ContactId, OverrideSources };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);
        }

        public async Task<bool> UpdateFollowUp(int LmsGroupmembersIds, int LmsGroupId, string FollowUpContent, Int16 FollowUpStatus, DateTime FollowUpdate, int FollowUpUserId)
        {
            string storeProcCommand = "select lmsgroup_updatefollowups(@LmsGroupmembersIds, @LmsGroupId, @FollowUpContent, @FollowUpStatus, @FollowUpdate, @FollowUpUserId)";
            object? param = new { LmsGroupmembersIds, LmsGroupId, FollowUpContent, FollowUpStatus, FollowUpdate, FollowUpUserId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param) > 0;
        }

        public async Task<Int32> CheckAndSaveLmsGroup(int contactid, int userinfouserid, int lmsgroupid, int sourcetype, string lmscustomfields = null, int lmsgrpmemberid = 0, int score = 0, string label = null, string publisher = null)
        {
            string storeProcCommand = "select contact_check_lms_groupmembers(@contactid, @userinfouserid, @lmsgroupid, @sourcetype, @lmscustomfields, @lmsgrpmemberid, @score, @label, @publisher)";
            object? param = new { contactid, userinfouserid, lmsgroupid, sourcetype, lmscustomfields, lmsgrpmemberid, score, label, publisher };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);
        }

        public async Task<Int32> CheckLmsSource(int contactid, int lmsgroupid, int sourcetype)
        {
            string storeProcCommand = "select contact_sourcecheck(@contactid, @lmsgroupid, @sourcetype )";
            object? param = new { contactid, lmsgroupid, sourcetype };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);
        }

        public async Task<Int32> CheckLmsgrpUserId(int contactid, int lmsgroupid)
        {
            string storeProcCommand = "select lmsgroupmember_useridcheck(@contactid, @lmsgroupid)";
            object? param = new { contactid, lmsgroupid };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);
        }

        public async Task<LmsGroupMembers?> GetLmsDetails(int lmsgroupmemberid = 0, int contactid = 0)
        {
            string storeProcCommand = "select * from lmsgroupmembers_get_details(@lmsgroupmemberid, @contactid)";
            object? param = new { lmsgroupmemberid, contactid };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<LmsGroupMembers?>(storeProcCommand, param);

        }

        public List<LmsGroupMembers> GetLmsDetailsList(DataTable LmsCampaignContactId)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in LmsCampaignContactId.Columns)
                column.ColumnName = ti.ToLower(column.ColumnName);
            string jsonData = JsonConvert.SerializeObject(LmsCampaignContactId, Formatting.Indented);
            string storeProcCommand = "select * from lmsgroupmembers_get_details_list (@LmsCampaignContactIds)";
            object? param = new { LmsCampaignContactIds=new JsonParameter(jsonData) };

            using var db = GetDbConnection(connection.Connection);
            return db.Query<LmsGroupMembers>(storeProcCommand, param).ToList();
        }

        public async Task<IEnumerable<MLLeadsDetails>> GetLmsGrpDetailsByContactId(int ContactId, int lmsgroupid)
        {
            string storeProcCommand = "select * from lms_grpdetails_get(@ContactId, @lmsgroupid)";
            object? param = new { ContactId, lmsgroupid };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLLeadsDetails>(storeProcCommand, param);
        }

        public async Task<IEnumerable<int>> LmsGrpGetUserInfoList(int ContactId)
        {
            List<int> FormIdList = new List<int>();
            string storeProcCommand = "select lmsgroupmembers_getuserdetails(@ContactId)";
            object? param = new { ContactId };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param);

            while (reader.Read())
            {
                FormIdList.Add(Convert.ToInt32(reader["FormId"]));
            }
            return FormIdList;
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
                    connection = null;
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
