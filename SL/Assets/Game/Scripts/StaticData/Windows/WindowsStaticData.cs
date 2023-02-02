using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.StaticData.Windows
{
	[CreateAssetMenu(fileName = "WindowStaticData", menuName = "StaticData/WindowStaticData")]
	public class WindowsStaticData : ScriptableObject
	{
		public List<WindowConfig> Configs;
	}
}