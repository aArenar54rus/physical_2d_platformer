using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;


namespace Arenar.Services.LevelsService
{
    public class SimpleLevelsService : ILevelsService
    {
        private ZenjectSceneLoader zenjectSceneLoader;
        
        private const string LOCATION_NAME = "Level";
        
        
        private int lastLevelIndex = -1;       
        
        
        public SimpleLevelsService(ZenjectSceneLoader zenjectSceneLoader)
        {
            this.zenjectSceneLoader = zenjectSceneLoader;
        }
        
        
        public void LoadLevel(int levelIndex, Action<SceneContext> onComplete = null)
        {
            if (lastLevelIndex > 0)
            {
                SceneManager.UnloadScene(LOCATION_NAME + lastLevelIndex);
            }

            lastLevelIndex = levelIndex;
            string locationName = LOCATION_NAME + levelIndex;
            zenjectSceneLoader.LoadSceneAsync(locationName, LoadSceneMode.Additive)
                .completed += asyncOperation =>
            {
                var loadedScene = SceneManager.GetSceneByName(locationName);
                if (!loadedScene.IsValid() || !loadedScene.isLoaded)
                {
                    Debug.LogError($"Scene {locationName} did not load correctly!");
                    onComplete?.Invoke(null);
                    return;
                }

                SceneContext sceneContext = null;
                foreach (var rootObject in loadedScene.GetRootGameObjects())
                {
                    sceneContext = rootObject.GetComponentInChildren<SceneContext>();
                    if (sceneContext != null)
                        break;
                }
                
                onComplete?.Invoke(sceneContext);
            };
        }
    }
}