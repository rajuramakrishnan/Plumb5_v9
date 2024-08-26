﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLFeatureGroupsDetailsSQL : CommonDataBaseInteraction, IDLFeatureGroupsDetails
    {
        CommonInfo connection;

        public DLFeatureGroupsDetailsSQL()
        {
            connection = GetDBConnection();
        }

        public async Task<int> Save(FeatureGroupsDetails featureGroups)
        {
            string storeProcCommand = "FeatureGroups_Details";
            object? param = new { Action = "Save", featureGroups.FeatureId, featureGroups.FeatureGroupId, featureGroups.PricePerUnit };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<FeatureGroupsDetails>> GetFeatureGroupsDetails(int Id)
        {
            string storeProcCommand = "FeatureGroups_Details";
            object? param = new { Action = "GET", Id };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<FeatureGroupsDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }
        public async Task<bool> Delete(int FeatureGroupId)
        {
            string storeProcCommand = "FeatureGroups_Details";
            object? param = new { Action = "Delete", FeatureGroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<List<FeatureGroupsDetails>> GetFeatureDetails(int FeatureId, int FeatureGroupId)
        {
            string storeProcCommand = "FeatureGroups_Details";
            object? param = new { Action = "GetFeatureDetails", FeatureId, FeatureGroupId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<FeatureGroupsDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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
