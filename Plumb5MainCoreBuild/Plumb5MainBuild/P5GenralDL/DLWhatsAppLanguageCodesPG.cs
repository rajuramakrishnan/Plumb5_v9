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
    public class DLWhatsAppLanguageCodesPG : CommonDataBaseInteraction, IDLWhatsAppLanguageCodes
    {
        CommonInfo connection;
        public DLWhatsAppLanguageCodesPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWhatsAppLanguageCodesPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<WhatsAppLanguageCodes?> GetWhatsAppShortenLanguageCode(string VendorName, string TemplateLanguage)
        {
            string storeProcCommand = "select * from whatsapp_languagecodes_getshortenlanguagecode(@VendorName, @TemplateLanguage)";
            object? param = new { VendorName, TemplateLanguage };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WhatsAppLanguageCodes>(storeProcCommand, param);

        }
        #region Dispose Method

        bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {

                }
            }
            //dispose unmanaged resources
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion End of Dispose Method
    }
}
