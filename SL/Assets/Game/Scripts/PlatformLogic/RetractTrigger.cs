using System;
using System.Collections;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class RetractTrigger : MonoBehaviour
	{
		[SerializeField] private RetractablePlatform[] _retractablePlatforms;

		private IInputService _inputService;

		public bool IsActive;
		public float PushOutTime;
		public float PushInTime;
		public float DelayBeetwen = 3;
		public bool _triggered;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
		}
		

		private void Update()
		{
			if (!IsActive && _triggered)
			{
				IsActive = true;
				StartCoroutine(RetractingPlatforms());
			}
		}

		private IEnumerator RetractingPlatforms()
		{
			if (IsActive)
			{
				foreach (RetractablePlatform retractablePlatform in _retractablePlatforms)
				{
					retractablePlatform.PushOut();
					yield return new WaitForSeconds(PushOutTime);
				}

				yield return new WaitForSeconds(DelayBeetwen);
				
				foreach (RetractablePlatform retractablePlatform in _retractablePlatforms)
				{
					retractablePlatform.PushIn();
					yield return new WaitForSeconds(PushInTime);
				}
			}

			IsActive = false;
		}
	}

}