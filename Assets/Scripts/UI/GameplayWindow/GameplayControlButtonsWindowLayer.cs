using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

namespace Arenar.Services.UI
{
	public class GameplayControlButtonsWindowLayer : CanvasWindowLayer
	{
		[SerializeField]
		private OnScreenButton leftButton;
		[SerializeField]
		private OnScreenButton rightButton;
		[SerializeField]
		private OnScreenButton jumpButton;
		
		
		public OnScreenButton LeftButton => leftButton;
		public OnScreenButton RightButton => rightButton;
		public OnScreenButton JumpButton => jumpButton;
	}
}