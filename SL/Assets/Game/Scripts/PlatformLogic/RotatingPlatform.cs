using System;
using Game.Scripts.Enemy;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class RotatingPlatform : MonoBehaviour
	{
		[SerializeField] private AnimationCurve _zAnimation;
		[SerializeField] private float _duration;
		[SerializeField] private float _speed;
		[SerializeField] private CollisionObserver _collisionObserver;
		[SerializeField] private Transform heroPosition;
		private float _expiredTime;

		private void Start()
		{
			//_collisionObserver.CollisionEnter += CollisionEnter;
			//_collisionObserver.CollisionExit += CollisionExit;
		}

		private void OnDisable()
		{
			//_collisionObserver.CollisionEnter -= CollisionEnter;
			//_collisionObserver.CollisionExit -= CollisionExit;
		}
		private void CollisionEnter(Collision2D obj)
		{
			heroPosition = obj.transform;
		}

		private void CollisionExit(Collision2D obj)
		{
			heroPosition = null;
			obj.transform.localRotation = Quaternion.Euler(0,0,0);
		}

		private void FixedUpdate()
		{
			float progressZ = CalculateAxisAnimation(_duration);
			
			transform.localEulerAngles = new Vector3(0, 0, _zAnimation.Evaluate(progressZ) * _speed);

			if (heroPosition != null)
			{
				transform.localEulerAngles = new Vector3(0, 0, _zAnimation.Evaluate(progressZ) * _speed);
			}
		}

		private float CalculateAxisAnimation(float duration)
		{
			_expiredTime += Time.deltaTime;

			if (_expiredTime > duration)
				_expiredTime = 0;

			float progress = _expiredTime / duration;
			return progress;
		}
	}
}