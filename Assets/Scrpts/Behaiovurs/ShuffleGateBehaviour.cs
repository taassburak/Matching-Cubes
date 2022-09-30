using Scripts.Controllers;
using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Behaviours
{
    public class ShuffleGateBehaviour : MonoBehaviour, IInteract
    {
        [SerializeField] bool _isRamdomShuffleGate;

        public void Interact(GameManager gameManager)
        {
            if (_isRamdomShuffleGate)
            {
                gameManager.EventManager.BlocksShuffledRandomly();
            }
            else
            {
                gameManager.EventManager.BlocksShuffledByColors();
            }
        }
    }
}
