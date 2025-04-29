using System;
using Zenject;

namespace Arenar.Services.LevelsService
{
    public interface ILevelsService
    {
        void LoadLevel(int levelIndex, Action<SceneContext> onComplete = null);

        void UnloadLastLevel();
    }
}