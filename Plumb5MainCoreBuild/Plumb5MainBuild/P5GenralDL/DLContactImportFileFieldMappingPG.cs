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
    public class DLContactImportFileFieldMappingPG : CommonDataBaseInteraction, IDLContactImportFileFieldMapping
    {
        readonly CommonInfo connection;
        public DLContactImportFileFieldMappingPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLContactImportFileFieldMappingPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(ContactImportFileFieldMapping importFileFieldMapping)
        {
            string storeProcCommand = "select * from contactimportfilefield_mapping_save(@ImportOverViewId, @FileFieldName, @FileFieldIndex, @P5ColumnName, @FrontEndName, @IsMapped, @IsNameChanged)";
            object? param = new { importFileFieldMapping.ImportOverViewId, importFileFieldMapping.FileFieldName, importFileFieldMapping.FileFieldIndex, importFileFieldMapping.P5ColumnName, importFileFieldMapping.FrontEndName, importFileFieldMapping.IsMapped, importFileFieldMapping.IsNameChanged };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        #region Dispose Method
        bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    
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
