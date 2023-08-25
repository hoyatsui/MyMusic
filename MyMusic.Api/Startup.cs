using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyMusic.Core;
using MyMusic.Data;
using Microsoft.EntityFrameworkCore;
using MyMusic.Core.Services;
using MyMusic.Services;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using MyMusic.Core.Auth;
using Microsoft.AspNetCore.Identity;
using MyMusic.Api.Settings;
using MyMusic.Api.Extensions;
using Microsoft.OpenApi.Models;

namespace MyMusic.Api
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
            // Add dependency injection for Unit Of Work.
            // There are three types of lifetime: Singleton, Scoped, Transient.
            // Singleton: The service is created once and reused. Objects are the same for every request. 
            // Scoped: The service is created once per request.
            // Transient: The service is created each time it is requested. Objects are different for every request. One new instance is provided to every controller and every service.
            services.AddScoped<IUnitOfWork, UnitOfWork>(); // AddScoped() method registers the service with a scoped lifetime
            services.AddControllers();
            //Here we add our MyMusicDbContext , tell to use SqlServer using the Default connection strings in appsettings.json and that our migrations should be run in MyMusic.Data .
            services.AddDbContext<MyMusicDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("MyMusic.Data")));
            // Add dependency injection for services.
            services.AddTransient<IMusicService, MusicService>();
            services.AddTransient<IArtistService, ArtistService>();
            // Add Swagger.
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT containing userid claim",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                var security =
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                },
                                UnresolvedReference = true
                            },
                            new List<string>()
                        }
                    };
                options.AddSecurityRequirement(security);
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My Music API", Version = "v1" });
            });
            // Add AutoMapper.
            services.AddAutoMapper(typeof(Startup));
            // Add Identity.
            services.AddIdentity<User, Role>(
            ).AddEntityFrameworkStores<MyMusicDbContext>()  // AddEntityFrameworkStores tells that our MyMusicDbContext is going to be where our Identity’s information is stored
            .AddDefaultTokenProviders(); // AddDefaultTokenProviders adds token provider to generate tokens for password reset, change email etc.

            // Add JWT Authentication.
            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            services.AddAuth(jwtSettings);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAuth(); // Add JWT Authentication.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(); // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwaggerUI(options => // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My Music V1");
            });
        }
    }
}
