using System;
using System.Collections.Generic;
using Arenar.Character;
using UnityEngine;

namespace Arenar.Services.Creator
{
    public class CharacterSpawnController
    {
        public event Action<ICharacterEntity> OnCreatePlayerCharacter;
        public event Action<ICharacterEntity> OnCreateEnemyCharacter;
        
        
        private Transform charactersContainer;
        
        private CharacterFactory characterFactory;
        
        private Dictionary<CharacterType, List<ICharacterEntity>> activeCharacters = new();
        private Dictionary<CharacterType, Queue<ICharacterEntity>> createdCharacters = new();


        public ICharacterEntity PlayerCharacter {
            get
            {
                if (activeCharacters.ContainsKey(CharacterType.Player))
                    return activeCharacters[CharacterType.Player][0];

                return null;
            }
        }
        
        private Transform CharactersContainer
        {
            get
            {
                if (charactersContainer == null)
                {
                    charactersContainer = GameObject.Instantiate(new GameObject()).transform;
                    charactersContainer.gameObject.name = "Characters Container";
                }

                return charactersContainer;
            }
        }


        public CharacterSpawnController(CharacterFactory characterFactory)
        {
            this.characterFactory = characterFactory;
        }
        
        
        public ICharacterEntity GetCharacter(CharacterType characterType)
        {
            ICharacterEntity characterEntity = null;

            if (!createdCharacters.ContainsKey(characterType))
            {
                createdCharacters.Add(characterType, new Queue<ICharacterEntity>());
            }

            if (createdCharacters[characterType].Count > 0)
            {
                characterEntity = createdCharacters[characterType].Dequeue();
            }
            else
            {
                characterEntity = characterFactory.Create(characterType);
                characterEntity.CharacterTransform.SetParent(CharactersContainer);
            }

            characterEntity.CharacterTransform.gameObject.SetActive(true);
            characterEntity.Initialize();
            
            if (!activeCharacters.ContainsKey(characterType))
                activeCharacters.Add(characterType, new List<ICharacterEntity>());
            activeCharacters[characterType].Add(characterEntity);
            
            if (characterType == CharacterType.Player)
                OnCreatePlayerCharacter?.Invoke(characterEntity);
            else
                OnCreateEnemyCharacter?.Invoke(characterEntity);

            return characterEntity;
        }
        
        public void ReturnCharacter(ICharacterEntity characterEntity)
        {
            characterEntity.DeActivate();
            characterEntity.DeInitialize();
            
            if (!createdCharacters.ContainsKey(characterEntity.CharacterType))
                createdCharacters.Add(characterEntity.CharacterType, new Queue<ICharacterEntity>());
            createdCharacters[characterEntity.CharacterType].Enqueue(characterEntity);

            if (activeCharacters.ContainsKey(characterEntity.CharacterType))
                activeCharacters[characterEntity.CharacterType].Remove(characterEntity);
            
            characterEntity.CharacterTransform.gameObject.SetActive(false);
        }
        
        public void ReturnAllCharacters()
        {
            var localActiveCharacters = new Dictionary<CharacterType, List<ICharacterEntity>>(this.activeCharacters);
            foreach ((CharacterType _, List<ICharacterEntity> entities) in localActiveCharacters)
            {
                foreach (var character in entities)
                    ReturnCharacter(character);
            }
        }
    }
}