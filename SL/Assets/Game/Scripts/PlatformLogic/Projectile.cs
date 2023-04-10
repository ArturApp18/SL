using System.Security.Cryptography;
using Game.Scripts.Enemy;
using Game.Scripts.Logic;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float speed;
        [SerializeField] private float Damage;
        [SerializeField] private RotateToHero _toHero;

        private void Start()
        {
            if (_toHero.IsFacingRight)
            {
                _rigidbody.velocity = transform.right * speed;
            }
            else if (!_toHero.IsFacingRight)
            {
                _rigidbody.velocity = -transform.right * speed;
            }
            Destroy(gameObject, 3);
        }
        private void OnCollisionEnter2D(Collision2D col)
        {
            Destroy(gameObject);
    
            col.transform.GetComponent<IHealth>().TakeDamage(Damage);
        }
    }
}
