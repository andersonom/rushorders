using System;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RushOrders.Core.Interfaces.Repositories;
using RushOrders.Core.Models;
using RushOrders.Data.Context;
using RushOrders.Data.Repositories;
using RushOrders.Middleware;
using RushOrders.Core.Validations;
using Swashbuckle.AspNetCore.Swagger;
using FluentValidation.AspNetCore;
using RushOrders.Core.Interfaces.Services;
using RushOrders.Service;

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
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICustomerService, CustomerService>();

            //services.AddScoped<MongoContext>();
            //Database in memory
            services.AddDbContext<SqlContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()), ServiceLifetime.Singleton);

            services.AddMvc()
                .AddFluentValidation()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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
