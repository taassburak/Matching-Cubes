using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Scripts.Behaviours;

namespace Scripts.Managers
{
    public class EventManager : CustomBehaviour
    {
        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
        }

        public event Action OnGameStarted;
        public event Action<bool> OnLevelFinished;
        public event Action<BlockBehaviour> OnNewBlockCollected;
        public event Action<List<BlockBehaviour>, bool> OnBlockRemoved;
        public event Action<bool> OnBlocksShuffled;

        public void GameStarted()
        {
            OnGameStarted?.Invoke();
        }
        
        public void LevelCompleted()
        {
            OnLevelFinished?.Invoke(true);
        }

        public void LevelFailed()
        {
            OnLevelFinished?.Invoke(false);
        }

        public void NewBlockCollected(BlockBehaviour blockBehaviour)
        {
            OnNewBlockCollected?.Invoke(blockBehaviour);
        }

        public void BlockRemoved(List<BlockBehaviour> blockBehaviours, bool isMatched)
        {
            OnBlockRemoved?.Invoke(blockBehaviours, isMatched);
        }


        public void BlocksShuffledRandomly()
        {
            OnBlocksShuffled?.Invoke(true);
        }

        public void BlocksShuffledByColors()
        {
            OnBlocksShuffled?.Invoke(false);
        }

    }
}
