using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachPhysics
{
    public class BoidBehaviour : MonoBehaviour
    {
        public Particle boidData = new Particle();
        public ParticleDataScriptable particleData;
        public bool perching = false;
        public float perchTime;
        public float perchTimer { get; set; }
        private void Start()
        {
            var x = Random.Range(0, 10);
            var y = Random.Range(0, 10);
            var z = Random.Range(0, 10);
            var pos = new Vector3(x, y, z);
            particleData.Position = pos;
            boidData.Position = particleData.Position;
            boidData.Displacement = particleData.Displacement;
            boidData.Velocity = particleData.Velocity;
            boidData.Acceleration = particleData.Acceleration;
            boidData.Force = particleData.Force;
            boidData.Mass = particleData.Mass;
            perchTimer = perchTime;
            
        }
        private void Update()
        {
            transform.position = boidData.Position;
        }
    }
}
