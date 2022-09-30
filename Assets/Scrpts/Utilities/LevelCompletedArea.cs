using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedArea : MonoBehaviour, IInteract
{
    public void Interact(GameManager gameManager)
    {
        gameManager.PlayerController.PlayerMovementBehaviour.Speed = 0;
        gameManager.UIManager.FinishPanel.ShowPanel();
        gameManager.EventManager.LevelCompleted();
        Debug.Log("LevelCompleted");
    }
}
