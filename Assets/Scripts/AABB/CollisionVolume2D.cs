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
            public bool collider2;
            public bool isColliding;

            private void Update()
            {
                if (!collider2)
                {
                    var h = Input.GetAxis("Horizontal");
                    var v = Input.GetAxis("Vertical");
                    transform.position += new Vector3(h, v, 0);
                }
                else
                {
                    var h = Input.GetAxis("HorizontalTwo");
                    var v = Input.GetAxis("VerticalTwo");
                    transform.position += new Vector3(h, v, 0);
                }
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
