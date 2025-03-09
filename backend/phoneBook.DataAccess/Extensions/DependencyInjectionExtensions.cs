using Microsoft.Extensions.DependencyInjection;
using phoneBook.DataAccess.Repository;
using phoneBook.DataAccess.Services.Contacts;
using phoneBook.DataAccess.Services.Token;
using phoneBook.Entities.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phoneBook.DataAccess.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
