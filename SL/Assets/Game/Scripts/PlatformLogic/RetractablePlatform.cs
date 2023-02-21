using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class RetractablePlatform : MonoBehaviour
	{

		[SerializeField] private float _speed;

		public bool MoveToStartPosition;
		public bool MoveToFinishPosition;
		public float Distance;

		public Vector3 _startPos;
		public Vector3 _finishPos;

		private void Start()
		{
			_startPos = transform.localPosition;
			_finishPos = new Vector3(_startPos.x, _startPos.y + Distance);
		}

		public void PushIn()
		{
			MoveToFinishPosition = false;
			MoveToStartPosition = true;
		}

		public void PushOut()
		{
			MoveToStartPosition = false;
			MoveToFinishPosition = true;
		}

		private void Update()
		{
			if (MoveToStartPosition && transform.localPosition != _startPos)
			{
				transform.localPosition = Vector3.Lerp(transform.localPosition, _startPos, Time.deltaTime * _speed);
			}
			else if (MoveToFinishPosition && transform.localPosition != _finishPos)
			{
				transform.localPosition = Vector3.Lerp(transform.localPosition, _finishPos, Time.deltaTime * _speed);
			}
		}
	}

}