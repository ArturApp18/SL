using System.Collections;
using Game.Scripts.Data;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Scripts.Enemy
{
	public class LootPiece : MonoBehaviour
	{
		public GameObject CDisk;
		public GameObject PickupFxPrefab;
		public TextMeshPro LootText;
		public GameObject PickupPopup;
		
		private Loot _loot;
		private bool _picked;
		private WorldData _worldData;

		public void Construct(WorldData worldData)
		{
			_worldData = worldData;
		}
		
		public void Initialize(Loot loot)
		{
			_loot = loot;
		}

		private void OnTriggerEnter2D(Collider2D col) =>
			Pickup();

		private void Pickup()
		{
			if (_picked)
				return;

			_picked = true;

			UpdateWorldData();
			HideCDisk();
			PlayPickupFx();
			ShowText();
			StartCoroutine(StartDestroyTimer());
		}

		private void UpdateWorldData()
		{
			_worldData.LootData.Collect(_loot);
		}

		private void HideCDisk() =>
			CDisk.SetActive(false);

		private void PlayPickupFx() =>
			Instantiate(PickupFxPrefab, transform.position, quaternion.identity);

		private void ShowText()
		{
			LootText.text = $"{_loot.Value}";
			PickupPopup.SetActive(true);
		}

		private IEnumerator StartDestroyTimer()
		{
			yield return new WaitForSeconds(1.5f);
			
			Destroy(gameObject);
		}
	}
}