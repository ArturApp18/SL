using UnityEngine;

namespace Game.Scripts.Enemy
{
	public class KnockBackEffect : MonoBehaviour
	{
		[SerializeField] private Rigidbody2D _rigidbody2D;
		[SerializeField] private float knockBackForceUp;
		[SerializeField] private float knockBackForce;

		public void KnockBack(Transform hero)
		{
			Vector2 knockBackDirection = new Vector2(transform.position.x - hero.position.x, 0);
			_rigidbody2D.velocity = new Vector2(knockBackDirection.x, knockBackForceUp) * knockBackForce;
		}
	}
}