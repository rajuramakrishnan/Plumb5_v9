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
    public class DLCustomEventsSegmentBuilderPG : CommonDataBaseInteraction, IDLCustomEventsSegmentBuilder
    {
        CommonInfo connection;

        public DLCustomEventsSegmentBuilderPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async Task<Int32> Save(CustomEventsSegmentBuilder segmentBuilder)
        {
            try
            {
                string storeProcCommand = "select customevents_segmentbuilder_save(@UserInfoUserId, @GroupId, @Status, @SegmentQuery, @SegmentJson, @ExecutionType, @ExecutionIntervalMinutes, @OneTimeExecutionDateTime, @EveryDayExecutionTime)";
                object? param = new { segmentBuilder.UserInfoUserId, segmentBuilder.GroupId, segmentBuilder.Status, segmentBuilder.SegmentQuery, segmentBuilder.SegmentJson, segmentBuilder.ExecutionType, segmentBuilder.ExecutionIntervalMinutes, segmentBuilder.OneTimeExecutionDateTime, segmentBuilder.EveryDayExecutionTime };
                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<bool> Update(CustomEventsSegmentBuilder segmentBuilder)
        {
            string storeProcCommand = "select   from customevents_segmentbuilder_Update(@GroupId, @SegmentQuery, @SegmentJson, @UpdatedDate)";
            object? param = new { segmentBuilder.GroupId, segmentBuilder.SegmentQuery, segmentBuilder.SegmentJson, segmentBuilder.UpdatedDate };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param)>0;
        }

        public async Task<IEnumerable<CustomEventsSegmentBuilder>> GET(int GroupId)
        {
            string storeProcCommand = "select * from customevents_segmentbuilder_get(@GroupId)";
            object? param = new { GroupId };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<CustomEventsSegmentBuilder>(storeProcCommand, param);
        }

        public async Task<IEnumerable<MLSegmentOutputColumns>> GetTestResultByQuery(string FilterQuery)
        {
            string storeProcCommand = "select * from customevents_segmentbuilder_testquery(@FilterQuery)";

            object? param = new { FilterQuery }; 
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLSegmentOutputColumns>(storeProcCommand, param);
        }

        public async Task<IEnumerable<CustomEventsSegmentBuilder>> GetList(CustomEventsSegmentBuilder segmentBuilder, List<string> fieldName)
        {
            string storeProcCommand = "select * from customevents_segmentbuilder_getlist(@Status,@fieldName)";
            string fieldNames = fieldName != null ? string.Join(",", fieldName.ToArray()) : null;
            object? param = new { segmentBuilder.Status, fieldNames };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<CustomEventsSegmentBuilder>(storeProcCommand, param);
        }

        public async void StartFilter(int SegmentBuilderId)
        {
            string storeProcCommand = "select customevents_segmentbuilder_startfilter(@SegmentBuilderId)";
            object? param = new { SegmentBuilderId };
            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool> UpdateDeleteStatus(int GroupId)
        {
            string storeProcCommand = "select customevents_segmentbuilder_updatedeletestatus(@GroupId)";
            object? param = new { GroupId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param)>0;
        }
        #region Dispose Method
        bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

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
