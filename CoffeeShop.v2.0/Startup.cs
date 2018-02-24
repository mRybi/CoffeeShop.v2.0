using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using CoffeeShop.v2.Data;
using Microsoft.EntityFrameworkCore;
using CoffeeShop.v2.Data.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
using CoffeeShop.v2.Helpers;
using System.Reflection;

namespace CoffeeShop.v2._0
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //byte[] key = Encoding.ASCII.GetBytes(s: Configuration.GetSection("AppSettings:Token").Value);
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value);
            var key = Encoding.ASCII.GetBytes("super secret key");

            //services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=CoffeeShop;Trusted_Connection=True;MultipleActiveResultSets=true"));
            services.AddTransient<Seed>();
            services.AddScoped<ICoffeeRepository, CoffeeRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepositry>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>())));
            services.AddMvc();
            services.AddCors();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddScoped<LogUserActivity>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seed seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //seed.SeedCart();
            //seed.DeleteCoffees();
            //seed.SeedCoffees();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
