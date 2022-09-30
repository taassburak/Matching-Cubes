using Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Behaviours
{

    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private PlayerController _playerController;
        public void Initialize(PlayerController playerController)
        {
            _playerController = playerController;
            _playerController.GameManager.EventManager.OnChangeAnimation += SetAnimation;
        }

        private void OnDestroy()
        {
            _playerController.GameManager.EventManager.OnChangeAnimation -= SetAnimation;
        }

        public void SetAnimation(int stackCount, bool isDead)
        {
            if (isDead)
            {
                _animator.SetBool("dead", true);
                _animator.SetBool("running", false);
                _animator.SetBool("surfing", false);
                return;
            }
            if (stackCount > 0)
            {
                _animator.SetBool("surfing", true);
                _animator.SetBool("running", false);
                _animator.SetBool("dead", false);
            }
            else
            {
                _animator.SetBool("dead", false);
                _animator.SetBool("running", true);
                _animator.SetBool("surfing", false);
            }
           

            
        }

        

    }

}