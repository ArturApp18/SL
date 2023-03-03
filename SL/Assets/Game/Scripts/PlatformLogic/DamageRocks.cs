using System;
using System.Collections;
using Game.Scripts.Enemy;
using Game.Scripts.Logic;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
    public class DamageRocks : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private float _damage;
        [SerializeField] private float maxDuration;

        private bool _entered;

        public float Timer;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerStay += TriggerStay;
        }

        private void Update()
        {
            if (_entered)
            {
                Timer += Time.deltaTime;
            }
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= TriggerEnter;
            _triggerObserver.TriggerStay -= TriggerStay;
        }

        private void TriggerEnter(Collider2D obj)
        {
            obj.GetComponent<IHealth>().TakeDamage(_damage);
            _entered = true;
        }

        private void TriggerStay(Collider2D obj)
        {
            if (Timer > maxDuration)
            {
                obj.GetComponent<IHealth>().TakeDamage(_damage);
                Timer = 0;
            }
        }

    }
}
