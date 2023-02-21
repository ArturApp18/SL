using System;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class DebugDrawSphere : MonoBehaviour
	{
		public float _radius;

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			_radius = 0.05f;
			Gizmos.DrawSphere(transform.position, _radius);
		}
	}
}