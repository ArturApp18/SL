using System;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class TiltPlatform : MonoBehaviour
	{
		[SerializeField]
		private Rigidbody2D _rigibody;

		private void Update()
		{
			
			_rigibody.rotation  = Mathf.Clamp(_rigibody.rotation, -30, 30);
		}
	}
}