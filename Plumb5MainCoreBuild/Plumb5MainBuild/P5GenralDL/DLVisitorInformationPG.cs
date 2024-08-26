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
    public class DLVisitorInformationPG : CommonDataBaseInteraction, IDLVisitorInformation
    {
        CommonInfo connection;

        public DLVisitorInformationPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLVisitorInformationPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<List<VisitorInformation>> GetList(VisitorInformation VisitorInformation)
        {
            string storeProcCommand = "select * from visitorinformation_details_getlist(@ContactId, @MachineId)";
            object? param = new { VisitorInformation.ContactId, VisitorInformation.MachineId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<VisitorInformation>(storeProcCommand, param)).ToList();
        }

        public async Task<VisitorInformation?> Get(VisitorInformation VisitorInformation)
        {
            string storeProcCommand = "select * from visitorinformation_details_get(@ContactId, @MachineId)";
            object? param = new { VisitorInformation.ContactId, VisitorInformation.MachineId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<VisitorInformation?>(storeProcCommand, param);
        }

        public async Task<MLVisitorInformation?> GetContactDetails(MLVisitorInformation VisitorInformation)
        {
            string storeProcCommand = "select * from visitorinformation_details_getcontactdetails(@MachineId, @EmailId, @PhoneNumber, @DeviceId)";
            object? param = new { VisitorInformation.MachineId, VisitorInformation.EmailId, VisitorInformation.PhoneNumber, VisitorInformation.DeviceId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLVisitorInformation?>(storeProcCommand, param);
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
