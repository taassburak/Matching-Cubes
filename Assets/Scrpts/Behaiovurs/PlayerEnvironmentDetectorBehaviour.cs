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
        }

    }

}