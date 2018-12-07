using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachPhysics
{
    namespace ZachAABB
    {
        public class CollisionVolume2D : MonoBehaviour
        {
            public float Size;
            public float minX;
            public float maxX;
            public float minY;
            public float maxY;
            
            public bool isColliding;

            private void Update()
            {
                minX = this.transform.position.x - Size;
                maxX = this.transform.position.x + Size;
                minY = this.transform.position.y - Size;
                maxY = this.transform.position.y + Size;
            }

            private void OnDrawGizmos()
            {
                if (isColliding)
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.white;
                Gizmos.DrawLine(new Vector3(minX, minY, transform.position.z), new Vector3(maxX, minY, transform.position.z));
                Gizmos.DrawLine(new Vector3(minX, minY, transform.position.z), new Vector3(minX, maxY, transform.position.z));
                Gizmos.DrawLine(new Vector3(maxX, minY, transform.position.z), new Vector3(maxX, maxY, transform.position.z));
                Gizmos.DrawLine(new Vector3(maxX, maxY, transform.position.z), new Vector3(minX, maxY, transform.position.z));
            }
        }
    }
}
