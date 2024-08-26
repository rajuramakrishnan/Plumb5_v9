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
    public class DLFormFieldsSQL : CommonDataBaseInteraction, IDLFormFields
    {
        CommonInfo connection = null;
        public DLFormFieldsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLFormFieldsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int16> Save(FormFields formFields)
        {
            string storeProcCommand = "Form_Field";
            object? param = new { Action= "Save", formFields.UserInfoUserId, formFields.FormId, formFields.Name, formFields.FieldType, formFields.SubFields, formFields.RelationField, formFields.Mandatory, formFields.FormScore, formFields.PhoneValidationType, formFields.FieldDisplay, formFields.CalendarDisplayType, formFields.ContactMappingField, formFields.FieldPriority, formFields.FormLayoutOrder, formFields.FormUniqueIdentifier, formFields.FieldShowOrHide };

            using var db = GetDbConnection(connection.Connection);
            return Convert.ToInt16( await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure));
        }

        public async Task<bool> Update(FormFields formFields)
        {
            string storeProcCommand = "Form_Field";
            object? param = new { Action = "Update",formFields.Id, formFields.UserInfoUserId, formFields.FormId, formFields.Name, formFields.FieldType, formFields.SubFields, formFields.RelationField, formFields.Mandatory, formFields.FormScore, formFields.PhoneValidationType, formFields.FieldDisplay, formFields.CalendarDisplayType, formFields.ContactMappingField, formFields.FieldPriority, formFields.FormLayoutOrder, formFields.FormUniqueIdentifier, formFields.FieldShowOrHide };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure)>0;
        }

        public async Task<bool> Delete(Int16 Id)
        {
            string storeProcCommand = "Form_Field)";
            object? param = new { Action = "Delete", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> DeleteFields(Int16 FormId)
        {
            string storeProcCommand = "Form_Field";

            object? param = new { Action = "DeleteFields", FormId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<IEnumerable<FormFields>> GET(int FormId)
        {
            string storeProcCommand = "Form_Field";
            object? param = new { Action = "GET", FormId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<FormFields>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<IEnumerable<FormFields>> GET()
        {
            string storeProcCommand = "Form_Field";

            object? param = new { Action = "GET" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<FormFields>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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
