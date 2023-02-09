using System;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class ExampleRotation : MonoBehaviour
	{
		[SerializeField] private AnimationCurve _zAnimation;
		[SerializeField] private float _duration;
		[SerializeField] private float _speed;
		private float _expiredTime;

		private void Update()
		{
			float progressZ = CalculateAxisAnimation(_duration);

			transform.Rotate(0, 0, _zAnimation.Evaluate(progressZ) * _speed);
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