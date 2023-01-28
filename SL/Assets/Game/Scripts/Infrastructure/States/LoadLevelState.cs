using System;
using Game.Scripts.CameraLogic;
using Game.Scripts.Hero;
using Game.Scripts.Infrastructure.Factories;
using Game.Scripts.Infrastructure.Services.PersistentProgress;
using Game.Scripts.Logic;
using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Infrastructure.States
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private const string InitialPointTag = "InitialPoint";
		
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IGameFactory _gameFactory;
		private readonly IPersistentProgressService _progressService;

		public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IPersistentProgressService progressService)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_gameFactory = gameFactory;
			_progressService = progressService;
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
			InitGameWold();
			InformProgressReaders();
			_stateMachine.Enter<GameLoopState>();
		}

		private void InformProgressReaders()
		{
			foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
				progressReader.LoadProgress(_progressService.Progress);
		}

		private void InitGameWold()
		{
			GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));

			InitHud(hero);

			CameraFollow(hero);
		}

		private void InitHud(GameObject hero)
		{
			GameObject hud = _gameFactory.CreateHud();

			hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
		}

		private void CameraFollow(GameObject hero)
		{
			Camera.main
				.GetComponent<CameraFollow>()
				.Follow(hero);
		}
	}
}