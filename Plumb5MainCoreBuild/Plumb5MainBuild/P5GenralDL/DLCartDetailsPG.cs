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
    public class DLCartDetailsPG : CommonDataBaseInteraction, IDLCartDetails
    {
        CommonInfo connection;
        public DLCartDetailsPG()
        {
            connection = GetDBConnection();
        }
    
        public async Task<Int32> SaveDetails(CartDetails cartDetails)
        {
            string storeProcCommand = "select * from Cart_Details(@Action, @UserInfoUserId, @TxId, @FeatureId, @FeatureName, @PurchaseLink, @MonthlyPrice, @YearlyPrice, @UnitType, @OpttedRange, @SelectedYearly, @PriceInINRorDollar)";
            object? param = new { Action= "Save", cartDetails.UserInfoUserId, cartDetails.TxId, cartDetails.FeatureId, cartDetails.FeatureName, cartDetails.PurchaseLink, cartDetails.MonthlyPrice, cartDetails.YearlyPrice, cartDetails.UnitType, cartDetails.OpttedRange, cartDetails.SelectedYearly, cartDetails.PriceInINRorDollar };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<List<CartDetails>> Get(string TxId)
        {
            string storeProcCommand = "select * from Cart_Details(@Action,@TxId)";
            object? param = new { Action= "Get", TxId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<CartDetails>(storeProcCommand, param)).ToList();

        }
    }
}
