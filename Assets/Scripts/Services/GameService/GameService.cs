using Arenar.CameraService;
using Arenar.Character;
using Arenar.Location;
using Arenar.Services.Creator;
using Arenar.Services.LevelsService;
using Arenar.Services.PlayerInputService;
using Arenar.Services.UI;
using UnityEngine;
using Zenject;


namespace Arenar.Services
{
    public class GameService : IGameService
    {
        private ICameraService cameraService;
        private IPlayerInputService playerInputService;
        private ILevelsService levelsService;
        private CharacterSpawnController spawnController;
        private ICanvasService canvasService;
        

        private GameService(ICameraService cameraService,
                            IPlayerInputService playerInputService,
                            ILevelsService levelsService,
                            ICanvasService canvasService,
                            CharacterSpawnController spawnController)
        {
            this.cameraService = cameraService;
            this.playerInputService = playerInputService;
            this.levelsService = levelsService;
            this.canvasService = canvasService;
            this.spawnController = spawnController;
        }
        
        
        public void StartGame(GameData gameData)
        {
            levelsService.LoadLevel(gameData.levelIndex, CompleteLoadScene);
        }

        private void CompleteLoadScene(SceneContext loadedSceneContext)
        {
            Transform playerPointTransform = loadedSceneContext.Container.Resolve<PlayerSpawnPoint>().Point;
            EnemiesSpawnPoints enemiesSpawnPoints = loadedSceneContext.Container.Resolve<EnemiesSpawnPoints>();

            var player = spawnController.GetCharacter(CharacterType.Player);
            player.CharacterTransform.position = playerPointTransform.position;
            player.CharacterType = CharacterType.Player;
            player.Activate();

            if (player.TryGetCharacterComponent<ICharacterLiveComponent>(out ICharacterLiveComponent characterLiveComponent))
                characterLiveComponent.OnCharacterDeath += PlayerDeathHandler;

            foreach (var spawnPoint in enemiesSpawnPoints.SpawnPoints)
            {
                var enemy = spawnController.GetCharacter(spawnPoint.EnemyType);
                enemy.CharacterTransform.position = spawnPoint.Point.position;
                enemy.CharacterType = spawnPoint.EnemyType;
                enemy.Activate();
                
                if (enemy.TryGetCharacterComponent<ICharacterLiveComponent>(out ICharacterLiveComponent enemyCharacterLiveComponent))
                    enemyCharacterLiveComponent.OnCharacterDeath += EnemyDeathHandler;
            }
            
            playerInputService.SetInputControlType(InputActionMapType.UI, false);
            playerInputService.SetInputControlType(InputActionMapType.Gameplay, true);
            
            cameraService.SetCameraState<CameraStateGameplay>(player.CharacterTransform, player.CharacterTransform);
        }

        public void EndGame()
        {
            canvasService.TransitionController.PlayTransition<TransitionOverlayCanvasWindowController, GameplayWindow, MainMenuWindow>(false, false,
                () =>
                {
                    playerInputService.SetInputControlType(InputActionMapType.UI, true);
                    playerInputService.SetInputControlType(InputActionMapType.Gameplay, false);
                    
                    spawnController.ReturnAllCharacters();
                    levelsService.UnloadLastLevel();
                    cameraService.SetCameraState<CameraStateMainMenu>(null, null);
                });
        }
        
        private void PlayerDeathHandler(ICharacterEntity player)
        {
            EndGame();
            
            if (player.TryGetCharacterComponent<ICharacterLiveComponent>(out ICharacterLiveComponent enemyCharacterLiveComponent))
                enemyCharacterLiveComponent.OnCharacterDeath -= PlayerDeathHandler;
        }
        
        private void EnemyDeathHandler(ICharacterEntity enemy)
        {
            spawnController.ReturnCharacter(enemy);
            if (enemy.TryGetCharacterComponent<ICharacterLiveComponent>(out ICharacterLiveComponent enemyCharacterLiveComponent))
                enemyCharacterLiveComponent.OnCharacterDeath -= EnemyDeathHandler;
        }
    }
}