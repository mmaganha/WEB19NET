using Microsoft.Extensions.DependencyInjection;
using ProjetoWEB19NET.Dominio.Interfaces;
using ProjetoWEB19NET.Dominio.Services;
using ProjetoWEB19NET.Estrutura.AcessoDados.Repositorio;

namespace ProjetoWEB19NET.IoT
{
    public static class ResolveDependecias
    {
        public static IServiceCollection Dependencias(this IServiceCollection services)
        {
            services.AddScoped<IReceita, RepositorioReceita>();
            services.AddScoped<ServiceReceita>();

            services.AddScoped<ICategoria, RepositorioCategoria>();
            services.AddScoped<ServiceCategoria>();
            return services;
        }
    }
}
