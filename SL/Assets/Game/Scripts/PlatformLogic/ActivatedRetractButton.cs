using Game.Scripts.Enemy;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class ActivatedRetractButton : MonoBehaviour
	{
		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private RetractTrigger _retractTrigger;
		private void Start()
		{
			_triggerObserver.TriggerEnter += TriggerEnter;
			_triggerObserver.TriggerExit += TriggerExit;
		}

		private void OnDisable()
		{
			_triggerObserver.TriggerEnter -= TriggerEnter;
			_triggerObserver.TriggerExit -= TriggerExit;
		}

		private void TriggerEnter(Collider2D obj)
		{
			_retractTrigger._triggered = true;
		}

		private void TriggerExit(Collider2D obj)
		{
			_retractTrigger._triggered = false;
		}
	}
}