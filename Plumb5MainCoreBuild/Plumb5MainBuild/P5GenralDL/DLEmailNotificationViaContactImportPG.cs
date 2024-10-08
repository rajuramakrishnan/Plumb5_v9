﻿using Dapper;
using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    internal class DLEmailNotificationViaContactImportPG : CommonDataBaseInteraction, IDLEmailNotificationViaContactImport
    {
        CommonInfo connection;
        public DLEmailNotificationViaContactImportPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLEmailNotificationViaContactImportPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<List<EmailNotificationViaContactImport>> GetList(EmailNotificationViaContactImport emailNotificationViaContactImport)
        {
            string storeProcCommand = "select * from EmailNotification_ViaContactImport(@Action,@Id,@NotificationEmailId)";
            object? param = new { Action = "GetList", emailNotificationViaContactImport.Id, emailNotificationViaContactImport.NotificationEmailId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<EmailNotificationViaContactImport>(storeProcCommand, param)).ToList();

        }
    }
}
