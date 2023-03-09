using System;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class SlideTouch : MonoBehaviour
	{
		private Vector2 _startTouchPosition;
		private Vector2 _currentTouchPosition;
		private Vector2 _endTouchPosition;
		public bool _stopTouch;
		public float _swipeRange;
		public float _tapRange;

		private void Update()
		{
			Swipe();
		}

		private void Swipe()
		{
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				_startTouchPosition = Input.GetTouch(0).position;
			}

			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				_currentTouchPosition = Input.GetTouch(0).position;
				Vector2 distance = _currentTouchPosition - _startTouchPosition;
				
				if (!_stopTouch)
				{
					if (distance.x < _swipeRange)
					{
						_stopTouch = true;
					}
				}
			}

			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				_stopTouch = false;

				_endTouchPosition = Input.GetTouch(0).position;

				Vector2 distance = _endTouchPosition - _startTouchPosition;

				if (MathF.Abs(distance.x)< _tapRange && Mathf.Abs(distance.y)<_tapRange)
				{
					Debug.Log("Tap");
				}


			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawSphere(transform.position, _swipeRange);
		}
	}
}