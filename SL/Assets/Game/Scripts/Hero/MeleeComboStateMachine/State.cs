using System;
using UnityEngine;

namespace Game.Scripts.Hero.MeleeComboStateMachine
{
	public abstract class State
	{
		protected ComboStateMachine ComboStateMachine;
		protected float latetime { get; set; }
		protected float fixedTime { get; set; }
		
		public virtual void OnEnter(ComboStateMachine comboStateMachine)
		{
			ComboStateMachine = comboStateMachine;
		}
		
		public virtual void OnUpdate(){}

		public virtual void OnFixedUpdate()
		{
			fixedTime -= Time.deltaTime;
		}
		
		public virtual void OnLateUpdate()
		{
			latetime += Time.deltaTime;
		}
		public virtual void OnExit(){}
	}

}