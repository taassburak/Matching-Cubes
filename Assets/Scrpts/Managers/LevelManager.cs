using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers
{


    public class LevelManager : CustomBehaviour
    {
        [SerializeField] GameObject _levelPrefab; // creating a looping level system for one level;
        private GameObject _currentLevel;
        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
            GameManager.EventManager.OnGameStarted += StartGame;
        }

        private void OnDestroy()
        {
            GameManager.EventManager.OnGameStarted += StartGame;
        }

        private void StartGame()
        {
            ClearLevel();

            _currentLevel = Instantiate(_levelPrefab);
            _currentLevel.transform.position = Vector3.zero;
        }

        public void ContinueToNextLevel() // For button
        {
            GameManager.EventManager.GameStarted();
        }

        public void RetryCurrentLevel() // For button
        {
            GameManager.EventManager.GameStarted();
        }

        private void ClearLevel()
        {
            if (_currentLevel != null)
            {
                Destroy(_currentLevel.gameObject);
            }
        }

    }

}