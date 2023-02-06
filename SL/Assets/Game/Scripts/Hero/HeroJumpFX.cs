using System.Collections;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class HeroJumpFX : MonoBehaviour
	{
		[SerializeField] private AnimationCurve _yAnimation;
		[SerializeField] private AnimationCurve _scaleAnimation;
		
		public void PlayJump(Transform hero, float duration)
		{
			StartCoroutine(JumpAnimationPlaying(hero, duration));
		}

		private IEnumerator JumpAnimationPlaying(Transform hero, float duration)
		{
			float expiredSeconds = 0;
			float progress = 0;

			Vector2 startPosition = hero.position;

			while (progress < 1)
			{
				expiredSeconds += Time.deltaTime;
				progress = expiredSeconds / duration;

				hero.position = startPosition + new Vector2(0, _yAnimation.Evaluate(progress));
				yield return null;
			}
		}
	}
}