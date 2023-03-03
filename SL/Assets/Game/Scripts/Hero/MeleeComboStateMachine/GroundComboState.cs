using UnityEngine;

namespace Game.Scripts.Hero.MeleeComboStateMachine
{
	public class GroundComboState : MeleeBaseState
	{
		public override void OnEnter(ComboStateMachine comboStateMachine)
		{
			base.OnEnter(comboStateMachine);
			
			_attackIndex = 2;
			Duration = 0.5f;
			_animator.PlayAttack();
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