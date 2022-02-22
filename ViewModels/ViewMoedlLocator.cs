using Microsoft.Extensions.DependencyInjection;

namespace WPFCoreMVVM_EF.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
