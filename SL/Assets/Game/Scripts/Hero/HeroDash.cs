using System.Collections;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class HeroDash : MonoBehaviour
	{
		[SerializeField] private CharacterController2D _controller2D;
		[SerializeField] private SlideTouch _slideTouch;
		[SerializeField] private TrailRenderer _trail;
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private HeroAnimator _animator;
		[SerializeField] private float _dashingVelocity = 14f;
		[SerializeField] private float _dashingTime = 0.5f;
		[SerializeField] private float _dashDelay = 2;
		[SerializeField] private HeroFlip _heroFlip;
		[SerializeField] private HeroMove _heroMove;

		private Vector2 _dashingDirection;
		private bool _isDashing;

		public bool CanDash = true;

		private IInputService _inputService;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
		}

		private void Update()
		{
			if (Input.GetButtonDown("Dash") && CanDash || _slideTouch.Slide && CanDash)
			{
				_isDashing = true;
				CanDash = false;
				_trail.emitting = true;
				_dashingDirection = new Vector2(_slideTouch.SlideDirection, 0);
				if (_dashingDirection == Vector2.zero)
				{
					if (_heroFlip.IsFacingRight)
					{
						_dashingDirection = Vector2.right;
					}
					else
					{
						_dashingDirection = Vector2.left;
					}
					
				}

				StartCoroutine(StopDashing());
			}

			_animator.IsDashing(_isDashing);
			
			if (_isDashing)
			{
				_heroMove.CanMove = false;
				_rigidbody.velocity = _dashingDirection.normalized * _dashingVelocity;
			}
		}

		private IEnumerator StopDashing()
		{
			yield return new WaitForSeconds(_dashingTime);
			_rigidbody.velocity = Vector2.zero;
			_isDashing = false;
			_heroMove.CanMove = true;
			_trail.emitting = false;
			yield return new WaitForSeconds(_dashDelay);
			CanDash = true;
			
		}
	}
}