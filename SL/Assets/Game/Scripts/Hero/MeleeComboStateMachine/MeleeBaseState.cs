using Game.Scripts.Services.Input;

namespace Game.Scripts.Hero.MeleeComboStateMachine
{
	public class MeleeBaseState : State
	{
		public float Duration;
		
		protected HeroAnimator _animator;
		protected bool _shouldCombo;
		protected int _attackIndex;
		
		private IInputService _inputService;

		public override void OnEnter(ComboStateMachine comboStateMachine)
		{
			base.OnEnter(comboStateMachine);
		}

		public override void OnUpdate()
		{
			base.OnUpdate();

			if (_inputService.IsAttackButton())
			{
				_shouldCombo = true;
			}
		}

		public override void OnExit()
		{
			base.OnExit();
		}
	}

}