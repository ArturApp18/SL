using Game.Scripts.Data;
using Game.Scripts.Enemy;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Infrastructure.Services.PersistentProgress;
using Game.Scripts.Logic;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.Hero
{
	[RequireComponent(typeof(HeroAnimator))]
	public class HeroAttack : MonoBehaviour, ISavedProgressReader
	{
		public HeroAnimator HeroAnimator;
		public Transform AttackPoint;
		private IInputService _input;

		private static int _layerMask;
		private Collider2D[] _hits = new Collider2D[3];
		private Stats _stats;
		
		public Stats Stats
		{
			get
			{
				return _stats;
			}
			set
			{
				_stats = value;
			}
		}


		public void Construct(IInputService input)
		{
			_input = input;
		}
		
		private void Awake()
		{
			_layerMask = 1 << LayerMask.NameToLayer("Hittable");
		}

		private void Update()
		{
			if (_input.IsAttackButton() && !HeroAnimator.IsAttacking)
				HeroAnimator.PlayAttack();
		}

		public void OnAttack()
		{
			for (int i = 0; i < Hit(); i++)
			{
				_hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(Stats.Damage);
				_hits[i].transform.parent.GetComponent<KnockBackEffect>().KnockBack(transform);
			}
		}

		public void LoadProgress(PlayerProgress progress) =>
			Stats = progress.HeroStats;

		private int Hit() =>
			Physics2D.OverlapCircleNonAlloc(StartPoint(), Stats.DamageRadius, _hits, _layerMask);

		private void ChooseSide()
		{
			
		}

		private Vector2 StartPoint() =>
			new Vector2(AttackPoint.position.x, AttackPoint.position.y);

	}
}