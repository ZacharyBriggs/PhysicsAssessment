using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachPhysics
{
    [CreateAssetMenu]
    public class ParticleDataScriptable : ScriptableObject
    {
        public Vector3 Position;
        public Vector3 Displacement;
        public Vector3 Velocity;
        public Vector3 Acceleration;
        public Vector3 Force;
        public float Mass;
    }
}
