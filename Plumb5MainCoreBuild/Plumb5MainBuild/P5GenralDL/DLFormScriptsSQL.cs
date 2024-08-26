﻿using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace P5GenralDL
{
    internal class DLFormScriptsSQL : CommonDataBaseInteraction, IDLFormScripts
    {
        CommonInfo connection;
        public DLFormScriptsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLFormScriptsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<int> Save(FormScripts formScripts)
        {
            string storeProcCommand = "Form_Script";
            object? param = new { Action = "Save", formScripts.FormId, formScripts.FormType, formScripts.FormScript, formScripts.Description, formScripts.FormScriptStatus, formScripts.FormScriptType, formScripts.ConfigurationDetails, formScripts.PageUrl, formScripts.AlternatePageUrls };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> Update(FormScripts formScripts)
        {
            string storeProcCommand = "Form_Script";
            object? param = new { Action = "Update", formScripts.Id, formScripts.FormId, formScripts.FormType, formScripts.FormScript, formScripts.Description, formScripts.FormScriptType, formScripts.ConfigurationDetails, formScripts.PageUrl, formScripts.AlternatePageUrls };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure)>0;

        }

        public async Task<bool> ToogleStatus(int Id, bool FormScriptStatus)
        {
            string storeProcCommand = "Form_Script";
            object? param = new { Action = "UpdateStatus", Id, FormScriptStatus };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<bool> Delete(int Id)
        {
            string storeProcCommand = "Form_Script";
            object? param = new { Action = "Delete", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<int> GetMaxCount(MLFormScripts formScripts)
        {
            string storeProcCommand = "Form_Script";
            object? param = new { Action = "GetMaxCount", formScripts.FormId, formScripts.FormType, formScripts.FormScriptStatus, formScripts.FormScriptType, formScripts.Id, formScripts.PageUrl, formScripts.FormIdentifier };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<MLFormScripts>> Get(MLFormScripts formScripts, int OffSet, int FetchNext)
        {
            string storeProcCommand = "Form_Script";
            object? param = new { Action = "GetAllDetails", formScripts.FormId, formScripts.FormType, formScripts.FormScriptStatus, formScripts.FormScriptType, formScripts.Id, formScripts.PageUrl, OffSet, FetchNext, formScripts.FormIdentifier };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLFormScripts>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<List<FormScripts>> GetBasedOnURL(FormScripts formScripts)
        {
            string storeProcCommand = "Form_Script";
            object? param = new { Action = "GET", formScripts.FormId, formScripts.FormType, formScripts.FormScriptType, formScripts.Id, formScripts.PageUrl };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<FormScripts>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();


        }



        public async Task<FormScripts?> GetDetail(FormScripts formScripts)
        {
            string storeProcCommand = "Form_Script";
            object? param = new { Action = "GET", formScripts.FormId, formScripts.FormType, formScripts.FormScriptType, formScripts.Id, formScripts.PageUrl };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<FormScripts>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> UpdateAlternateUrl(int FormId, string AlternatePageUrls)
        {
            string storeProcCommand = "Form_Script";
            object? param = new { Action= "UpdateAlternateUrl", FormId, AlternatePageUrls };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
