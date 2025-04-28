using Arenar.Services.PlayerInputService;
using UnityEngine;
using Zenject;


namespace Arenar.Character
{
    public class CharacterInputControlComponent : ICharacterControlComponent, ITickable
    {
        private TickableManager tickableManager;
        private IPlayerInputService inputService;
        private ICharacterEntity characterOwner;
        
        private ICharacterLiveComponent liveComponent;
        private ICharacterMovementComponent movementComponent;

        private bool controlStatus = false;
        
        
        private PlayerInput PlayerInput =>
            (PlayerInput)inputService.InputActionCollection;
        
        public Vector2 MoveAction =>
            PlayerInput.Player.Move.ReadValue<Vector2>();
        
        public bool JumpAction =>
            PlayerInput.Player.Jump.IsPressed();

        
        [Inject]
        public void Construct(ICharacterEntity characterOwner,
                              IPlayerInputService inputService,
                              TickableManager tickableManager)
        {
            this.characterOwner = characterOwner;
            this.inputService = inputService;
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

            Debug.LogError("MoveAction = " + MoveAction);
            movementComponent.Move(MoveAction);
            
            if (JumpAction)
                movementComponent.Jump();
        }
    }
}