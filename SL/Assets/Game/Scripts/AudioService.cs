using System;
using Game.Scripts.Enemy;
using UnityEngine;

namespace Game.Scripts
{
	public class AudioService : MonoBehaviour
	{
		[SerializeField] private AudioSource musicsIdle;
		[SerializeField] private AudioSource musicsIdle2;
		[SerializeField] private TriggerObserver trigger;

		private void Start()
		{
			trigger.TriggerEnter += TriggerEnter;
			trigger.TriggerExit += TriggerExit;
		}

		private void OnDisable()
		{
			trigger.TriggerEnter -= TriggerEnter;
			trigger.TriggerExit -= TriggerExit;
		}
		

		private void TriggerEnter(Collider2D obj)
		{
			musicsIdle.Pause();
			musicsIdle2.Play();
		}

		private void TriggerExit(Collider2D obj)
		{
			musicsIdle2.Stop();
			musicsIdle.Play();
		}
	}
}