﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace P5GenralDL
{
    public class DLFeatureUnitTypeSQL : CommonDataBaseInteraction, IDLFeatureUnitType
    {
        CommonInfo connection;

        public DLFeatureUnitTypeSQL()
        {
            connection = GetDBConnection();
        }

        public async Task<List<FeatureUnitType>> GetList()
        {
            string storeProcCommand = "Feature_UnitType";
            object? param = new { Action = "GET" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<FeatureUnitType>(storeProcCommand)).ToList();
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
