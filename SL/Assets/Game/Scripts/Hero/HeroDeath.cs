using System;
using Game.Scripts.Data;
using Game.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Game.Scripts.Hero
{
	[RequireComponent(typeof(HeroHealth))]
	public class HeroDeath : MonoBehaviour
	{
		public HeroHealth Health;

		public HeroMove Move;
		public HeroAttack Attack;
		public HeroAim Aim;
		public HeroAnimator Animator;

		public GameObject DeathFx;
		private bool _isDead;

		private void Start()
		{
			Health.HealthChanged += HealthChanged;
		}

		private void OnDestroy()
		{
			Health.HealthChanged -= HealthChanged;
		}

		private void HealthChanged()
		{
			if (!_isDead && Health.Current <= 0)
				Die();
		}

		private void Die()
		{
			_isDead = true;
			
			Move.enabled = false;
			Attack.enabled = false;
			Aim.enabled = false;
			Animator.PlayDeath();
			
			Instantiate(DeathFx, transform.position, Quaternion.identity);
		}
	}
}