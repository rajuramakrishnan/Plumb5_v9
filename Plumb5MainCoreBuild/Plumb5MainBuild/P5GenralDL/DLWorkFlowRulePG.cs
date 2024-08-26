﻿using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using IP5GenralDL;
using Newtonsoft.Json;


namespace P5GenralDL
{
    public class DLWorkFlowRulePG : CommonDataBaseInteraction, IDLWorkFlowRule
    {
        CommonInfo connection;
        public DLWorkFlowRulePG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWorkFlowRulePG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(WorkFlowSetRules setRule)
        {
            try
            {
                List<WorkFlowSetRules> workflowrules = new List<WorkFlowSetRules>();
                workflowrules.Add(setRule);
                var settings = new JsonSerializerSettings();
                settings.ContractResolver = new LowercaseContractResolver();
                var workflowrulesjson = JsonConvert.SerializeObject(workflowrules, Formatting.Indented, settings);

                string storeProcCommand = "select * from workflow_rules_save(@workflowrulesjson)";
                object? param = new { workflowrulesjson };

                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<bool> Update(WorkFlowSetRules setRule)
        {
            List<WorkFlowSetRules> workflowrules = new List<WorkFlowSetRules>();
            workflowrules.Add(setRule);
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new LowercaseContractResolver();
            var workflowrulesjson = JsonConvert.SerializeObject(workflowrules, Formatting.Indented, settings);

            string storeProcCommand = "select * from workflow_rules_update(@workflowrulesjson)";
            object? param = new { workflowrulesjson };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<WorkFlowSetRules?> GetDetails(WorkFlowSetRules setRule)
        {
            string storeProcCommand = "select * from workflow_setrule_get(@RuleId,@TriggerHeading)";
            object? param = new { setRule.RuleId, setRule.TriggerHeading };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowSetRules?>(storeProcCommand, param);
        }

        public async Task<bool> Delete(Int32 RuleId)
        {
            string storeProcCommand = "select * from workflow_setrule_delete(@RuleId)";
            object? param = new { RuleId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> ToogleStatus(MLWorkFlowSetRules setRule)
        {
            string storeProcCommand = "select * from workflow_setrule_tooglestatus(@RuleId,@TriggerStatus)";
            object? param = new { setRule.RuleId, setRule.TriggerStatus };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<int> GetMaxCount(string RuleName = null)
        {
            string storeProcCommand = "select * from workflow_setrule_getmaxcount(@RuleName)";
            object? param = new { RuleName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<List<MLWorkFlowSetRules>> GetAllRules(int OffSet, int FetchNext, string RuleName = null)
        {
            string storeProcCommand = "select * from workflow_setrule_getallrulesdata(@OffSet, @FetchNext, @RuleName)";
            object? param = new { OffSet, FetchNext, RuleName };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLWorkFlowSetRules?>(storeProcCommand, param)).ToList();
        }

        public async Task<List<MLWorkFlowSetRules>> GetAllRule()
        {
            string storeProcCommand = "select * from workflow_setrule_getallrules()";

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLWorkFlowSetRules?>(storeProcCommand)).ToList();
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

