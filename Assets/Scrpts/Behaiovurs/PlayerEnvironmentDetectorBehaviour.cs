using Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Scripts.Behaviours
{

    public class PlayerEnvironmentDetectorBehaviour : MonoBehaviour
    {
        private PlayerController _playerController;
        private Coroutine _countdownSpeedBoostCo;

        private float _pathDistance;
        private CinemachineSmoothPath _currentPath;
        private bool _isPathingActive;
        public void Initialize(PlayerController playerController)
        {
            _playerController = playerController;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                if (!_playerController.GodMode)
                {
                    _playerController.PlayerMovementBehaviour.Speed = 0f;
                    _playerController.PlayerAnimationController.SetAnimation(0, true);
                    Debug.Log("Game over");
                }
            }

            if (other.gameObject.CompareTag("SpeedBoost"))
            {
                //_playerController.GameManager.EventManager.GodModCombo();
                _playerController.PlayerMovementBehaviour.Speed = 8f;
            }

            if (other.gameObject.CompareTag("RampBoost"))
            {
                InputController.IsInputDeactivated = true;

                _currentPath =  other.gameObject.GetComponent<PathBehaviour>().Path;
                _isPathingActive = true;
                
            }
        }

        private void Update()
        {
            if (_isPathingActive)
            {

                _playerController.PlayerMovementBehaviour.transform.position = new Vector3(_playerController.PlayerMovementBehaviour.transform.position.x, _currentPath.EvaluatePositionAtUnit(_pathDistance, CinemachinePathBase.PositionUnits.Distance).y, _currentPath.EvaluatePositionAtUnit(_pathDistance, CinemachinePathBase.PositionUnits.Distance).z);
                //_playerController.PlayerMovementBehaviour.transform.position = _currentPath.EvaluatePositionAtUnit(_pathDistance, CinemachinePathBase.PositionUnits.Distance);
                

                _pathDistance += 10f * Time.deltaTime;
                if (_pathDistance >= _currentPath.PathLength)
                {
                    InputController.IsInputDeactivated = false;
                    _isPathingActive = false;
                    _pathDistance = 0;
                    var temp = _playerController.PlayerMovementBehaviour.transform.position;
                    temp.y = 0;
                    _playerController.PlayerMovementBehaviour.transform.position = temp;
                }
                
            }
        }
    }

}