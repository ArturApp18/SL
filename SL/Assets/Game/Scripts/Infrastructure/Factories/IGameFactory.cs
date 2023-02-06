using System.Collections.Generic;
using System.Threading.Tasks;
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
		Task<GameObject> CreateHero(Vector3 at);
		Task<GameObject> CreateHud();
		void Cleanup();
		Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent);
		Task<LootPiece> CreateLoot();
		Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId);

		Task CreateSaveTriggers(Vector2 at);
		Task CreateLevelTransfer(Vector2 at);
		Task WarmUp();
	}
}