using CleanArchitecture.Api.Options;
using CleanArchitecture.Application;
using CleanArchitecture.Infra.Data.Context;
using CleanArchitecture.Infra.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;

namespace CleanArchitecture.Api
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
            #region Register DbContext

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"));
            });

            #endregion

            #region Register Controller

            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            #endregion

            #region Register CORS

            services.AddCors();

            #endregion

            #region Register Swagger

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
            });

            #endregion

            #region Register Dependency

            RegisterServices(services);

            #endregion

            #region Register Jwt

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

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
                    ValidateAudience = false
                };
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger();
            app.UseSwaggerUI(option => 
            { 
                option.SwaggerEndpoint(swaggerOptions.JsonRoute, swaggerOptions.Description); 
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
        }
    }
}
