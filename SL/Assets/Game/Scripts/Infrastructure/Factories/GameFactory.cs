using System.Collections.Generic;
using Game.Scripts.Enemy;
using Game.Scripts.Infrastructure.AssetManagement;
using Game.Scripts.Infrastructure.Services.PersistentProgress;
using Game.Scripts.Infrastructure.Services.Randomize;
using Game.Scripts.Infrastructure.Services.StaticData;
using Game.Scripts.Infrastructure.States;
using Game.Scripts.Logic;
using Game.Scripts.Logic.EnemySpawners;
using Game.Scripts.StaticData;
using Game.Scripts.UI.Elements;
using Game.Scripts.UI.Services.Window;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Infrastructure.Factories
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssets _assets;
		private readonly IStaticDataService _staticData;
		private readonly IRandomService _randomService;
		private readonly IPersistentProgressService _progressService;
		private readonly IWindowsService _windowsService;
		private readonly IGameStateMachine _stateMachine;

		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
		public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
		private GameObject HeroGameObject { get; set; }

		public GameFactory(IAssets assets, IStaticDataService staticData, IPersistentProgressService persistentProgressService, IRandomService randomService, IWindowsService windowsService, IGameStateMachine stateMachine)
		{
			_assets = assets;
			_staticData = staticData;
			_progressService = persistentProgressService;
			_randomService = randomService;
			_windowsService = windowsService;
			_stateMachine = stateMachine;
		}

		public GameObject CreateHero(Vector3 at)
		{
			//HeroStaticData heroData = _staticData.ForHero(at);
			HeroGameObject = InstantiatedRegistered(AssetPath.HeroPath, at);

			/*State health = _progressService.Progress.HeroState;
			health.CurrentHP = heroData.Hp;
			health.MaxHP = heroData.Hp;

			HeroGameObject.GetComponent<HeroMove>().MovementSpeed = heroData.MoveSpeed;
			Stats heroStats = _progressService.Progress.HeroStats;
			heroStats.Damage = heroData.Damage;
			heroStats.DamageRadius = heroData.Cleavage;*/
			return HeroGameObject;
		}

		
		public GameObject CreateHud()
		{
			GameObject hud = InstantiatedRegistered(AssetPath.HUDPath);

			hud.GetComponentInChildren<LootCounter>()
				.Construct(_progressService.Progress.WorldData);

			foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
			{
				openWindowButton.Construct(_windowsService);
			}
			
			return hud;
			
		}

		public LootPiece CreateLoot()
		{
			LootPiece lootPiece = InstantiatedRegistered(AssetPath.LootPath)
				.GetComponent<LootPiece>();

			lootPiece.Construct(_progressService.Progress.WorldData);

			return lootPiece;
		}

		public GameObject CreateMonster(MonsterTypeId typeId, Transform parent)
		{
			MonsterStaticData monsterData = _staticData.ForMonster(typeId);
			GameObject monster = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);

			IHealth health = monster.GetComponent<IHealth>();
			health.Current = monsterData.Hp;
			health.Max = monsterData.Hp;

			monster.GetComponent<AgentMoveToHero>().Construct(HeroGameObject.transform);
			monster.GetComponent<AgentMoveToHero>().MovementSpeed = monsterData.MoveSpeed;

			LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
			lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);
			lootSpawner.Construct(this, _randomService);

			Attack attack = monster.GetComponent<Attack>();
			attack.Construct(HeroGameObject.transform);
			attack.Damage = monsterData.Damage;
			attack.Cleavage = monsterData.Cleavage;
			attack.EffectiveDistance = monsterData.EffectiveDistance;

			return monster;
		}

		public void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId)
		{
			EnemySpawnPoint spawnPoint = InstantiatedRegistered(AssetPath.SpawnerPath, at)
				.GetComponent<EnemySpawnPoint>();

			spawnPoint.Construct(this);
			spawnPoint.Id = spawnerId;
			spawnPoint.MonsterTypeId = monsterTypeId;
		}

		public void CreateSaveTriggers(Vector2 at)
		{
			SaveTrigger saveTrigger = InstantiatedRegistered(AssetPath.SaveTriggerPath, at)
				.GetComponent<SaveTrigger>();
		}
		
		public void CreateLevelTransfer(Vector2 at)
		{
			LevelTransfer levelTransfer = InstantiatedRegistered(AssetPath.LevelTransferPath, at)
				.GetComponent<LevelTransfer>();
			levelTransfer.Construct(_stateMachine);
		}

		public void Cleanup()
		{
			ProgressReaders.Clear();
			ProgressWriters.Clear();
		}


		public void Register(ISavedProgressReader progressReader)
		{
			if (progressReader is ISavedProgress progressWriter)
				ProgressWriters.Add(progressWriter);


			ProgressReaders.Add(progressReader);
		}

		private GameObject InstantiatedRegistered(string prefabPath, Vector2 position)
		{
			GameObject gameObject = _assets.Instantiate(prefabPath, at: position);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private GameObject InstantiatedRegistered(string prefabPath)
		{
			GameObject gameObject = _assets.Instantiate(prefabPath);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private void RegisterProgressWatchers(GameObject gameObject)
		{
			foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
				Register(progressReader);
		}

	}

}