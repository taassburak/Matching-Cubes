using Scripts.Managers;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Behaviours;
using System.Reflection.Emit;
using Sirenix.OdinInspector.Editor.Validation;
using System;

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
            _distructibleTempBlockList = new List<BlockBehaviour>();
            GameManager.EventManager.OnBlockRemoved2Instance += RemoveBlockFromCurrentBlockList;
            GameManager.EventManager.OnNewBlockCollected += AddBlockToCurrentBlockList;
            GameManager.EventManager.OnBlocksShuffled += Shuffle;
            GameManager.EventManager.OnGameStarted += RefreshBlockList;
        }

        private void OnDestroy()
        {
            GameManager.EventManager.OnBlockRemoved2Instance -= RemoveBlockFromCurrentBlockList;
            GameManager.EventManager.OnNewBlockCollected -= AddBlockToCurrentBlockList;
            GameManager.EventManager.OnBlocksShuffled -= Shuffle;
            GameManager.EventManager.OnGameStarted -= RefreshBlockList;
        }

        [Button]
        private void AddBlockToCurrentBlockList(BlockBehaviour newBlockBehaviour)
        {
            
            newBlockBehaviour.Initialize(this);
            _currentBlockList.Add(newBlockBehaviour);
            newBlockBehaviour.transform.SetParent(GameManager.PlayerController.PlayerMovementBehaviour.transform);
            newBlockBehaviour.transform.localPosition = Vector3.zero;
            newBlockBehaviour.IsTaken = true;
            UpdateBlocksSorting(false);
            GameManager.EventManager.AnimationChanged(_currentBlockList.Count, false);
        }

        [Button]
        public void RemoveBlockFromCurrentBlockList(ref List<BlockBehaviour> removedblockBehaviours, bool isMatched, bool isBlockObstacle)
        {
            if (isMatched)
            {
                Debug.Log("CONGRATS!");
                _comboCounter++;
                if (_comboCounter >= 3)
                {
                    GameManager.EventManager.GodModeCombo();
                    _comboCounter = 0;
                }
            }
            else
            {
                Debug.Log("Collide with obstacle");
                _comboCounter = 0;
            }
            
            StartCoroutine(RemoveBlockFromCurrentBlockListCo(removedblockBehaviours, isBlockObstacle));

            
        }


        private IEnumerator RemoveBlockFromCurrentBlockListCo(List<BlockBehaviour> removedblockBehaviours, bool isBlockObstacle)
        {

            for (int i = 0; i < removedblockBehaviours.Count; i++)
            {
                if (removedblockBehaviours[i] != null)
                {
                    Destroy(removedblockBehaviours[i].gameObject);
                    _currentBlockList.Remove(removedblockBehaviours[i]);
                }
            }

            float time = 0;
            if (isBlockObstacle)
            {
                time = 0.20f;
            }
            else
            {
                time = 0.05f;
            }

            yield return new WaitForSeconds(time);
            GameManager.EventManager.AnimationChanged(_currentBlockList.Count, false);
            UpdateBlocksSorting(true);
            
            removedblockBehaviours.Clear();
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

        private void UpdateBlocksSorting(bool isRemoving)
        {
            for (int i = _currentBlockList.Count - 1; i >= 0 ; i--)
            {
                
                var temp = (_currentBlockList.Count-1) - i;
                
                if (_currentBlockList[i] != null)
                {
                    _currentBlockList[i].ChangePosition(temp, isRemoving);
                }
            }
            GameManager.PlayerController.PlayerMovementBehaviour.SetCharacterHeight();
            if (_currentBlockList.Count > 0)
            {
                GameManager.EventManager.TrailChanged(true, _currentBlockList[_currentBlockList.Count - 1].transform.GetComponent<Renderer>().material.color);
            }
            else
            {
                GameManager.EventManager.TrailChanged(false, Color.white); // temp solution..
            }
            CheckAnyThreeBlocksMatched();
        }
        
        [Button]
        private void CheckAnyThreeBlocksMatched()
        {
            
            _distructibleTempBlockList.Clear();
            for (int i = 1; i < _currentBlockList.Count - 1; i++)
            {
                if (_currentBlockList[i].Color == _currentBlockList[i-1].Color && _currentBlockList[i].Color == _currentBlockList[i+1].Color)
                {
                    _distructibleTempBlockList.Add(_currentBlockList[i]);
                    _distructibleTempBlockList.Add(_currentBlockList[i - 1]);
                    _distructibleTempBlockList.Add(_currentBlockList[i + 1]);
                    GameManager.EventManager.BlockRemoved(ref _distructibleTempBlockList, true, true);
                }
            }

        }

        private void RefreshBlockList()
        {
            for (int i = 0; i < _currentBlockList.Count; i++)
            {
                Destroy(_currentBlockList[i].gameObject);
            }
            _currentBlockList.Clear();
            _comboCounter = 0;
        }

    }

}