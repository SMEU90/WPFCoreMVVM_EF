
namespace WPFCoreMVVM_EF.Services.Interfaces
{
    public interface IServiceLocator////////////???
    {
        void Register<TInterface, TImplementation>() where TImplementation : TInterface;

        TInterface Get<TInterface>();
    }
}
