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
    public class DLContactAPINotSavedSQL : CommonDataBaseInteraction, IDLContactAPINotSaved
    { 
        CommonInfo connection;

        public DLContactAPINotSavedSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLContactAPINotSavedSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(ContactAPINotSaved contactAPINotSaved)
        {
            string storeProcCommand = "ContactAPI_NotSaved";
            object? param = new { Action ="Save",contactAPINotSaved.ApiKey, contactAPINotSaved.AdsId, contactAPINotSaved.Name, contactAPINotSaved.EmailId, contactAPINotSaved.PhoneNumber, contactAPINotSaved.GroupId, contactAPINotSaved.Location, contactAPINotSaved.Age, contactAPINotSaved.Gender, contactAPINotSaved.MaritalStatus, contactAPINotSaved.Education, contactAPINotSaved.Occupation, contactAPINotSaved.Interests, contactAPINotSaved.EmailUnsubscribe, contactAPINotSaved.EmailUnsubscribeDate, contactAPINotSaved.OptInOverAllNewsLetter, contactAPINotSaved.SmsUnsubscribe, contactAPINotSaved.SmsUnsubscribeDate, contactAPINotSaved.SmsOptInOverAllNewsLetter, contactAPINotSaved.USSDUnsubscribe, contactAPINotSaved.USSDUnsubscribeDate, contactAPINotSaved.USSDOptInOverAllNewsLetter, contactAPINotSaved.ReferalUrl, contactAPINotSaved.LastMessageSent, contactAPINotSaved.MethodName, contactAPINotSaved.IpAddress, contactAPINotSaved.Remark, contactAPINotSaved.UtmTagSource, contactAPINotSaved.FirstUtmMedium, contactAPINotSaved.FirstUtmCampaign, contactAPINotSaved.FirstUtmTerm, contactAPINotSaved.FirstUtmContent, contactAPINotSaved.LastName, contactAPINotSaved.IsAccountHolder, contactAPINotSaved.AccountType, contactAPINotSaved.AccountOpenedSource, contactAPINotSaved.LastActivityLoginDate, contactAPINotSaved.LastActivityLoginSource, contactAPINotSaved.CustomerScore, contactAPINotSaved.AccountAmount, contactAPINotSaved.IsCustomer, contactAPINotSaved.IsMoneyTransferCustomer };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
