using Zenject;

namespace Arenar.Services.LevelsService
{
    public class LevelsServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILevelsService>()
                .To<SimpleLevelsService>()
                .AsSingle().NonLazy();
        }
    }
}