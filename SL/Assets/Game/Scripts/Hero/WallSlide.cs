using System;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class WallSlide : MonoBehaviour
	{
		[SerializeField] private CharacterController2D _controller;
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private WallDetection _wallDetection;
		[SerializeField] private bool _canWallSlide;
		[SerializeField] private bool _isWallSliding;
		[SerializeField] private HeroAnimator _animator;
		[SerializeField] private HeroMove _heroMove;
		[SerializeField] private HeroFlip _heroFlip;
		private IInputService _input;


		public bool WallSliding;
		public float WallSlidingSpeed;
		public bool IsWallSliding
		{
			get
			{
				return _isWallSliding;
			}
			set
			{
				_isWallSliding = value;
			}
		}
		public bool CanWallSlide
		{
			get
			{
				return _canWallSlide;
			}
			set
			{
				_canWallSlide = value;
			}
		}

		public void Construct(IInputService input)
		{
			_input = input;
		}

		private void Update()
		{
			if (!_controller.m_Grounded && _rigidbody.velocity.y < 0)
			{
				CanWallSlide = true;
			}

			
		}

		private void FixedUpdate()
		{
			if (_input.Axis.y < 0)
			{
				_canWallSlide = false;
			}
			if (_wallDetection.IsWallDetected && CanWallSlide)
			{
				IsWallSliding = true;
				_heroFlip.CanFlip = false;
				_animator.StartWallSlide(IsWallSliding);
				_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.1f);
				Debug.Log("hello");
			}
			else if (!_wallDetection.IsWallDetected)
			{
				IsWallSliding = false;
				_heroMove.CanMove = true;
				_heroFlip.CanFlip = true;
				_animator.StartWallSlide(IsWallSliding);
				Debug.Log("hell000000000o");
			}
		}

	}

}