using System;
using System.Collections.Generic;
using Zenject;

namespace Arenar.Character
{
    public class PlayerComponentsInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallCharactersComponents();
        }

        private void InstallCharactersComponents()
        {
            Dictionary<Type, ICharacterComponent> characterComponentsPool = new Dictionary<Type, ICharacterComponent>();
            
            ICharacterLiveComponent liveComponent = new LiveComponent();
            characterComponentsPool.Add(typeof(ICharacterLiveComponent), liveComponent);
            Container.BindInstance(liveComponent).AsSingle();
            Container.Inject(liveComponent);

            ICharacterMovementComponent movementComponent = new PhysicalCharacterMovementComponent();
            characterComponentsPool.Add(typeof(ICharacterMovementComponent), movementComponent);
            Container.BindInstance(movementComponent).AsSingle();
            Container.Inject(movementComponent);

            ICharacterControlComponent controlComponent = new CharacterInputControlComponent();
            characterComponentsPool.Add(typeof(ICharacterControlComponent), controlComponent);
            Container.BindInstance(controlComponent).AsSingle();
            Container.Inject(controlComponent);
        }
    }
}