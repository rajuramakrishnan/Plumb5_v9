﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace P5GenralDL
{
    public class DLContactMergeConfigurationSQL : CommonDataBaseInteraction, IDLContactMergeConfiguration
    {
        private CommonInfo connection;
        public DLContactMergeConfigurationSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLContactMergeConfigurationSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(ContactMergeConfiguration settings)
        {
            const string storeProcCommand = "Contact_MergeConfiguration";
            object? param = new { Action="Save",settings.UserInfoUserId, settings.PrimaryEmail, settings.PrimarySMS, settings.AlternateEmail, settings.AlternateSMS };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }
        public async Task<ContactMergeConfiguration?> GetSettingDetails()
        {
            const string storeProcCommand = "Contact_MergeConfiguration";
            object? param = new { Action = "GetSettingDetails" };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<ContactMergeConfiguration?>(storeProcCommand, param);
        }
        public async Task<IEnumerable<ContactMergeConfiguration>> GetSettingDetailsAsync()
        {
            const string storeProcCommand = "Contact_MergeConfiguration";
            object? param = new { Action = "GetSettingDetails" };
            using var db = GetDbConnection(connection.Connection);  
            return await db.QueryAsync<ContactMergeConfiguration>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> Delete(int Id)
        {
            const string storeProcCommand = "Contact_MergeConfiguration";

            object? param = new { Action = "Delete", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure)>0;
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
