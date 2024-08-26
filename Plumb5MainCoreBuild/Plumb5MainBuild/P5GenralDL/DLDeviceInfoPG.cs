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
    public class DLDeviceInfoPG : CommonDataBaseInteraction, IDLDeviceInfo
    {
        CommonInfo connection;
        public DLDeviceInfoPG()
        {
            connection = GetDBConnection();
        }

        public DLDeviceInfoPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<DeviceInfo?> GetDeviceInfo(string useragent)
        {
            string storeProcCommand = "select * from getdeviceinfo(@useragent)";
            object? param = new { useragent };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<DeviceInfo?>(storeProcCommand, param);

        }

        public async Task<List<DeviceInfo>> GetDeviceInfoByDeviceId(List<int> ListOfIds)
        {
            string storeProcCommand = "select * from getdeviceinfobydeviceid(@ListOfIds)";
            object? param = new { ListOfIds = string.Join(",", ListOfIds) };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<DeviceInfo>(storeProcCommand, param)).ToList();

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
