using System.Threading.Tasks;

namespace WPFCoreMVVM_EF.Infrastructure
{
    internal delegate Task ActionAsync();

    internal delegate Task ActionAsync<in T>(T parameter);
}
