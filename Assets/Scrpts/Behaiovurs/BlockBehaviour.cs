using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Enums;
public class BlockBehaviour : MonoBehaviour
{
    public BlockColors Color => _color;
    [SerializeField] BlockColors _color;
}
