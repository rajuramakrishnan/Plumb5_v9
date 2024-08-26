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
    public class DLContactImportFromFilePG : CommonDataBaseInteraction, IDLContactImportFromFile
    {
        CommonInfo connection;
        public DLContactImportFromFilePG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLContactImportFromFilePG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public void ImportData(DataTable dt, int Contact_ImportOverviewId)
        {
            CommonBulkInsert(connection.Connection, dt, "contactimportfromfile");
        }

        public async Task<DataSet> StartImport(int Contact_ImportOverviewId, ContactMergeConfiguration mergeConfiguration)
        {
            string storeProcCommand = "select * from contact_importfrom_file_startimport(@Contact_ImportOverviewId, @PrimaryEmail, @PrimarySMS, @AlternateEmail, @AlternateSMS)";
            object? param = new { Contact_ImportOverviewId, mergeConfiguration.PrimaryEmail, mergeConfiguration.PrimarySMS, mergeConfiguration.AlternateEmail, mergeConfiguration.AlternateSMS };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetImportResult(int Contact_ImportOverviewId, ContactMergeConfiguration mergeConfiguration)
        {
            string storeProcCommand = "select * from contactimportresult_returntemp()";

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<DataSet> SaveGroupRejectedandLMSRejected(int Contact_ImportOverviewId)
        {
            string storeProcCommand = "select * from contactgroupoverview_lmsgroupoverview(@Contact_ImportOverviewId)";
            object? param = new { Contact_ImportOverviewId };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task ImportRejectedResults(int Contact_ImportOverviewId, int GroupImportOverViewId, int LmsGroupImportOverViewId)
        {
            string storeProcCommand = "select * from contactimportresult_rejectdata(@Contact_ImportOverviewId,@GroupImportOverViewId,@LmsGroupImportOverViewId)";
            object? param = new { Contact_ImportOverviewId, GroupImportOverViewId, LmsGroupImportOverViewId };

            using var db = GetDbConnection(connection.Connection);
            await db.QueryAsync<DataSet>(storeProcCommand, param);
        }

        public async Task<int> DeleteImportResult()
        {
            string storeProcCommand = "select * from contactimportresult_returntempdelete()";

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand);
        }

        public async Task<int> DeleteTempData()
        {
            string storeProcCommand = "select * from contactimportresult_temptabledelete()";

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand);
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
