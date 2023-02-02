using System.Collections.Generic;
using Game.Scripts.Enemy;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Infrastructure.Services.PersistentProgress;
using Game.Scripts.StaticData;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Factories
{
	public interface IGameFactory : IService
	{
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		GameObject CreateHero(Vector3 at);
		GameObject CreateHud();
		void Cleanup();
		GameObject CreateMonster(MonsterTypeId typeId, Transform parent);
		LootPiece CreateLoot();
		void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId);

		void CreateSaveTriggers(Vector2 at);
		void CreateLevelTransfer(Vector2 at);
	}
}