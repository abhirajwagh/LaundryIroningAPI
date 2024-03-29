﻿using LaundryIroningBusiness.Entity;
using LaundryIroningCommon;
using LaundryIroningContract.Business;
using LaundryIroningContract.Infrastructure;
using LaundryIroningContract.Repository;
using LaundryIroningData.Data;
using LaundryIroningData.DataContext;
using LaundryIroningEntity.Contract;
using LaundryIroningRepository.CommonRepository;
using LaundryIroningRepository.SQLRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LaundryIroningAPI.Container
{
    public class DIContainer
    {
        public static class SQLContainer
        {
            public static void Injector(IServiceCollection services)
            {
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddScoped<IBuildConnectionString, BuildConnectionString>();
                services.AddScoped<IExecuterStoreProc, SqlProcExecuterRepository>();
                services.AddScoped<DbContext, ApiDBContext>();

                services.AddScoped<IUserBusiness, UserBusiness>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IUserTypesRepository, UserTypesRepository>();

                services.AddScoped<IIroningBusiness, IroningBusiness>();
                services.AddScoped<IIroningRepository, IroningRepository>();

                services.AddScoped<ILaundryBusiness, LaundryBusiness>();
                services.AddScoped<ILaundryRepository, LaundryRepository>();

                services.AddScoped<IIroningLaundryBusiness, IroningLaundryBusiness>();
                services.AddScoped<IIroningLaundryRepository, IroningLaundryRepository>();
                services.AddScoped<IOrderAgentMappingRepository, OrderAgentMappingRepository>();

                services.AddScoped<IPromoCodesRepository, PromoCodesRepository>();

                
            }
                   
        }
    }
}
