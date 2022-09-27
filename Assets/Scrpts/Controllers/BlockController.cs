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
        [SerializeField] private BlockBehaviour _blockPrefab;
        private List<BlockBehaviour> _distructibleTempBlockList;
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

        [Button]
        private void AddBlockToCurrentBlockList(BlockBehaviour newBlockBehaviour)
        {
            var newBlock = Instantiate(_blockPrefab, _currentBlockList[_currentBlockList.Count - 1].transform.position, Quaternion.identity);
            //_currentBlockList.Add(newBlockBehaviour);
            _currentBlockList.Add(newBlock);
            UpdateBlocksSorting(false);
            //CheckAnyThreeBlocksMatched();
        }

        [Button]
        private void RemoveBlockFromCurrentBlockList()
        {
            //Destroy(_currentBlockList[_currentBlockList.Count - 2].gameObject);
            //_currentBlockList.Remove(_currentBlockList[_currentBlockList.Count - 2]);
            //CheckAnyThreeBlocksMatched();
            for (int i = 0; i < _distructibleTempBlockList.Count; i++)
            {
                if (_distructibleTempBlockList[i] != null)
                {
                    Destroy(_distructibleTempBlockList[i].gameObject);
                    _currentBlockList.Remove(_distructibleTempBlockList[i]);
                }
            }
            UpdateBlocksSorting(true);
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

            UpdateBlocksSorting(false);
        }

        private void UpdateBlocksSorting(bool isRemoving)
        {
            for (int i = _currentBlockList.Count - 1; i >= 0 ; i--)
            {
                var temp = _currentBlockList[i].transform.position;
                temp.y = (_currentBlockList.Count-1) - i;
                //_currentBlockList[i].transform.position = temp;
                _currentBlockList[i].ChangePosition(temp, isRemoving);
            }
            CheckAnyThreeBlocksMatched();
        }
        
        [Button]
        private void CheckAnyThreeBlocksMatched()
        {
            _distructibleTempBlockList = new List<BlockBehaviour>();
            for (int i = 1; i < _currentBlockList.Count - 1; i++)
            {
                if (_currentBlockList[i].Color == _currentBlockList[i-1].Color && _currentBlockList[i].Color == _currentBlockList[i+1].Color)
                {
                    _distructibleTempBlockList.Add(_currentBlockList[i]);
                    _distructibleTempBlockList.Add(_currentBlockList[i - 1]);
                    _distructibleTempBlockList.Add(_currentBlockList[i + 1]);
                    GameManager.EventManager.BlockRemoved();
                }
            }

        }

    }

}