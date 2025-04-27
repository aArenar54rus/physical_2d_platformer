using Arenar.Character;
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
		
		private readonly DiContainer container;
		private readonly TickableManager tickableManager;
		private readonly InitializableManager initializableManager;
		
		private Dictionary<CharacterType, List<ICharacterEntity>> activeCharacters = new();
		private Dictionary<CharacterType, Queue<ICharacterEntity>> deactiveCharacters = new();


		public CharacterFactory(DiContainer container,
								TickableManager tickableManager,
								InitializableManager initializableManager)
		{
			this.container = container;
			this.tickableManager = tickableManager;
			this.initializableManager = initializableManager;
			
			characterRoot = new GameObject("CharacterRoot").transform;
		}


		public void CreateCharacter(CharacterType characterType)
		{
			ICharacterEntity characterEntity = null;
			
			if (deactiveCharacters.ContainsKey(characterType))
			{
				if (deactiveCharacters[characterType].Count > 0)
				{
					characterEntity = deactiveCharacters[characterType].Dequeue();
				}
			}
			else
			{
				deactiveCharacters.Add(characterType, new Queue<ICharacterEntity>());
			}

			if (characterEntity == null)
			{
				characterEntity = GameObject.Instantiate(kittyCharacter, characterRoot);
			}
		}
		
		
		private void InstallPostBindings(DiContainer subContainer,
										KittyCharacter characterControl,
										CharacterType characterEntityType)
		{
			subContainer.ResolveRoots();
            
			subContainer.Rebind<TickableManager>()
				.FromInstance(tickableManager)
				.NonLazy();
            
			subContainer.Rebind<InitializableManager>()
				.FromInstance(initializableManager)
				.NonLazy();
            
			subContainer.Bind(typeof(ICharacterEntity),
					typeof(ICharacterDataStorage<CharacterDataStorage>))
				.To<KittyCharacter>()
				.FromInstance(characterControl)
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
		}
	}
}