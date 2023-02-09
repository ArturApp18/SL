using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class Example : MonoBehaviour
	{
		[SerializeField] private AnimationCurve _yAnimation;
		[SerializeField] private AnimationCurve _xAnimation;
		[SerializeField] private float _duration = 1;
		[SerializeField] private float _wieght = 5;
		[SerializeField] private float _height = 5;

		private float _expiredTime;

		private void Update()
		{
			float progressY = CalculateAxisAnimation(_duration);
			float progressX = CalculateAxisAnimation(_duration);

			transform.localPosition = new Vector3(_xAnimation.Evaluate(progressX) * _wieght, _yAnimation.Evaluate(progressY) *_height, 0);
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