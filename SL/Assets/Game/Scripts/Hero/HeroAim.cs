using System;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class HeroAim : MonoBehaviour
	{
		[SerializeField] private Transform _aim;
		[SerializeField] private CharacterController2D _controller;
		[SerializeField] private FlipHero _flipHero;
		[SerializeField] private float _returnTime;

		private IInputService _inputService;
		public float _degrece = 90f;
		[SerializeField]
		private float Test;
		[SerializeField]
		private float Test2;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
		}

		private void FixedUpdate()
		{
			Test = _inputService.Axis.x;
			Test2 = _inputService.Axis.x;
			Aim();
		}

		private void Aim()
		{
			Vector3 angle = _aim.localEulerAngles;
			_degrece = angle.z;

			if (HandlerIdle())
			{
				BackToHomeRotation(angle);
			}
			else
			{
				HandleAim(LookingDirection());
			}

			if (_inputService.AimAxis.x > 0 && !_flipHero.IsFacingRight)
			{
				_flipHero.Flip();
				Debug.Log("1.5");
			}
			else if (_inputService.AimAxis.x < 0 && _flipHero.IsFacingRight)
			{
				_flipHero.Flip();
				Debug.Log("1");
			}
		}

		private float LookingDirection()
		{
			if (_flipHero.IsFacingRight)
			{
				return -180;
			}
			else
			{
				return 180;
			}
		}

		private void HandleAim(float lookingDirection)
		{
			_aim.localEulerAngles = new Vector3(0, 0, Math.Clamp(Mathf.Atan2(_inputService.AimAxis.x, _inputService.AimAxis.y) * lookingDirection / Mathf.PI, -135, -45));
		}

		private void BackToHomeRotation(Vector3 angle)
		{
			Vector3 homeRotation;
			
			if (angle.z > 180)
			{
				homeRotation = new Vector3(0, 0, 269.9f);
			}
			else
			{
				homeRotation = Vector3.zero;
			}

			_aim.localEulerAngles = Vector3.Slerp(angle, homeRotation, Time.deltaTime * _returnTime);
		}

		private bool HandlerIdle()
		{
			return _inputService.AimAxis.x == 0 && _inputService.AimAxis.y == 0;
		}
	}
}
