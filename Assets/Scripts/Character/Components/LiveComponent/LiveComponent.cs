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
        private ICharacterMovementComponent movementComponent;
        
        private CollisionObserver collisionObserver;
        private CharacterData characterData;
        private int health;


        public int Health
        {
            get => health;
            set => health = Mathf.Clamp(value, 0, HealthMax);
        }
        
        public int HealthMax { get; set; }


        [Inject]
        public void Construct(ICharacterEntity characterOwner, ICharacterDataStorage<CharacterDataStorage> characterDataStorage)
        {
            this.characterOwner = characterOwner;
            this.characterData = characterDataStorage.Data.CharacterData;
            collisionObserver = characterDataStorage.Data.CollisionObserver;
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
            characterOwner.TryGetCharacterComponent(out movementComponent);

            collisionObserver.onTriggerCharacter += TriggerCharacterHandler;
        }
        
        public void OnDeactivate()
        {
            OnCharacterChangeHealthValue = null;
            OnCharacterDeath = null;
            
            collisionObserver.onTriggerCharacter -= TriggerCharacterHandler;
            
            health = 0;
        }


        private void TriggerCharacterHandler(ICharacterEntity character)
        {
            if (character.CharacterType == CharacterType.Player)
            {
                return;
            }
            
            movementComponent.Jump();

            var height = character.CharacterTransform.position.y - characterOwner.CharacterTransform.position.y;
            if (height < 0.5f)
                GetDamage();
        }
    }
}