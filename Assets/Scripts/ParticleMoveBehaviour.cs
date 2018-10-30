using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachPhysics
{
    public class ParticleMoveBehaviour : MonoBehaviour
    {
        private Particle particle = new Particle();
        public MovementObject moveType;
        public Vector3 Force = new Vector3(0,0,0);
        public float Mass = 0;
        // Use this for initialization
        void Start()
        {
            particle.Position = this.transform.position;
            particle.Force = this.Force;
            particle.Mass = this.Mass;
        }
        
        // Update is called once per frame
        void Update()
        {
            this.transform.position = moveType.Move(particle.Position, particle.Force, particle.Mass);
            particle.Position = this.transform.position;
            particle.Force = this.Force;
            particle.Mass = this.Mass;
        }
    }
}