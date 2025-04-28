using Arenar.Services;
using Zenject;

namespace Arenar.Installers
{
    public class GameServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameService>().To<GameService>().AsSingle().NonLazy();
        }
    }
}