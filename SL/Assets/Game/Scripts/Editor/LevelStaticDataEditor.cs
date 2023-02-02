using System.Linq;
using Game.Scripts.Logic;
using Game.Scripts.Logic.EnemySpawners;
using Game.Scripts.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Editor
{
	[CustomEditor(typeof(LevelStaticData))]
	public class LevelStaticDataEditor : UnityEditor.Editor
	{
		private const string SaveTriggerTag = "SaveTrigger";
		private const string InitialPointTag = "InitialPoint";
		private const string Leveltransfer = "LevelTransfer";

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			LevelStaticData levelData = (LevelStaticData) target;

			if (GUILayout.Button("Collect"))
			{
				levelData.EnemySpawners =
					FindObjectsOfType<EnemySpawnMarker>()
						.Select(x => new EnemySpawnerData(x.GetComponent<UniqueId>().Id, x.MonsterTypeId, x.transform.position))
						.ToList();
				
				levelData.LevelKey = SceneManager.GetActiveScene().name;
				levelData.InitialHeroPosition = GameObject.FindWithTag(InitialPointTag).transform.position;
				levelData.SaveTriggers = GameObject.FindGameObjectWithTag(SaveTriggerTag).transform.position;
		
				levelData.LevelTransfers = GameObject.FindGameObjectWithTag(Leveltransfer).transform.position;
			}


			EditorUtility.SetDirty(target);
		}
	}
}
	


