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
    public class DLLeadScoreThresholdSettingsPG : CommonDataBaseInteraction , IDLLeadScoreThresholdSettings
    {
        CommonInfo connection;
        public DLLeadScoreThresholdSettingsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async Task<IEnumerable<LeadScoreThresholdSettings>>   GetList()
        {
            string storeProcCommand = "select * from leadscore_thresholdsettings_getlist()"; 
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<LeadScoreThresholdSettings>(storeProcCommand, param);
        }

        public void UpdateLeadBasedOnScore(LeadScoreThresholdSettings thresholdSettings)
        {
            string storeProcCommand = "select leadscore_thresholdsettings_updateleadbasedonscore(@Score, @Label, @StageId, @GroupId, @AgentId )";
            object? param = new { thresholdSettings.Score, thresholdSettings.Label, thresholdSettings.StageId, thresholdSettings.GroupId, thresholdSettings.AgentId };
            using var db = GetDbConnection(connection.Connection);
            db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<Int32> Save(LeadScoreThresholdSettings thresholdSettings)
        {
            string storeProcCommand = "select leadscore_thresholdsettings_save(@Score, @Label, @StageId, @GroupId, @AgentId)"; 
            object? param = new { thresholdSettings.Score, thresholdSettings.Label, thresholdSettings.StageId, thresholdSettings.GroupId, thresholdSettings.AgentId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<bool>  Delete(int id = 0)
        {
            string storeProcCommand = "select leadscore_thresholdsettings_delete(@Id)"; 
            object? param = new { id };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param)>0;
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
