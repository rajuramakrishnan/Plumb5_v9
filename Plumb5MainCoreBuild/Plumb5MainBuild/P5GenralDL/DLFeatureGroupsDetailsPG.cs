﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLFeatureGroupsDetailsPG : CommonDataBaseInteraction, IDLFeatureGroupsDetails
    {
        CommonInfo connection;

        public DLFeatureGroupsDetailsPG()
        {
            connection = GetDBConnection();
        }

        public async Task<int> Save(FeatureGroupsDetails featureGroups)
        {
            string storeProcCommand = "select featuregroups_details_save(@FeatureId, @FeatureGroupId, @PricePerUnit)";
            object? param = new { featureGroups.FeatureId, featureGroups.FeatureGroupId, featureGroups.PricePerUnit };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<List<FeatureGroupsDetails>> GetFeatureGroupsDetails(int Id)
        {
            string storeProcCommand = "select * from featuregroups_details_get(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<FeatureGroupsDetails>(storeProcCommand, param)).ToList();
        }
        public async Task<bool> Delete(int FeatureGroupId)
        {
            string storeProcCommand = "select featuregroups_details_delete(@FeatureGroupId)";
            object? param = new { FeatureGroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<List<FeatureGroupsDetails>> GetFeatureDetails(int FeatureId, int FeatureGroupId)
        {
            string storeProcCommand = "select * from featuregroups_details_getfeaturedetails(@FeatureId, @FeatureGroupId)";
            object? param = new { FeatureId, FeatureGroupId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<FeatureGroupsDetails>(storeProcCommand, param)).ToList();
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
