using System;
using UnityEngine;

namespace Game.Scripts.Infrastructure
{
	public class GameRunner : MonoBehaviour
	{
		public GameBootstrapper BootstrapperPrefab;
		private void Awake()
		{
			GameBootstrapper bootstrapper = FindObjectOfType<GameBootstrapper>();

			if(bootstrapper == null)
				Instantiate(BootstrapperPrefab);
		}
	}
}