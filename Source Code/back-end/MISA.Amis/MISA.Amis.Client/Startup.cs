using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MISA.Core.Entities.Directory;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Infrastructures;
using MISA.Core.Interfaces.Services;
using MISA.Core.Services;
using MISA.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Amis.Client
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("*")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();

                                  });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MISA.Amis.Client", Version = "v1" });
            });

            // Use exceptions to modify the response
            services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()));

            // Config DI:
            // 1. Base:
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            // 2. Employee
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MISA.Amis.Client v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);
            // global cors policy
            //app.UseCors(x => x
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .SetIsOriginAllowed(origin => true) // allow any origin
            //    .AllowCredentials()); // allow credentials
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
