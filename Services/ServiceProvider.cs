using WPFCoreMVVM_EF.Services.Interfaces;

namespace WPFCoreMVVM_EF.Services
{
    public class ServiceProvider//////////??
    {
        public static IServiceLocator Instance { get; private set; }

        public static void RegisterServiceLocator(IServiceLocator s)
        {
            Instance = s;
        }
    }
}
