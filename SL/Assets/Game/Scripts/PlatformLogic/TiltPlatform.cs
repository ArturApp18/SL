using System;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class TiltPlatform : MonoBehaviour
	{
		[SerializeField] private Rigidbody2D _rigibody;

		private void Update()
		{
			transform.localRotation = Quaternion.Euler(0,0,Mathf.Clamp(_rigibody.rotation, -30, 30));
			transform.localPosition = Vector3.zero;
		}
	}
}