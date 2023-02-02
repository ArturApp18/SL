using UnityEngine;

namespace Game.Scripts.Infrastructure.Services.Randomize
{
	public class RandomService : IRandomService
	{
		public int Next(int min, int max) =>
			Random.Range(min, max);
	}
}