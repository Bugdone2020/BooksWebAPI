using BooksWebAPI_DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksWebAPI_BL;
using BooksWebAPI_BL.Services.BookService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BooksWebAPI_BL.Auth;
using BooksWebAPI_BL.Services.AuthService;

namespace BooksWebAPI
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // specifies whether the publisher will be validated when validating the token
                            ValidateIssuer = true,
                            // a string representing the publisher
                            ValidIssuer = AuthOptions.ISSUER,
                            // whether the consumer of the token will be validated
                            ValidateAudience = true,
                            // setting consumer token
                            ValidAudience = AuthOptions.AUDIENCE,
                            // whether lifetime will be validated
                            ValidateLifetime = true,
                            // security key setting
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // security key validation
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddScoped
                ( typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddDbContext<EFCoreDbContext>(options =>
               options.UseSqlServer("name=ConnectionStrings:Default"));//(Configuration["ConnectionStrings:Default"])

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BooksWebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BooksWebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
