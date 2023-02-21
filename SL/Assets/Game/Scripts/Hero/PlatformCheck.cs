using System;
using Game.Scripts.Enemy;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class PlatformCheck : MonoBehaviour
	{
		[SerializeField] private TriggerObserver PlayerTransform;
		[SerializeField] private Transform platformBody;
		[SerializeField] private Transform _rigidBody;
		[SerializeField] private Vector3 _lastPlatformPosition;
		private bool _isOnPlatform;

		private void Start()
		{
			PlayerTransform.TriggerEnter += TriggerEnter;
			PlayerTransform.TriggerExit += TriggerExit;
		}

		private void FixedUpdate()
		{
			if (_isOnPlatform)
			{
				Vector3 deltaPosition = platformBody.position - _lastPlatformPosition;
				_rigidBody.position = _rigidBody.position + deltaPosition;
				_lastPlatformPosition = platformBody.position;
			}
		}

		private void OnDisable()
		{
			PlayerTransform.TriggerEnter -= TriggerEnter;
			PlayerTransform.TriggerExit -= TriggerExit;
		}

		private void TriggerEnter(Collider2D obj)
		{
			platformBody = obj.gameObject.GetComponent<Transform>();
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