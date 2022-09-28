using Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Behaviours
{

    public class PlayerObstacleDetectorBehaviour : MonoBehaviour
    {
        private PlayerController _playerController;
        private Coroutine _countdownSpeedBoostCo;
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
                _playerController.GodMode = true;
                _playerController.PlayerMovementBehaviour.Speed = 12f;
                if (_countdownSpeedBoostCo == null)
                {
                    StartCoroutine(CountdownForSpeedBoostCo());
                }
            }
        }

        private IEnumerator CountdownForSpeedBoostCo()
        {
            yield return new WaitForSeconds(4f);
            _playerController.PlayerMovementBehaviour.Speed = 5f;
            _playerController.GodMode = false;
            _countdownSpeedBoostCo = null;
        }

        

    }

}