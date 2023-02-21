using System;
using Cinemachine;
using UnityEngine;

namespace Game.Scripts.CameraLogic
{
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField] private CinemachineVirtualCamera _virtualCamera;

		public void Follow(GameObject following)
		{
			_virtualCamera.Follow = following.transform;
		}

	}
}