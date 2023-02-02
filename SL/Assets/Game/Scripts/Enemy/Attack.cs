using System.Linq;
using Game.Scripts.Logic;
using UnityEngine;

namespace Game.Scripts.Enemy
{
	[RequireComponent(typeof(EnemyAnimator))]
	public class Attack : MonoBehaviour
	{
		public EnemyAnimator Animator;

		public float AttackCooldown = 3f;
		public float Cleavage = 0.5f;
		public float EffectiveDistance = 0.5f;
		public float Damage = 10f;
		public Transform AttackPoint;

		private Transform _heroTransform;
		private float _attackCooldown;
		private bool _isAttacking;
		private int _layerMask;
		private bool _attackIsActive;
		private Collider2D[] _hits = new Collider2D[1];

		public void Construct(Transform heroTransform) =>
			_heroTransform = heroTransform;

		private void Awake()
		{
			_layerMask = 1 << LayerMask.NameToLayer("Player");
		}

		private void Update()
		{
			UpdateCooldown();

			if (CanAttack())
				StartAttack();
		}

		private void OnAttack()
		{
			if (Hit(out Collider2D hit))
			{
				PhysicsDebug.DrawDebub(StartPoint(), Cleavage, 1);
				hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
			}
		}

		private void OnAttackEnded()
		{
			_attackCooldown = AttackCooldown;
			_isAttacking = false;
		}

		public void EnableAttack() =>
			_attackIsActive = true;

		public void DisableAttack() =>
			_attackIsActive = false;

		private bool Hit(out Collider2D hit)
		{
			int hitsCount = Physics2D.OverlapCircleNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);

			hit = _hits.FirstOrDefault();

			return hitsCount > 0;
		}

		private Vector2 ChooseAttackSide()
		{
			if (_heroTransform.position.x < transform.position.x)
				return Vector2.left;
			else
				return Vector2.right;
		}

		private void UpdateCooldown()
		{
			if (!CooldownIsUp())
				_attackCooldown -= Time.deltaTime;
		}

		private void StartAttack()
		{
			Animator.PlayAttack();

			_isAttacking = true;
		}

		private Vector2 StartPoint()
		{
			return new Vector2(transform.position.x, transform.position.y) + ChooseAttackSide() * EffectiveDistance;
		}

		private bool CanAttack() =>
			_attackIsActive && !_isAttacking && CooldownIsUp();

		private bool CooldownIsUp() =>
			_attackCooldown <= 0;

	}
}