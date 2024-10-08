﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace P5GenralDL
{
    public class DLVisitorInfoForBrowserRuleSQL : CommonDataBaseInteraction, IDLVisitorInfoForBrowserRule
    {
        private CommonInfo _connection = null;
        public DLVisitorInfoForBrowserRuleSQL(int adsId)
        {
            _connection = GetDBConnection(adsId);
        }

        public DLVisitorInfoForBrowserRuleSQL(string connectionString)
        {
            _connection = new CommonInfo() { Connection = connectionString };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="MachineId"></param>
        /// <returns></returns>
        public async Task<List<int>> ResponseFormList(string MachineId)
        {
            List<int> FormIdList = new List<int>();
            string storeProcCommand = "Forms_SP_AllFormsLoading";
            object? param = new { Action = "ResponseFormList", MachineId };

            using var db = GetDbConnection(_connection.Connection);
            using var reader = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

            while (reader.Read())
            {
                FormIdList.Add(Convert.ToInt32(reader["FormId"]));
            }

            return FormIdList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MachineId"></param>
        /// <param name="FormId"></param>
        /// <returns></returns>
        public async Task<DataSet> FormLeadDetailsAnswerDependency(string MachineId, int FormId)
        {
            DataSet ds = new DataSet();
            string storeProcCommand = "Forms_SP_AllFormsLoading";
            object? param = new { Action = "ResponseFormList", MachineId, FormId };

            using var db = GetDbConnection(_connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MachineId"></param>
        /// <param name="FormId"></param>
        /// <returns></returns>
        public async Task<short> GetFormImpression(string MachineId, int FormId)
        {
            string storeProcCommand = "Forms_SP_AllFormsLoading";
            object? param = new { Action = "FormImpression", MachineId, FormId };

            using var db = GetDbConnection(_connection.Connection);
            return await db.ExecuteScalarAsync<short>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MachineId"></param>
        /// <param name="FormId"></param>
        /// <returns></returns>
        public async Task<short> GetFormCloseCount(string MachineId, int FormId)
        {
            string storeProcCommand = "Forms_SP_AllFormsLoading";
            object? param = new { Action = "FormCloseCount", MachineId, FormId };

            using var db = GetDbConnection(_connection.Connection);
            return await db.ExecuteScalarAsync<short>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MachineId"></param>
        /// <param name="FormId"></param>
        /// <returns></returns>
        public async Task<short?> GetFormResponseCount(string MachineId, int FormId)
        {
            string storeProcCommand = "Forms_SP_AllFormsLoading";
            object? param = new { Action = "FormResponseCount", MachineId, FormId };

            using var db = GetDbConnection(_connection.Connection);
            return await db.ExecuteScalarAsync<short?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmailId"></param>
        /// <returns></returns>
        public async Task<byte> OnlineSentimentIs(string EmailId)
        {
            if (!String.IsNullOrEmpty(EmailId))
            {
                string storeProcCommand = "FormRules_EngauageAndQ5";
                object? param = new { Action = "OnlineSentimentIs", EmailId };

                using var db = GetDbConnection(_connection.Connection);
                return await db.ExecuteScalarAsync<byte>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContactId"></param>
        /// <returns></returns>
        public async Task<short> NurtureStatusIs(int ContactId)
        {
            if (ContactId > 0)
            {
                string storeProcCommand = "Forms_SP_AllFormsLoading";
                object? param = new { Action = "NurtureStatusIs", ContactId };

                using var db = GetDbConnection(_connection.Connection);
                return await db.ExecuteScalarAsync<short>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContactId"></param>
        /// <returns></returns>
        public async Task<short> LoyaltyPoints(int ContactId)
        {
            if (ContactId > 0)
            {
                string storeProcCommand = "FormRules_EngauageAndQ5";
                object? param = new { Action = "Loyaltypoints", ContactId };

                using var db = GetDbConnection(_connection.Connection);
                return await db.ExecuteScalarAsync<short>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            }
            return 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContactId"></param>
        /// <returns></returns>
        public async Task<byte> ConnectedSocially(int ContactId)
        {
            if (ContactId > 0)
            {
                string storeProcCommand = "FormRules_EngauageAndQ5";
                object? param = new { Action = "Connectedsocially", ContactId };

                using var db = GetDbConnection(_connection.Connection);
                return await db.ExecuteScalarAsync<byte>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            }
            return 0;
        }

        #region Dispose Method

        private bool _disposed = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                if (disposing)
                {
                    _connection = null;
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
