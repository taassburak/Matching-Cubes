using Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Behaviours
{

    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void SetAnimation(int stackCount)
        {
            if (stackCount > 0)
            {
                _animator.SetBool("surfing", true);
                _animator.SetBool("running", false);
            }
            else
            {
                _animator.SetBool("running", true);
                _animator.SetBool("surfing", false);
            }
        }

    }

}