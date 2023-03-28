using System;
using Game.Scripts.Logic;
using UnityEngine;

namespace Game.Scripts.Enemy
{
	[RequireComponent(typeof(EnemyAnimator))]
	public class EnemyHealth : MonoBehaviour, IHealth
	{
		public EnemyAnimator Animator;

		[SerializeField] private float _current;
		[SerializeField] private float _max;
		[SerializeField] private Rigidbody2D _rigidbody2D;
		[SerializeField]
		private float knockBackForceUp;
		[SerializeField]
		private float knockBackForce;

		public event Action HealthChanged;
		public float Current
		{
			get => _current;
			set => _current = value;
		}
		public float Max
		{
			get => _max;
			set => _max = value;
		}

		public void TakeDamage(float damage)
		{
			Current -= damage;
			
			Animator.PlayHit();
			
			HealthChanged?.Invoke();
		}

		public void KnockBack(Transform hero)
		{
			Vector2 knockBackDirection = new Vector2(transform.position.x - hero.position.x, 0);
			_rigidbody2D.velocity = new Vector2(knockBackDirection.x, knockBackForceUp) * knockBackForce;
		}
	}
}