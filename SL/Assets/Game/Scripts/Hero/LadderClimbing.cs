using System;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class LadderClimbing : MonoBehaviour
	{
		private bool _isClimbing;

		private void Update()
		{
			if (_isClimbing)
			{
				
			}
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			_isClimbing = true;
		}

		private void OnCollisionExit2D(Collision2D col)
		{
			_isClimbing = false;
		}
	}
}