using Game.Scripts.Enemy;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class PopUpActionButton : MonoBehaviour
	{
		private static readonly int Fading = Animator.StringToHash("Fading");

		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private Transform _popUpPosition;
		[SerializeField] private Animator _animator;
		[SerializeField] private GameObject _popUpButton;
		[SerializeField] private float _speed;

		private Color _imageColor;
		public bool _triggered = false;
		public float _modifier = -0.15f;

		private void Start()
		{
			_triggerObserver.TriggerEnter += TriggerEnter;
			_triggerObserver.TriggerExit += TriggerExit;
		}
	
		private void OnDisable()
		{
			_triggerObserver.TriggerEnter -= TriggerEnter;
			_triggerObserver.TriggerExit -= TriggerExit;
		}

		private void TriggerEnter(Collider2D obj)
		{
			_popUpButton.transform.localPosition = _popUpPosition.localPosition;
			_triggered = true;
			_animator.SetBool(Fading, _triggered);
		}

		private void TriggerExit(Collider2D obj)
		{
			_triggered = false;
			_animator.SetBool(Fading, _triggered);
		}
	}
}