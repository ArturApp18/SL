using Game.Scripts.Services;
using Game.Scripts.StaticData;
using Game.Scripts.StaticData.Windows;
using Game.Scripts.UI.Services;
using Game.Scripts.UI.Services.Window;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Services.StaticData
{
	public interface IStaticDataService : IService
	{
		void LoadStaticData();
		MonsterStaticData ForMonster(MonsterTypeId typeId);
		LevelStaticData ForLevel(string sceneKey);
		WindowConfig ForWindow(WindowId shop);
		HeroStaticData ForHero();

		//HeroStaticData ForHero(GameObject hero);
	}

}