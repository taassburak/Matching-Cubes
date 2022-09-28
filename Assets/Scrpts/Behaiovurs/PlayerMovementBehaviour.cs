using Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Scripts.Behaviours
{
    public class PlayerMovementBehaviour : MonoBehaviour
    {
        [SerializeField] GameObject _characterObject;
        private PlayerController _playerController;
        public void Initialize(PlayerController playerController)
        {
            _playerController = playerController;
            InputController.OnDrag += SetCharacterVerticalPosition;
        }
        private void OnDestroy()
        {
            InputController.OnDrag -= SetCharacterVerticalPosition;
        }

        void Update()
        {
           transform.Translate(Vector3.forward * Time.deltaTime * 5f);
        }

        public void SetCharacterHeight()
        {
            transform.DOKill();
            var temp = _characterObject.transform.position;
            temp.y = _playerController.GameManager.BlockController.CurrentBlockList.Count;
            //_characterObject.transform.position = temp;
            _characterObject.transform.DOMoveY(temp.y, 0.25f).SetEase(Ease.OutBack);
        }

        public void SetCharacterVerticalPosition(Vector2 vector2)
        {
            var temp = transform.position;
            temp.x = vector2.x;
            transform.position += new Vector3(temp.x, temp.y ,0);

        }
    }
}