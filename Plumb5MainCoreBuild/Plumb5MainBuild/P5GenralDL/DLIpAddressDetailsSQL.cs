﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLIpAddressDetailsSQL : CommonDataBaseInteraction, IDLIpAddressDetails
    {
        CommonInfo connection;
        public DLIpAddressDetailsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLIpAddressDetailsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<IpligenceDAS?> IpAddressBelongsToINRorUSD(string IpAddress)
        {
            decimal IpDecimal = GetIpAddressInDecimalFormat(IpAddress);
            string storeProcCommand = "Ipligence_DAS_Info";
            object? param = new { @Action= "GetDetails", IpDecimal };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<IpligenceDAS?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public decimal GetIpAddressInDecimalFormat(string IpAddress)
        {
            double result = 0;
            String[] ipAddressInArray = IpAddress.Split('.');

            for (int i = 0; i < ipAddressInArray.Count(); i++)
            {
                int power = 3 - i;
                int ip = 0;
                try
                {
                    int.TryParse(ipAddressInArray[i], out ip);
                }
                catch
                {
                    ip = 0;
                }
                result += ip * Math.Pow(256, power);
            }
            return Convert.ToDecimal(result);
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
