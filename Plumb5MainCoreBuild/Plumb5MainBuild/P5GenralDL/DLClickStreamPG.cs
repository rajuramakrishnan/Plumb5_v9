﻿using P5GenralML;
using IP5GenralDL;
using System.Data;
using System.Globalization;
using System.ComponentModel;
using DBInteraction;
using Dapper;
using Azure.Core;
using System;
using Azure;

namespace P5GenralDL
{
    public class DLClickStreamPG : CommonDataBaseInteraction , IDLClickStream
    {
        private bool _disposed = false;
        readonly CommonInfo connection;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountId"></param>
        public DLClickStreamPG(int accountId)
        {
            connection = GetDBConnection(accountId);
        }

        public DLClickStreamPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<DataSet> Select_Aggregate_Data(_Plumb5MLClickStream mlObj)
        {
            try
            {
                var storeProcCommand = "select * from selectvisitordetails(@MachineId,@ContactId,@DeviceId,@Key,@DomainName)"; 
                object? param = new { mlObj.MachineId, mlObj.ContactId, mlObj.DeviceId, mlObj.Key, mlObj.DomainName };
                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;
            }
            catch (Exception ex)
            {
                AddDbError(ex.ToString(), DateTime.Now.ToString(), "plumb5-db", ex.StackTrace.ToString());
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<DataSet> Select_ClickStream_PageDetails(_Plumb5MLClickStreamPageDetails mlObj)
        {
            try
            {
                var storeProcCommand = "select * from  selectclickstreampages(@SessionId)";  
                object? param = new { mlObj.SessionId };
                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;
            }
            catch (Exception ex)
            {
                AddDbError(ex.ToString(), DateTime.Now.ToString(), "plumb5-db", ex.StackTrace.ToString());
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<DataSet> Select_ClickStream_PageDetailsMobile(_Plumb5MLClickStreamPageDetailsMobile mlObj)
        {
            try
            {
                var storeProcCommand = "select * from selectclickstreampagesmobile (@SessionId,@DeviceId,@Key,@contactId)";
                object? param = new { mlObj.SessionId, mlObj.DeviceId, mlObj.Key, mlObj.contactId };
                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;
            }
            catch (Exception ex)
            {
                AddDbError(ex.ToString(), DateTime.Now.ToString(), "plumb5-db", ex.StackTrace.ToString());
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<DataSet> Select_Transaction(_Plumb5MLTransactionData mlObj)
        {
            try
            {
                var storeProcCommand = "select * from selecttransctionofvisitor (@MachineId,@Start,@End)";
                 
                object? param = new { mlObj.MachineId, mlObj.Start, mlObj.End };
                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;
            }
            catch (Exception ex)
            {
                AddDbError(ex.ToString(), DateTime.Now.ToString(), "plumb5-db", ex.StackTrace.ToString());
                return null;
            }
        }
        /// <summary>
        /// Inserting Notes
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<DataSet> Insert_Notes(_Plumb5MLAddNotes mlObj)
        {
            try
            {
                var storeProcCommand = "select * from note_insert(@Note,@MachineId,@ContactId,@ImageName)";
                object? param = new { mlObj.Note, mlObj.MachineId, mlObj.ContactId, mlObj.ImageName };
                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;
                  
            }
            catch (Exception ex)
            {
                AddDbError(ex.ToString(), DateTime.Now.ToString(), "plumb5-db", ex.StackTrace.ToString());
                return null;
            }
        }
        /// <summary>
        /// Selecting Notes
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<DataSet> Select_Notes(_Plumb5MLAddNotes mlObj)
        {
            try
            {
                var storeProcCommand = "select * from note_get(@MachineId,@ContactId)";
                object? param = new { mlObj.MachineId, mlObj.ContactId };
                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;
            }
            catch (Exception ex)
            {
                AddDbError(ex.ToString(), DateTime.Now.ToString(), "plumb5-db", ex.StackTrace.ToString());
                return null;
            }
        }
        //  Mobile Click Stream Details
        public async Task<DataSet> Select_MobileVisitor(_Plumb5MLClickStream mlObj)
        {
            try
            {
                var storeProcCommand = "select * from selectmobilevisitordetails(@MachineId,@Key)";
                object? param = new { mlObj.MachineId, mlObj.Key };
                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;
            }
            catch (Exception ex)
            {
                AddDbError(ex.ToString(), DateTime.Now.ToString(), "plumb5-db", ex.StackTrace.ToString());
                return null;
            }
        }
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
    }
}
