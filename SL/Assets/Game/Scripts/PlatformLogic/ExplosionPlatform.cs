using System;
using System.Collections;
using Game.Scripts.Enemy;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class ExplosionPlatform : MonoBehaviour
	{
		[SerializeField] private float _explosionTimer;
	
		public TriggerObserver TriggerObserver;

		private Coroutine _aggroCoroutine;
		private bool _timerAlreadyStarted;

		private void Start()
		{
			TriggerObserver.TriggerEnter += TriggerEnter;
		}

		private void TriggerEnter(Collider2D obj)
		{
			if (_timerAlreadyStarted)
				return;

			Destroy(gameObject, _explosionTimer);
		}
	}
}
