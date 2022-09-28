using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
namespace Scripts.Behaviours
{

    public class PathBehaviour : MonoBehaviour
    {
        public CinemachineSmoothPath Path => _path;
        [SerializeField] private CinemachineSmoothPath _path;
    }

}