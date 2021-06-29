using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MISA.ApplicationCore;
using MISA.ApplicationCore.Interfaces;
/*using MISA.ApplicationCore.Interfaces.Repository;
using MISA.ApplicationCore.Interfaces.Service;*/
using MISA.ApplicationCore.Service;
using MISA.CukCuk.Web.Middwares;
using MISA.Infrastructure;
using System;

namespace MISA.CukCuk.Web
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

            services.AddControllers();
            // add new middaware process mapping data with guid
            SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));
            // add swagger doc
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MISA.CukCuk.Web", Version = "v1" });
            });
            // Add scoped for customer repository
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            // Add scoped for employee repository
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            // Add scoped for base repository
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MISA.CukCuk.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
