using System;
using Game.Scripts.Enemy;
using UnityEngine;

namespace Game.Scripts.Weapon
{
	public class Bullet : MonoBehaviour
	{
		[SerializeField] private Rigidbody2D _rigidbody2D;
		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private ParticleSystem _impactVFX;
		public delegate void OnDisableCallBack(Bullet instance);
		public OnDisableCallBack Disable;
		private void Start()
		{
			_triggerObserver.TriggerEnter += TriggerEnter;
		}

		private void OnDisable()
		{
			_triggerObserver.TriggerEnter -= TriggerEnter;
		}

		public void Shoot(Vector2 position, Vector2 direction, float speed)
		{
			_rigidbody2D.velocity = Vector2.zero;
			transform.position = position;
			transform.forward = direction;

			_rigidbody2D.AddForce(direction * speed, ForceMode2D.Impulse);
		}

		private void TriggerEnter(Collider2D obj)
		{
			_impactVFX.transform.forward = -1 * transform.forward;
			_impactVFX.Play();
			_rigidbody2D.velocity = Vector2.zero;
		}
		

		private void OnParticleSystemStopped()
		{
			Disable?.Invoke(this);
		}

	}

}