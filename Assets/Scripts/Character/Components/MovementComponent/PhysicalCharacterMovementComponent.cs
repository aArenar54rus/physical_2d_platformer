using UnityEngine;
using Zenject;

namespace Arenar.Character
{
    public class PhysicalCharacterMovementComponent : ICharacterMovementComponent
    {
        private ICharacterEntity characterOwner;
        private Rigidbody2D rb;
        private CharacterData characterData;
        
        
        [Inject]
        public void Construct(ICharacterEntity characterOwner, ICharacterDataStorage<CharacterDataStorage> characterDataStorage)
        {
            this.characterOwner = characterOwner;
            rb = characterDataStorage.Data.CharacterRigidbody2D;
            characterData = characterDataStorage.Data.CharacterData;
        }
        
        public void Initialize()
        {
            
        }
        
        public void DeInitialize()
        {
            
        }
        
        public void OnActivate()
        {
            
        }
        
        public void OnDeactivate()
        {
            
        }
        
        public void Move(Vector2 movement)
        {
            if (movement == Vector2.zero)
                return;
            
            Vector2 velocity = movement.normalized * characterData.Speed;

            rb.velocity = velocity;
            /*
            float rotation = rotationSpeed * Time.fixedDeltaTime;
            transform.Rotate(Vector3.forward, -rotation);
            */
        }
        
        public void Jump()
        {
            rb.AddForce(Vector2.up * characterData.JumpForce, ForceMode2D.Impulse);
        }
    }
}