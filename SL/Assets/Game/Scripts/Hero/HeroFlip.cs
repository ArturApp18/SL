using System;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class HeroFlip : MonoBehaviour
	{
		[SerializeField] private bool _isFacingRight;
		[SerializeField] private bool _canFlip;
		[SerializeField] private float _facingDirection;
		[SerializeField] private WallDetection _wallDetection;
		[SerializeField] private CharacterController2D _controller;

		private IInputService _inputService;

		public void Construct(IInputService input)
		{
			_inputService = input;
		}

		public bool IsFacingRight
		{
			get
			{
				return _isFacingRight;
			}
		}
		public bool CanFlip
		{
			get
			{
				return _canFlip;
			}
			set
			{
				_canFlip = value;
			}
		}
		public float FacingDirection
		{
			get
			{
				return _facingDirection;
			}
			set
			{
				_facingDirection = value;
			}
		}

		private void Update()
		{
			if (CanFlip)
			{
				if (_inputService.AimAxis.x == 0)
				{
					if (_inputService.Axis.x > 0 && !IsFacingRight)
					{
						Flip();
						Debug.Log("1.5");
					}
					else if (_inputService.Axis.x < 0 && IsFacingRight)
					{
						Flip();
						Debug.Log("1");
					}
				}
				else
				{
					if (_inputService.AimAxis.x > 0 && !IsFacingRight)
					{
						Flip();
						Debug.Log("1.5");
					}
					else if (_inputService.AimAxis.x < 0 && IsFacingRight)
					{
						Flip();
						Debug.Log("1");
					}
				}
			}
			if (_controller.m_Grounded && _wallDetection.IsWallDetected)
			{
				if (IsFacingRight && _inputService.Axis.x < 0)
				{
					Flip();
				}
				else if(!IsFacingRight && _inputService.Axis.x > 0)
				{
					Flip();
				}
			}
		}

		public void Flip()
		{
			FacingDirection = FacingDirection * -1;
			_isFacingRight = !_isFacingRight;
			transform.Rotate(0,180, 0);
		}
	}
}