﻿using Dapper;
using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLCampaignIdentifierPG : CommonDataBaseInteraction, IDLCampaignIdentifier
    {
        CommonInfo connection;
        public DLCampaignIdentifierPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public DLCampaignIdentifierPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<Int32> Save(CampaignIdentifier identifier)
        {
            string storeProcCommand = "select * from campaign_identifier_save(@UserGroupId,@UserInfoUserId,@Name,@CampaignDescription)";
            object? param = new { identifier.UserGroupId, identifier.UserInfoUserId, identifier.Name, identifier.CampaignDescription };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);
        }

        public async Task<bool> Update(CampaignIdentifier identifier)
        {
            string storeProcCommand = "select * from campaign_identifier_update(@Id,@Name,@CampaignDescription)";
            object? param = new { identifier.Id, identifier.Name, identifier.CampaignDescription };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }

        public async Task<bool> Archive(int Id)
        {
            string storeProcCommand = "select * from campaign_identifier_archive(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }

        public async Task<bool> ToogleStatus(CampaignIdentifier identifier)
        {
            string storeProcCommand = "select * from campaign_identifier_tooglestatus(@Id,@CampaignStatus)";
            object? param = new { identifier.Id, identifier.CampaignStatus };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }

        public async Task<List<CampaignIdentifier>> GetList(CampaignIdentifier identifier, int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from campaign_identifier_getlist(@UserInfoUserId,@OffSet,@FetchNext,@Id,@Name)";
            object? param = new { identifier.UserInfoUserId, OffSet, FetchNext, identifier.Id, identifier.Name };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<CampaignIdentifier>(storeProcCommand, param)).ToList();

        }

        public async Task<CampaignIdentifier?> Get(CampaignIdentifier identifier)
        {
            string storeProcCommand = "select * from campaign_identifier_get(@Id,@Name)";
            object? param = new { identifier.Id, identifier.Name };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<CampaignIdentifier?>(storeProcCommand, param);

        }

        public async Task<int> MaxCount(CampaignIdentifier identifier)
        {
            string storeProcCommand = "select * from campaign_identifier_maxcount(@UserInfoUserId,@Id,@Name)";
            object? param = new { identifier.UserInfoUserId, identifier.Id, identifier.Name };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<List<CampaignIdentifier>> GetCustomisedCampaignDetails(IEnumerable<int> ListOfId, List<string> fieldName = null)
        {
            string storeProcCommand = "select * from campaign_identifier_getlist(@ListOfId,@fieldName)";
            object? param = new { Action = "GetList", ListOfId = string.Join(",", new List<int>(ListOfId).ToArray()), fieldName = string.Join(",", fieldName.ToArray()) };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<CampaignIdentifier>(storeProcCommand, param)).ToList();

        }

        public async Task<List<CampaignIdentifier>> GetAllCampaigns(IEnumerable<int> ListOfId)
        {
            string storeProcCommand = "select * from campaign_identifier_getallcampaigns(@ListOfId)";
            object? param = new { ListOfId= string.Join(",", new List<int>(ListOfId).ToArray()) };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<CampaignIdentifier>(storeProcCommand, param)).ToList();

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
