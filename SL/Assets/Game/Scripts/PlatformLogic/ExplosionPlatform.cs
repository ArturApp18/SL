using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class ExplosionPlatform : MonoBehaviour
	{
		private static readonly int Reelingg = Animator.StringToHash("Reeling");
		
		[SerializeField] private float _explosionTimer;
		[SerializeField] private float _explosionAfterJump;
		[SerializeField] private GameObject _explosionVFX;
		[SerializeField] private Animator _animator;
		
		public TriggerObserver ExplosioObserver;
		public TriggerObserver JumpExplosionrObserver;

		private bool _timerAlreadyStarted;

		private void Start()
		{
			ExplosioObserver.TriggerEnter += TriggerEnter;
			JumpExplosionrObserver.TriggerExit += TriggerExit;
		}

		private void OnDisable()
		{
			ExplosioObserver.TriggerEnter -= TriggerEnter;
			JumpExplosionrObserver.TriggerExit -= TriggerExit;
		}

		private IEnumerator Explode(float explosionTimer)
		{
			
			Destroy(gameObject, explosionTimer);
			yield return new WaitForSeconds(explosionTimer - 0.3f);
			Instantiate(_explosionVFX, transform.position, Quaternion.identity);
		}

		private void TriggerEnter(Collider2D obj)
		{
			if (_timerAlreadyStarted)
				return;

			_animator.SetBool(Reelingg, true);
			StartCoroutine(Explode(_explosionTimer));
		}

		private void TriggerExit(Collider2D obj)
		{
			StartCoroutine(Explode(_explosionAfterJump));
		}
	}
}
