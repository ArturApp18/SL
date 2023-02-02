using System;
using Game.Scripts.Data;
using Game.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Windows
{
	public abstract class WindowBase : MonoBehaviour
	{
		public Button CloseButton;

		protected IPersistentProgressService ProgressService;
		protected PlayerProgress Progress => ProgressService.Progress;

		public void Construct(IPersistentProgressService progressService) =>
			ProgressService = progressService;

		private void Awake() =>
			OnAwake();

		private void Start()
		{
			Initialize();
			SubscribeUpdates();
		}

		private void OnDestroy() =>
			CleanUp();

		protected virtual void OnAwake() =>
			CloseButton.onClick.AddListener(() => Destroy(gameObject));

		protected virtual void Initialize()
		{
			
		}

		protected virtual void SubscribeUpdates()
		{
			
		}

		protected virtual void CleanUp()
		{
			
		}
	}
}