using System;
using UnityEngine;

namespace Game.Scripts.Enemy
{
	[RequireComponent(typeof(EnemyAnimator))]
	[RequireComponent(typeof(AgentMoveToHero))]
	public class AnimateAlongAgent : MonoBehaviour
	{
		private const float MinimalVelocity = 0.1f;
		
		public AgentMoveToHero Agent;
		public EnemyAnimator Animator;


		private void Update()
		{
			if(ShouldMove())
				Animator.Move();
			else
				Animator.StopMoving();
		}

		private bool ShouldMove() =>
			Agent.Rigidbody.velocity.magnitude > MinimalVelocity;

	}
}