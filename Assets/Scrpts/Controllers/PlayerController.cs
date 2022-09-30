using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Behaviours;
using Scripts.Managers;
using Sirenix.OdinInspector;

namespace Scripts.Controllers
{

    public class PlayerController : CustomBehaviour
    {
        public bool GodMode { get; set; }
        public PlayerAnimationController PlayerAnimationController => _playerAnimationController;
        public PlayerMovementBehaviour PlayerMovementBehaviour => _playerMovementBehaviour;
        [SerializeField] private PlayerMovementBehaviour _playerMovementBehaviour;
        [SerializeField] private PlayerBlockDetectorBehaviour _playerBlockDetectorBehaviour;
        [SerializeField] private PlayerEnvironmentDetectorBehaviour _playerEnvironmentDetectorBehaviour;
        [SerializeField] private PlayerAnimationController _playerAnimationController;

        private Coroutine _godModeCoroutine;
        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);

            _playerMovementBehaviour.Initialize(this);
            _playerBlockDetectorBehaviour.Initialize(this);
            _playerEnvironmentDetectorBehaviour.Initialize(this);
            _playerAnimationController.Initialize(this);
            GameManager.EventManager.OnGodModCombo += StartingGodMode;
            GameManager.EventManager.OnGameStarted += RefreshCharacterPosition; 
        }

        private void OnDestroy()
        {
            GameManager.EventManager.OnGodModCombo -= StartingGodMode;
            GameManager.EventManager.OnGameStarted -= RefreshCharacterPosition;
        }

        [Button]
        public void StartingGodMode()
        {
            GodMode = true;
            GameManager.UIManager.InGamePanel.ShowPanel();
            GameManager.UIManager.InGamePanel.SetFeverModeText(true);
            StopGodMode();
        }

        public void StopGodMode()
        {
            if (_godModeCoroutine == null)
            {
                _godModeCoroutine = StartCoroutine(GodModeCountDownCo());
            }
            
        }

        private IEnumerator GodModeCountDownCo()
        {
            yield return new WaitForSeconds(3.5f);
            GodMode = false;
            _godModeCoroutine = null;
            GameManager.UIManager.InGamePanel.SetFeverModeText(false);
            GameManager.UIManager.InGamePanel.HidePanel();
        }

        private void RefreshCharacterPosition()
        {
            PlayerMovementBehaviour.transform.position = Vector3.zero;
            GodMode = false;
            PlayerMovementBehaviour.Speed = 5f;
            _playerAnimationController.SetAnimation(0, false);
            _playerMovementBehaviour.SetCharacterHeight();
        }
    }

}