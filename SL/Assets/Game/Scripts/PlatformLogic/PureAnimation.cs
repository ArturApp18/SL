using System;
using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class PureAnimation
	{
		public TransformChanges LastChanges { get; private set; }

		private Coroutine _lastAnimation;
		private MonoBehaviour _context;

		public PureAnimation(MonoBehaviour context)
		{
			_context = context;
		}

		public void Play(float duration, Func<float, TransformChanges> body)
		{
			if (_lastAnimation != null)
				_context.StopCoroutine(_lastAnimation);

			_lastAnimation = _context.StartCoroutine(GetAnimation(duration, body));
		}

		private IEnumerator GetAnimation(float duration, Func<float, TransformChanges> body)
		{
			var expiredSeconds = 0f;
			var progress = 0f;

			while (progress<1)
			{
				expiredSeconds += Time.deltaTime;
				progress = expiredSeconds / duration;

				LastChanges = body.Invoke(progress);
				
				yield return null;
			}
		}
	}

	public class TransformChanges
	{
		public Transform lastChanges;
		public TransformChanges(Vector3 position, Vector3 scale)
		{
			
		}
	}
}