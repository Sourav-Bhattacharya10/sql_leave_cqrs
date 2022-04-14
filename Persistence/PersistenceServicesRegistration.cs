using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using Persistence.Contracts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LeaveManagementDbContext>(options => 
        {
            options.UseSqlServer(configuration.GetConnectionString("LeaveManagementConnectionString"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();

        return services;
    }
}