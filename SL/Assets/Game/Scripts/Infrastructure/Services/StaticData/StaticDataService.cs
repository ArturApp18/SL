using System.Collections.Generic;
using System.Linq;
using Game.Scripts.StaticData;
using Game.Scripts.StaticData.Windows;
using Game.Scripts.UI.Services;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Services.StaticData
{
	public class StaticDataService : IStaticDataService
	{
		private const string StaticdataMonsters = "StaticData/Monsters";
		private const string StaticdataLevels = "StaticData/Levels";
		private const string StaticdataHero = "StaticData/Hero/Hero";
		private const string StaticdataWindowconfig = "StaticData/UI/WindowStaticData";

		private Dictionary<GameObject, HeroStaticData> _hero;
		private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
		private Dictionary<string, LevelStaticData> _levels;
		private Dictionary<WindowId, WindowConfig> _windowConfigs;

		public void LoadMonsters()
		{
			_monsters = Resources
				.LoadAll<MonsterStaticData>(StaticdataMonsters)
				.ToDictionary(x => x.MonsterTypeId, x => x);
			
			_levels = Resources
				.LoadAll<LevelStaticData>(StaticdataLevels)
				.ToDictionary(x => x.LevelKey, x => x);
			
			_windowConfigs = Resources
				.Load<WindowsStaticData>(StaticdataWindowconfig)
				.Configs
				.ToDictionary(x => x.WindowId, x => x);
		}

		public MonsterStaticData ForMonster(MonsterTypeId typeId) =>
			_monsters.TryGetValue(typeId, out MonsterStaticData staticData)
				? staticData
				: null;

		public LevelStaticData ForLevel(string sceneKey) =>
			_levels.TryGetValue(sceneKey, out LevelStaticData staticData)
				? staticData
				: null;

		public WindowConfig ForWindow(WindowId windowId) =>
			_windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig)
				? windowConfig
				: null;
		
	}
}
