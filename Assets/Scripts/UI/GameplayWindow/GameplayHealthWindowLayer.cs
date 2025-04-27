using UnityEngine;

namespace Arenar.Services.UI
{
	public class GameplayHealthWindowLayer : CanvasWindowLayer
	{
		[SerializeField]
		private RectTransform healthContainer;
		[SerializeField]
		private HealthContainer healthPrefab;


		public RectTransform HealthContainerParent => healthContainer;
		public HealthContainer HealthPrefab => healthPrefab;
	}
}