using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachPhysics
{
    public class BoidBehaviour : MonoBehaviour
    {
        public Particle boidData = new Particle();
        public ParticleDataScriptable particleData;
        private void Start()
        {
            particleData.Position = transform.position;
            boidData.Position = particleData.Position;
            boidData.Displacement = particleData.Displacement;
            boidData.Velocity = particleData.Velocity;
            boidData.Acceleration = particleData.Acceleration;
            boidData.Force = particleData.Force;
            boidData.Mass = particleData.Mass;
            
        }
        private void Update()
        {
            transform.position = boidData.Position;
        }

    }
}
