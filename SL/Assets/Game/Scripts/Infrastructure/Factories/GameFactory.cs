using System;
using System.Collections.Generic;
using Game.Scripts.Infrastructure.AssetManagement;
using Game.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Factories
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssets _assets;

		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
		public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
		public GameObject HeroGameObject { get; set; }
		public event Action HeroCreated;

		public GameFactory(IAssets assets)
		{
			_assets = assets;
		}

		public GameObject CreateHero(GameObject at)
		{
			HeroGameObject = InstantiatedRegistered(AssetPath.HeroPath, at.transform.position);
			HeroCreated?.Invoke();
			return HeroGameObject;
		}

		public GameObject CreateHud() =>
			InstantiatedRegistered(AssetPath.HUDPath);

		public void Cleanup()
		{
			ProgressReaders.Clear();
			ProgressWriters.Clear();
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

		private void Register(ISavedProgressReader progressReader)
		{
			if(progressReader is ISavedProgress progressWriter)
				ProgressWriters.Add(progressWriter);

			ProgressReaders.Add(progressReader);
		}
	}

}