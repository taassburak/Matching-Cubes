using Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

namespace Scripts.Behaviours
{
    public class PlayerMovementBehaviour : MonoBehaviour
    {
       
        public float Speed { get; set; }
        [SerializeField] GameObject _characterObject;
        private PlayerController _playerController;

        #region "Pathing variables"
        public bool IsPathingState { get; set; }
        public CinemachineSmoothPath CurrentPath { get; set; }
        private float _pathDistance;
        #endregion

        public void Initialize(PlayerController playerController)
        {
            _playerController = playerController;
            InputController.OnDrag += SetCharacterHorizontalPosition;
            Speed = 5f;
        }
        private void OnDestroy()
        {
            InputController.OnDrag -= SetCharacterHorizontalPosition;
        }

        void Update()
        {
            if (!InputController.IsInputDeactivated)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * Speed);
            }

            if (IsPathingState)
            {
                transform.position = new Vector3(_playerController.PlayerMovementBehaviour.transform.position.x, CurrentPath.EvaluatePositionAtUnit(_pathDistance, CinemachinePathBase.PositionUnits.Distance).y, CurrentPath.EvaluatePositionAtUnit(_pathDistance, CinemachinePathBase.PositionUnits.Distance).z);
                //_playerController.PlayerMovementBehaviour.transform.position = _currentPath.EvaluatePositionAtUnit(_pathDistance, CinemachinePathBase.PositionUnits.Distance);


                _pathDistance += 10f * Time.deltaTime;
                if (_pathDistance >= CurrentPath.PathLength)
                {
                    InputController.IsInputDeactivated = false;
                    IsPathingState = false;
                    _pathDistance = 0;
                    var temp = _playerController.PlayerMovementBehaviour.transform.position;
                    temp.y = 0;
                    _playerController.PlayerMovementBehaviour.transform.position = temp;
                }
            }
        }

        public void SetCharacterHeight()
        {
            if (!InputController.IsInputDeactivated)
            {

                transform.DOKill();
                var temp = _characterObject.transform.position;
                temp.y = _playerController.GameManager.BlockController.CurrentBlockList.Count;
                //_characterObject.transform.position = temp;
                _characterObject.transform.DOMoveY(temp.y, 0.25f).SetEase(Ease.OutBack);
            }
        }

        public void SetCharacterHorizontalPosition(Vector2 vector2)
        {
            if (!InputController.IsInputDeactivated)
            {
                var temp = transform.position;
                temp.x = vector2.x;
                transform.position += new Vector3(temp.x, temp.y, 0);
            }

        }
    }
}