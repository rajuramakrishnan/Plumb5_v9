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
    public class DLControlGroupsSQL : CommonDataBaseInteraction, IDLControlGroups
    {
        CommonInfo connection;
        public DLControlGroupsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public async Task<int> Save(ControlGroups controlGroups)
        {
            string storeProcCommand = "Control_Groups";

            object? param = new
            {
                Action = "Save",
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
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<string?> GetControlGroupCampaignName(string CampaignName)
        {
            string storeProcCommand = "Control_Groups";
            object? param = new { Action = "GetControlGroupCampaignName", CampaignName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<string?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
