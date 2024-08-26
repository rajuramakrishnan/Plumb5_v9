﻿using Dapper;
using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    internal class DLGcmFcmSettingsPG : CommonDataBaseInteraction, IDLGcmFcmSettings
    {
        CommonInfo connection = null;
        public DLGcmFcmSettingsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLGcmFcmSettingsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<object> GetNotification(MLGcmFcmSettings mlObj)
        {
            try
            {
                string storeProcCommand = "select * from SelectGcmFcmSettings( @Action, @SenderId, @ApiKey, @PackageName, @Type, @IsDefault )";
                object? param = new { mlObj.Action, mlObj.SenderId, mlObj.ApiKey, mlObj.PackageName, mlObj.Type, mlObj.IsDefault };

                using var db = GetDbConnection(connection.Connection);
                return await db.QueryAsync<object>(storeProcCommand, param);

            }
            catch
            {
                return 0;
            }
        }

        public async Task<object> GettingIOSSettings(APNsettings mlObj)
        {
            try
            {
                string storeProcCommand = "select * from SelectIOS_PushSettings( @Action, @CertificateName, @PassPhrase, @PushMode, @IOSPackageName )";
                object? param = new { mlObj.Action, mlObj.CertificateName, mlObj.PassPhrase, mlObj.PushMode, mlObj.IOSPackageName };

                using var db = GetDbConnection(connection.Connection);
                return await db.QueryAsync<object>(storeProcCommand, param);

            }
            catch
            {
                return 0;
            }
        }
    }
}