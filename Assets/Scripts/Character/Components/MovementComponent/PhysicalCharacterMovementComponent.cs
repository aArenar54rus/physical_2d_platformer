using UnityEngine;
using Zenject;

namespace Arenar.Character
{
    public class PhysicalCharacterMovementComponent : ICharacterMovementComponent, IFixedTickable
    {
        private const float SPEED_SMOOTHING = 0.1f;
        
        
        private ICharacterEntity characterOwner;
        private TickableManager tickableManager;
        private Rigidbody2D rb;
        private CharacterData characterData;

        private Vector2 direction;
        private bool isJump;
        
        
        [Inject]
        public void Construct(ICharacterEntity characterOwner,
                              TickableManager tickableManager,
                              ICharacterDataStorage<CharacterDataStorage> characterDataStorage)
        {
            this.characterOwner = characterOwner;
            this.tickableManager = tickableManager;
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
            tickableManager.AddFixed(this);
        }
        
        public void OnDeactivate()
        {
            tickableManager.RemoveFixed(this);
        }
        
        public void Move(Vector2 movement)
        {
            direction = movement;
        }
        
        public void Jump()
        {
            isJump = true;
        }
        
        public void FixedTick()
        {
            Vector2 targetVelocity = new Vector2(direction.x * characterData.Speed, rb.velocity.y);
            rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, SPEED_SMOOTHING);
            
            if (Mathf.Abs(rb.velocity.x) > characterData.MaxHorizontalSpeed)
            {
                rb.AddForce(targetVelocity, ForceMode2D.Force);
            }

            if (isJump)
            {
                rb.AddForce(Vector2.up * characterData.JumpForce, ForceMode2D.Impulse);
                isJump = false;
            }
        }
    }
}