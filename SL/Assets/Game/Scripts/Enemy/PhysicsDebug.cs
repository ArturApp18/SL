using UnityEngine;

namespace Game.Scripts.Enemy
{
	public class PhysicsDebug
	{
		public static void DrawDebub(Vector3 worldPos, float radius, float seconds)
		{
			Debug.DrawRay(worldPos, radius * Vector3.up, Color.red, seconds);
			Debug.DrawRay(worldPos, radius * Vector3.down, Color.red, seconds);
			Debug.DrawRay(worldPos, radius * Vector3.left, Color.red, seconds);
			Debug.DrawRay(worldPos, radius * Vector3.right, Color.red, seconds);
		}
	}
}