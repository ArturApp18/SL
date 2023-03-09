using UnityEngine;

namespace Game.Scripts.Hero
{
	public class WallDetection : MonoBehaviour
	{
		[SerializeField] private Transform _wallCheck;
		[SerializeField] private float _wallCheckDistance;
		[SerializeField] private CharacterController2D _controller;
		public bool IsWallDetected;
		private void Update()
		{
			IsWallDetected = Physics2D.Raycast(_wallCheck.position, transform.right, _wallCheckDistance, _controller.MWhatIsWall);
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawLine(_wallCheck.position, new Vector2(_wallCheck.position.x + _wallCheckDistance, _wallCheck.position.y));
		}
	}
}