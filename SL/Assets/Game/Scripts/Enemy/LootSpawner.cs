using System.Threading.Tasks;
using Game.Scripts.Data;
using Game.Scripts.Infrastructure.Factories;
using Game.Scripts.Infrastructure.Services.Randomize;
using UnityEngine;

namespace Game.Scripts.Enemy
{
	public class LootSpawner : MonoBehaviour
	{
		public EnemyDeath EnemyDeath;
		private IGameFactory _factory;
		private IRandomService _random;
		private int _lootMin;
		private int _lootMax;

		public void Construct(IGameFactory factory, IRandomService random)
		{
			_factory = factory;
			_random = random;
		}

		private void Start()
		{
			EnemyDeath.Happened += SpawnLoot;
		}

		private async void SpawnLoot()
		{
			LootPiece loot = await _factory.CreateLoot();
			loot.transform.position = transform.position;

			Loot lootItem = GenerateLoot();
			loot.Initialize(lootItem);
		}

		private Loot GenerateLoot()
		{
			return new Loot 
			{
				Value = _random.Next(_lootMin, _lootMax),
			};
		}

		public void SetLoot(int min, int max)
		{
			_lootMin = min;
			_lootMax = max;
		}
	}
}