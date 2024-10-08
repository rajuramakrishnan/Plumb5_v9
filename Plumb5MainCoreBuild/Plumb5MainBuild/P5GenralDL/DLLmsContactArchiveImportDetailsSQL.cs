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
    public class DLLmsContactArchiveImportDetailsSQL : CommonDataBaseInteraction, IDLLmsContactArchiveImportDetails
    {
        CommonInfo connection;
        public DLLmsContactArchiveImportDetailsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLmsContactArchiveImportDetailsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<bool> SaveBulkLmsContactImportDetails(DataTable lmsimportdetails)
        {
            string storeProcCommand = "LmsContactArchive_ImportDetails";
            object? param = new { Action = "Save", lmsimportdetails };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<List<LmsContactArchiveDetails>> GetDetails(int LmsContactRemoveOverViewId, short ArchivedStatus = 0)
        {
            string storeProcCommand = "LmsContactArchive_ImportDetails";
            object? param = new { Action = "GetDetails", LmsContactRemoveOverViewId, ArchivedStatus };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<LmsContactArchiveDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }


        public async Task<bool> Delete(int LmsContactRemoveOverViewId)
        {
            string storeProcCommand = "LmsContactArchive_ImportDetails";
            object? param = new { Action = "Delete", LmsContactRemoveOverViewId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<IEnumerable<DataSet>> GetAllDetails(int LmsContactRemoveOverViewId)
        {
            string storeProcCommand = "LmsContactArchive_ImportDetails";
            object? param = new { Action = "GetDetails", LmsContactRemoveOverViewId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<DataSet>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<IEnumerable<DataSet>> GetCountDetails(int LmsContactRemoveOverViewId)
        {
            string storeProcCommand = "LmsContactArchive_ImportDetails";
            object? param = new { Action = "GetCountDetails", LmsContactRemoveOverViewId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<DataSet>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }


    }
}