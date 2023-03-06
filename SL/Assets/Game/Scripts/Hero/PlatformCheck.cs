using System;
using Game.Scripts.Enemy;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class PlatformCheck : MonoBehaviour
	{
		[SerializeField] private TriggerObserver PlayerTransform;
		[SerializeField] private Transform platformBody;
		[SerializeField] private Transform _rigidBody;
		[SerializeField] private Vector3 _lastPlatformPosition;
		public bool _isOnPlatform;
		private IInputService _input;

		private void Awake()
		{
			_input = AllServices.Container.Single<IInputService>();
		}

		private void Start()
		{
			PlayerTransform.TriggerEnter += TriggerEnter;
			PlayerTransform.TriggerExit += TriggerExit;
		}

		private void OnDisable()
		{
			PlayerTransform.TriggerEnter -= TriggerEnter;
			PlayerTransform.TriggerExit -= TriggerExit;
		}

		private void Update()
		{
			if (_isOnPlatform)
			{
				Vector3 deltaPosition = platformBody.position - _lastPlatformPosition;
				_rigidBody.position += deltaPosition;
				_lastPlatformPosition = platformBody.position;
			}
		}

		private void TriggerEnter(Collider2D obj)
		{
			platformBody = obj.gameObject.transform;
			_lastPlatformPosition = platformBody.position;
			Debug.Log("velocity" + platformBody.transform);
			_isOnPlatform = true;
		}

		private void TriggerExit(Collider2D obj)
		{
			_isOnPlatform = false;
			platformBody = null;
		}
	}

}