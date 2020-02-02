using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using OnionDI.Data.Repositories;
using OnionDI.Data.EF.Context;

using OnionDI.Domain.Repositories;
using OnionDI.Domain.UnitOfWork;

namespace OnionDI.Data.Dependencies
{
    public static class DataServicesExtensions
    {
        public static void AddDataProvides(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            services.AddDbContext<DataContext>(optionsAction);
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}