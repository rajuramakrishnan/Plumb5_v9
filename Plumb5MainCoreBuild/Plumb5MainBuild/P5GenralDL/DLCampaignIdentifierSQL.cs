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
    public class DLCampaignIdentifierSQL : CommonDataBaseInteraction, IDLCampaignIdentifier
    {
        CommonInfo connection;
        public DLCampaignIdentifierSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public DLCampaignIdentifierSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<Int32> Save(CampaignIdentifier identifier)
        {
            string storeProcCommand = "Campaign_Identifier";
            object? param = new { Action="Save", identifier.UserGroupId, identifier.UserInfoUserId, identifier.Name, identifier.CampaignDescription };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> Update(CampaignIdentifier identifier)
        {
            string storeProcCommand = "Campaign_Identifier";
            object? param = new { Action= "Update", identifier.Id, identifier.Name, identifier.CampaignDescription };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<bool> Archive(int Id)
        {
            string storeProcCommand = "Campaign_Identifier";
            object? param = new {Action= "Archive", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<bool> ToogleStatus(CampaignIdentifier identifier)
        {
            string storeProcCommand = "Campaign_Identifier";
            object? param = new { Action="ToogleStatus", identifier.Id, identifier.CampaignStatus };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<List<CampaignIdentifier>> GetList(CampaignIdentifier identifier, int OffSet, int FetchNext)
        {
            string storeProcCommand = "Campaign_Identifier";
            object? param = new { Action="GetList", identifier.UserInfoUserId, OffSet, FetchNext, identifier.Id, identifier.Name };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<CampaignIdentifier>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<CampaignIdentifier?> Get(CampaignIdentifier identifier)
        {
            string storeProcCommand = "Campaign_Identifier";
            object? param = new {Action="Get", identifier.Id, identifier.Name};

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<CampaignIdentifier?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<int> MaxCount(CampaignIdentifier identifier)
        {
            string storeProcCommand = "Campaign_Identifier";
            object? param = new { Action = "MaxCount", identifier.UserInfoUserId, identifier.Id, identifier.Name };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<CampaignIdentifier>> GetCustomisedCampaignDetails(IEnumerable<int> ListOfId, List<string> fieldName)
        {
            string storeProcCommand = "Campaign_Identifier";
            object? param = new { Action="GetList", ListOfId= string.Join(",", new List<int>(ListOfId).ToArray()), fieldName =string.Join(",", fieldName.ToArray())};

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<CampaignIdentifier>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
                        
        }

        public async Task<List<CampaignIdentifier>> GetAllCampaigns(IEnumerable<int> ListOfId)
        {
            string storeProcCommand = "Campaign_Identifier";
            object? param = new { Action="GetAllCampaigns", ListOfId= string.Join(",", new List<int>(ListOfId).ToArray()) };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<CampaignIdentifier>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

           
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
