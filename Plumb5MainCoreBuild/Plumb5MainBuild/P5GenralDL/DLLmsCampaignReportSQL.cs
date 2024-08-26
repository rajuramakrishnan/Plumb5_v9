﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLLmsCampaignReportSQL : CommonDataBaseInteraction, IDLLmsCampaignReport
    {
        CommonInfo connection;
        public DLLmsCampaignReportSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public async Task<Int32> GetMaxCount(string UserIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int OrderbyVal, LmsCustomReport filterLead)
        {
            List<LmsCustomReport> reportObject = new List<LmsCustomReport>();
            reportObject.Add(filterLead);
            DataTable filterLeadTable = new DataTable();
            filterLeadTable = ToDataTables(reportObject);
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in filterLeadTable.Columns)
                column.ColumnName = ti.ToLower(column.ColumnName);
            string storeProcCommand = "Lms_CampaignReport";
            object? param = new {Action= "MaxCount" ,UserIdList, FromDateTime, ToDateTime, OrderbyVal, filterLeadTable };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        private static DataTable ToDataTables<T>(IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prp = props[i];
                table.Columns.Add(prp.Name, Nullable.GetUnderlyingType(prp.PropertyType) ?? prp.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        public async Task<DataSet> GetLmsCampaignReport(string UserIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int OffSet, int FetchNext, int OrderbyVal, LmsCustomReport filterLead)
        {
            List<LmsCustomReport> reportObject = new List<LmsCustomReport>();
            reportObject.Add(filterLead);
            DataTable filterLeadTable = new DataTable();
            filterLeadTable = ToDataTables(reportObject);
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in filterLeadTable.Columns)
                column.ColumnName = ti.ToLower(column.ColumnName);
            string storeProcCommand = "Lms_CampaignReport";
            object? param = new { Action= "GetLmsCampaignReport",UserIdList, FromDateTime, ToDateTime, OffSet, FetchNext, OrderbyVal, filterLeadTable };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;


        }
        public async Task<DataSet> GetLmsPhoneCallResponseDetails(int UserInfoUserId, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int OrderbyVal, string CalledNumber, LmsCustomReport filterLead, string CallEvents)
        {
            List<LmsCustomReport> reportObject = new List<LmsCustomReport>();
            reportObject.Add(filterLead);
            DataTable filterLeadTable = new DataTable();
            filterLeadTable = ToDataTables(reportObject);
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in filterLeadTable.Columns)
                column.ColumnName = ti.ToLower(column.ColumnName);
            string storeProcCommand = "Lms_CampaignReport";
            object? param = new { Action = "GetLmsPhoneCallResponseDetails", UserInfoUserId, FromDateTime, ToDateTime, OrderbyVal, CalledNumber, filterLeadTable, CallEvents };

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
