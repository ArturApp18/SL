using System.Collections.Generic;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Factories
{
	public interface IGameFactory : IService
	{
		GameObject CreateHero(GameObject at);
		void CreateHud();
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		void Cleanup();
	}
}