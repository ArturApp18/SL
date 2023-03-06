using System.Collections.Generic;
using System.Threading.Tasks;
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
		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
		public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();


		private readonly IAssets _assets;
		private readonly IStaticDataService _staticData;
		private readonly IRandomService _randomService;
		private readonly IPersistentProgressService _progressService;
		private readonly IWindowsService _windowsService;
		private readonly IGameStateMachine _stateMachine;

		private GameObject HeroGameObject { get; set; }

		public GameFactory(IAssets assets, IStaticDataService staticData, IPersistentProgressService persistentProgressService, IRandomService randomService,
			IWindowsService windowsService, IGameStateMachine stateMachine)
		{
			_assets = assets;
			_staticData = staticData;
			_progressService = persistentProgressService;
			_randomService = randomService;
			_windowsService = windowsService;
			_stateMachine = stateMachine;
		}

		public async Task WarmUp()
		{
			await _assets.Load<GameObject>(AssetAddress.Loot);
			await _assets.Load<GameObject>(AssetAddress.Spawner);
		}

		public async Task<GameObject> CreateHero(Vector3 at)
		{
			//HeroStaticData heroData = _staticData.ForHero(at);
			HeroGameObject = await InstantiatedRegisteredAsync(AssetAddress.HeroPath, at);

			/*State health = _progressService.Progress.HeroState;
			health.CurrentHP = heroData.Hp;
			health.MaxHP = heroData.Hp;

			HeroGameObject.GetComponent<HeroMove>().MovementSpeed = heroData.MoveSpeed;
			Stats heroStats = _progressService.Progress.HeroStats;
			heroStats.Damage = heroData.Damage;
			heroStats.DamageRadius = heroData.Cleavage;*/
			return HeroGameObject;
		}


		public async Task<GameObject> CreateHud()
		{
			GameObject hud = await InstantiatedRegisteredAsync(AssetAddress.HUDPath);

			hud.GetComponentInChildren<LootCounter>()
				.Construct(_progressService.Progress.WorldData);

			foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
				openWindowButton.Construct(_windowsService);

			return hud;
		}

		public async Task<LootPiece> CreateLoot()
		{
			GameObject prefab = await _assets.Load<GameObject>(AssetAddress.Loot);
			LootPiece lootPiece = InstantiatedRegistered(prefab)
				.GetComponent<LootPiece>();

			lootPiece.Construct(_progressService.Progress.WorldData);

			return lootPiece;
		}

		public async Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent)
		{
			MonsterStaticData monsterData = _staticData.ForMonster(typeId);

			GameObject prefab = await _assets.Load<GameObject>(monsterData.PrefabReference);


			GameObject monster = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);

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

		public async Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId)
		{
			GameObject prefab = await _assets.Load<GameObject>(AssetAddress.Spawner);
			EnemySpawnPoint spawnPoint = InstantiatedRegistered(prefab, at)
				.GetComponent<EnemySpawnPoint>();

			spawnPoint.Construct(this);
			spawnPoint.Id = spawnerId;
			spawnPoint.MonsterTypeId = monsterTypeId;
		}

		public async Task CreateSaveTriggers(Vector2 at)
		{
			GameObject saveTrigger = await InstantiatedRegisteredAsync(AssetAddress.SaveTriggerPath, at);
		}

		public async Task CreateLevelTransfer(Vector2 at, string levelKey)
		{
			GameObject levelTransfer = await InstantiatedRegisteredAsync(AssetAddress.LevelTransferPath, at);
			var transfer = levelTransfer.GetComponent<LevelTransfer>();
			transfer.TransferTo = levelKey;
			transfer.Construct(_stateMachine);
		}

		public void Cleanup()
		{
			ProgressReaders.Clear();
			ProgressWriters.Clear();

			_assets.Cleanup();
		}


		private void Register(ISavedProgressReader progressReader)
		{
			if (progressReader is ISavedProgress progressWriter)
				ProgressWriters.Add(progressWriter);


			ProgressReaders.Add(progressReader);
		}

		private async Task<GameObject> InstantiatedRegisteredAsync(string prefabPath, Vector2 position)
		{
			GameObject gameObject = await _assets.Instantiate(prefabPath, at: position);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private async Task<GameObject> InstantiatedRegisteredAsync(string prefabPath)
		{
			GameObject gameObject = await _assets.Instantiate(prefabPath);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private GameObject InstantiatedRegistered(GameObject prefab, Vector2 position)
		{
			GameObject gameObject = Object.Instantiate(prefab, position, Quaternion.identity);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private GameObject InstantiatedRegistered(GameObject prefab)
		{
			GameObject gameObject = Object.Instantiate(prefab);
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