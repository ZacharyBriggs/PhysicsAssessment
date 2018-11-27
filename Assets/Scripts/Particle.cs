using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachPhysics
    {
    [System.Serializable]
    public class Particle
    {
        public bool IsAnchored;
        public MovementObject Moveable;
        public Vector3 Position { get; set; }
        public Vector3 Displacement { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Acceleration { get; set; }
        public Vector3 Force { get; set; }
        public float Mass;
    }
}
