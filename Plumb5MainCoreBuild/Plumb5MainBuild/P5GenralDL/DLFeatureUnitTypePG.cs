﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLFeatureUnitTypePG : CommonDataBaseInteraction, IDLFeatureUnitType
    {
        CommonInfo connection;

        public DLFeatureUnitTypePG()
        {
            connection = GetDBConnection();
        }

        public async Task<List<FeatureUnitType>> GetList()
        {
            string storeProcCommand = "select * from feature_unittype_get()";

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
