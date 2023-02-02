using Game.Scripts.Data;
using Game.Scripts.Enemy;
using Game.Scripts.Infrastructure.Factories;
using Game.Scripts.Infrastructure.Services.PersistentProgress;
using Game.Scripts.StaticData;
using UnityEngine;

namespace Game.Scripts.Logic.EnemySpawners
{
	public class EnemySpawnPoint : MonoBehaviour, ISavedProgress
	{
		public MonsterTypeId MonsterTypeId;
		public string Id { get; set; }

		private bool _slain;
		private IGameFactory _factory;
		private EnemyDeath _enemyDeath;
		public bool Slain => _slain;

		public void Construct(IGameFactory factory)
		{
			_factory = factory;
		}

		public void LoadProgress(PlayerProgress progress)
		{
			if (progress.KillData.ClearedSpawners.Contains(Id))
			{
				_slain = true;
			}
			else
			{
				Spawn();
			}
		}

		private void Spawn()
		{
			GameObject monster = _factory.CreateMonster(MonsterTypeId, transform);
			_enemyDeath = monster.GetComponent<EnemyDeath>();
			_enemyDeath.Happened += Slay;
		}

		private void Slay()
		{
			if (_enemyDeath != null)
				_enemyDeath.Happened -= Slay;

			_slain = true;
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			if (_slain)
				progress.KillData.ClearedSpawners.Add(Id);
		}
	}

}