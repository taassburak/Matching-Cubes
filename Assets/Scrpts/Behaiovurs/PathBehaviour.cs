using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Scripts.Managers;
using System;
using Scripts.Controllers;

namespace Scripts.Behaviours
{

    public class PathBehaviour : MonoBehaviour, IInteract
    {
        public CinemachineSmoothPath Path => _path;
        [SerializeField] private CinemachineSmoothPath _path;

        public void Interact(GameManager gameManager)
        {
            gameManager.PlayerController.PlayerMovementBehaviour.CurrentPath = _path;
            gameManager.PlayerController.PlayerMovementBehaviour.IsPathingState = true;
            gameManager.EventManager.TrailChanged(false, Color.black);
        }


    }

}