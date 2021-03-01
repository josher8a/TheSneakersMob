using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Stripe;
using TheSneakersMob.Extensions.IdentityServer;
using TheSneakersMob.Extensions.Swagger;
using TheSneakersMob.Infrastructure.Data;
using TheSneakersMob.Infrastructure.Email;
using TheSneakersMob.Infrastructure.Stripe;
using TheSneakersMob.Models;
using TheSneakersMob.Services.Auctions;
using TheSneakersMob.Services.Profiles;
using TheSneakersMob.Services.Sells;

namespace TheSneakersMob
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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddDbContext<ApplicationDbContext>(options => {
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
               options.EnableSensitiveDataLogging();
            });

            services.AddIdentity<ApplicationUser,IdentityRole>(options => 
                {
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>()
                .AddInMemoryClients(Config.GetClients(Configuration["IdentityServer:ApiRedirectUrl"]));

            services.AddAuthentication()
                .AddIdentityServerJwt()
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                })
                .AddGoogle(options =>
                {
                    options.ClientId = Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                });

            services.AddControllers()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            services.AddCustomSwagger(Configuration);
            services.ConfigureCustomPolicies();
            services.AddAutoMapper(typeof(Startup));
            services.AddAuthorization();
            services.Configure<EmailSettings>(Configuration.GetSection("SendGrid"));
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<StripeService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<SellRepository>();
            services.AddTransient<AuctionRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            
            context.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedData.Seed(userManager, context);
            }
            StripeConfiguration.ApiKey = Configuration["Stripe:SecretKey"];

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheSneakersMob API V1");
                c.OAuthClientId("the_sneakers_mob_api");
                c.OAuthAppName("TheSneakersMob Api - Swagger");
                c.RoutePrefix = string.Empty;
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }

    static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "The SneakersMob API",
                    Version = "v1",
                });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(configuration["IdentityServer:AuthorizationUrl"]),
                            Scopes = new Dictionary<string, string>()
                            {
                                { "TheSneakersMobAPI", "TheSneakersMob API" }
                            }
                        }
                    }
                });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
                var xmlFile = Path.ChangeExtension(typeof(Startup).Assembly.Location, ".xml");
                c.IncludeXmlComments(xmlFile);
            });
            return services;
        }

         public static IServiceCollection ConfigureCustomPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("MustOwnSell", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new MustOwnSellRequirement());
                });
                options.AddPolicy("MustOwnAuction", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new MustOwnAuctionRequirement());
                });
                options.AddPolicy("MustOwnProfile", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new MustOwnProfileRequirement());
                });
            });

            services.AddScoped<IAuthorizationHandler, MustOwnSellHandler>();
            services.AddScoped<IAuthorizationHandler, MustOwnAuctionHandler>();
            services.AddScoped<IAuthorizationHandler, MustOwnProfileHandler>();
            return services;
        }

    }
}
