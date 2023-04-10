using System;
using Game.Scripts.Data;
using Game.Scripts.Infrastructure.Services.PersistentProgress;
using Game.Scripts.Logic;
using UnityEngine;

namespace Game.Scripts.Hero
{
	[RequireComponent(typeof(HeroAnimator))]
	public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
	{
		public HeroAnimator Animator;
		private State _state;
		[SerializeField] private Rigidbody2D _rigidbody2D;
		[SerializeField] private float knockBackForceUp;
		[SerializeField] private float knockBackForce;

		public event Action HealthChanged;
		public float Current
		{
			get => _state.CurrentHP;
			set
			{
				if (_state.CurrentHP != value)
				{
					_state.CurrentHP = value;
					HealthChanged?.Invoke();
				}
			}
		}
		public float Max
		{
			get => _state.MaxHP;
			set => _state.MaxHP = value;
		}


		public void LoadProgress(PlayerProgress progress)
		{
			_state = progress.HeroState;
			HealthChanged?.Invoke();
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			progress.HeroState.CurrentHP = Current;
			progress.HeroState.MaxHP = Max;
		}

		public void TakeDamage(float damage)
		{
			if(Current <= 0)
				return;

			Current -= damage;
			Animator.PlayHit();
		}

		public void KnockBack(Transform hero)
		{
			Vector2 knockBackDirection = new Vector2(transform.position.x - hero.position.x, 0);
			_rigidbody2D.velocity = new Vector2(knockBackDirection.x, knockBackForceUp) * knockBackForce;
		}
	}
}