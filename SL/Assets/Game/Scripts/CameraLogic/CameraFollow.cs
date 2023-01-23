using UnityEngine;

namespace Game.Scripts.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _following;
        [SerializeField] private float _distance;
        [SerializeField] private float _offsetY;

        private void LateUpdate()
        {
            if (_following == null)
                return;

            Vector3 position = new Vector3(0, 0, -_distance) + FollowingPoint();

            transform.position = position;
        }

        public void Follow(GameObject following) =>
            _following = following.transform;

        private Vector3 FollowingPoint()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += _offsetY;
            
            return followingPosition;
        }
    }
}