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
    public class DLAdminGeoIPDatabaseDetailsSQL : CommonDataBaseInteraction, IDLAdminGeoIPDatabaseDetails
    {
        CommonInfo connection;

        public DLAdminGeoIPDatabaseDetailsSQL()
        {
            connection = GetDBConnection();
        }

        public async Task<Int32> SaveGeoIPDatabaseDetails(AdminGeoIPDatabaseDetails geoIPDatabaseDetails)
        {
            string storeProcCommand = "GeoIPDatabase_Details";
            object? param = new { Action = "Save", geoIPDatabaseDetails.IPDBFileName, geoIPDatabaseDetails.IPFileUrl, geoIPDatabaseDetails.TotalRows };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> MaxCount()
        {
            string storeProcCommand = "GeoIPDatabase_Details";
            object? param = new { Action = "MaxCount" };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<AdminGeoIPDatabaseDetails>> GetReport(int Offset, int FetchNext)
        {
            string storeProcCommand = "GeoIPDatabase_Details";
            object? param = new { Action = "Get", Offset, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<AdminGeoIPDatabaseDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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
