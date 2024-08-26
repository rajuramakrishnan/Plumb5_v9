﻿using Dapper;
using DBInteraction;
using P5GenralML;
using IP5GenralDL;
using System.Data;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.IdentityModel.Protocols;

namespace P5GenralDL
{
    public class DLAnalyticReportsSQL : CommonDataBaseInteraction, IDLAnalyticReports
    {
        CommonInfo connection;
        public DLAnalyticReportsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public async Task<Int32> Save(AnalyticReports analyticReports)
        {
            string storeProcCommand = "Analytic_Reports";
            object? param = new { Action = "Save", analyticReports.UserInfoUserId, analyticReports.Name, analyticReports.AnalyticQuery, analyticReports.AnalyticJson, analyticReports.GroupBy, analyticReports.Status };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<Int32> Update(AnalyticReports analyticReports)
        {
            string storeProcCommand = "Analytic_Reports";
            object? param = new { Action = "Update", analyticReports.UserInfoUserId, analyticReports.Name, analyticReports.AnalyticQuery, analyticReports.AnalyticJson, analyticReports.GroupBy };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<AnalyticReports>> GetAnalyticsSaveReport(string UserIdList)
        {
            string storeProcCommand = "Analytic_Reports";
            object? param = new { Action = "GetList", UserIdList };
            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<AnalyticReports>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }
        public async Task<bool> DeleteSavedSearch(int Id)
        {
            string storeProcCommand = "Analytic_Reports";
            object? param = new { Action = "Delete", Id };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<AnalyticReports?> GetFilterConditionDetails(int FilterConditionId)
        {
            string storeProcCommand = "Analytic_Reports";
            object? param = new { Action = "GET", Id = FilterConditionId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<AnalyticReports?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<Int32> GetMaxCount(AnalyticCustomReports filterLead, string Groupby, DateTime FromDateTime, DateTime ToDateTime)
        {
            List<AnalyticCustomReports> reportObject = new List<AnalyticCustomReports>();
            reportObject.Add(filterLead);
            DataTable filterLeadTable = new DataTable();
            filterLeadTable = ToDataTables(reportObject);
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in filterLeadTable.Columns)
                column.ColumnName = ti.ToLower(column.ColumnName);

            string storeProcCommand = "Analytic_Reports";
            object? param = new { Action = "MaxCount", filterLeadTable, Groupby, FromDateTime, ToDateTime };
            using var db = GetDbConnection(connection.Connection);

            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }


        public async Task<DataSet> GetAnalyticReports(AnalyticCustomReports filterDataJson, string Groupby, int offset, int fetchNext, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime)
        {
            List<AnalyticCustomReports> reportObject = new List<AnalyticCustomReports>();
            reportObject.Add(filterDataJson);
            DataTable filterLeadTable = new DataTable();
            filterLeadTable = ToDataTables(reportObject);
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in filterLeadTable.Columns)
                column.ColumnName = ti.ToLower(column.ColumnName);
            string storeProcCommand = "Analytic_Reports";
            object? param = new { Action = "GET", filterLeadTable, Groupby, offset, fetchNext, FromDateTime, ToDateTime };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
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
