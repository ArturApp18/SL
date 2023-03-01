using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class MovingPlatform : MonoBehaviour
	{
		[SerializeField] private AnimationCurve _yAnimation;
		[SerializeField] private AnimationCurve _xAnimation;
		[SerializeField] private Rigidbody2D _rigidBody;

		[SerializeField] private float _duration = 1;
		[SerializeField] private float _wieght = 5;
		[SerializeField] private float _height = 5;

		private float _expiredTime;
		private bool _isEntered;
		private Quaternion _rotation;

		private void FixedUpdate()
		{
			float progressY = CalculateAxisAnimation(_duration);
			float progressX = CalculateAxisAnimation(_duration);

			_rigidBody.MovePosition(new Vector2(_xAnimation.Evaluate(progressX) * _wieght, _yAnimation.Evaluate(progressY) * _height));
		}

		private float CalculateAxisAnimation(float duration)
		{
			_expiredTime += Time.fixedDeltaTime;

			if (_expiredTime > duration)
				_expiredTime = 0;

			float progress = _expiredTime / duration;
			return progress;
		}
	}
}