using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachPhysics
    {
    [System.Serializable]
    public class Particle
    {
        public bool Anchored;
        public bool Active = true;
        public MovementObject Moveable;
        public Vector3 Position { get; set; }
        public Vector3 Displacement { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Acceleration { get; set; }
        public Vector3 Force { get; set; }
        public float Mass = 1;

        public Particle()
        {
            Position = new Vector3(0, 0, 0);
        }
        public Particle(Vector3 pos)
        {
            Position = pos;
        }
        public void AddForce(Vector3 force)
        {
            Force += force;
        }

        public void Update(float dt)
        {
            if (!Anchored)
            {
                Acceleration = Force * Mass;
                Velocity = Velocity + Acceleration * dt;
                if (Velocity.magnitude > 5)
                    Velocity = Velocity.normalized;
                Position = Position + Velocity * dt;
            }
            Force = Vector3.zero;
        }
    }
}
