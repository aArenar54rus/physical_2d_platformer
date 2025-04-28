using Arenar.Character;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Arenar.Services.Creator
{
	public class CharacterFactory : MonoBehaviour
	{
		[SerializeField]
		private KittyCharacter kittyCharacter;

		private Transform characterRoot;
		
		private DiContainer container;
		private TickableManager tickableManager;
		private InitializableManager initializableManager;
		

		[Inject]
		public void Construct(DiContainer container,
							TickableManager tickableManager,
							InitializableManager initializableManager)
		{
			this.container = container;
			this.tickableManager = tickableManager;
			this.initializableManager = initializableManager;
			
			characterRoot = new GameObject("CharacterRoot").transform;
		}
		
		public ICharacterEntity Create(CharacterType characterType)
		{
			ICharacterEntity characterEntity = null;
			characterEntity = GameObject.Instantiate(kittyCharacter, characterRoot)
				.GetComponent<ICharacterEntity>();
			InstallPostBindings(characterEntity, characterType);
			
			return characterEntity;
		}
		
		private void InstallPostBindings(ICharacterEntity characterEntity, CharacterType characterEntityType)
		{
			DiContainer subContainer = container.CreateSubContainer();
			subContainer.ResolveRoots();
			
			subContainer.Rebind<TickableManager>()
				.FromInstance(tickableManager)
				.NonLazy();
            
			subContainer.Rebind<InitializableManager>()
				.FromInstance(initializableManager)
				.NonLazy();
			
			switch (characterEntityType)
			{
				case CharacterType.Player:
					subContainer.Install<PlayerComponentsInstaller>();
					break;
                
				case CharacterType.RedEnemy:
					subContainer.Install<EnemyComponentsInstaller>();
					break;
			}
			
			subContainer.Bind(typeof(ICharacterEntity),
					typeof(ICharacterDataStorage<CharacterDataStorage>))
				.To<KittyCharacter>()
				.FromInstance(characterEntity)
				.AsSingle();
			
			subContainer.Inject(characterEntity);
			var components = subContainer.Resolve<Dictionary<Type, ICharacterComponent>>();
			foreach (var component in components)
				subContainer.Inject(component.Value);
		}
	}
}