﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using Microsoft.Data.SqlClient;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLCustomEventImportFromFileSQL : CommonDataBaseInteraction, IDLCustomEventImportFromFile
    {
        CommonInfo conne;

        public DLCustomEventImportFromFileSQL(int adsId)
        {
            conne = GetDBConnection(adsId);
        }

        public async void ImportData(DataTable dt, int EventImportOverViewId)
        {

            SqlConnection connection = new SqlConnection(conne.Connection);
            try
            {
                string storeProcCommand = "CustomEvent_ImportFrom_File";
                object? param = new { Action = "SaveData", EventImportOverViewId, dt };
                using var db = GetDbConnection(conne.Connection); 
                db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<DataSet> StartImport(int EventImportOverViewId, ContactMergeConfiguration mergeConfiguration)
        {
            string storeProcCommand = "CustomEvent_ImportFrom_File";
            object? param = new { Action= "StartImport", EventImportOverViewId, mergeConfiguration.PrimaryEmail, mergeConfiguration.PrimarySMS, mergeConfiguration.AlternateEmail, mergeConfiguration.AlternateSMS };
            using var db = GetDbConnection(conne.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        #region Dispose Method
        bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    conne = null;
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
