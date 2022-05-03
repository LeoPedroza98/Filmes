using Filmes.API.Providers;
using Filmes.DATA.Interface;
using Filmes.DATA.Interface.All;
using Filmes.DATA.Repository.All;
using Filmes.SERVICE.Interface;
using Filmes.SERVICE.Interface.All;
using Filmes.SERVICE.Services;
using Filmes.SERVICE.Services.All;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Filmes.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddInjections(this IServiceCollection services)
        {
            services.AddScoped<IFilmeProvider, FilmeProvider>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IAutenticadorService, AutenticadorService>();

            #region All
            services.AddTransient<IFilmeRepository, FilmeRepository>();
            services.AddTransient<IFilmeService, FilmeService>();

            services.AddTransient<IDiretorRepository, DiretorRepository>();
            services.AddTransient<IDiretorService, DiretorService>();

            services.AddTransient<IFilmeDiretorRepository,FilmeDiretorRepository>();
            services.AddTransient<IFilmeDiretorService,FilmeDiretorService>();

            services.AddTransient<IPerfilUsuarioRepository, PerfilUsuarioRepository>();
            services.AddTransient<IPerfilUsuarioService, PerfilUsuarioService>();

            services.AddTransient<IGeneroRepository, GeneroRepository>();
            services.AddTransient<IGeneroService, GeneroService>();

            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            #endregion
        }

        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JwtIssuer"],
                    ValidAudience = configuration["JwtIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
