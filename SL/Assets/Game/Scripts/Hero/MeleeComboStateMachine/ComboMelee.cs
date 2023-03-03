using System;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.Hero.MeleeComboStateMachine
{
	public class ComboMelee : MonoBehaviour
	{
		private ComboStateMachine _comboStateMachine;
		[SerializeField] private Collider2D _hitBox;
		private IInputService _inputService;

		private void Update()
		{
			if (_inputService.IsAttackButton() && _comboStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
			{
				_comboStateMachine.SetNextState(new GroundEntryState());
			}
		}
	}
}