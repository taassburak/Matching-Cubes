using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Controllers
{

    public class TrailController : CustomBehaviour
    {
        [SerializeField] TrailRenderer _trailRenderer;
        private Color _oldColor;
        private TrailRenderer _currnetTrail = null;

        private List<TrailRenderer> _instantiatedTrails;

        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);

            _instantiatedTrails = new List<TrailRenderer>();
            GameManager.EventManager.OnTrailChanged += SetTrail;
            GameManager.EventManager.OnGameStarted += ResetTrails;

        }

        private void OnDestroy()
        {
            GameManager.EventManager.OnTrailChanged -= SetTrail;
            GameManager.EventManager.OnGameStarted -= ResetTrails;
        }

        private void ResetTrails()
        {
            _currnetTrail = null;
            foreach (var trail in _instantiatedTrails)
            {
                Destroy(trail.gameObject);
            }
        }

        private void SetTrail(bool isActive, Color color)
        {
            if (isActive)
            {
                _trailRenderer.SetActive(true);
               

                
                if (_currnetTrail != null)
                {
                    _instantiatedTrails = new List<TrailRenderer>();
                    if (_oldColor != color)
                    {
                        _currnetTrail.transform.SetParent(null);
                        var newLine = Instantiate(_trailRenderer, GameManager.PlayerController.PlayerMovementBehaviour.transform.position + new Vector3(0,-0.48f,0), Quaternion.Euler(90,90,0), GameManager.PlayerController.PlayerMovementBehaviour.transform);
                        newLine.startColor = color;
                        newLine.endColor = color;
                        _oldColor = color;
                        _currnetTrail = newLine;
                        _instantiatedTrails.Add(_currnetTrail);
                    }
                }
                else
                {
                    var newLine = Instantiate(_trailRenderer, GameManager.PlayerController.PlayerMovementBehaviour.transform.position + new Vector3(0, -0.48f, 0), Quaternion.Euler(90, 90, 0), GameManager.PlayerController.PlayerMovementBehaviour.transform);
                    newLine.startColor = color;
                    newLine.endColor = color;
                    _currnetTrail = newLine;
                    _instantiatedTrails.Add(_currnetTrail);
                }

            }
            else
            {
                _currnetTrail.transform.SetParent(null);
            }
        }


    }

}