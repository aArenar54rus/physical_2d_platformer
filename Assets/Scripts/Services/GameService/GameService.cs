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
        private IPlayerInputService playerInputService;
        private ILevelsService levelsService;
        private CharacterSpawnController spawnController;
        

        private GameService(IPlayerInputService playerInputService,
                            ILevelsService levelsService,
                            CharacterSpawnController spawnController)
        {
            this.playerInputService = playerInputService;
            this.levelsService = levelsService;
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
            
            player.Activate();
            
            playerInputService.SetInputControlType(InputActionMapType.UI, false);
            playerInputService.SetInputControlType(InputActionMapType.Gameplay, true);
        }

        public void EndGame()
        {
            //throw new System.NotImplementedException();
        }
        
        
    }
}