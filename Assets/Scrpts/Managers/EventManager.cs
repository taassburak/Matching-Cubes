using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
        public event Action OnBlockRemoved;
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

        public void BlockRemoved()
        {
            OnBlockRemoved?.Invoke();
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
