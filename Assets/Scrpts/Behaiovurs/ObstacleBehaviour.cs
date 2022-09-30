using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ObstacleType
{
    Block,
    Lava
}

public class ObstacleBehaviour : MonoBehaviour, IInteract
{
    public bool IsBlockObstacle => _isBlockObstacle;
    [SerializeField] private bool _isBlockObstacle;
    [SerializeField] private ObstacleType _obstacleType;
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
