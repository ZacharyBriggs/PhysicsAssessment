using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachPhysics
{
    public class BoidMoveBehaviour : MonoBehaviour
    {
        public List<BoidBehaviour> allBoids;
        Vector3 v1;
        Vector3 v2;
        Vector3 v3;
        [Range(0, 5)]
        public float cFac;
        [Range(0, 5)]
        public float dFac;
        [Range(0, 5)]
        public float aFac;
        [Range(0, 10)]
        public float vLim;
        [Range(0, 5)]
        public float lFac;
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var Boid in allBoids)
            {
                v1 = BoidsRuleOne(Boid.boidData) * cFac;
                v2 = BoidsRuleTwo(Boid.boidData) * dFac;
                v3 = BoidsRuleThree(Boid.boidData) * aFac;
                Boid.boidData.Velocity = Boid.boidData.Velocity + v1 + v2 + v3 * Time.deltaTime;
                if (Boid.boidData.Velocity.magnitude > vLim)
                    Boid.boidData.Velocity = (Boid.boidData.Velocity / Boid.boidData.Velocity.magnitude) * vLim;
                Boid.boidData.Position = Boid.boidData.Position + Boid.boidData.Velocity;
            }
        }

        public Vector3 BoidsRuleOne(Particle currentBoid)
        {
            var numBoids = allBoids.Count;
            Vector3 pc = Vector3.zero;
            foreach (var boid in allBoids)
            {
                if (boid.boidData != currentBoid)
                    pc += boid.boidData.Position;
            }
            pc = pc / (numBoids - 1);
            return (pc - currentBoid.Position)/50;
        }

        public Vector3 BoidsRuleTwo(Particle currentBoid)
        {
            Vector3 c = Vector3.zero;
            foreach (var boid in allBoids)
            {
                if (boid.boidData != currentBoid)
                {
                    if ((boid.boidData.Position - currentBoid.Position).magnitude < 2)
                    {
                        c = c - (boid.boidData.Position - currentBoid.Position);
                    }
                }
            }
            return c;
        }

        public Vector3 BoidsRuleThree(Particle currentBoid)
        {
            var numBoids = allBoids.Count;
            Vector3 pv = Vector3.zero;
            foreach (var boid in allBoids)
            {
                if (boid.boidData != currentBoid)
                {
                    pv += boid.boidData.Velocity;
                }
            }
            pv = pv / (numBoids - 1);
            return (pv - currentBoid.Velocity)/8;
        }
    }
}
