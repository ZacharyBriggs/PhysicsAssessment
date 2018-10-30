using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachPhysics
{
    [CreateAssetMenu()]
    public class LinearMove : MovementObject
    {
        public override Vector3 Move(Vector3 pos, Vector3 force, float mass)
        {
            Vector3 vel = force / mass;
            return pos + vel * Time.deltaTime;
        }
    }
}
