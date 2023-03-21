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
		[SerializeField] private WallDetection _wallDetection;
		[SerializeField] private WallSlide _wallSlide;
		[SerializeField] private float _jumpTimeCounter = 0.2f;
		[SerializeField] private float _groundeTimeCounter;
		[SerializeField] private float _cutJumpHeight;
		[SerializeField] private HeroMove _heroMove;
		[SerializeField] private HeroFlip _heroFlip;
		private float _jumpPressedRemember = 0;
		private float _groundedRemember;

		private bool _isJumping = false;

		private IInputService _input;

		[SerializeField] private int _maxJumps;
		[SerializeField] private int _currentJumps;
		[SerializeField] private bool _isDoubleJumping;

		public float JumpForce;
		public float DoubleJumpForce;

		public void Construct(IInputService input)
		{
			_input = input;
		}

		private void Awake()
		{
			_currentJumps = _maxJumps;
		}

		private void Update()
		{
			_groundedRemember -= Time.deltaTime;
			
			if (_characterController2D.m_Grounded)
			{
				_groundedRemember = _groundeTimeCounter;
				Animator.StartLanding();
			}
			else
			{
				Animator.StopLanding();
			}
			
			_jumpPressedRemember -= Time.deltaTime;
			
			if (_input.IsJumpButtonDown() )
			{
				_jumpPressedRemember = _jumpTimeCounter;
				Animator.PlayJump();
			}

			if (!_characterController2D.m_Grounded && _currentJumps > 0 && _input.IsJumpButtonDown() && !_wallDetection.IsWallDetected)
			{
				_isDoubleJumping = true;
			}
		}

		private void FixedUpdate()
		{
			if (!_wallDetection.IsWallDetected)
			{
				if (_input.IsJumpButtonUp() && Rigidbody.velocity.y > 0)
				{
					Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Rigidbody.velocity.y * _cutJumpHeight);
					Debug.Log("yo");

				}

				if (_jumpPressedRemember > 0 && _groundedRemember > 0)
				{
					_jumpPressedRemember = 0;
					_groundedRemember = 0;
					Jump(JumpForce * transform.up);
					Debug.Log("yojump");
				}

				if (_isDoubleJumping)
				{
					_heroMove.CanMove = true;
					Jump(DoubleJumpForce * transform.up);
					_isDoubleJumping = false;
				}
			}
		}

		private void Jump(Vector2 force)
		{
			Rigidbody.AddForce(force, ForceMode2D.Impulse);
			_currentJumps--;
		}
		
		public void OnLanding()
		{
			_heroMove.CanMove = true;
			_heroFlip.CanFlip = true;

			_wallSlide.IsWallSliding = false;
			Animator.StopJump();
			Animator.StartWallSlide(false);
			_currentJumps = _maxJumps;
			_isDoubleJumping = false;
			Debug.Log("123");
		}
	}

}