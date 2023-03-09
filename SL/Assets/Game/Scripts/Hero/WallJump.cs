using System;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class WallJump : MonoBehaviour
	{
		[SerializeField] private Vector2 _wallJumpDirection;
		[SerializeField] private Rigidbody2D _rigidbody2D;
		[SerializeField] private WallSlide _wallSlide;
		[SerializeField] private HeroMove _heroMove;
		[SerializeField] private bool _canWallJump = true;
		[SerializeField] private HeroFlip _heroFlip;
		[SerializeField] private HeroAnimator _animator;

		private IInputService _inputService;
		public bool CanWallJump
		{
			get
			{
				return _canWallJump;
			}
			set
			{
				_canWallJump = value;
			}
		}

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
		}

		private void Update()
		{
			if (_inputService.IsJumpButtonDown() && _wallSlide.IsWallSliding && CanWallJump)
			{
				WallJumping();
			}

			if (!_wallSlide.IsWallSliding)
			{
				_canWallJump = false;
			}
			else
			{
				_canWallJump = true;
			}

			if (_inputService.IsJumpButtonDown())
			{
				_wallSlide.CanWallSlide = false;
			}
		}

		private void WallJumping()
		{
			_heroMove.CanMove = false;
			_heroFlip.CanFlip = false;


			Vector2 direction = new Vector2(_wallJumpDirection.x * -_heroFlip.FacingDirection, _wallJumpDirection.y);
			
			_rigidbody2D.AddForce(direction, ForceMode2D.Impulse);
			_animator.PlayJump();
			_heroFlip.Flip();
		}
	}
}