
using System;
using System.Text;
using AutoMapper;
using CroissantApi.Domain.Repositories;
using CroissantApi.Domain.Services;
using CroissantApi.Helpers;
using CroissantApi.Persistence.Context;
using CroissantApi.Persistence.Repositories;
using CroissantApi.Persistence.Seed;
using CroissantApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Supermarket.API.Extensions;

namespace CroissantApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        readonly string ApiCorsPolicyName = "ApiCorsPolicyName";
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
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IRuleRepository, RuleRepository>();
            services.AddScoped<IPaymentRecordRepository, PaymentRecordRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IRuleService, RuleService>();
            services.AddScoped<IPaymentRecordService, PaymentRecordService>();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddCustomSwagger();

            // Allow CORS on localhost:3000
            services.AddCors(options =>
            {
                options.AddPolicy(
                    ApiCorsPolicyName,
                    builder => {
                        builder
                            // .WithOrigins("http://localhost:3000", "https://localhost:3000")
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                        ;
                    }
                );
            });

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
            });

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

                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<CroissantContext>();

                    AppSeed SeedBuilder = new AppSeed(ctx);
                    SeedBuilder.LoadSeeds();
                }
             }

            // app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseCors(); // Not working without the lines bellow...
            app.UseCors(
                options => options.SetIsOriginAllowed(origin => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireCors(ApiCorsPolicyName)
                ;
            });
        }
    }
}
