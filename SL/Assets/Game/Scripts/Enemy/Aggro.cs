using System.Collections;
using UnityEngine;

namespace Game.Scripts.Enemy
{
	public class Aggro : MonoBehaviour
	{
		public TriggerObserver TriggerObserver;
		public Follow Follow;

		public float Cooldown;
		private Coroutine _aggroCoroutine;
		private bool _hasAggroTarget;

		private void Start()
		{
			TriggerObserver.TriggerEnter += TriggerEnter;
			TriggerObserver.TriggerExit += TriggerExit;

			SwitchFollowOff();
		}

		private void TriggerEnter(Collider2D obj)
		{
			if (!_hasAggroTarget)
			{
				_hasAggroTarget = true;
				
				StopAggroCoroutine();
				
				SwitchFollowOn();
				Debug.Log(obj.transform.position);
			}
		}

		private void TriggerExit(Collider2D obj)
		{
			if (_hasAggroTarget)
			{
				_hasAggroTarget = false;
				_aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
			}
		}

		private void StopAggroCoroutine()
		{
			if(_aggroCoroutine != null)
			{
				StopCoroutine(SwitchFollowOffAfterCooldown());
				_aggroCoroutine = null;
			}
		}

		private IEnumerator SwitchFollowOffAfterCooldown()
		{
			yield return new WaitForSeconds(Cooldown);
			SwitchFollowOff();
		}

		private void SwitchFollowOn() =>
			Follow.enabled = true;

		private void SwitchFollowOff() =>
			Follow.enabled = false;
	}
}