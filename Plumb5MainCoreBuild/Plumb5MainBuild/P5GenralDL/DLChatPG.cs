﻿using Dapper;
using DBInteraction;
using Newtonsoft.Json;
using P5GenralML;
using PushSharp.Core;
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
    public class DLChatPG : CommonDataBaseInteraction, IDLChat
    {
        CommonInfo connection;
        public DLChatPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public DLChatPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
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

        public async Task<Int32> Save(ChatDetails chat)
        {
            List<ChatDetails> chatObject = new List<ChatDetails>();
            chatObject.Add(chat);
            DataTable chatDetails = new DataTable();
            chatDetails = ToDataTables(chatObject);
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in chatDetails.Columns)
                column.ColumnName = ti.ToLower(column.ColumnName);

            string storeProcCommand = "select * from chat_details_save(@chatDetails)";
            object? param = new { chatDetails = new JsonParameter(JsonConvert.SerializeObject(chatDetails)) };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<bool> Update(ChatDetails chat)
        {
            List<ChatDetails> chatObject = new List<ChatDetails>();
            chatObject.Add(chat);
            DataTable chatDetails = new DataTable();
            chatDetails = ToDataTables(chatObject);
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in chatDetails.Columns)
                column.ColumnName = ti.ToLower(column.ColumnName);

            string storeProcCommand = "select * from chat_details_update(@chatDetails)";

            object? param = new { chatDetails = new JsonParameter(JsonConvert.SerializeObject(chatDetails)) };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param)>0;

        }

        public async Task<List<ChatDetails>> GET(ChatDetails chat, int OffSet, int FetchNext, List<string> fieldName = null)
        {
            string storeProcCommand = "select * from chat_details_get(@Id,@Name,@OffSet,@FetchNext)";
            object? param = new { chat.Id, chat.Name, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ChatDetails>(storeProcCommand, param)).ToList();

        }

        public async Task<ChatDetails?> GET(ChatDetails chat, List<string> fieldName = null)
        {
            string storeProcCommand = "select * from chat_details_getdetails(@Id,@Name)";
            object? param = new { chat.Id, chat.Name };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<ChatDetails?>(storeProcCommand, param);


        }

        public async Task<int> GetMaxCount(ChatDetails chat)
        {
            string storeProcCommand = "select * from chat_details_maxcount(@Name)";
            object? param = new { chat.Name };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<bool> Delete(Int32 Id)
        {
            string storeProcCommand = "select * from chat_details_delete(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> ToogleStatus(Int16 chatId, bool ChatStatus)
        {
            try
            {
                string storeProcCommand = "select * from chat_details_tooglestatus(@chatId,@ChatStatus)";
                object? param = new { chatId, ChatStatus };

                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
            }
            catch (Exception ex)
            {
                 return false;
            }

        }

        public async Task<bool> ChangePriority(int Id, Int16 ChatPriority)
        {
            string storeProcCommand = "select * from chat_details_updateprioritystatus(@Id,@ChatPriority)";
            object? param = new { Id, ChatPriority };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }

        public async Task<bool> UpdateAgentOnlineStatus(ChatDetails chat)
        {
            string storeProcCommand = "select * from chat_details_updateagentonline(@Id,@IsAgentOnline)";
            object? param = new { chat.Id, chat.IsAgentOnline };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

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
