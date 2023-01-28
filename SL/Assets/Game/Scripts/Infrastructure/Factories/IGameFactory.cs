using System;
using System.Collections.Generic;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Factories
{
	public interface IGameFactory : IService
	{
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		GameObject HeroGameObject { get; }
		event Action HeroCreated;
		GameObject CreateHero(GameObject at);
		GameObject CreateHud();
		void Cleanup();
	}
}