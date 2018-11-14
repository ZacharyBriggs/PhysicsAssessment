using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachPhysics
{
    namespace ZachAABB
    {
        public class CollisionVolume2D : MonoBehaviour
        {
            public float minX;
            public float maxX;
            public float minY;
            public float maxY;
            public bool isColliding;
        }
    }
}
