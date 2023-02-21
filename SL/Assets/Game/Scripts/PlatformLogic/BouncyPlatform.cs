using System;
using Game.Scripts.Enemy;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
    public class BouncyPlatform : MonoBehaviour
    {
        [SerializeField] private CollisionObserver _collisionObserver;
        [SerializeField] private float _bounce;

        private void Start()
        {
            _collisionObserver.CollisionEnter += CollisionEnter;
        }

        private void OnDisable()
        {
            _collisionObserver.CollisionEnter -= CollisionEnter;
        }

        private void CollisionEnter(Collision2D obj)
        {
            obj.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _bounce, ForceMode2D.Impulse);
        }
    }
}
