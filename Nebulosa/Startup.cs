using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Nebulosa.Bussines.Interface;
using Nebulosa.Dal.Context;
using Nebulosa.Entities.Entities;
using Nebulosa.Repository.Genericos;
using Nebulosa.Repository.Repository;
using Nebulosa.Services.AuthenticationJWT;
using Nebulosa.Services.LikeService;
using Nebulosa.Services.PostService;
using Nebulosa.Services.UsuarioService;
using Nebulosa.Services.ValidateImgAvatar;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebulosa
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
            // create cors policy
            services.AddCors(options =>
                options.AddPolicy("DefaultCorsPolicy", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod())
            );
            services.AddControllers();




            services.AddMvc(setupAction =>
            {
                setupAction.EnableEndpointRouting = false;
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;


            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

            })
         .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            //ADDING CONTEXT en el starstups.
            services.AddDbContext<NebulaDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("NebulaDb")));


            //ADDING IDENTITY
            services.AddIdentity<Usuario, Rol>(options =>
            {
                //options.Lockout.AllowedForNewUsers = true;
                //options.Lockout.MaxFailedAccessAttempts = 10;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;


                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddRoles<Rol>()
                .AddRoleManager<RoleManager<Rol>>()
                .AddEntityFrameworkStores<NebulaDbContext>()
                .AddDefaultTokenProviders();



            //ADD authenticacion JWT
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretSECRETITOxd")),
                    ValidIssuer = "https://localhost:4200",
                    ValidAudience = "https://localhost:4200",
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            services.AddSession(options =>
            {
                options.Cookie.Name = "TokenStorage";
                options.IdleTimeout = TimeSpan.FromHours(8);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(4);
            });

            services.AddTransient<IUnity, UnityOfWork>();
            services.AddScoped<IAuthenticateService, AuthenticationService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IValidateImgUserService, ValidateImgUserService>();
            services.AddScoped<ILikeService, LikeService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


            // app.UseCors("AllowWebApp");



            //else
            //{

            //    app.UseHsts();
            //}


            app.UseAuthorization();
            app.UseAuthentication();
            app.UseSession();

            app.UseHttpsRedirection();

            //app.UseMvc();

            app.UseRouting();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
