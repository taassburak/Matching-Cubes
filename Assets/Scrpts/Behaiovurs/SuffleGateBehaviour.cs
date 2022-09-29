using Scripts.Controllers;
using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Behaviours
{
    public class SuffleGateBehaviour : MonoBehaviour, IInteract
    {
        [SerializeField] bool _isRamdomSuffleGate;

        public void Interact(GameManager gameManager)
        {
            if (_isRamdomSuffleGate)
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
