using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace Arenar.Services.UI
{
	public class HealthContainer : MonoBehaviour
	{
		[SerializeField]
		private Image healthBack;
		[SerializeField]
		private Image healthFill;

		[Space(10), Header("Health scale effect settings")]
		[SerializeField]
		private float scaleUpValue = 1.1f;
		[SerializeField]
		private float scaleUpDuration = 0.3f;
		[SerializeField]
		private float scaleDownDuration = 0.3f;

		private Sequence activateSequence;


		private void Start()
		{
			activateSequence = DOTween.Sequence();
			activateSequence.Append(healthBack.transform.DOScale(scaleUpValue, scaleUpDuration));
			activateSequence.Append(healthBack.transform.DOScale(1.0f, scaleDownDuration));
		}


		public void SetStatus(bool status)
		{
			healthFill.enabled = status;

			if (status)
			{
				activateSequence?.Kill();
				activateSequence.Play();
			}
		}
	}
}