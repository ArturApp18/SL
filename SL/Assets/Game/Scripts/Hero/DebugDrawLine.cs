using System;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class DebugDrawLine : MonoBehaviour
	{
		private void OnDrawGizmos()
		{
			Debug.DrawLine(transform.position, transform.position + transform.forward * 50);
		}
	}

}