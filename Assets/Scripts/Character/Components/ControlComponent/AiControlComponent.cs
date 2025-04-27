using UnityEngine;
using Zenject;

namespace Arenar.Character
{
    public class AiControlComponent : ICharacterControlComponent, ITickable
    {
        private TickableManager tickableManager;
        private ICharacterEntity characterOwner;
        
        private ICharacterLiveComponent liveComponent;
        private ICharacterMovementComponent movementComponent;
        
        private bool controlStatus = false;
        
        private Vector2 direction = Vector2.zero;
        private readonly float changeDirectionChance = 10;
        
        
        [Inject]
        public void Construct(ICharacterEntity characterOwner,
                              TickableManager tickableManager)
        {
            this.characterOwner = characterOwner;
            this.tickableManager = tickableManager;
        }
        
        
        public void Initialize()
        {
        }
        
        public void DeInitialize()
        {
        }
        
        public void OnActivate()
        {
            characterOwner.TryGetCharacterComponent(out liveComponent);
            characterOwner.TryGetCharacterComponent(out movementComponent);
            tickableManager.Add(this);
            direction = new Vector2((Random.Range(-100f, 100f) > 0) ? 1 : -1, 0);
            controlStatus = true;
        }

        public void OnDeactivate()
        {
            tickableManager.Remove(this);
            controlStatus = false;
        }

        public void SetControlStatus(bool status)
        {
            controlStatus = status;
        }
        
        public void Tick()
        {
            if (!controlStatus || liveComponent.Health == 0)
                return;
            
            movementComponent.Move(direction);
            
            int random = Random.Range(0, 100);
            if (random < changeDirectionChance)
            {
                direction *= -1;
            }
        }
    }
}