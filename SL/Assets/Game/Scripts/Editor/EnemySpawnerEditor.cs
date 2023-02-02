using Game.Scripts.Logic;
using Game.Scripts.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Editor
{
	[CustomEditor(typeof(EnemySpawnMarker))]
	public class EnemySpawnMarkerEditor : UnityEditor.Editor
	{
		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
		public static void RenderCustomGizmo(EnemySpawnMarker spawnPoint, GizmoType gizmo)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(spawnPoint.transform.position, 0.5f);
		}
	}
}