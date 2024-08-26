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
    public class DLLmsContactImportRejectedDetailsSQL : CommonDataBaseInteraction, IDLLmsContactImportRejectedDetails
    {
        CommonInfo connection;
        public DLLmsContactImportRejectedDetailsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLmsContactImportRejectedDetailsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<bool> SaveBulkLmsContactRejectedDetails(DataTable lmsreportdetails)
        {
            string storeProcCommand = "LmsContact_RejectedDetails";
            object? param = new { Action = "Save", lmsreportdetails };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure)>0;

        }

        public async Task<int> GetMaxCount(int LmsContactImportOverviewId)
        {
            string storeProcCommand = "LmsContact_RejectedDetails";
            object? param = new { Action = "GetMaxCount", LmsContactImportOverviewId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<LmsContactImportRejectedDetails>> GetList(int LmsContactImportOverviewId, int OffSet, int FetchNext)
        {
            string storeProcCommand = "LmsContact_RejectedDetails";
            object? param = new { Action = "GetList", OffSet, FetchNext, LmsContactImportOverviewId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<LmsContactImportRejectedDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }
    }
}
