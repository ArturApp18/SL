using System;
using UnityEngine.Events;

namespace Game.Scripts.Data
{
	[Serializable] public class PlayerProgress
	{
		public WorldData WorldData;

		public PlayerProgress(string initialLevel)
		{
			WorldData = new WorldData(initialLevel);
		}

	}

}