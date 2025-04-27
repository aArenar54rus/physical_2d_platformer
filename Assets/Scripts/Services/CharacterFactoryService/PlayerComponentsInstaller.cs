using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            
            ICharacterLiveComponent animationComponent = new LiveComponent();
            characterComponentsPool.Add(typeof(ICharacterLiveComponent), animationComponent);
            Container.BindInstance(animationComponent).AsSingle();
            Container.Inject(animationComponent);
        }
    }
}