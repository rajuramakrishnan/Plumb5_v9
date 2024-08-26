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
    public class DLWhatsAppConfigurationNamePG : CommonDataBaseInteraction, IDLWhatsAppConfigurationName
    {
        CommonInfo connection;
        public DLWhatsAppConfigurationNamePG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWhatsAppConfigurationNamePG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<List<MLWhatsAppConfigurationName>> GetConfigurationNames()
        {
            try
            {
                string storeProcCommand = "select * from whatsapp_configurationname_getconfigurationname()";

                using var db = GetDbConnection(connection.Connection);
                return (await db.QueryAsync<MLWhatsAppConfigurationName>(storeProcCommand)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<MLWhatsAppConfigurationName>> GetConfigurationNamesList()
        {
            string storeProcCommand = "select * from whatsapp_configurationname_get()";
           
            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLWhatsAppConfigurationName>(storeProcCommand)).ToList();

        }

        #region Dispose Method
        private bool _disposed = false;
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                if (disposing)
                {
                    connection = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion End of Dispose Method
    }
}
