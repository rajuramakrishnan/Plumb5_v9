﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLWindowsServiceDetailsSQL : CommonDataBaseInteraction, IDLWindowsServiceDetails
    {
        CommonInfo connection;
        public DLWindowsServiceDetailsSQL()
        {
            connection = GetDBConnection();
        }

        public async Task<List<WindowsServiceDetails>> GetAllService()
        {
            string storeProcCommand = "WindowsService_Details";
            object? param = new { Action = "GetAllService" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WindowsServiceDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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
