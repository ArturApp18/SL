using UnityEngine;

namespace Game.Scripts.Hero.MeleeComboStateMachine
{
	public class GroundFinisherState : MeleeBaseState
	{
		
		public override void OnEnter(ComboStateMachine comboStateMachine)
		{
			base.OnEnter(comboStateMachine);
			
			_attackIndex = 3;
			Duration = 0.75f;
			_animator.PlayAttack3();
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