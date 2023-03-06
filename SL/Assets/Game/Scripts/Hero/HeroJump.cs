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

		[SerializeField] private float _jumpTimeCounter = 0.2f;
		[SerializeField] private float _groundeTimeCounter;
		[SerializeField] private float _cutJumpHeight;

		private float _jumpPressedRemember = 0;
		private float _groundedRemember;

		private bool _isJumping = false;

		private IInputService _input;
		
		[SerializeField] private int _maxJumps;
		[SerializeField] private int _currentJumps;
		[SerializeField] private bool _isDoubleJumping; 
		[SerializeField] private bool _isWallJumping;
		[SerializeField] private WallSlide _wallSlide;
		
		public Vector2 WallJumpDirection = new Vector2(8,16);

		public float JumpForce;
		public float DoubleJumpForce;
		public float WallJumpForce;
		public float WallJumpTime;
		public float WallJumpCounter;
		public float WallJumpCDuration;

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

			if (!_characterController2D.m_Grounded && _currentJumps > 0 && _input.IsJumpButtonDown())
			{
				_isDoubleJumping = true;
			}
		}

		private void FixedUpdate()
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
				Jump(DoubleJumpForce * transform.up);
				_isDoubleJumping = false;
			}
		}

		private void Jump(Vector2 force)
		{
			Rigidbody.AddForce(force, ForceMode2D.Impulse);
			_currentJumps--;
		}

		private void WallJump()
		{
			if (_wallSlide.WallSliding)
			{
				_isWallJumping = false;
				WallJumpForce = -transform.right.x;
				WallJumpCounter = WallJumpTime;
			}
			else
			{
				WallJumpCounter -= Time.deltaTime;
			}

			if (_input.IsJumpButtonDown() && _characterController2D.m_WallDetected)
			{
				_isWallJumping = true;
				Rigidbody.AddForce(WallJumpForce * -transform.right, ForceMode2D.Impulse);
				WallJumpCounter = 0f;

			
			}
		}

		public void OnLanding()
		{
			Animator.StopJump();
			_currentJumps = _maxJumps;
			_isDoubleJumping = false;
		}
	}

}