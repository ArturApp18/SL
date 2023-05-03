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
		[SerializeField] private Transform _rigidBody;

		private void Awake()
		{
			AllServices.Container.Single<IInputService>();
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

		private void FixedUpdate()
		{ 
			/*if (_isOnPlatform)
			{
				Vector3 deltaPosition = platformBody.position - _lastPlatformPosition;
				_rigidBody.position += deltaPosition;
				_lastPlatformPosition = platformBody.position;
			}*/
		}

		private void TriggerEnter(Collider2D obj)
		{
			_rigidBody.parent = obj.gameObject.transform;
		}

		private void TriggerExit(Collider2D obj)
		{
			_rigidBody.parent = null;
		}
	}

}