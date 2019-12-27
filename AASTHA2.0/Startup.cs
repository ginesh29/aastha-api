using AASTHA2.Entities;
using AASTHA2.Interfaces;
using AASTHA2.Middleware;
using AASTHA2.Repositories;
using AASTHA2.Services;
using AASTHA2.Validator;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AASTHA2
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services
                .AddMvc()
                .AddFluentValidation(fv =>
                {
                    //fv.ImplicitlyValidateChildProperties = true;
                    fv.RegisterValidatorsFromAssemblyContaining<PatientValidator>();
                }
                ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2).
                AddJsonOptions(option =>
                    {
                        option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        option.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        option.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                        option.SerializerSettings.DateFormatString = "dd MMM yyyy h:mm:ss tt";
                    });
            services.AddDbContext<AASTHAContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("AASTHADB"));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ServicesWrapper>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "AASTHA API",
                    Description = "AASTHA Meternity Core Web API"
                });
                c.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Type = "apiKey"
                    });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>{
                {
                        "Bearer", Enumerable.Empty<string>() }
                });
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Log.Logger = new LoggerConfiguration()
           .WriteTo.File($"Logs/Events/EventLog-{DateTime.Now.ToString("ddMMyyyy")}.log")
           .WriteTo.Logger(x => x.Filter.ByIncludingOnly(y => y.Level == LogEventLevel.Error || y.Level == LogEventLevel.Fatal)
           .WriteTo.File($"Logs/Exceptions/ExceptionLog-{DateTime.Now.ToString("ddMMyyyy")}.log"))
           .WriteTo.Logger(x => x.Filter.ByIncludingOnly(y => y.Level == LogEventLevel.Warning)
           .WriteTo.File($"Logs/Warnings/WarningLog-{DateTime.Now.ToString("ddMMyyyy")}.log"))
           .CreateLogger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseCors("AllowAnyOrigin");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AASTHA API V1");
            });
            app.UseResponseWrapper();
            app.UseExceptionWrapper();

            app.UseMvc();
        }
    }
}
