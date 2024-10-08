﻿using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using IP5GenralDL;

namespace P5GenralDL
{
    public class DLFeatureSQL : CommonDataBaseInteraction, IDLFeature
    {
        CommonInfo connection;

        public DLFeatureSQL()
        {
            connection = GetDBConnection();
        }

        public async Task<Int16> SaveDetails(Feature feature)
        {
            string storeProcCommand = "Feature_Details";
            object? param = new { Action = "GET", feature.Name, feature.FeatureUnitTypeId, feature.PricePerUnit, feature.MinUnitValue, feature.MaxUnitValue, feature.PurchaseLink, feature.IsMainFeature, feature.ApplicationPath, feature.DisplayNameInDashboard, feature.PricePerUnitInINR };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateDetails(Feature feature)
        {
            string storeProcCommand = "Feature_Details";
            object? param = new { Action = "Update", feature.Id, feature.Name, feature.FeatureUnitTypeId, feature.PricePerUnit, feature.MinUnitValue, feature.MaxUnitValue, feature.PurchaseLink, feature.IsMainFeature, feature.ApplicationPath, feature.DisplayNameInDashboard, feature.PricePerUnitInINR };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<List<Feature>> GetList(int OffSet, int FetchNext)
        {
            string storeProcCommand = "Feature_Details";
            object? param = new { Action = "GET", OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Feature>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }


        public async Task<Feature?> GetDetail(Int16 Id)
        {
            string storeProcCommand = "Feature_Details";
            object? param = new { Action = "GETDetails", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<Feature?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }


        public async Task<Feature?> GetDetail(string featureName)
        {
            string storeProcCommand = "Feature_Details";
            object? param = new { Action = "GETDetailsByFeatureName", featureName };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<Feature?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }


        public async Task<bool> Delete(Int16 Id)
        {
            string storeProcCommand = "Feature_Details";
            object? param = new { Action = "DEL", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<List<Feature>> GetDetailByFeatureGroupId(int FeatureGroupId)
        {
            string storeProcCommand = "Feature_Details";
            object? param = new { Action = "GetDetailByFeatureGroupId", FeatureGroupId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Feature>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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
