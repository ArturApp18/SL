using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float speed;

        private void Start()
        {
            _rigidbody.velocity = Vector2.left * speed;
        
        }
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject, 5);
            }
        }
    }
}
