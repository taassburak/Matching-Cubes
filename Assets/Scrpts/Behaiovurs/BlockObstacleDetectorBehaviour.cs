using Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Behaviours
{

    public class BlockObstacleDetectorBehaviour : MonoBehaviour
    {
        private BlockController _blockController;
        
        public void Initialize(BlockController blockController)
        {
            _blockController = blockController;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                if (!_blockController.GameManager.PlayerController.GodMode)
                {

                    List<BlockBehaviour> blockBehaviours;
                    blockBehaviours = new List<BlockBehaviour>();
                    blockBehaviours.Add(transform.GetComponent<BlockBehaviour>());
                    _blockController.GameManager.EventManager.BlockRemoved(ref blockBehaviours, false, other.GetComponent<ObstacleBehaviour>().IsBlockObstacle);
                }
            }
        }
    }

}