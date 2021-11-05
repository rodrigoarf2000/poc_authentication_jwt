using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace FastCore.Api.Extensions
{
    /// <summary>
    /// Extenção de configuração.
    /// </summary>
    public static class JwtAuthenticationExtensions
    {
        /// <summary>
        /// Metodo de extenção Jwt para este serviço.
        /// </summary>
        /// <param name="services">Objeto com os serviços da aplicação contido nele.</param>
        /// <param name="configuration">Objeto de configuração da aplicação.</param>
        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Configura de geração do token do projeto.
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Signature"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Ativa o uso do token como forma de autorizar o acesso a recursos deste projeto.
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }
    }
}
