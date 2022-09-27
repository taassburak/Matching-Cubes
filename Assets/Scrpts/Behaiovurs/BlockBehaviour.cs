using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Enums;
using DG.Tweening;
public class BlockBehaviour : MonoBehaviour
{
    public BlockColors Color => _color;
    [SerializeField] BlockColors _color;


    public void ChangePosition(Vector3 newPosition, bool isRemoving)
    {
        if (isRemoving)
        {
            transform.DOMove(newPosition, 0.35f).SetEase(Ease.InBack);
        }
        else
        {
            transform.DOMove(newPosition, 0.35f);
        }
    }
}
