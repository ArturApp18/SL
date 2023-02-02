using System.Collections.Generic;
using Game.Scripts.Logic;
using UnityEngine;

namespace Game.Scripts.StaticData
{
	[CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
	public class LevelStaticData : ScriptableObject
	{
		public string LevelKey;

		public List<EnemySpawnerData> EnemySpawners;

		public Vector2 InitialHeroPosition;
		public Vector2 SaveTriggers;
		public Vector2 LevelTransfers;
		
		
	}

}