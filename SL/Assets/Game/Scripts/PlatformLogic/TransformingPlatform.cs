using System;
using Game.Scripts.Enemy;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Logic;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class TransformingPlatform : MonoBehaviour
	{
		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private bool _inActiveZone;
		[SerializeField] private GameObject _platform;
		[SerializeField] private Animator _animator;
		private IInputService _input;
		private static readonly int Up = Animator.StringToHash("Up");
		private static readonly int Down = Animator.StringToHash("Down");
		public bool _closedDoor;
		public bool _isAnimating;

		private void Awake()
		{
			_input = AllServices.Container.Single<IInputService>();
		}

		private void Start()
		{
			_triggerObserver.TriggerEnter += TriggerEnter;
			_triggerObserver.TriggerExit += TriggerExit;
		}

		private void OnDestroy()
		{
			_triggerObserver.TriggerEnter -= TriggerEnter;
			_triggerObserver.TriggerExit -= TriggerExit;
		}

		private void Update()
		{
			if (_inActiveZone)
			{
				if (_input.IsActionButtonDown() && _closedDoor && !_isAnimating)
				{
					_isAnimating = true;
					Debug.Log("qwerty");
					_closedDoor = false;
					_animator.SetTrigger(Up);
				}
				else if(_input.IsActionButtonDown() && !_closedDoor && !_isAnimating)
				{
					_isAnimating = true;
					_animator.SetTrigger(Down);
					_closedDoor = true;
				}
			}
		}

		public void OnAnimationEnded()
		{
			_isAnimating = false;
		}
		private void TriggerEnter(Collider2D obj)
		{
			_inActiveZone = true;
		}

		private void TriggerExit(Collider2D obj)
		{
			_inActiveZone = false;
		}
	}

}