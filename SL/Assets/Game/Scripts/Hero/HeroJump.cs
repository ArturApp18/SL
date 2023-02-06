using System;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.Hero
{
	[RequireComponent(typeof(HeroAnimator), typeof(Rigidbody2D))]
	public class HeroJump : MonoBehaviour
	{
		public Rigidbody2D Rigidbody;
		public HeroAnimator Animator;
		
		[SerializeField] private CharacterController2D _characterController2D;

		[SerializeField] private float _jumpForce;
		[SerializeField] private float _jumpTimeCounter = 0.2f;
		[SerializeField] private float _groundeTimeCounter;
		[SerializeField] private float _cutJumpHeight;
		
		private float _jumpPressedRemember = 0;
		private float _groundedRemember;

		private bool _isJumping = false;
		
		private IInputService _input;

		private void Awake()
		{
			_input = AllServices.Container.Single<IInputService>();
		}

		private void Update()
		{
			_groundedRemember -= Time.deltaTime;
			if (_characterController2D.m_Grounded)
			{
				_groundedRemember = _groundeTimeCounter;
			}
			_jumpPressedRemember -= Time.deltaTime;
			if (_input.IsJumpButtonDown())
			{
				_jumpPressedRemember = _jumpTimeCounter;
				Animator.PlayJump();
			}

			if (_characterController2D.m_Grounded)
			{
				Animator.StartLanding();
			}
			else
			{
				Animator.StopLanding();
			}
		}

		private void FixedUpdate()
		{
			if (_input.IsJumpButtonUp())
			{
				if (Rigidbody.velocity.y > 0)
				{
					Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Rigidbody.velocity.y * _cutJumpHeight);
				}
			}
			if (_jumpPressedRemember > 0 && _groundedRemember > 0)
			{
				_jumpPressedRemember = 0;
				_groundedRemember = 0;
				Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _jumpForce);
			}
		}

		public void OnLanding()
		{
			Animator.StopPlayJump();
		}
	}

}