using Arenar.Character;
using UnityEngine;
using Zenject;


namespace Arenar.Services.Creator
{
	public class CharacterFactory
	{
		[SerializeField]
		private KittyCharacter kittyCharacter;

		private Transform characterRoot;
		
		private readonly DiContainer container;
		private readonly TickableManager tickableManager;
		private readonly InitializableManager initializableManager;
		

		public CharacterFactory(DiContainer container,
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
            
			subContainer.Bind(typeof(ICharacterEntity),
					typeof(ICharacterDataStorage<CharacterDataStorage>))
				.To<ICharacterEntity>()
				.FromInstance(characterEntity)
				.AsSingle();

			switch (characterEntityType)
			{
				case CharacterType.Player:
					subContainer.Install<PlayerComponentsInstaller>();
					break;
                
				case CharacterType.RedEnemy:
					subContainer.Install<EnemyComponentsInstaller>();
					break;
			}
			
			subContainer.Inject(characterEntity);
		}
	}
}