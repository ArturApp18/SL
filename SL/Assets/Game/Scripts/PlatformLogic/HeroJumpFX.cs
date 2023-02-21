using Game.Scripts.PlatformLogic;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class HeroJumpFX : MonoBehaviour
	{
		[SerializeField] private AnimationCurve _yAnimation;
		[SerializeField] private AnimationCurve _xAnimation;
		[SerializeField] private float _height;
		[SerializeField] private PureAnimation _playTime;

		private void Awake()
		{
			_playTime = new PureAnimation(this);
		}

	
	}
}