using System;
using System.Collections;
using Game.Scripts.Infrastructure;
using Game.Scripts.Logic;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class HeroAnimator : MonoBehaviour, IAnimationStateReader
	{
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private Animator _animator;

		private static readonly int MoveHash = Animator.StringToHash("Walking");
		private static readonly int JumpHash = Animator.StringToHash("Jumping");
		private static readonly int AttackHash1 = Animator.StringToHash("Attack1");
		private static readonly int AttackHash2 = Animator.StringToHash("Attack2");
		private static readonly int AttackHash3 = Animator.StringToHash("Attack3");
		private static readonly int HitHash = Animator.StringToHash("Hit");
		private static readonly int DieHash = Animator.StringToHash("Die");
		private static readonly int IsJump = Animator.StringToHash("IsJumping");
		private static readonly int IsRun = Animator.StringToHash("IsRun");
		private static readonly int IsWalk = Animator.StringToHash("IsWalk");
		private static readonly int Landing = Animator.StringToHash("Landing");
		private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
		private static readonly int IsJumpAttack = Animator.StringToHash("IsJumpAttack");
		private static readonly int CanClimb = Animator.StringToHash("CanClimb");
		private static readonly int CanSlide = Animator.StringToHash("CanSlide");

		private static readonly int IsReload = Animator.StringToHash("IsReload");
		private readonly int _idleStateHash = Animator.StringToHash("Idle");
		private readonly int _idleStateFullHash = Animator.StringToHash("Base Layer.Idle");
		private readonly int _attackStateHash = Animator.StringToHash("Attack");
		private readonly int _walkingStateHash = Animator.StringToHash("Run");
		private readonly int _deathStateHash = Animator.StringToHash("Death");
		private readonly int _jumpStateHash = Animator.StringToHash("Jump");
		private readonly int _ledgeClimbStateHash = Animator.StringToHash("Ledge");
		private static readonly int IsCrouch = Animator.StringToHash("IsCrouch");


		public event Action<AnimatorState> StateEntered;
		public event Action<AnimatorState> StateExited;

		public AnimatorState State { get; private set; }

		public bool IsAttacking
		{
			get
			{
				return State == AnimatorState.Attack;
			}
		}

		public bool IsJumping
		{
			get
			{
				return State == AnimatorState.Jumping;
			}
		}

		private void Update()
		{
			_animator.SetFloat(MoveHash, _rigidbody.velocity.magnitude, 0.1f, Time.deltaTime);
		}


		public void StartRun()
		{
			_animator.SetBool(IsRun, true);
		}

		public void StopRun()
		{
			_animator.SetBool(IsRun, false);
		}

		public void StartWallSlide(bool isWallSliding)
		{
			_animator.SetBool(CanSlide, isWallSliding);
		}

		public void StartLedge(bool canClimb)
		{
			_animator.SetBool(CanClimb, canClimb);
		}

		public void StopLedge()
		{
			_animator.SetBool(CanClimb, false);
		}

		public void PlayHit()
		{
			_animator.SetTrigger(HitHash);
		}

		public void PlayReload()
		{
			_animator.SetBool(IsReload, true);
		}

		public void StopReload()
		{
			_animator.SetBool(IsReload, false);
		}

		public void StartCrouch(bool isCrouching)
		{
			_animator.SetBool(IsCrouch, isCrouching);
		}
		public void StopCrouch()
		{
			_animator.SetBool(IsCrouch, false);
		}

		public void PlayJump()
		{
			_animator.SetBool(IsJump, true);
		}

		public void StopJump()
		{
			_animator.SetBool(IsJump, false);
		}

		public void StartLanding()
		{
			_animator.SetBool(Landing, true);
		}

		public void StopLanding()
		{
			_animator.SetBool(Landing, false);
		}

		public void PlayAttack()
		{
			_animator.SetTrigger(AttackHash1);
		}

		public void PlayAttack2()
		{
			_animator.SetTrigger(AttackHash2);
		}

		public void PlayAttack3()
		{
			_animator.SetTrigger(AttackHash3);
		}

		public void PlayDeath()
		{
			_animator.SetTrigger(DieHash);
		}


		public void EnteredState(int stateHash)
		{
			State = StateFor(stateHash);
			StateEntered?.Invoke(State);
		}

		public void ExitedState(int stateHash)
		{
			StateExited?.Invoke(StateFor(stateHash));
		}

		private AnimatorState StateFor(int stateHash)
		{
			AnimatorState state;
			if (stateHash == _idleStateHash)
			{
				state = AnimatorState.Idle;
			}
			else if (stateHash == _attackStateHash)
			{
				state = AnimatorState.Attack;
			}
			else if (stateHash == _walkingStateHash)
			{
				state = AnimatorState.Walking;
			}
			else if (stateHash == _deathStateHash)
			{
				state = AnimatorState.Died;
			}
			else if (stateHash == _jumpStateHash)
			{
				state = AnimatorState.Jumping;
			}
			else if (stateHash == _ledgeClimbStateHash)
			{
				state = AnimatorState.LedgeCLimb;
			}
			else
			{
				state = AnimatorState.Unknown;
			}

			return state;
		}

	}
}