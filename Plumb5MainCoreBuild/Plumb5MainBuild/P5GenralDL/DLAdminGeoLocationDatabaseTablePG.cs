﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;


namespace P5GenralDL
{
    public class DLAdminGeoLocationDatabaseTablePG : CommonDataBaseInteraction, IDLAdminGeoLocationDatabaseTable
    {
        CommonInfo connection;
        public DLAdminGeoLocationDatabaseTablePG()
        {
            connection = GetDBConnection();
        }
        public async Task<bool> CreateTempTableCreation(string dynamicTableName)
        {
            string storeProcCommand = "select * from geolocation_databasetable_temptablecreation(@dynamicTableName)";
            object? param = new { dynamicTableName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> RenameTableName(string dynamicTableName)
        {
            string storeProcCommand = "select * from geolocation_databasetable_renametablename(@dynamicTableName)";
            object? param = new { dynamicTableName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }
    }
}


