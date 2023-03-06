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
		private const string StaticDataMonsters = "StaticData/Monsters";
		private const string StaticDataLevels = "StaticData/Levels";
		private const string StaticDataHero = "StaticData/Hero/HeroData";
		private const string StaticDataWindowConfig = "StaticData/UI/WindowStaticData";

		private HeroStaticData _hero;
		private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
		private Dictionary<string, LevelStaticData> _levels;
		private Dictionary<WindowId, WindowConfig> _windowConfigs;

		public void LoadStaticData()
		{
			_monsters = Resources
				.LoadAll<MonsterStaticData>(StaticDataMonsters)
				.ToDictionary(x => x.MonsterTypeId, x => x);

			_hero = Resources
				.Load<HeroStaticData>(StaticDataHero);

			_levels = Resources
				.LoadAll<LevelStaticData>(StaticDataLevels)
				.ToDictionary(x => x.LevelKey, x => x);
			
			_windowConfigs = Resources
				.Load<WindowsStaticData>(StaticDataWindowConfig)
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

		public HeroStaticData ForHero() =>
			_hero;

	}
}
