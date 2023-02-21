using System;
using Game.Scripts.Enemy;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class GravityPlatform : MonoBehaviour
	{
		[SerializeField] private GameObject _hero;
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private Vector2 _pullForce;
		[SerializeField] private TriggerObserver _triggerObserver;

		[SerializeField] private float _influenceRange;
		[SerializeField] private float _intensity;
		[SerializeField] private float _distanceToPlayer;
		private bool _isHeroNotNull;

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

		private void Update()
		{
			if (_hero != null)
			{
				_distanceToPlayer = Vector2.Distance(_hero.transform.position, transform.position);
				if (_distanceToPlayer <= _influenceRange)
				{
					_pullForce = ( transform.position - _hero.transform.position ).normalized / _distanceToPlayer * _intensity;
					_rigidbody.AddForce(_pullForce, ForceMode2D.Force);
				}
			}
		}

		private void TriggerEnter(Collider2D obj)
		{
			_hero = obj.gameObject;
			_rigidbody = _hero.GetComponent<Rigidbody2D>();
			
		}

		private void TriggerExit(Collider2D obj)
		{
			_hero = null;
		}

	}
}