using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachPhysics
{
    [CreateAssetMenu()]
    public class WeirdMove : MovementObject
    {
        public override Vector3 Move(Vector3 pos, Vector3 force, float mass)
        {
            return pos + (force / mass) * (Time.deltaTime + 2);
        }
    }
}
