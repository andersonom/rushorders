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

namespace RushOrders
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //TODO: Encapsulate repositories inside service layer, not calling directly from controller
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<MongoContext>();

            services.AddDbContext<SqlContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlContext")));

            services.AddMvc()
                .AddFluentValidation()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IValidator<Customer>, CustomerValidator>();
            services.AddTransient<IValidator<Order>, OrderValidator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Orders API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionMiddleware();
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders API V1");
            });
        }
    }
}
