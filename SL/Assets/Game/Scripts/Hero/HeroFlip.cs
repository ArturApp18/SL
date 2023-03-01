using System;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class HeroFlip : MonoBehaviour
	{
		[SerializeField] private bool _isFacingRight;

		private IInputService _inputService;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
		}

		public bool IsFacingRight
		{
			get
			{
				return _isFacingRight;
			}
		}

		private void Update()
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

		public void Flip()
		{
			_isFacingRight = !_isFacingRight;

            
			transform.Rotate(0,180, 0);
		}
	}
}