using Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers
{
    public class GameManager : CustomBehaviour
    {
        public UIManager UIManager => _uiManager;
        public EventManager EventManager => _eventManager;
        public PlayerController PlayerController => _playerController;
        public BlockController BlockController => _blockController;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private EventManager _eventManager;
        [SerializeField] private BlockController _blockController;
        [SerializeField] private PlayerController _playerController;
        private void Awake()
        {
            Application.targetFrameRate = 60;

            _uiManager.Initialize(this);
            _eventManager.Initialize(this);
            _blockController.Initialize(this);
            _playerController.Initialize(this);
        }
    }
}


