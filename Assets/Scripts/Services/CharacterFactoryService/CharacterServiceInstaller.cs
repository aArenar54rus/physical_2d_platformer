using Arenar.Services.Creator;
using Zenject;

namespace Arenar.Services
{
    public class CharacterServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CharacterSpawnController>()
                .AsSingle().NonLazy();
            
            Container.Bind<CharacterFactory>()
                .AsSingle().NonLazy();
        }
    }
}