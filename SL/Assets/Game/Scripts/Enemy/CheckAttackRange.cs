using System;
using UnityEngine;

namespace Game.Scripts.Enemy
{
	[RequireComponent(typeof(EnemyAnimator))]
	public class CheckAttackRange : MonoBehaviour
	{
		public Attack Attack;
		public TriggerObserver TriggerObserver;

		private void Start()
		{
			TriggerObserver.TriggerEnter += TriggerEnter;
			TriggerObserver.TriggerExit += TriggerExit;

			Attack.DisableAttack();
		}

		private void TriggerExit(Collider2D obj)
		{
			Attack.DisableAttack();
		}

		private void TriggerEnter(Collider2D obj)
		{
			Attack.EnableAttack();
		}
	}
}