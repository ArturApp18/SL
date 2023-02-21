using UnityEngine;

namespace Game.Scripts.Hero
{
	public class FlipHero : MonoBehaviour
	{
		[SerializeField] private bool _isFacingRight;
		
		public bool IsFacingRight
		{
			get
			{
				return _isFacingRight;
			}
		}

		public void Flip()
		{
			_isFacingRight = !_isFacingRight;

            
			transform.Rotate(0,180, 0);
		}
	}
}