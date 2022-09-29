using Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Scripts.Managers;

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
            if (other.gameObject.GetComponent<IInteract>() != null)
            {
                other.GetComponent<IInteract>().Interact(_playerController.GameManager);
            }


            if (other.gameObject.CompareTag("Obstacle"))
            {
                //if (!_playerController.GodMode)
                //{
                //    _playerController.PlayerMovementBehaviour.Speed = 0f;
                //    _playerController.PlayerAnimationController.SetAnimation(0, true);
                    
                //}
                //Interface Done..
            }

            if (other.gameObject.CompareTag("SpeedBoost"))
            {
                //_playerController.GameManager.EventManager.GodModCombo();
                //_playerController.PlayerMovementBehaviour.Speed = 8f;
                //Interface Done..
            }

            if (other.gameObject.CompareTag("RampBoost"))
            {
                //InputController.IsInputDeactivated = true;

                //_currentPath =  other.gameObject.GetComponent<PathBehaviour>().Path;
                //_isPathingActive = true;

                //Interface Done..
                
            }

            //if (other.gameObject.CompareTag("SuffleGate"))
            //{
            //    //if (other.gameObject.GetComponent<SuffleGateBehaviour>().IsRandomSuffleGate)
            //    //{
            //    //    _playerController.GameManager.EventManager.BlocksShuffledRandomly();
            //    //}
            //    //else
            //    //{
            //    //    _playerController.GameManager.EventManager.BlocksShuffledByColors();
            //    //}

            //    //Interface Done..
            //}
        }

    }

}