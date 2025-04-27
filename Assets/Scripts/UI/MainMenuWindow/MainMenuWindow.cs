using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Arenar.Services.UI
{
    public class MainMenuWindow : CanvasWindow
    {
        [Space(10)]
        [SerializeField]
        private Button playGameButton;


        public Button PlayGameButton => playGameButton;
    }
}