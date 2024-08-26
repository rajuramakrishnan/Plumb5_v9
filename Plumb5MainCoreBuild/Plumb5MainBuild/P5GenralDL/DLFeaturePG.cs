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
    public class DLFeaturePG : CommonDataBaseInteraction, IDLFeature
    {
        CommonInfo connection;

        public DLFeaturePG()
        {
            connection = GetDBConnection();
        }

        public async Task<Int16> SaveDetails(Feature feature)
        {
            string storeProcCommand = "select feature_details_save(@Name, @FeatureUnitTypeId, @PricePerUnit, @MinUnitValue, @MaxUnitValue, @PurchaseLink, @IsMainFeature, @ApplicationPath, @DisplayNameInDashboard, @PricePerUnitInINR)";
            object? param = new { feature.Name, feature.FeatureUnitTypeId, feature.PricePerUnit, feature.MinUnitValue, feature.MaxUnitValue, feature.PurchaseLink, feature.IsMainFeature, feature.ApplicationPath, feature.DisplayNameInDashboard, feature.PricePerUnitInINR };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param);
        }

        public async Task<bool> UpdateDetails(Feature feature)
        {
            string storeProcCommand = "select feature_details_update(@Id, @Name, @FeatureUnitTypeId, @PricePerUnit, @MinUnitValue, @MaxUnitValue, @PurchaseLink, @IsMainFeature, @ApplicationPath, @DisplayNameInDashboard, @PricePerUnitInINR)";
            object? param = new { feature.Id, feature.Name, feature.FeatureUnitTypeId, feature.PricePerUnit, feature.MinUnitValue, feature.MaxUnitValue, feature.PurchaseLink, feature.IsMainFeature, feature.ApplicationPath, feature.DisplayNameInDashboard, feature.PricePerUnitInINR };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }

        public async Task<List<Feature>> GetList(int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from feature_details_get(@OffSet, @FetchNext)";
            object? param = new { OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Feature>(storeProcCommand, param)).ToList();
        }


        public async Task<Feature?> GetDetail(Int16 Id)
        {
            string storeProcCommand = "select * from feature_details_getdetails(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<Feature?>(storeProcCommand, param);
        }


        public async Task<Feature?> GetDetail(string featureName)
        {
            string storeProcCommand = "select * from feature_details_getdetailsbyfeaturename()";
            object? param = new { featureName };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<Feature?>(storeProcCommand, param);
        }


        public async Task<bool> Delete(Int16 Id)
        {
            string storeProcCommand = "select feature_details_del(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<List<Feature>> GetDetailByFeatureGroupId(int FeatureGroupId)
        {
            string storeProcCommand = "select * from feature_details_getdetailbyfeaturegroupid(@FeatureGroupId)";
            object? param = new { FeatureGroupId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Feature>(storeProcCommand, param)).ToList();
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
