using System;
using Game.Scripts.Data;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Infrastructure.Services.PersistentProgress;
using Game.Scripts.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Hero
{
	public class HeroMove : MonoBehaviour, ISavedProgress
	{
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private HeroAnimator _animator;

		[SerializeField] private bool _canMove;
		[SerializeField] private float _acceleration;
		[SerializeField] private float _decceleration;
		[SerializeField] private float _velocityPower;
		
		private IInputService _inputService;

		public float MovementSpeed;

		private bool _jump;
		private bool _crouch = false;
		private float _horizontalMove;
		public bool CanMove
		{
			get
			{
				return _canMove;
			}
			set
			{
				_canMove = value;
			}
		}

		public void Construct(IInputService input)
		{
			_inputService = input;
		}

		private void Update()
		{
			if (_canMove)
			{
				_horizontalMove = _inputService.Axis.x;
				if (_inputService.Axis.x > 0.1 || _inputService.Axis.x < -0.1)
				{
					_animator.StartRun();
				}
				else
				{
					_animator.StopRun();
				}
			}
		}

		private void FixedUpdate()
		{
			if (_canMove)
			{
				float targetSpeed = _horizontalMove * MovementSpeed;
				float speedDif = targetSpeed - _rigidbody.velocity.x;
				float accelRate = ( Mathf.Abs(MovementSpeed) > 0.01f ) ? _acceleration : _decceleration;
				float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _velocityPower) * Mathf.Sign(speedDif);
				_rigidbody.AddForce(movement * Vector2.right, ForceMode2D.Force);

			}
		}

		public void UpdateProgress(PlayerProgress progress) =>
			progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());

		private static string CurrentLevel() =>
			SceneManager.GetActiveScene().name;

		public void LoadProgress(PlayerProgress progress)
		{
			if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
			{
				Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
				if (savedPosition != null)
					Warp(to: savedPosition);
			}
		}

		private void Warp(Vector3Data to)
		{
			transform.position = to.AsUnityVector();
		}
	}

}