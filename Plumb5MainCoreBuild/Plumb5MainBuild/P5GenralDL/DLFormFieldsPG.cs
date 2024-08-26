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
    public class DLFormFieldsPG : CommonDataBaseInteraction, IDLFormFields
    {
        CommonInfo connection = null;
        public DLFormFieldsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLFormFieldsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int16> Save(FormFields formFields)
        {
            string storeProcCommand = "select form_field_save(@UserInfoUserId, @FormId, @Name, @FieldType, @SubFields, @RelationField, @Mandatory, @FormScore, @PhoneValidationType, @FieldDisplay, @CalendarDisplayType, @ContactMappingField, @FieldPriority, @FormLayoutOrder, @FormUniqueIdentifier, @FieldShowOrHide )";
            object? param = new { formFields.UserInfoUserId, formFields.FormId, formFields.Name, formFields.FieldType, formFields.SubFields, formFields.RelationField, formFields.Mandatory, formFields.FormScore, formFields.PhoneValidationType, formFields.FieldDisplay, formFields.CalendarDisplayType, formFields.ContactMappingField, formFields.FieldPriority, formFields.FormLayoutOrder, formFields.FormUniqueIdentifier, formFields.FieldShowOrHide };

            using var db = GetDbConnection(connection.Connection);
            return Convert.ToInt16(await db.ExecuteScalarAsync<int>(storeProcCommand, param));
        }

        public async Task<bool>  Update(FormFields formFields)
        {
            string storeProcCommand = "select form_field_update(@Id, @UserInfoUserId, @FormId, @Name, @FieldType, @SubFields, @RelationField, @Mandatory, @FormScore, @PhoneValidationType, @FieldDisplay, @CalendarDisplayType, @ContactMappingField, @FieldPriority, @FormLayoutOrder, @FormUniqueIdentifier, @FieldShowOrHide )";
            object? param = new { formFields.Id, formFields.UserInfoUserId, formFields.FormId, formFields.Name, formFields.FieldType, formFields.SubFields, formFields.RelationField, formFields.Mandatory, formFields.FormScore, formFields.PhoneValidationType, formFields.FieldDisplay, formFields.CalendarDisplayType, formFields.ContactMappingField, formFields.FieldPriority, formFields.FormLayoutOrder, formFields.FormUniqueIdentifier, formFields.FieldShowOrHide };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param)>0;
        }

        public async Task<bool> Delete(Int16 Id)
        {
            string storeProcCommand = "select form_field_delete(@Id)"; 
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> DeleteFields(Int16 FormId)
        {
            string storeProcCommand = "select form_field_deletefields(@FormId)";

            object? param = new { FormId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<IEnumerable<FormFields>> GET(int FormId)
        {
            string storeProcCommand = "select * from form_field_get(@FormId)"; 
            object? param = new { FormId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<FormFields>(storeProcCommand, param);
        }

        public async Task<IEnumerable<FormFields>> GET()
        {
            string storeProcCommand = "select * from form_field_get()";

            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<FormFields>(storeProcCommand, param);
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
