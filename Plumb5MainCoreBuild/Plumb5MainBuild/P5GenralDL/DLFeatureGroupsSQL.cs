﻿using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IP5GenralDL;

namespace P5GenralDL
{
    public class DLFeatureGroupsSQL : CommonDataBaseInteraction, IDLFeatureGroups
    {
        CommonInfo connection;

        public DLFeatureGroupsSQL()
        {
            connection = GetDBConnection();
        }
        public async Task<int> MaxCount()
        {
            string storeProcCommand = "Feature_Groups";
            object? param = new { Action = "MaxCount" };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand);
        }

        public async Task<List<FeatureGroups>> GetReport(int Offset, int FetchNext)
        {
            string storeProcCommand = "Feature_Groups";
            object? param = new { Action = "Get", Offset, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<FeatureGroups>(storeProcCommand)).ToList();
        }

        public async Task<int> Save(FeatureGroups featureGroups)
        {
            string storeProcCommand = "Feature_Groups";
            object? param = new { Action = "Save", featureGroups.Name, featureGroups.Description, featureGroups.CurrencyType };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand);
        }

        public async Task<bool> Update(FeatureGroups featureGroups)
        {
            string storeProcCommand = "Feature_Groups";
            object? param = new { Action = "Update", featureGroups.Id, featureGroups.Name, featureGroups.Description, featureGroups.CurrencyType };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand) > 0;
        }

        public async Task<bool> UpdateDataPurgeSettings(int Id, string PugesettingValue)
        {
            string storeProcCommand = "Feature_Groups";
            object? param = new { Action = "UpdatePurgeSettings", Id, PugesettingValue };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand) > 0;
        }
        public async Task<bool> Delete(int Id)
        {
            string storeProcCommand = "Feature_Groups";
            object? param = new { Action = "Delete", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand) > 0;
        }

        public async Task<List<FeatureGroups>> GetFeatureGroupList()
        {
            string storeProcCommand = "Feature_Groups";
            object? param = new { Action = "GetFeatureGroupList" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<FeatureGroups>(storeProcCommand)).ToList();
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
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion End of Dispose Method
    }
}
