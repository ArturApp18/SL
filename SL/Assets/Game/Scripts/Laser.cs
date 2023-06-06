using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class Laser : MonoBehaviour
    {
    
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Camera _camera;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Quaternion _rotation;
        [SerializeField] private GameObject _endVFX;
        [SerializeField] private GameObject _startVFX;


        private List<ParticleSystem> _particleSystems = new List<ParticleSystem>();
        private void Start()
        {
            FillLists(_startVFX);
            FillLists(_endVFX);
            DisableLaser();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                EnableLaser();
            }

            if (Input.GetButton("Fire1"))
            {
                UpdateLaser();
            }

            if (Input.GetButtonUp("Fire1"))
            {
                DisableLaser();
            }
            
            RotateToMouse();
        }

        private void UpdateLaser()
        {
            var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _startVFX.transform.position = _firePoint.position;
            
            _lineRenderer.SetPosition(0, _firePoint.position);
            
            _lineRenderer.SetPosition(1, mousePos);

            Vector2 direction = mousePos - transform.position;
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction.normalized, direction.magnitude);

            if (hit2D)
            {
                _lineRenderer.SetPosition(1, hit2D.point);
            }

            _endVFX.transform.position = _lineRenderer.GetPosition(1);
        }

        private void EnableLaser()
        {
            _lineRenderer.enabled = true;

            for (int i = 0; i < _particleSystems.Count; i++)
            {
                _particleSystems[i].Play();
            }
        }

        private void DisableLaser()
        {
            _lineRenderer.enabled = false;
            
            for (int i = 0; i < _particleSystems.Count; i++)
            {
                _particleSystems[i].Stop();
            }
        }

        private void RotateToMouse()
        {
            Vector2 direction = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rotation.eulerAngles = new Vector3(0, 0, angle);
            transform.rotation = _rotation;
        }

        private void FillLists(GameObject vfx)
        {
            for (int i = 0; i < vfx.transform.childCount; i++)
            {
                var particle = vfx.transform.GetChild(i).GetComponent<ParticleSystem>();
                if (particle != null)
                {
                    _particleSystems.Add(particle);
                }
            }
        }
    }
}
