using UnityEngine;

namespace Game.Scripts.Hero.MeleeComboStateMachine
{
	public class GroundEntryState : MeleeBaseState
	{
		public override void OnEnter(ComboStateMachine comboStateMachine)
		{
			base.OnEnter(comboStateMachine);
			
			_attackIndex = 1;
			Duration = 0.5f;
			_animator.PlayAttack2();
			Debug.Log("Playyer Attack" + _attackIndex + "Fired!");
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			
			if (fixedTime >= Duration)
			{
				if (_shouldCombo)
					ComboStateMachine.SetNextState(new GroundComboState());
				else 
					ComboStateMachine.SetNextStateToMain();
			}
		}
	}
}