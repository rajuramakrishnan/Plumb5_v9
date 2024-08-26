﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            //service.AddDbContext<PlumbmasterContext>(options =>
            //    options.UseNpgsql(config.GetConnectionString("MasterConnection")
            //));

            //service.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = "127.0.0.1:6379";
            //});

            //service.AddScoped<IDLAccountRepository, DLAccount>();
            //service.AddScoped<IDLMailSentRepository, DLMailSent>();
            //service.AddScoped<IDLMailClickRepository, DLMailClick>();
            //service.AddScoped<IDLUserInfoRepository, DLUserInfo>();
            //service.AddScoped<IDLUserInValidLoginRepository, DLUserInValidLogin>();
            //service.AddScoped<IDLAccountTimeZone, DLAccountTimeZone>();
            //services.AddScoped<IDLAccount, DLAccount>();
            //services.AddScoped<Func<int, IDLAccountTimeZone>>
            //    (
            //        (provider) => new Func<int, IDLAccountTimeZone>((account_Id) => new DLAccountTimeZone(account_Id))
            //    );
            return services;
        }
    }
}
