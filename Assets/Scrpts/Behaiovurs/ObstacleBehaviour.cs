using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour, IInteract
{
    public void Interact(GameManager gameManager)
    {
        if (!gameManager.PlayerController.GodMode)
        {
            gameManager.PlayerController.PlayerMovementBehaviour.Speed = 0;
            gameManager.EventManager.AnimationChanged(0, true);
            gameManager.UIManager.FinishPanel.ShowPanel();
            gameManager.EventManager.LevelFailed();
            Debug.Log("Game over");
        }
    }
}
