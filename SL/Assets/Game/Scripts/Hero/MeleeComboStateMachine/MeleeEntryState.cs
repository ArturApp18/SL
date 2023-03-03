namespace Game.Scripts.Hero.MeleeComboStateMachine
{
	public class MeleeEntryState : State
	{
		public override void OnEnter(ComboStateMachine comboStateMachine)
		{
			base.OnEnter(comboStateMachine);

			State nextState = (State) new GroundEntryState();
			ComboStateMachine.SetNextState(nextState);
		}
	}
}