using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infra.IoC
{
    public static class DependencyContainerConfig
    {
        public static void RegisterServices(IServiceCollection services)
        {

            //CleanArchitecture.Application
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthService, AuthService>();

            //CleanArchitecture.Domain.Interfaces | CleanArchitecture.Infra.Data.Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            ////CleanArchitecture.Domain.Interfaces | CleanArchitecture.Infra.Data.Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
