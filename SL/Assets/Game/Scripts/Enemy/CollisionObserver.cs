using System;
using UnityEngine;

namespace Game.Scripts.Enemy
{
	public class CollisionObserver : MonoBehaviour
	{
		public event Action<Collision2D> CollisionEnter;
		public event Action<Collision2D> CollisionExit;

		private void OnCollisionEnter2D(Collision2D col)
		{
			CollisionEnter?.Invoke(col);
		}

		private void OnCollisionExit2D(Collision2D col)
		{
			CollisionExit?.Invoke(col);
		}
	}
}