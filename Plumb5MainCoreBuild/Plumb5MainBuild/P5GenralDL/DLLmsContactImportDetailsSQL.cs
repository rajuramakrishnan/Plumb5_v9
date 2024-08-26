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
    public class DLLmsContactImportDetailsSQL : CommonDataBaseInteraction, IDLLmsContactImportDetails
    {
        CommonInfo connection;
        public DLLmsContactImportDetailsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLmsContactImportDetailsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<bool> SaveBulkLmsContactImportDetails(DataTable lmsimportdetails)
        {
            string storeProcCommand = "LmsContact_ImportDetails";
            object? param = new { Action = "Save", lmsimportdetails };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<List<LmsContactImportDetails>> GetDetails(int LmsContactImportOverViewId)
        {
            string storeProcCommand = "LmsContact_ImportDetails";
            object? param = new { Action = "GetDetails", LmsContactImportOverViewId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<LmsContactImportDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<bool> Delete(int LmsContactImportOverViewId)
        {
            string storeProcCommand = "LmsContact_ImportDetails";
            object? param = new { Action = "Delete", LmsContactImportOverViewId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<DataSet> GetAllDetails(int LmsContactImportOverViewId)
        {
            string storeProcCommand = "LmsContact_ImportDetails";
            object? param = new { Action = "GetDetails", LmsContactImportOverViewId };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<DataSet> GetCountDetails(int LmsContactImportOverViewId)
        {
            string storeProcCommand = "LmsContact_ImportDetails";
            object? param = new { Action = "GetCountDetails", LmsContactImportOverViewId };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        
    }
}
