using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CinemaChain.API.Auth;
using CinemaChain.API.AuthModel;
using CinemaChain.API.Helpers;
using CinemaChain.API.ViewModels.Validations;
using CinemaChain.Data.Context;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Infrastructure.Services;
using CinemaChain.Models.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using CinemaChain.Data.Interfaces.Repositories;
using CinemaChain.Data.Repositories;

namespace CinemaChain.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            string connection = Configuration.GetConnectionString("DBConnection");
            services.AddDbContext<DB_Context>(options => options.UseSqlServer(connection));
            InitDependency(services);
            services.AddSingleton<IJWTFactory, JWTFactory>();
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var builder = services.AddIdentityCore<AppUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            }).AddRoles<IdentityRole>();
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<DB_Context>().AddDefaultTokenProviders();


            var tokenValidationParameters = new TokenValidationParameters
            {
                //ValidateIssuer = true,
                //ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                //ValidateAudience = true,
                //ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                Configuration.Bind("JwtBearer", options);
                options.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.TokenValidationParameters = tokenValidationParameters;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,

                    ValidateIssuer = true,
                    ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                    ValidateAudience = true,
                    ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _signingKey,

                    RequireExpirationTime = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("clientpol", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });


            services.AddAutoMapper();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddCors();

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseExceptionHandler(
            builder =>
               {
                   builder.Run(
                              async context =>
                              {
                                  context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                  context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                                  var error = context.Features.Get<IExceptionHandlerFeature>();
                                  if (error != null)
                                  {
                                      //context.Response.AddApplicationError(error.Error.Message);
                                      await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                                  }
                              });
               });

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
        public void InitDependency(IServiceCollection services)
        {
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<ICinemaService, CinemaService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IFilmService, FilmService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOwnerService, OwnerService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISeanceService, SeanceService>();
            services.AddTransient<IBusySeatService, BusySeatService>();
            services.AddTransient<ISeatService, SeatService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFilmImageService, FilmImageService>();
            services.AddTransient<ICinemaImageService, CinemaImageService>();
        }
    }
}
