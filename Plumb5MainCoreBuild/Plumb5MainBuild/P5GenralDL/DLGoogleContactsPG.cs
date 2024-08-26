﻿using Dapper;
using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLGoogleContactsPG : CommonDataBaseInteraction, IDLGoogleContacts
    {
        CommonInfo connection = null;
        public DLGoogleContactsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLGoogleContactsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> GetContactsCount(int GroupId)
        {
            string storeProcCommand = "select * from get_google_contacts(@GroupId )";
            object? param = new { GroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<List<GoogleContact>> GetContacts(int GoogleImportSettingsId, int GroupId, int Offset, int FetchNext)
        {
            string storeProcCommand = "select * from get_google_contacts(@GoogleImportSettingsId, @GroupId, @Offset, @FetchNext )";
            object? param = new { GoogleImportSettingsId, GroupId, Offset, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<GoogleContact>(storeProcCommand, param)).ToList();

        }
    }
}

