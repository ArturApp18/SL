using System;
using Game.Scripts.Enemy;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class SlideTouch : MonoBehaviour
	{
		[SerializeField] private GameObject _trail;
		private Vector2 _startTouchPosition;
		private Vector2 _currentTouchPosition;
		private Vector2 _endTouchPosition;

		private IInputService _inputService;
		
		public bool _stopTouch;
		public float _swipeRange;
		public float _tapRange;

		public Vector2 testDistance1;
		public Vector2 testDistance2;

		public bool Slide;
		public int SlideDirection;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
		}

		private void Update()
		{
			Swipe();
		}

		private void Swipe()
		{
			if (_inputService.Axis == Vector2.zero && _inputService.AimAxis == Vector2.zero)
			{
				if (Input.touchCount > 0 && GetFirstTouch().phase == TouchPhase.Began)
				{
					_startTouchPosition = Input.GetTouch(0).position;
					_trail.transform.position = _startTouchPosition;
					_trail.SetActive(true);
				}
				
				if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
				{
					_currentTouchPosition = Input.GetTouch(0).position;
					_trail.transform.position = _currentTouchPosition;
					Vector2 distance = _currentTouchPosition - _startTouchPosition;

					testDistance1 = distance;
					if (!_stopTouch)
					{
						if (distance.x < -_swipeRange)
						{
							Slide = true;
							SlideDirection = -1;
							_stopTouch = true;
							Debug.Log("LEft");
						}
						else if (distance.x > _swipeRange)
						{
							Slide = true;
							SlideDirection = 1;
							_stopTouch = true;
							Debug.Log("Right");
						}
					}
				}

				if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
				{
					_stopTouch = false;

					_endTouchPosition = Input.GetTouch(0).position;

					Vector2 distance = _endTouchPosition - _startTouchPosition;
					testDistance2 = distance;
					if (MathF.Abs(distance.x) < _tapRange && Mathf.Abs(distance.y) < _tapRange)
					{
						Debug.Log("Tap");
					}

					_trail.SetActive(false);
					Slide = false;
				}
			}
			else
			{
				if (Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Began)
				{
					_startTouchPosition = Input.GetTouch(1).position;
				}

				if (Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Moved)
				{
					_currentTouchPosition = Input.GetTouch(1).position;
					Vector2 distance = _currentTouchPosition - _startTouchPosition;

					testDistance1 = distance;
					if (!_stopTouch)
					{
						if (distance.x < -_swipeRange)
						{
							Slide = true;
							SlideDirection = -1;
							_stopTouch = true;
							Debug.Log("LEft");
						}
						else if (distance.x > _swipeRange)
						{
							Slide = true;
							SlideDirection = 1;
							_stopTouch = true;
							Debug.Log("Right");
						}
					}
				}

				if (Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Ended)
				{
					_stopTouch = false;

					_endTouchPosition = Input.GetTouch(1).position;

					Vector2 distance = _endTouchPosition - _startTouchPosition;
					testDistance2 = distance;
					if (MathF.Abs(distance.x) < _tapRange && Mathf.Abs(distance.y) < _tapRange)
					{
						Debug.Log("Tap");
					}

					Slide = false;
				}
			}
		}

		public Touch GetFirstTouch() =>
			Input.GetTouch(0);
	}

}