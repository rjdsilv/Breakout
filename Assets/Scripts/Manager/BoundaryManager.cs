using System;
using UnityEngine;

namespace Breakout.Assets.Scripts.Manager
{
    [Serializable]
    public class BoundaryManager : MonoBehaviour
    {
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;
    }
}