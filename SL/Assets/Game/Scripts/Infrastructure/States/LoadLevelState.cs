using Game.Scripts.CameraLogic;
using Game.Scripts.Hero;
using Game.Scripts.Infrastructure.Factories;
using Game.Scripts.Infrastructure.Services.PersistentProgress;
using Game.Scripts.Infrastructure.Services.StaticData;
using Game.Scripts.Logic;
using Game.Scripts.StaticData;
using Game.Scripts.UI.Elements;
using Game.Scripts.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Infrastructure.States
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IGameFactory _gameFactory;
		private readonly IPersistentProgressService _progressService;
		private readonly IStaticDataService _staticData;
		private readonly IUIFactory _uiFactory;

		public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory,
			IPersistentProgressService progressService, IStaticDataService staticData, IUIFactory uiFactory)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_gameFactory = gameFactory;
			_progressService = progressService;
			_staticData = staticData;
			_uiFactory = uiFactory;
		}

		public void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_gameFactory.Cleanup();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		public void Exit() =>
			_loadingCurtain.Hide();

		private void OnLoaded()
		{
			InitUIRoot();
			InitGameWorld();
			InformProgressReaders();
			_stateMachine.Enter<GameLoopState>();
		}

		private void InitUIRoot() =>
			_uiFactory.CreateUIRoot();

		private void InformProgressReaders()
		{
			foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
				progressReader.LoadProgress(_progressService.Progress);
		}

		private void InitGameWorld()
		{
			LevelStaticData levelData = LevelStaticData();

			InitSpawners(levelData);
			InitSaveTriggers(levelData);
			InitLevelTransfer(levelData);
			GameObject hero = InitHero(levelData);

			InitHud(hero);

			CameraFollow(hero);
		}

		private GameObject InitHero(LevelStaticData levelData) =>
			_gameFactory.CreateHero(levelData.InitialHeroPosition);

		private void InitSpawners(LevelStaticData levelData)
		{
			foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
			{
				_gameFactory.CreateSpawner(spawnerData.Position, spawnerData.Id, spawnerData.MonsterTypeId);
			}
		}

		private void InitSaveTriggers(LevelStaticData levelData)
		{
			_gameFactory.CreateSaveTriggers(levelData.SaveTriggers);
		}

		private void InitLevelTransfer(LevelStaticData levelData)
		{
			_gameFactory.CreateLevelTransfer(levelData.LevelTransfers);
		}

		private void InitHud(GameObject hero)
		{
			GameObject hud = _gameFactory.CreateHud();

			hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
		}

		private LevelStaticData LevelStaticData() =>
			_staticData.ForLevel(SceneManager.GetActiveScene().name);

		private void CameraFollow(GameObject hero)
		{
			Camera.main
				.GetComponent<CameraFollow>()
				.Follow(hero);
		}
	}
}