using System;
using System.Collections;
using Game.Scripts.Enemy;
using Game.Scripts.Logic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
    public class DamageRocks : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private float _damage;
        [SerializeField] private float maxDuration;
        [SerializeField] private float _lasthitTme;

        public bool _entered;

        public float Timer;
        public bool _hitted;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
            _triggerObserver.TriggerStay += TriggerStay;
            _lasthitTme = maxDuration + 1;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= TriggerEnter;
            _triggerObserver.TriggerExit -= TriggerExit;
            _triggerObserver.TriggerStay -= TriggerStay;
        }

        private void Update()
        {
            if (_entered)
            {
                Timer += Time.deltaTime;
            }
            _lasthitTme += Time.deltaTime;
        }

        private void TriggerEnter(Collider2D obj)
        {
            if (!_entered && _lasthitTme > maxDuration)
            {
                obj.GetComponent<IHealth>().TakeDamage(_damage);
                _entered = true;
                _hitted = true;
                _lasthitTme = 0;
            }
        }


        private void TriggerExit(Collider2D obj)
        {
            _entered = false;
        }

        private void TriggerStay(Collider2D obj)
        {
            if (Timer > maxDuration)
            {
                obj.GetComponent<IHealth>().TakeDamage(_damage);
                Timer = 0;
                _lasthitTme = 0;
            }
        }

    }
}
