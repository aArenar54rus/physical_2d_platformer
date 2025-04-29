using Arenar.Character;
using Arenar.Services.Creator;
using Arenar.Services.PlayerInputService;
using System.Collections.Generic;
using UnityEngine;


namespace Arenar.Services.UI
{
    public class GameplayWindowController : CanvasWindowController
    {
        private GameplayWindow gameplayWindow;
        private GameplayHealthWindowLayer healthVisualLayer;
        private CharacterSpawnController spawnController;
        
        private HealthContainer[] activeHealthContainer;
        private Queue<HealthContainer> healthContainers = new Queue<HealthContainer>();


        public GameplayWindowController(IPlayerInputService playerInputService, CharacterSpawnController spawnController)
            : base(playerInputService)
        {
            this.spawnController = spawnController;
        }

        
        public override void Initialize(ICanvasService canvasService)
        {
            base.Initialize(canvasService);
            gameplayWindow = canvasService.GetWindow<GameplayWindow>();
            healthVisualLayer = gameplayWindow.GetWindowLayer<GameplayHealthWindowLayer>();
            
            gameplayWindow.OnShowEnd.AddListener(OnWindowShowEnd_SelectElements);
            gameplayWindow.OnHideBegin.AddListener(OnWindowHideBegin_DeselectElements);
        }


        protected override void OnWindowShowEnd_SelectElements()
        {
            var player = spawnController.PlayerCharacter;
            if (player == null)
            {
                spawnController.OnCreatePlayerCharacter += CreatePlayerHandler;
            }
            else
            {
                CreatePlayerHandler(player);
            }


            void CreatePlayerHandler(ICharacterEntity playerEntity)
            {
                spawnController.OnCreatePlayerCharacter -= CreatePlayerHandler;
                
                if (playerEntity.TryGetCharacterComponent<ICharacterLiveComponent>(out ICharacterLiveComponent liveComponent))
                {
                    activeHealthContainer = new HealthContainer[liveComponent.Health];

                    for (int i = 0; i < liveComponent.HealthMax; i++)
                    {
                        GetHealthContainer(i).SetStatus(liveComponent.Health > i);
                    }

                    liveComponent.OnCharacterChangeHealthValue += PlayerChangeHealthValueHandler;
                }
            }
        }


        protected override void OnWindowHideBegin_DeselectElements()
        {
            foreach (HealthContainer healthContainer in activeHealthContainer)
                healthContainers.Enqueue(healthContainer);
            activeHealthContainer = null;
        }
        
        private void PlayerChangeHealthValueHandler(int health, int healthMax)
        {
            for (int i = 0; i < activeHealthContainer.Length; i++)
                activeHealthContainer[i].SetStatus(health > i);
        }

        private HealthContainer GetHealthContainer(int index)
        {
            HealthContainer container = null;
            if (healthContainers.Count > 0)
            {
                container = healthContainers.Dequeue();
            }
            else
            {
                container = GameObject.Instantiate(healthVisualLayer.HealthPrefab, healthVisualLayer.HealthContainerParent)
                    .GetComponent<HealthContainer>();
            }
            activeHealthContainer[index] = container;
            
            return container;
        }
    }
}