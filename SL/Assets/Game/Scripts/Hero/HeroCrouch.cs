using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class HeroCrouch : MonoBehaviour
	{
		[SerializeField] private HeroAnimator _animator;
		[SerializeField] private CharacterController2D _controller2D;
		private IInputService _inputService;
		[SerializeField]
		private float test;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
		}

		private void Update()
		{
			test = _inputService.Axis.y;
			if (_inputService.Axis.y < -0.5)
			{
				_controller2D.Move(true);
			}
			else
			{
				_controller2D.Move(false);
			}
		}

		public void OnCrouching(bool isCrouching)
		{
			_animator.StartCrouch(isCrouching);
		}
	}
}