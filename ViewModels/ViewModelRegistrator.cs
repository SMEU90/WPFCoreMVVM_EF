using Microsoft.Extensions.DependencyInjection;

namespace WPFCoreMVVM_EF.ViewModels
{
    internal static class ViewModelRegistrator
    {
        public static IServiceCollection AddViews(this IServiceCollection services) => services
           .AddSingleton<MainWindowViewModel>()
        ;
    }
}