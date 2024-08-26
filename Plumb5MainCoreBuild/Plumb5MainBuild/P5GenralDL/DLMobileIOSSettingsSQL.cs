﻿using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Reflection.PortableExecutable;

namespace P5GenralDL
{
    public class DLMobileIOSSettingsSQL : CommonDataBaseInteraction, IDLMobileIOSSettings
    {
        CommonInfo connection;
        public DLMobileIOSSettingsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLMobileIOSSettingsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<MobileGcmSettings?> GetGcmSettings()
        {
            string storeProcCommand = "SelectVisitorAutoSuggest";
            object? param = new { Action = "GetMobileGcmSettings" };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MobileGcmSettings?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<MobileIOSSettings?> GetMobileIOSSettings()
        {
            string storeProcCommand = "Mobil_GCM_Setting_PROC";
            object? param = new { Action = "GetMobileIOSSettings" };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MobileIOSSettings>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<MobileGcmSettings>> GetGcmProjectNoPackageName()
        {
            string storeProcCommand = "Mobile_Gcm_Settings";
            object? param = new { Action = "GetActiveSettings" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MobileGcmSettings>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }


        public async Task<List<MobileIOSSettings>> GetSettings()
        {
            string storeProcCommand = "Mobile_IOSSettings";
            object? param = new { Action = "GetSettings" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MobileIOSSettings>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<bool> Save(MobileDeviceInfo rData)
        {
            string storeProcCommand = "Mobile_DeviceInfo";
            object? param = new { rData.GcmRegId, rData.DeviceId, rData.Manufacturer, rData.Name, rData.OS, rData.OsVersion, rData.AppVersion, rData.CarrierName, rData.Resolution };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }



        public async Task<bool> SaveInitSession(MobileTrackData rData)
        {
            string storeProcCommand = "Mobile_Tracker";
            object? param = new { rData.CarrierName, rData.SessionId, rData.ScreenName, rData.DeviceId, rData.VisitorIp, rData.IpDecimal, rData.CampaignId, rData.NewSession, rData.Offline, rData.TrackDate, rData.GeofenceId, rData.Locality, rData.City, rData.State, rData.Country, rData.CountryCode, rData.Latitude, rData.Longitude, rData.PageParameter, rData.WorkFlowDataId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }


        public async Task<bool> SaveLogData(MobileEventData eventData)
        {
            string storeProcCommand = "Mobile_EventTracker";
            object? param = new { eventData.DeviceId, eventData.SessionId, eventData.Type, eventData.Name, eventData.Value };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<bool> SaveEndSession(MobileEndRequest eData)
        {
            string storeProcCommand = "Mobile_EndSession";
            object? param = new { eData.SessionId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<bool> RegisterUser(MobileUserInfo userData)
        {
            string storeProcCommand = "Mobile_UserInfo";
            object? param = new { userData.DeviceId, userData.Name, userData.EmailId, userData.PhoneNumber };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;


        }

        public async Task<bool> SaveFormResponses(MobileFormRequest formData)
        {
            string storeProcCommand = "Mobile_FormResponses";
            object? param = new { formData.MobileFormId, formData.DeviceId, formData.SessionId, formData.ScreenName, formData.FormResponses, formData.BannerView, formData.BannerClick, formData.BannerClose, formData.GeofenceName, formData.BeaconName, formData.ButtonName, formData.WorkFlowDataId, formData.P5UniqueId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }
        public async Task<FormResponseReportToSetting?> GetresponseSettings(int MobileFormId)
        {
            string storeProcCommand = "MobileResponseSettings";
            object? param = new { Action = "GetReportSettings", MobileFormId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<FormResponseReportToSetting?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<MobileInAppDisplaySettings>> GetInAppDisplaySettings(InAppRequest InAppRequest)
        {
            string storeProcCommand = "MobileSelectInAppTemplate";
            object? param = new { Action = InAppRequest.DeviceId, Key = "DisplayAll", BannerId = 2, RecentStatus = 0 };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MobileInAppDisplaySettings>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }


        public async Task<string> GetIpInformation(int AccountId, double IpDecimal)
        {
            CommonInfo _connection = new CommonInfo();
            IConfiguration Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            _connection.Connection = Configuration.GetSection("ConnectionStrings:MasterConnection").Value;
            string returnvalue = "";
            string storeProcCommand = "GetAccount";
            object? param = new { Action = "GetConnectionForMobile", AccountId, IpDecimal = IpDecimal.ToString() };
            using var db = GetDbConnection(connection.Connection);
            var reader = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

            if (reader.Read())
                returnvalue = reader["IpDetails"].ToString();

            return returnvalue;

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
