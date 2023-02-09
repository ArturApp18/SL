using UnityEngine;
using UnityEngine.U2D;

public class GrowingPlatform : MonoBehaviour
{
	[SerializeField] private SpriteShapeController _spriteShape;
	[SerializeField]
	private Vector3 startPosition;
	[SerializeField]
	private Vector3 endPosition;
	[SerializeField]
	private float time;
	[SerializeField]
	private int position;

	private void Start()
	{
		_spriteShape.spline.SetPosition(position, Vector3.Lerp(startPosition, endPosition, time));
	}
}
