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

        public delegate void OnBlockRemoved2<T>(ref T item,bool isMatched, bool isBlockObstacle);
        public OnBlockRemoved2<List<BlockBehaviour>> OnBlockRemoved2Instance;

        public event Action OnGameStarted;
        public event Action<bool> OnLevelFinished;
        public event Action<BlockBehaviour> OnNewBlockCollected;
        public event Action<List<BlockBehaviour>, bool> OnBlockRemoved;
        public event Action<bool> OnBlocksShuffled;
        public event Action OnGodModCombo;
        public event Action<int, bool> OnChangeAnimation;
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

        public void BlockRemoved(ref List<BlockBehaviour> blockBehaviours, bool isMatched, bool isBlockObstacle)
        {
            //OnBlockRemoved?.Invoke(ref blockBehaviours, isMatched);

            OnBlockRemoved2Instance?.Invoke(ref blockBehaviours, isMatched, isBlockObstacle);
        }


        public void BlocksShuffledRandomly()
        {
            OnBlocksShuffled?.Invoke(true);
        }

        public void BlocksShuffledByColors()
        {
            OnBlocksShuffled?.Invoke(false);
        }

        public void GodModeCombo()
        {
            OnGodModCombo?.Invoke();
        }

        public void AnimationChanged(int count, bool isDead)
        {
            OnChangeAnimation?.Invoke(count, isDead);
        }
    }
}
