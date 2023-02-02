using Game.Scripts.Services;
using Game.Scripts.StaticData;
using Game.Scripts.StaticData.Windows;
using Game.Scripts.UI.Services;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Services.StaticData
{
	public interface IStaticDataService : IService
	{
		void LoadMonsters();
		MonsterStaticData ForMonster(MonsterTypeId typeId);
		LevelStaticData ForLevel(string sceneKey);
		WindowConfig ForWindow(WindowId shop);
		
		//HeroStaticData ForHero(GameObject hero);
	}

}