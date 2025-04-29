using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Arenar.Character
{
	public class EnemyComponentsInstaller : Installer
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

			ICharacterMovementComponent movementComponent = new PhysicalCharacterMovementComponent();
			characterComponentsPool.Add(typeof(ICharacterMovementComponent), movementComponent);
			Container.BindInstance(movementComponent).AsSingle();

			ICharacterControlComponent controlComponent = new AiControlComponent();
			characterComponentsPool.Add(typeof(ICharacterControlComponent), controlComponent);
			Container.BindInstance(controlComponent).AsSingle();
			
			ICharacterRaycastComponent raycastComponent = new CharacterRaycastComponent();
			characterComponentsPool.Add(typeof(ICharacterRaycastComponent), raycastComponent);
			Container.BindInstance(raycastComponent).AsSingle();
			
			Container.BindInstance(characterComponentsPool).AsSingle();
		}
	}
}