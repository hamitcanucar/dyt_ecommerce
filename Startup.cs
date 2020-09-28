using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using dytsenayasar.Context;
using dytsenayasar.Services.Abstract;
using dytsenayasar.Services.Concrete;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using dytsenayasar.Util.RazorViewRenderer;
using Microsoft.Extensions.Localization;
using dytsenayasar.Util.JsonLocalizer;
using dytsenayasar.Util.BackgroundQueueWorker;

namespace dytsenayasar
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Dyt Api", Version = "v1" });
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("postgres")));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCors();
            services.AddControllersWithViews().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
                o.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseNpgsql(Configuration.GetConnectionString("postgres"), pgOpt =>
                {
                    pgOpt.EnableRetryOnFailure();
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<IContentDeliveryService, ContentDeliveryService>();
            services.AddScoped<IUserRequestService, UserRequestService>();

            services.AddSingleton<IFileManager, FileManager>();
            services.AddSingleton<IFileTypeChecker, FileTypeChecker>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IRazorViewRenderer, RazorViewRenderer>();
            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();

            services.AddHttpClient<INotificationService, NotificationService>();

            //Background Worker Dependencies
            services.AddSingleton<IBackgroundWorkHelper, BackgroundWorkHelper>();
            services.AddHostedService<BackgroundWorker>();

            services.AddCors();
            services.AddControllersWithViews().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
                o.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var result = context.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseRequestLocalization(o =>
            {
                var cultues = new[]{
                    new CultureInfo("tr-TR"),
                    new CultureInfo("en-US")
                };

                o.SupportedCultures = cultues;
                o.SupportedUICultures = cultues;
                o.DefaultRequestCulture = new RequestCulture("en-US");
            });

            var corsOrigins = Configuration.GetSection("Cors:Origins").Get<string[]>();
            app.UseCors(o =>
            {
                o.WithOrigins(corsOrigins);
                o.AllowAnyMethod();
                o.AllowAnyHeader();
                o.WithExposedHeaders("Count");
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/V1/swagger.json", "Api"));
            }
        }
    }
}
