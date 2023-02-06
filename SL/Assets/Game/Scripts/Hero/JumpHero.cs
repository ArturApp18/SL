/*using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class JumpHero : MonoBehaviour
	{
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private HeroJumpFX _fx;
		[SerializeField] private float _length;
		[SerializeField] private float _duration;

		private PureAnimation _playTime;

		private void Awake()
		{
			_playTime = new PureAnimation(this);
		}

		public void Jump(Vector3 direction)
		{
			Vector2 target = transform.position + ( direction * _length );
			Vector2 startPosition = transform.position;
			PureAnimation fxPlayTime = _fx.PlayJump(transform, _duration);

			_playTime.Play(_duration, (progress) =>
			{
				transform.position = Vector2.Lerp(startPosition, target, progress) + fxPlayTime.LastChanges.Position;
				return null;
			});

		}
	}

	public class PureAnimation
	{
		public TransformChanges LastChanges {}
	}
}*/