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
    public class DLAutoContactValidationSQL: CommonDataBaseInteraction, IDLAutoContactValidation
    {
        CommonInfo connection;
        public DLAutoContactValidationSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public DLAutoContactValidationSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<bool> Save(int AccountId, int GroupId)
        {
            string storeProcCommand = "AutoContact_Validation";
            object? param = new { Action="Save", AccountId, GroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<DataSet> GetAllAccountIds()
        {
            string storeProcCommand = "AutoContact_Validation";
            object? param = new { Action = "GetAllAccountId" };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<bool> Delete(int AccountId, int GroupId)
        {
            string storeProcCommand = "AutoContact_Validation";
            object? param = new { Action = "Delete", AccountId, GroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;           
        }
    }
}
