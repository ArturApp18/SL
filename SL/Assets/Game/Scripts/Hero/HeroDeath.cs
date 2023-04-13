using Game.Scripts.Infrastructure.States;
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
		public HeroDash Dash;
		public HeroAnimator Animator;

		public GameObject DeathFx;
		private bool _isDead;
		private IGameStateMachine _stateMachine;
		public string TransferTo { get; set; }


		public void Construct(IGameStateMachine stateMachine)
		{
			_stateMachine = stateMachine;
		}
		
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
			Dash.enabled = false;
			Animator.PlayDeath();
			Instantiate(DeathFx, transform.position, Quaternion.identity);
		}

	}
}