using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using CroissantApi.Domain.Repositories;
using CroissantApi.Domain.Services;
using CroissantApi.Models;
using CroissantApi.Persistence.Context;
using CroissantApi.Persistence.Repositories;
using CroissantApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Supermarket.API.Extensions;
using CroissantApi.Persistence.Seed;

namespace CroissantApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            if (_env.IsProduction())
            {
                services.AddDbContext<CroissantContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            } else {
                // services.AddDbContext<CroissantContext>(opt => opt.UseInMemoryDatabase("DevelopmentDatabase"));
                services.AddDbContext<CroissantContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            }
            
            services.AddAutoMapper(typeof(Startup));
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRuleRepository, UserRuleRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IRuleRepository,RuleRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRuleService, UserRuleService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IRuleService, RuleService>();
            

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddCustomSwagger();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Croissant API V1");
            });

            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // using (var scope = app.ApplicationServices.CreateScope())
                // {
                //     var ctx = scope.ServiceProvider.GetService<CroissantContext>();

                //     AppSeed SeedBuilder = new AppSeed(ctx);
                //     SeedBuilder.LoadSeeds();
                // }
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
