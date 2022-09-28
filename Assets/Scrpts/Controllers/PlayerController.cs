using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Behaviours;
using Scripts.Managers;

namespace Scripts.Controllers
{

    public class PlayerController : CustomBehaviour
    {
        public bool GodMode { get; set; }
        public PlayerAnimationController PlayerAnimationController => _playerAnimationController;
        public PlayerMovementBehaviour PlayerMovementBehaviour => _playerMovementBehaviour;
        [SerializeField] private PlayerMovementBehaviour _playerMovementBehaviour;
        [SerializeField] private PlayerBlockDetectorBehaviour _playerBlockDetectorBehaviour;
        [SerializeField] private PlayerObstacleDetectorBehaviour _playerObstacleDetectorBehaviour;
        [SerializeField] private PlayerAnimationController _playerAnimationController;
        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);

            _playerMovementBehaviour.Initialize(this);
            _playerBlockDetectorBehaviour.Initialize(this);
            _playerObstacleDetectorBehaviour.Initialize(this);

        }
    }

}