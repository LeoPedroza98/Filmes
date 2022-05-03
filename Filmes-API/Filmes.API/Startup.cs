using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.OData.Extensions;
using System.Linq;
using Newtonsoft.Json;
using Filmes.API.Extensions;
using Filmes.DOMAIN.Entity.All;

namespace Filmes.API
{
    public class Startup
    {

        private readonly ILoggerFactory _loggerFactory;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                .AddConsole((options) => { })
                .AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information);
            });
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInjections();
            services.AddJwt(Configuration);

            services.AddDbContext<DATA.Context.FilmesContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("Principal"));
                options.UseLoggerFactory(_loggerFactory);
            });
            services.AddControllers(mvc => mvc.EnableEndpointRouting = false)
                 .AddNewtonsoftJson(options => {
                     options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                     options.UseCamelCasing(true);
                 }
                 );
            services.AddOData();

            services.AddCors(o => o.AddPolicy("CorsLibera", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            UpdateDatabase(app);

            app.UseCors("CorsLibera");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Filter().OrderBy().Count().MaxTop(null);
                routeBuilder.EnableDependencyInjection();
            });
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DATA.Context.FilmesContext>())
                {
                    context.Database.Migrate();

                    if (!context.Usuarios.Any())
                    {
                        context.Usuarios.Add(new DOMAIN.Entity.All.Usuario()
                        {
                            Login = "Master",
                            Senha = "AQAAAAEAACcQAAAAEANRCJM8tYz2tD2JeiByfFLTPLGrKjo5CdndcUAKVrSn9Uek59ymlGz4qXdKj89xUQ==",
                            PerfilId = PerfilUsuario.Master.Id,
                            Nome = "Master"
                        });
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
