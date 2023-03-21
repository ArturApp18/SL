using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class Reticle : MonoBehaviour
	{
		[SerializeField] private AnimationCurve _animationCurve;
		[SerializeField] private float _selectAnimTime;
		[SerializeField] private float _deselectAnim;
		[SerializeField] private float _amplitude;
		[SerializeField] private float _clamp;
		[SerializeField] private List<GameObject> points = new List<GameObject>();
		[SerializeField] private SlideTouch _touch;

		private List<Vector3> pointStartPos = new List<Vector3>();

		private GameObject _selectedObject;

		private void Awake()
		{
			foreach (GameObject point in points)
			{
				pointStartPos.Add(point.transform.localPosition);
			}
		}

		public void Selected(GameObject selected)
		{
			gameObject.SetActive(true);
			transform.position = selected.transform.position;
			_selectedObject = selected;

			Vector3 touchPos = _touch.GetFirstTouch().position;
			Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(touchPos);
			Vector3 touchRelativePos = touchWorldPos - transform.position;

			for (int i = 0; i < points.Count; i++)
			{
				float distance = Vector2.Distance(transform.position, touchWorldPos);
				Point point = points[i].GetComponent<Point>();
				HandlePoint(touchRelativePos, distance, i, point);
			}
		}

		private void HandlePoint(Vector3 touchRelativePos, float distance, int i, Point point)
		{
			int dir = 0;
			if (point.flip)
			{
				dir = -1;
			}
			else
			{
				dir = 1;
			}

			Vector3 startPos = pointStartPos[i];

			float magnitude = ( _amplitude * distance ) / distance;
			float pointIdentity = (startPos.x * magnitude) * dir;

			Vector3 targetPos = ( touchRelativePos * pointIdentity ) * dir;
			
			float lerpTime = ( _selectAnimTime / pointIdentity ) * Time.deltaTime;
			Vector3 lerpPos = Vector3.Lerp(point.transform.localPosition, targetPos, lerpTime);
		}

		public void Deselect()
		{
			_selectedObject = null;
			gameObject.SetActive(false);
		}
	}
}