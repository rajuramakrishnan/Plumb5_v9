﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLIpligenceCityListSQL : CommonDataBaseInteraction, IDLIpligenceCityList
    {
        CommonInfo connection;

        public DLIpligenceCityListSQL()
        {
            connection = GetDBConnection();
        }

        public async Task<List<IpligenceCityList>> GET(string CityName)
        {
            string storeProcCommand = "Ipligence_CityList";
            object? param = new { @Action = "GetCityList", CityName };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<IpligenceCityList>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }
        public async Task<IpligenceDAS?> GetCityCoOrdinate(string CityName)
        {
            string storeProcCommand = "Ipligence_CityList";
            object? param = new { @Action = "GetCityCoOrdinate", CityName };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<IpligenceDAS?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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

