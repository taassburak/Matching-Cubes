using DG.Tweening.Core.Easing;
using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostBehaviour : MonoBehaviour, IInteract
{
    private GameManager _gameManager;
    private Coroutine _currentStopSpeedBoostCo;
    public void Interact(GameManager gameManager)
    {
        _gameManager = gameManager;
        StartSpeedBoost();
    }

    private void StartSpeedBoost()
    {
        _gameManager.PlayerController.PlayerMovementBehaviour.Speed = 8f;
        if (_currentStopSpeedBoostCo == null)
        {
            StartCoroutine(StopSpeedBoostCo());
        }
    }

    private IEnumerator StopSpeedBoostCo()
    {
        yield return new WaitForSeconds(2.5f);
        _gameManager.PlayerController.PlayerMovementBehaviour.Speed = 5f;
        _currentStopSpeedBoostCo = null;
    }
}
