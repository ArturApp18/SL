using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class ComboAttack : MonoBehaviour
	{
		public HeroAnimator _animator;

		public static ComboAttack instance; 
		public bool IsAttacking = false;
		
		private IInputService _inputService;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
			instance = this;
		}

		private void Update()
		{
			Attacking();
		}

		private void Attacking()
		{
			if (_inputService.IsAttackButtonDown() && !IsAttacking)
			{
				IsAttacking = true;
			}
		}
	}
}