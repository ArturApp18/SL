using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class HeroJumpFX : MonoBehaviour
	{
		[SerializeField] private AnimationCurve _yAnimation;
		[SerializeField] private AnimationCurve _xAnimation;
		[SerializeField] private float _duration;

		private void Update()
		{
			MovingAnimationPlaying(transform, _duration);
		}

		private void MovingAnimationPlaying(Transform platform, float duration)
		{
			float expiredSeconds = 0;
			float progress = 0;

			Vector2 startPosition = platform.position;

			while (progress < 1)
			{
				expiredSeconds += Time.deltaTime;
				progress = expiredSeconds / duration;

				platform.position = startPosition + new Vector2(_xAnimation.Evaluate(progress), _yAnimation.Evaluate(progress));
			}
		}
	}
}