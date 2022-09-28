using Scripts.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Behaviours
{

    public class PlayerBlockDetectorBehaviour : MonoBehaviour
    {
        private PlayerController _playerController;
        public void Initialize(PlayerController playerController)
        {
            _playerController = playerController;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Block") && !other.gameObject.GetComponent<BlockBehaviour>().IsTaken)
            {
                _playerController.GameManager.EventManager.NewBlockCollected(other.GetComponent<BlockBehaviour>());
            }
        }
    }
}
