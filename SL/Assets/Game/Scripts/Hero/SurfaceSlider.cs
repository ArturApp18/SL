using UnityEngine;

namespace Game.Scripts.Hero
{
	public class SurfaceSlider : MonoBehaviour
	{
		private Vector3 _normal;

		public Vector3 Project(Vector3 forward) =>
			forward - Vector2.Dot(forward, _normal) * _normal;

		private void OnCollisionEnter2D(Collision2D col)
		{
			_normal = col.contacts[0].normal;
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.white;
			Gizmos.DrawLine(transform.position, transform.position + _normal * 3);
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, transform.position + Project(transform.forward));
		}
	}
}