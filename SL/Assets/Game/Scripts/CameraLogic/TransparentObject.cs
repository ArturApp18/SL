using Game.Scripts.Enemy;
using UnityEngine;

namespace Game.Scripts.CameraLogic
{
	public class TransparentObject : MonoBehaviour
	{
		[SerializeField] private float _timeModifier = 2f;
		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private SpriteRenderer renderer;
		
		private bool _isFading;

		private void Start()
		{
			_triggerObserver.TriggerEnter += TriggerEnter;
			_triggerObserver.TriggerExit += TriggerExit;
			_isFading = false;
		}

		private void TriggerEnter(Collider2D obj)
		{
			if (obj.CompareTag("Player"))
			{
				_isFading = true;
			}
		}

		private void TriggerExit(Collider2D obj)
		{
			if (obj.CompareTag("Player"))
			{
				_isFading = false;
			}
		}

		private void Update()
		{
			if (_isFading)
			{
				Color currentColor = renderer.material.color;
				float newAlpha = Mathf.Lerp(currentColor.a, 0.0f, _timeModifier * Time.deltaTime);
				renderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
			}
			else
			{
				Color currentColor = renderer.material.color;
				float newAlpha = Mathf.Lerp(currentColor.a, 1.0f,  _timeModifier * Time.deltaTime);
				renderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
			}
		}
	}
}