using Arenar.Services.PlayerInputService;

namespace Arenar.Services.UI
{
    public class MainMenuWindowController : CanvasWindowController
    {
        private MainMenuWindow mainMenuWindow;
        
        
        public MainMenuWindowController(IPlayerInputService playerInputService)
            : base(playerInputService) {}


        public override void Initialize(ICanvasService canvasService)
        {
            base.Initialize(canvasService);
            mainMenuWindow = canvasService.GetWindow<MainMenuWindow>();
            
            mainMenuWindow.PlayGameButton.onClick.AddListener(StartGameButtonHandler);
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
            
        }
    }
}