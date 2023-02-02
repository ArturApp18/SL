using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Infrastructure.States;
using UnityEngine;

namespace Game.Scripts.Logic
{
	public class LevelTransfer : MonoBehaviour
	{
		private const string Player = "Player";
		private IGameStateMachine _stateMachine;
		public string TransferTo;

		private bool _triggered;

		public void Construct(IGameStateMachine stateMachine)
		{
			_stateMachine = stateMachine;
		}
		
		private void OnTriggerEnter2D(Collider2D col)
		{
			if (col.CompareTag(Player))
			{
				_stateMachine.Enter<LoadLevelState, string>(TransferTo);
				_triggered = true;
			}
		}
	}
}