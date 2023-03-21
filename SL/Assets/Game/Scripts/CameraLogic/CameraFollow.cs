using System;
using Cinemachine;
using UnityEngine;

namespace Game.Scripts.CameraLogic
{
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField] private CinemachineVirtualCamera _virtualCamera;
		[SerializeField] private ParallaxLayer[] _parallaxLayers;
		public CinemachineVirtualCamera VirtualCamera
		{
			get
			{
				return _virtualCamera;
			}
		}


		public void Follow(GameObject following)
		{
			VirtualCamera.Follow = following.transform;
			if (_parallaxLayers != null)
			{
				foreach (ParallaxLayer parallaxLayer in _parallaxLayers)
				{
					parallaxLayer.FollowParallax(following.transform);
				}
			}
		}

	}
}