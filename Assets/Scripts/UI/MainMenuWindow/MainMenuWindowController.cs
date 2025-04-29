using Arenar.Services.PlayerInputService;

namespace Arenar.Services.UI
{
    public class MainMenuWindowController : CanvasWindowController
    {
        private MainMenuWindow mainMenuWindow;
        private IGameService gameService;


        public MainMenuWindowController(IPlayerInputService playerInputService, IGameService gameService)
            : base(playerInputService)
        {
            this.gameService = gameService;
        }


        public override void Initialize(ICanvasService canvasService)
        {
            base.Initialize(canvasService);
            mainMenuWindow = canvasService.GetWindow<MainMenuWindow>();
            
            mainMenuWindow.PlayGameButton.onClick.AddListener(StartGameButtonHandler);
            
            mainMenuWindow.OnShowEnd.AddListener(OnWindowShowEnd_SelectElements);
            mainMenuWindow.OnHideBegin.AddListener(OnWindowHideBegin_DeselectElements);
        }

        protected override void OnWindowShowEnd_SelectElements()
        {
            mainMenuWindow.PlayGameButton.interactable = true;
        }

        protected override void OnWindowHideBegin_DeselectElements()
        {
            mainMenuWindow.PlayGameButton.interactable = false;
        }
        
        private void StartGameButtonHandler()
        {
            GameData newGameData = new GameData();
            newGameData.levelIndex = 1;
            
            canvasService.TransitionController.PlayTransition<TransitionOverlayCanvasWindowController, MainMenuWindow, GameplayWindow>(false, false,
                () =>
                {
                    gameService.StartGame(newGameData);
                });
        }
    }
}