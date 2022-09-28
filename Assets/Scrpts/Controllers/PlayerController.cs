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
        [SerializeField] private PlayerEnvironmentDetectorBehaviour _playerEnvironmentDetectorBehaviour;
        [SerializeField] private PlayerAnimationController _playerAnimationController;

        private Coroutine _godModeCoroutine;
        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);

            _playerMovementBehaviour.Initialize(this);
            _playerBlockDetectorBehaviour.Initialize(this);
            _playerEnvironmentDetectorBehaviour.Initialize(this);
            GameManager.EventManager.OnGodModCombo += StartingGodMode;
        }

        private void OnDestroy()
        {
            GameManager.EventManager.OnGodModCombo -= StartingGodMode;
        }

        public void StartingGodMode()
        {
            GodMode = true;
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
        }
    }

}