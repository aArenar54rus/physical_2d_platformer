using System;
using UnityEngine;
using Zenject;


namespace Arenar.Character
{
    public class LiveComponent : ICharacterLiveComponent
    {
        public event Action<ICharacterEntity> OnCharacterDeath;
        public event Action<int, int> OnCharacterChangeHealthValue;
        
        
        private ICharacterEntity characterOwner;
        private CharacterData characterData;
        private int health;


        public int Health
        {
            get => health;
            set => health = Mathf.Clamp(value, 0, HealthMax);
        }
        
        public int HealthMax { get; set; }


        [Inject]
        public void Construct(ICharacterEntity characterOwner, ICharacterDataStorage<CharacterData> characterDataStorage)
        {
            this.characterOwner = characterOwner;
            characterData = characterDataStorage.Data;
        }

        public void GetDamage(int damage = 1)
        {
            if (Health == 0)
                return;
            
            Health -= damage;
            OnCharacterChangeHealthValue?.Invoke(Health, HealthMax);
            if (Health == 0)
            {
                OnCharacterDeath?.Invoke(characterOwner);
            }
        }
        
        public void Initialize()
        {
        }
        
        public void DeInitialize()
        {
        }

        public void OnActivate()
        {
            HealthMax = characterData.Health;
            Health = characterData.Health;
        }
        
        public void OnDeactivate()
        {
            OnCharacterChangeHealthValue = null;
            OnCharacterDeath = null;
            
            health = 0;
        }
    }
}