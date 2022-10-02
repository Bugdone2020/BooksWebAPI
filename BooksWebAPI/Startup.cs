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
using BooksWebAPI_BL.Services.LibraryService;
using BooksWebAPI_BL.Options;
using System.Text;
using BooksWebAPI_BL.Services.HashService;
using BooksWebAPI_BL.Services.SMTPService;
using BooksWebAPI_BL.Services.EncryptionService;

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
            services.AddHttpContextAccessor();
            services.Configure<AuthOptions>(options => 
            Configuration.GetSection(nameof(AuthOptions)).Bind(options));
            services.Configure<SmtpConfiguration>(options =>
            Configuration.GetSection(nameof(SmtpConfiguration)).Bind(options));
            services.Configure<EncryptionConfiguration>(options =>
            Configuration.GetSection(nameof(EncryptionConfiguration)).Bind(options));
            var authOptions = Configuration.GetSection(nameof(AuthOptions)).Get<AuthOptions>();

            // adding authentication services
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //authentication scheme - using jwt tokens
                    .AddJwtBearer(options => // connect authentication using jwt tokens
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // specifies whether the publisher will be validated when validating the token
                            ValidateIssuer = true,
                            // a string representing the publisher
                            ValidIssuer = authOptions.Issuer,
                            // whether the consumer of the token will be validated
                            ValidateAudience = true,
                            // setting consumer token
                            ValidAudience = authOptions.Audience,
                            // whether lifetime will be validated
                            ValidateLifetime = true,
                            // security key setting
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authOptions.Key)),
                            // security key validation
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddScoped
                ( typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILibraryService, LibraryService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<ISendingBlueSmtpService, SendingBlueSmtpService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddDbContext<EFCoreDbContext>(options =>
               options.UseSqlServer("name=ConnectionStrings:Default"));//(Configuration["ConnectionStrings:Default"])

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lesson1", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });
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

            app.UseAuthentication(); // add authentication middleware

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
