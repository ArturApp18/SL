using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Enemy
{
	[RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
	public class EnemyDeath : MonoBehaviour
	{
		public EnemyHealth Health;
		public Follow Follow;
		public Aggro Aggro;
		public EnemyAnimator Animator;
		public Collider2D HurtBox;
		public bool IsDeath = false;
		
		public GameObject DeathFx;

		public event Action Happened;

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
			if (Health.Current <= 0)
				Die();
		}

		private void Die()
		{
			IsDeath = true;
			Health.HealthChanged -= HealthChanged;
			
			Aggro.enabled = false;
			Follow.enabled = false;
			
			Animator.PlayDeath();

			SpawnDeathFx();
			StartCoroutine(DestroyTimer());

			Happened?.Invoke();
		}

		private void SpawnDeathFx() =>
			Instantiate(DeathFx, transform.position, Quaternion.identity);

		private IEnumerator DestroyTimer()
		{
			yield return new WaitForSeconds(2);
			Destroy(gameObject);
		}
	}
}