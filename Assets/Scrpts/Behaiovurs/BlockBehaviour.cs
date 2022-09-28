using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Enums;
using DG.Tweening;
using Scripts.Controllers;

namespace Scripts.Behaviours
{


    public class BlockBehaviour : MonoBehaviour
    {
        public BlockColors Color => _color;
        [SerializeField] BlockColors _color;
        [SerializeField]private BlockObstacleDetectorBehaviour _blockObstacleDetectorBehaviour;
        private BlockController _blockController;
        public bool IsTaken { get; set; }

        public void Initialize(BlockController blockController)
        {
            _blockController = blockController;
            _blockObstacleDetectorBehaviour.Initialize(_blockController);
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }

        public void ChangePosition(float newPositionY, bool isRemoving)
        {
            if (isRemoving)
            {
                transform.DOMoveY(newPositionY, 0.1f).SetEase(Ease.InBack);
            }
            else
            {
                transform.DOMoveY(newPositionY, 0.1f);
            }
        }
    }
}