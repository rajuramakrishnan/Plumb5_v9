﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using Npgsql;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLCustomEventsSQL : CommonDataBaseInteraction, IDLCustomEvents
    {

        readonly CommonInfo connection;
        public DLCustomEventsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public async Task<Int32> GetEventscounts(Nullable<DateTime> fromDateTime, Nullable<DateTime> ToDateTime, int customeventoverviewid, Contact contact, string machineid, Customevents customevents)
        {
            string storeProcCommand = "Custom_Events";

            object? param = new
            {
                Action = "MaxCount",
                fromDateTime,
                ToDateTime,
                customevents.Id,
                customeventoverviewid,
                contact.Name,
                contact.EmailId,
                contact.PhoneNumber,
                machineid,
                customevents.EventData1,
                customevents.EventData2,
                customevents.EventData3,
                customevents.EventData4,
                customevents.EventData5,
                customevents.EventData6,
                customevents.EventData7,
                customevents.EventData8,
                customevents.EventData9,
                customevents.EventData10,
                customevents.EventData11,
                customevents.EventData12,
                customevents.EventData13,
                customevents.EventData14,
                customevents.EventData15,
                customevents.EventData16,
                customevents.EventData17,
                customevents.EventData18,
                customevents.EventData19,
                customevents.EventData20,
                customevents.EventData21,
                customevents.EventData22,
                customevents.EventData23,
                customevents.EventData24,
                customevents.EventData25,
                customevents.EventData26,
                customevents.EventData27,
                customevents.EventData28,
                customevents.EventData29,
                customevents.EventData30,
                customevents.EventData31,
                customevents.EventData32,
                customevents.EventData33,
                customevents.EventData34,
                customevents.EventData35,
                customevents.EventData36,
                customevents.EventData37,
                customevents.EventData38,
                customevents.EventData39,
                customevents.EventData40,
                customevents.EventData41,
                customevents.EventData42,
                customevents.EventData43,
                customevents.EventData44,
                customevents.EventData45,
                customevents.EventData46,
                customevents.EventData47,
                customevents.EventData48,
                customevents.EventData49,
                customevents.EventData50,
            };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<Customevents>> GetEventsReportData(Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int customeventoverviewid, int ContactId, int OffSet, int FetchNext, Contact contact, string machineid, Customevents customevents)
        {
            string storeProcCommand = "Custom_Events";
            object? param = new
            {
                Action = "GetCustomEventCartReport",
                FromDateTime,
                ToDateTime,
                customevents.Id,
                customeventoverviewid,
                ContactId,
                OffSet,
                FetchNext,
                contact.Name,
                contact.EmailId,
                contact.PhoneNumber,
                machineid,
                customevents.EventData1,
                customevents.EventData2,
                customevents.EventData3,
                customevents.EventData4,
                customevents.EventData5,
                customevents.EventData6,
                customevents.EventData7,
                customevents.EventData8,
                customevents.EventData9,
                customevents.EventData10,
                customevents.EventData11,
                customevents.EventData12,
                customevents.EventData13,
                customevents.EventData14,
                customevents.EventData15,
                customevents.EventData16,
                customevents.EventData17,
                customevents.EventData18,
                customevents.EventData19,
                customevents.EventData20,
                customevents.EventData21,
                customevents.EventData22,
                customevents.EventData23,
                customevents.EventData24,
                customevents.EventData25,
                customevents.EventData26,
                customevents.EventData27,
                customevents.EventData28,
                customevents.EventData29,
                customevents.EventData30,
                customevents.EventData31,
                customevents.EventData32,
                customevents.EventData33,
                customevents.EventData34,
                customevents.EventData35,
                customevents.EventData36,
                customevents.EventData37,
                customevents.EventData38,
                customevents.EventData39,
                customevents.EventData40,
                customevents.EventData41,
                customevents.EventData42,
                customevents.EventData43,
                customevents.EventData44,
                customevents.EventData45,
                customevents.EventData46,
                customevents.EventData47,
                customevents.EventData48,
                customevents.EventData49,
                customevents.EventData50,
            };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Customevents>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<IEnumerable<Customevents>> UCPGetEventsName(DateTime? fromDateTime, DateTime? toDateTime, int customeventoverviewid, int contactID, string customEventName, int offSet, int fetchNext, string machineid)
        {

            string storeProcCommand = "Custom_Events";

            object? param = new { Action = "UCPGetCustomEventName", fromDateTime, toDateTime, customeventoverviewid, contactID, machineid };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Customevents>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<IEnumerable<Customevents>> UCPGetEventsReportData(Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int customeventoverviewid, int ContactId, int OffSet, int FetchNext, Contact contact, string machineid, Customevents customevents)
        {
            string storeProcCommand = "Custom_Events";

            object? param = new
            {
                Action = "UCPGetCustomEventCartReport",
                FromDateTime,
                ToDateTime,
                customevents.Id,
                customeventoverviewid,
                ContactId,
                OffSet,
                FetchNext,
                contact.Name,
                contact.EmailId,
                contact.PhoneNumber,
                machineid,
                customevents.EventData1,
                customevents.EventData2,
                customevents.EventData3,
                customevents.EventData4,
                customevents.EventData5,
                customevents.EventData6,
                customevents.EventData7,
                customevents.EventData8,
                customevents.EventData9,
                customevents.EventData10,
                customevents.EventData11,
                customevents.EventData12,
                customevents.EventData13,
                customevents.EventData14,
                customevents.EventData15,
                customevents.EventData16,
                customevents.EventData17,
                customevents.EventData18,
                customevents.EventData19,
                customevents.EventData20,
                customevents.EventData21,
                customevents.EventData22,
                customevents.EventData23,
                customevents.EventData24,
                customevents.EventData25,
                customevents.EventData26,
                customevents.EventData27,
                customevents.EventData28,
                customevents.EventData29,
                customevents.EventData30,
                customevents.EventData31,
                customevents.EventData32,
                customevents.EventData33,
                customevents.EventData34,
                customevents.EventData35,
                customevents.EventData36,
                customevents.EventData37,
                customevents.EventData38,
                customevents.EventData39,
                customevents.EventData40,
                customevents.EventData41,
                customevents.EventData42,
                customevents.EventData43,
                customevents.EventData44,
                customevents.EventData45,
                customevents.EventData46,
                customevents.EventData47,
                customevents.EventData48,
                customevents.EventData49,
                customevents.EventData50,
            };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Customevents>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }
        public async Task<bool> SearchAndAddtoGroup(int UserInfoUserId, int UserGroupId, int customeventoverviewid, Customevents customevents, int GroupId, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime)
        {
            string storeProcCommand = "Custom_Events";

            object? param = new
            {
                Action = "InsertToRequestedGroup",
                FromDateTime,
                ToDateTime,
                customeventoverviewid,
                customevents.EventData1,
                customevents.EventData2,
                customevents.EventData3,
                customevents.EventData4,
                customevents.EventData5,
                customevents.EventData6,
                customevents.EventData7,
                customevents.EventData8,
                customevents.EventData9,
                customevents.EventData10,
                customevents.EventData11,
                customevents.EventData12,
                customevents.EventData13,
                customevents.EventData14,
                customevents.EventData15,
                customevents.EventData16,
                customevents.EventData17,
                customevents.EventData18,
                customevents.EventData19,
                customevents.EventData20,
                customevents.EventData21,
                customevents.EventData22,
                customevents.EventData23,
                customevents.EventData24,
                customevents.EventData25,
                customevents.EventData26,
                customevents.EventData27,
                customevents.EventData28,
                customevents.EventData29,
                customevents.EventData30,
                customevents.EventData31,
                customevents.EventData32,
                customevents.EventData33,
                customevents.EventData34,
                customevents.EventData35,
                customevents.EventData36,
                customevents.EventData37,
                customevents.EventData38,
                customevents.EventData39,
                customevents.EventData40,
                customevents.EventData41,
                customevents.EventData42,
                customevents.EventData43,
                customevents.EventData44,
                customevents.EventData45,
                customevents.EventData46,
                customevents.EventData47,
                customevents.EventData48,
                customevents.EventData49,
                customevents.EventData50,
                GroupId,
                UserInfoUserId,
                UserGroupId
            };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }
        public async Task<Int32> GetAggergatecounts(DateTime fromDateTime, DateTime ToDateTime, int customeventoverviewid, string groupbyeventfields, string displayextrafields)
        {
            string storeProcCommand = "Custom_EventsAggregateDetails";
            object? param = new { Action = "AggergateMaxCount", fromDateTime, ToDateTime, customeventoverviewid, groupbyeventfields };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<DataSet> GetAggregateData(string FromDateTime, string ToDateTime, int customeventoverviewid, string groupbyeventfields, string displayextrafields, int OffSet, int FetchNext)
        {

            string storeProcCommand = "Custom_EventsAggregateDetails";
            object? param = new { Action = "GetAggergateEventReport", FromDateTime, ToDateTime, customeventoverviewid, groupbyeventfields, displayextrafields, OffSet, FetchNext };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }
        public async Task<IEnumerable<Customevents>> GetMaxAggregateDetails(DateTime fromDateTime, DateTime ToDateTime, int customeventoverviewid, int OffSet, int FetchNext, Customevents customevents, string queryforaggrgatedetails, string groupbyquery, string CustomFieldname)
        {
            string storeProcCommand = "Custom_EventsAggregateDetails";


            object? param = new
            {
                Action = "GetMaxAggVisitorDetails",
                fromDateTime,
                ToDateTime,
                customeventoverviewid,
                OffSet,
                FetchNext,
                customevents.EventData1,
                customevents.EventData2,
                customevents.EventData3,
                customevents.EventData4,
                customevents.EventData5,
                customevents.EventData6,
                customevents.EventData7,
                customevents.EventData8,
                customevents.EventData9,
                customevents.EventData10,
                customevents.EventData11,
                customevents.EventData12,
                customevents.EventData13,
                customevents.EventData14,
                customevents.EventData15,
                customevents.EventData16,
                customevents.EventData17,
                customevents.EventData18,
                customevents.EventData19,
                customevents.EventData20,
                customevents.EventData21,
                customevents.EventData22,
                customevents.EventData23,
                customevents.EventData24,
                customevents.EventData25,
                customevents.EventData26,
                customevents.EventData27,
                customevents.EventData28,
                customevents.EventData29,
                customevents.EventData30,
                customevents.EventData31,
                customevents.EventData32,
                customevents.EventData33,
                customevents.EventData34,
                customevents.EventData35,
                customevents.EventData36,
                customevents.EventData37,
                customevents.EventData38,
                customevents.EventData39,
                customevents.EventData40,
                customevents.EventData41,
                customevents.EventData42,
                customevents.EventData43,
                customevents.EventData44,
                customevents.EventData45,
                customevents.EventData46,
                customevents.EventData47,
                customevents.EventData48,
                customevents.EventData49,
                customevents.EventData50,
                queryforaggrgatedetails,
                groupbyquery
            };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Customevents>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<IEnumerable<Contact>> GeteventcontactData(DateTime fromDateTime, DateTime ToDateTime, int customeventoverviewid, int OffSet, int FetchNext, Customevents customevents)
        {
            string storeProcCommand = "Custom_EventsAggregateDetails";

            object? param = new
            {
                Action = "GetAggConactDetails",
                fromDateTime,
                ToDateTime,
                customeventoverviewid,
                OffSet,
                FetchNext,
                customevents.EventData1,
                customevents.EventData2,
                customevents.EventData3,
                customevents.EventData4,
                customevents.EventData5,
                customevents.EventData6,
                customevents.EventData7,
                customevents.EventData8,
                customevents.EventData9,
                customevents.EventData10,
                customevents.EventData11,
                customevents.EventData12,
                customevents.EventData13,
                customevents.EventData14,
                customevents.EventData15,
                customevents.EventData16,
                customevents.EventData17,
                customevents.EventData18,
                customevents.EventData19,
                customevents.EventData20,
                customevents.EventData21,
                customevents.EventData22,
                customevents.EventData23,
                customevents.EventData24,
                customevents.EventData25,
                customevents.EventData26,
                customevents.EventData27,
                customevents.EventData28,
                customevents.EventData29,
                customevents.EventData30,
                customevents.EventData31,
                customevents.EventData32,
                customevents.EventData33,
                customevents.EventData34,
                customevents.EventData35,
                customevents.EventData36,
                customevents.EventData37,
                customevents.EventData38,
                customevents.EventData39,
                customevents.EventData40,
                customevents.EventData41,
                customevents.EventData42,
                customevents.EventData43,
                customevents.EventData44,
                customevents.EventData45,
                customevents.EventData46,
                customevents.EventData47,
                customevents.EventData48,
                customevents.EventData49,
                customevents.EventData50
            };


            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Contact>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }
        public async Task<IEnumerable<Customevents>> GetUniquevisitorDetails(DateTime fromDateTime, DateTime ToDateTime, int customeventoverviewid, int OffSet, int FetchNext, Customevents customevents)
        {
            string storeProcCommand = "Custom_EventsAggregateDetails";

            object? param = new
            {
                Action = "GetAggVisitorDetails",
                fromDateTime,
                ToDateTime,
                customeventoverviewid,
                OffSet,
                FetchNext,
                customevents.EventData1,
                customevents.EventData2,
                customevents.EventData3,
                customevents.EventData4,
                customevents.EventData5,
                customevents.EventData6,
                customevents.EventData7,
                customevents.EventData8,
                customevents.EventData9,
                customevents.EventData10,
                customevents.EventData11,
                customevents.EventData12,
                customevents.EventData13,
                customevents.EventData14,
                customevents.EventData15,
                customevents.EventData16,
                customevents.EventData17,
                customevents.EventData18,
                customevents.EventData19,
                customevents.EventData20,
                customevents.EventData21,
                customevents.EventData22,
                customevents.EventData23,
                customevents.EventData24,
                customevents.EventData25,
                customevents.EventData26,
                customevents.EventData27,
                customevents.EventData28,
                customevents.EventData29,
                customevents.EventData30,
                customevents.EventData31,
                customevents.EventData32,
                customevents.EventData33,
                customevents.EventData34,
                customevents.EventData35,
                customevents.EventData36,
                customevents.EventData37,
                customevents.EventData38,
                customevents.EventData39,
                customevents.EventData40,
                customevents.EventData41,
                customevents.EventData42,
                customevents.EventData43,
                customevents.EventData44,
                customevents.EventData45,
                customevents.EventData46,
                customevents.EventData47,
                customevents.EventData48,
                customevents.EventData49,
                customevents.EventData50
            };


            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Customevents>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public Customevents? GetCustomevents(int ContactId, string CustomEventName, string CustomEventColumnNames, string TopData, string Orderby, string MachineId = null)
        {
            string storeProcCommand = "Custom_Events";
            object? param = new { Action = "GetCustomevents", ContactId, MachineId, CustomEventName, CustomEventColumnNames, TopData, Orderby };

            using var db = GetDbConnection(connection.Connection);
            return db.QueryFirstOrDefault<Customevents?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<Customevents>> GetRevenueEventsReportData(Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int customeventoverviewid, int OffSet, int FetchNext)
        {
            string storeProcCommand = "Custom_Events";
            object? param = new { Action = "GetRevenueCustomEventReport", FromDateTime, ToDateTime, customeventoverviewid, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Customevents>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }
        public async Task<IEnumerable<Customevents>> GetRevenuesingleEventsReportData(int customeventoverviewid, int Id)
        {
            string storeProcCommand = "Custom_Events";

            object? param = new { Action = "GetRevenueSingleCustomEventReport", customeventoverviewid, Id };
            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Customevents>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<Int32> GetRevenueMaxCount(int customeventoverviewid, DateTime fromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "Custom_Events";
            object? param = new { Action = "GetRevenueMaxCount", fromDateTime, ToDateTime, customeventoverviewid };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<DataSet> GetRevenueDetails(int customeventoverviewid, string dynamicefieldnames, DateTime fromDateTime, DateTime ToDateTime, int OffSet, int FetchNext, string BindType)
        {
            string storeProcCommand = "Custom_Events";
            object? param = new { Action = "GetRevenueReport", customeventoverviewid, fromDateTime, ToDateTime, dynamicefieldnames, OffSet, FetchNext, BindType };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
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