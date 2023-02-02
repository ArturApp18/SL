using System;
using Game.Scripts.UI.Services;
using Game.Scripts.UI.Windows;

namespace Game.Scripts.StaticData.Windows
{
	[Serializable]
	public class WindowConfig
	{
		public WindowId WindowId;
		public WindowBase Prefab;
	}
}