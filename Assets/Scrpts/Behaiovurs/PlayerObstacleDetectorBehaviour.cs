using Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Behaviours
{

    public class PlayerObstacleDetectorBehaviour : MonoBehaviour
    {
        private PlayerController _playerController;
        public void Initialize(PlayerController playerController)
        {
            _playerController = playerController;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Game over");
            }
        }

    }

}