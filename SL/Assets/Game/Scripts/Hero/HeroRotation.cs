using System;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class HeroRotation : MonoBehaviour
	{
		public bool IsFacingRight { get; private set; }

		private void Start()
		{
			IsFacingRight = true;
		}

		public void Turn()
		{
			Vector3 scale = transform.localScale; 
			scale.x *= -1;
			transform.localScale = scale;

			IsFacingRight = !IsFacingRight;
		}
	}
}