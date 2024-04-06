using Networking;
using Zenject;

namespace Main.Zenject
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ClientServerConnection>().AsSingle();
        }
    }
}