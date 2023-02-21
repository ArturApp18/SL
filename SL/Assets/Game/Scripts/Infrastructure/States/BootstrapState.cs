using System.ComponentModel;
using Game.Scripts.Infrastructure.AssetManagement;
using Game.Scripts.Infrastructure.Factories;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Infrastructure.Services.ObjectPool;
using Game.Scripts.Infrastructure.Services.PersistentProgress;
using Game.Scripts.Infrastructure.Services.Randomize;
using Game.Scripts.Infrastructure.Services.SaveLoad;
using Game.Scripts.Infrastructure.Services.StaticData;
using Game.Scripts.Services;
using Game.Scripts.Services.Input;
using Game.Scripts.StaticData;
using Game.Scripts.UI.Services.Factory;
using Game.Scripts.UI.Services.Window;
using UnityEngine.Device;
using UnityEngine.Pool;

namespace Game.Scripts.Infrastructure.States
{
	public class BootstrapState : IState
	{
		private const string Initial = "Initial";
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly AllServices _services;

		public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_services = services;

			RegisterServices();
		}

		public void Enter()
		{
			_sceneLoader.Load(Initial, EnterLoadLevel);
		}

		public void Exit() { }

		private void EnterLoadLevel() =>
			_stateMachine.Enter<LoadProgressState>();

		private void RegisterServices()
		{
			RegisterStaticData();
			RegisterAdsService();
			_services.RegisterSingle<IRandomService>(new RandomService());
			_services.RegisterSingle<IGameStateMachine>(_stateMachine);
			_services.RegisterSingle<IInputService>(InputService());
			RegisterAssetProvider();
			_services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
			
			_services.RegisterSingle<IUIFactory>(new UIFactory(
				_services.Single<IAssets>(), 
				_services.Single<IStaticDataService>(), 
				_services.Single<IPersistentProgressService>()));
			
			_services.RegisterSingle<IWindowsService>(new WindowsService(_services.Single<IUIFactory>()));
			
			_services.RegisterSingle<IGameFactory>(new GameFactory(
				_services.Single<IAssets>(),
				_services.Single<IStaticDataService>(),
				_services.Single<IPersistentProgressService>(), 
				_services.Single<IRandomService>(),
			_services.Single<IWindowsService>(),
				_stateMachine));
			
			_services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
		}

		private void RegisterAssetProvider()
		{
			AssetProvider assetProvider = new AssetProvider();
			assetProvider.Instantiate();
			_services.RegisterSingle<IAssets>(assetProvider);
		}

		private void RegisterAdsService()
		{
			//var adsService = new AdsService();
			//adsService.Initialize();
			//_services.RegisterSingle<IAdsService>(adsService);
		}


		private void RegisterStaticData()
		{
			IStaticDataService staticData = new StaticDataService();
			staticData.LoadMonsters();
			_services.RegisterSingle(staticData);
		}

		private static IInputService InputService()
		{
			return Application.isEditor
				? new StandaloneInputService()
				: new MobileInputService();
		}
	}

	public class AdsService
	{
		
	}
}