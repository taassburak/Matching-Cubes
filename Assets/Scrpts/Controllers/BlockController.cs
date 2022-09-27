using Scripts.Managers;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Controllers
{

    public class BlockController : CustomBehaviour
    {
        public List<BlockBehaviour> CurrentBlockList => _currentBlockList;
        [SerializeField]private List<BlockBehaviour> _currentBlockList;

        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
            //_currentBlockList = new List<BlockBehaviour>();

            GameManager.EventManager.OnBlockRemoved += RemoveBlockFromCurrentBlockList;
            GameManager.EventManager.OnNewBlockCollected += AddBlockToCurrentBlockList;
            GameManager.EventManager.OnBlocksShuffled += Shuffle;
        }

        private void OnDestroy()
        {
            GameManager.EventManager.OnBlockRemoved -= RemoveBlockFromCurrentBlockList;
            GameManager.EventManager.OnNewBlockCollected -= AddBlockToCurrentBlockList;
            GameManager.EventManager.OnBlocksShuffled -= Shuffle;
        }


        private void AddBlockToCurrentBlockList(BlockBehaviour newBlockBehaviour)
        {
            _currentBlockList.Add(newBlockBehaviour);
            UpdateBlocksSorting();
        }

        [Button]
        private void RemoveBlockFromCurrentBlockList(BlockBehaviour[] removedBlockBehaviours)
        {

            for (int i = 0; i < removedBlockBehaviours.Length; i++)
            {
                _currentBlockList.Remove(removedBlockBehaviours[i]);
            }
            UpdateBlocksSorting();
        }

        [Button]
        private void Shuffle(bool isRandomly)
        {
            if (isRandomly)
            {
                for (int i = _currentBlockList.Count - 1; i > 0; i--)
                {
                    int rnd = UnityEngine.Random.Range(0, i);
                    var temp = _currentBlockList[i];
                    _currentBlockList[i] = _currentBlockList[rnd];
                    _currentBlockList[rnd] = temp;
                }
            }
            else
            {
                //to DO: sort By Colors;
            }

            UpdateBlocksSorting();
        }

        private void UpdateBlocksSorting()
        {
            for (int i = _currentBlockList.Count - 1; i >= 0 ; i--)
            {
                var temp = _currentBlockList[i].transform.position;
                temp.y = (_currentBlockList.Count-1) - i;
                _currentBlockList[i].transform.position = temp;
            }
        }



    }

}