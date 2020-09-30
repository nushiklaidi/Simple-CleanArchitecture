using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infra.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //CleanArchitecture.Application
            services.AddScoped<IBookService, BookService>();

            //CleanArchitecture.Domain.Interfaces | CleanArchitecture.Infra.Data.Repositories
            services.AddScoped<IBookRepository, BookRepository>();
        }
    }
}
