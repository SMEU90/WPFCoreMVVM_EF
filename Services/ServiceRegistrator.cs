using Microsoft.Extensions.DependencyInjection;
using WPFCoreMVVM_EF.Services.Interfaces;

namespace WPFCoreMVVM_EF.Services
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
           .AddTransient<IDataService, DataService>()
           .AddTransient<IUserDialog, UserDialog>()
        ;
    }
}
