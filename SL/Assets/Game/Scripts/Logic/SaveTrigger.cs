using System;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Game.Scripts.Logic
{
	public class SaveTrigger : MonoBehaviour
	{
		private ISaveLoadService _saveLoadService;

		public BoxCollider2D Collider2D;

		public void Construct(ISaveLoadService saveLoadService)
		{
			_saveLoadService = saveLoadService;
		}
		private void Awake()
		{
			_saveLoadService = AllServices.Container.Single<ISaveLoadService>();
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			_saveLoadService.SaveProgress();
			Debug.Log("Progress Save");
			gameObject.SetActive(false);
		}

		private void OnDrawGizmos()
		{
			if(!Collider2D)
				return;
			
			Vector2 offset = Collider2D.offset;
			Gizmos.color = new Color32(30, 200, 30, 130);
			Gizmos.DrawCube(transform.position + new Vector3(offset.x, offset.y, 0)  ,Collider2D.size);
		}
	}
}