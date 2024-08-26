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
    public class DLControlGroupsPG : CommonDataBaseInteraction, IDLControlGroups
    {
        CommonInfo connection;
        public DLControlGroupsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public async Task<int> Save(ControlGroups controlGroups)
        {
            string storeProcCommand = "select control_groups_save(@GroupId,@ControlGroupName,@ControlGroupDescription,@ControlGroupId,@ControlGroupCount,@ControlGroupPercentage,@IsNotControlGroupChecked,@NonControlGroupName,@NonControlGroupDescription,@NonControlGroupId,@NonControlGroupCount,@NonControlGroupPercentage)";

            object? param = new
            {
                controlGroups.GroupId,
                controlGroups.ControlGroupName,
                controlGroups.ControlGroupDescription,
                controlGroups.ControlGroupId,
                controlGroups.ControlGroupCount,
                controlGroups.ControlGroupPercentage,
                controlGroups.IsNotControlGroupChecked,
                controlGroups.NonControlGroupName,
                controlGroups.NonControlGroupDescription,
                controlGroups.NonControlGroupId,
                controlGroups.NonControlGroupCount,
                controlGroups.NonControlGroupPercentage
            };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<string?> GetControlGroupCampaignName(string CampaignName)
        {
            string storeProcCommand = "select control_groups_getcontrolgroupcampaignname()";
            object? param = new { CampaignName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<string?>(storeProcCommand, param);
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
