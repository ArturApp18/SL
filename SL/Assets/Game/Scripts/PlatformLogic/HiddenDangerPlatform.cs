using System.Collections;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class HiddenDangerPlatform : MonoBehaviour
	{
		[SerializeField] private Transform[] rocks;
		[SerializeField] private float _duration;
		[SerializeField] private float _delayBeetwenRocks;

		public float speed;
		public bool IsActive;

		private void Update()
		{
			if (!IsActive)
			{
				StartCoroutine(MoveRocks());
			}
		}

		private IEnumerator MoveRocks()
		{
			
			foreach (Transform rock in rocks)
			{
				IsActive = true;
				float expiredTime = 0;
				Vector2 basePosition = rock.localPosition;
				Vector2 dangerRockPosition = new Vector2(rock.localPosition.x, rock.localPosition.y + 0.2f);
				while (_duration > expiredTime)
				{
					rock.localPosition = Vector2.Lerp(rock.localPosition, dangerRockPosition, expiredTime / _duration);
					yield return new WaitForSeconds(_delayBeetwenRocks);
					expiredTime = Time.deltaTime;
				}

				
			}
			
		}
	}
}