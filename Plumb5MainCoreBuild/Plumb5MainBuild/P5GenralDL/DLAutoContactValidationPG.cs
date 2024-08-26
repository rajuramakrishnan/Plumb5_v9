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
    public class DLAutoContactValidationPG : CommonDataBaseInteraction,IDLAutoContactValidation
    {
        CommonInfo connection;
        public DLAutoContactValidationPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public DLAutoContactValidationPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        
        public async Task<bool> Save(int AccountId, int GroupId)
        {
            string storeProcCommand = "select AutoContact_Validation(@Action,@AccountId,@GroupId)";
            object? param = new { Action = "Save", AccountId, GroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
            
        }

        public async Task<DataSet> GetAllAccountIds()
        {
            string storeProcCommand = "select * from AutoContact_Validation(@Action)";
            object? param = new { Action = "GetAllAccountId" };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<bool> Delete(int AccountId, int GroupId)
        {
            string storeProcCommand = "select AutoContact_Validation(@Action,@AccountId,@GroupId)";
            object? param = new { Action = "Delete", AccountId, GroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }
    }
}
