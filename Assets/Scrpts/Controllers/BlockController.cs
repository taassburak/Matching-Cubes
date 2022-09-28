using Scripts.Managers;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Behaviours;
using System.Reflection.Emit;
using Sirenix.OdinInspector.Editor.Validation;

namespace Scripts.Controllers
{

    public class BlockController : CustomBehaviour
    {
        public List<BlockBehaviour> CurrentBlockList => _currentBlockList;
        [SerializeField]private List<BlockBehaviour> _currentBlockList;
        [SerializeField] private BlockBehaviour _blockPrefab;
        private List<BlockBehaviour> _distructibleTempBlockList;

        private int _comboCounter;

        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
            _currentBlockList = new List<BlockBehaviour>();
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
            //var newBlock = Instantiate(_blockPrefab, _currentBlockList[_currentBlockList.Count - 1].transform.position, Quaternion.identity);
            newBlockBehaviour.Initialize(this);
            _currentBlockList.Add(newBlockBehaviour);
            newBlockBehaviour.transform.SetParent(GameManager.PlayerController.PlayerMovementBehaviour.transform);
            newBlockBehaviour.transform.localPosition = Vector3.zero;
            newBlockBehaviour.IsTaken = true;
            //_currentBlockList.Add(newBlock);
            UpdateBlocksSorting(false);
            GameManager.PlayerController.PlayerMovementBehaviour.SetCharacterHeight();
            //CheckAnyThreeBlocksMatched();
            GameManager.PlayerController.PlayerAnimationController.SetAnimation(_currentBlockList.Count,false);
        }

        [Button]
        public void RemoveBlockFromCurrentBlockList(List<BlockBehaviour> blockBehaviours, bool isMatched)
        {
            if (isMatched)
            {
                Debug.Log("CONGRATS!");
                _comboCounter++;
                if (_comboCounter >= 3)
                {
                    GameManager.EventManager.GodModCombo();
                    _comboCounter = 0;
                }
            }
            else
            {
                Debug.Log("Collide with obstacle");
                _comboCounter = 0;
            }
            StartCoroutine(RemoveBlockFromCurrentBlockListCo(blockBehaviours));
            
        }

        private IEnumerator RemoveBlockFromCurrentBlockListCo(List<BlockBehaviour> blockBehaviours)
        {
            yield return new WaitForSeconds(0.25f);

            for (int i = 0; i < blockBehaviours.Count; i++)
            {
                if (blockBehaviours[i] != null)
                {
                    Destroy(blockBehaviours[i].gameObject);
                    _currentBlockList.Remove(blockBehaviours[i]);
                }
            }
            GameManager.PlayerController.PlayerAnimationController.SetAnimation(_currentBlockList.Count, false);
            yield return new WaitForSeconds(0.1f);
            UpdateBlocksSorting(true);
            GameManager.PlayerController.PlayerMovementBehaviour.SetCharacterHeight();
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
                int currentIndex = -1;
                for (int i = 0; i < 3; i++)
                {
                    
                    for (int j = 0; j < _currentBlockList.Count; j++)
                    {
                        if (_currentBlockList[j].Color == (Enums.BlockColors)i)
                        {
                            currentIndex++;
                            var temp = _currentBlockList[j];
                            _currentBlockList[j] = _currentBlockList[currentIndex];
                            _currentBlockList[currentIndex] = temp;
                        }
                    }

                }
                


            }

            UpdateBlocksSorting(false);
        }

        [Button]
        public void a()
        {
            _currentBlockList[0] = _currentBlockList[1];
        }

        private void UpdateBlocksSorting(bool isRemoving)
        {
            for (int i = _currentBlockList.Count - 1; i >= 0 ; i--)
            {
                //var temp = _currentBlockList[i].transform.position;
                var temp = (_currentBlockList.Count-1) - i;
                //_currentBlockList[i].transform.position = temp;
                if (_currentBlockList[i] != null)
                {
                    _currentBlockList[i].ChangePosition(temp, isRemoving);
                }
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
                    GameManager.EventManager.BlockRemoved(_distructibleTempBlockList, true);
                }
            }

        }

    }

}