using Arenar.Services.Creator;
using UnityEngine;
using Zenject;

namespace Arenar.Services
{
    public class CharacterServiceInstaller : MonoInstaller
    {
        [SerializeField]
        private CharacterFactory characterFactory;
        
        
        public override void InstallBindings()
        {
            Container.Bind<CharacterSpawnController>()
                .AsSingle().NonLazy();
            
            Container.Bind<CharacterFactory>()
                .FromInstance(characterFactory)
                .AsSingle().NonLazy();
        }
    }
}