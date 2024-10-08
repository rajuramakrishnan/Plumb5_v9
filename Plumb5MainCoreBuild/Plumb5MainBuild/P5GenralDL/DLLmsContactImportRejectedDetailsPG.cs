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
    public class DLLmsContactImportRejectedDetailsPG : CommonDataBaseInteraction, IDLLmsContactImportRejectedDetails
    {
        CommonInfo connection;
        public DLLmsContactImportRejectedDetailsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLmsContactImportRejectedDetailsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<bool> SaveBulkLmsContactRejectedDetails(DataTable lmsreportdetails)
        {
            string storeProcCommand = "select * from LmsContact_RejectedDetails(@Action,@lmsreportdetails)";
            object? param = new { Action = "Save", lmsreportdetails };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }

        public async Task<int> GetMaxCount(int LmsContactImportOverviewId)
        {
            string storeProcCommand = "select * from LmsContact_RejectedDetails(@Action,@LmsContactImportOverviewId)";
            object? param = new { Action = "GetMaxCount", LmsContactImportOverviewId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<List<LmsContactImportRejectedDetails>> GetList(int LmsContactImportOverviewId, int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from LmsContact_RejectedDetails(@Action,@OffSet, @FetchNext, @LmsContactImportOverviewId)";
            object? param = new { Action = "GetList", OffSet, FetchNext, LmsContactImportOverviewId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<LmsContactImportRejectedDetails>(storeProcCommand, param)).ToList();

        }
    }
}