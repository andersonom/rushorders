using System;
using System.Collections.Generic;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RushOrders.Core.Interfaces.Repositories;
using RushOrders.Core.Models;
using RushOrders.Data.Context;
using RushOrders.Data.Repositories;
using RushOrders.Core.Validations;
using FluentValidation.AspNetCore;
using MongoDB.Driver;
using RushOrders.Core.Interfaces.Services;
using RushOrders.Service;
using Moq;
using RushOrders.Tests.Mock;

// ReSharper disable All

namespace RushOrders
{
    public class StartupTest
    {
        public StartupTest(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Services
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IOrderRepository, OrderMockedRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICustomerService, CustomerService>();

            ////Mocked MongoDb was aproach was replaced by using OrderMockedRepository since MongoCollectionImpl couldnt be mocked.
            //Mock<IMongoContext> mongoMoq = new Mock<IMongoContext>();
            //mongoMoq.SetupAllProperties();
            //mongoMoq.Setup(x => x.Database.GetCollection<Order>("Order", null));//.Returns(new MongoCollectionImpl<Order>());
            //services.AddSingleton<IMongoContext>(mongoMoq.Object);

            //Sql in memory
            services.AddDbContext<SqlContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()), ServiceLifetime.Singleton);

            services.AddMvc()
                .AddFluentValidation()//Fluent Validation
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Fluent Validation
            services.AddTransient<IValidator<Customer>, CustomerValidator>();
            services.AddTransient<IValidator<Order>, OrderValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
