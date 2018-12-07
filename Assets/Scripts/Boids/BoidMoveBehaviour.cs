using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachPhysics
{
    namespace ZachBoids
    {
        public class BoidMoveBehaviour : MonoBehaviour
        {
            private List<BoidBehaviour> allBoids = new List<BoidBehaviour>();
            public GameObject BoidPrefab;
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
            [Range(0, 100)]
            public float ruleOneMagicNum;
            [Range(0, 10)]
            public float MinDistBetweenBoids;
            [Range(0, 20)]
            public float RuleThreeMagicNum;
            public float XMin;
            public float XMax;
            public float YMin;
            public float YMax;
            public float ZMin;
            public float ZMax;
            public float GroundLevel;

            private void Start()
            {
                GetComponentsInChildren(allBoids);
            }
            // Update is called once per frame
            void Update()
            {
                foreach (var Boid in allBoids)
                {
                    if (Boid.perching)
                    {
                        if (Boid.perchTimer > 0)
                            Boid.perchTimer -= Time.deltaTime;
                        else
                        {
                            Boid.perching = false;
                            Boid.perchTimer = Boid.perchTime;
                            Boid.boidData.Position += new Vector3(0, 10, 0);
                        }
                    }

                    else
                    {
                        v1 = BoidsRuleOne(Boid.boidData) * cFac;
                        v2 = BoidsRuleTwo(Boid.boidData) * dFac;
                        v3 = BoidsRuleThree(Boid.boidData) * aFac;
                        Boid.boidData.Velocity = Boid.boidData.Velocity + v1 + v2 + v3 * Time.deltaTime;
                        Boid.boidData.Velocity += BoundPosition(Boid.boidData, 2);
                        if (Boid.boidData.Position.y < GroundLevel)
                        {
                            Boid.boidData.Position = new Vector3(Boid.boidData.Position.x, GroundLevel, Boid.boidData.Position.z);
                            //Boid.perching = true;
                        }
                        if (Boid.boidData.Velocity.magnitude > vLim)
                            Boid.boidData.Velocity = (Boid.boidData.Velocity / Boid.boidData.Velocity.magnitude) * vLim;
                        Boid.boidData.Position = Boid.boidData.Position + Boid.boidData.Velocity;
                    }
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
                return (pc - currentBoid.Position) / ruleOneMagicNum;
            }

            public Vector3 BoidsRuleTwo(Particle currentBoid)
            {
                Vector3 c = Vector3.zero;
                foreach (var boid in allBoids)
                {
                    if (boid.boidData != currentBoid)
                    {
                        if ((boid.boidData.Position - currentBoid.Position).magnitude < MinDistBetweenBoids)
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
                return (pv - currentBoid.Velocity) / RuleThreeMagicNum;
            }

            public Vector3 BoundPosition(Particle currentBoid, float DI)
            {
                Vector3 v = Vector3.zero;

                if (currentBoid.Position.x < XMin)
                    v.x = DI;
                if (currentBoid.Position.x > XMax)
                    v.x = -DI;
                if (currentBoid.Position.y < YMin)
                    v.y = DI;
                if (currentBoid.Position.y > YMax)
                    v.y = -DI;
                if (currentBoid.Position.z < ZMin)
                    v.z = DI;
                if (currentBoid.Position.z > ZMax)
                    v.z = -DI;
                return v;
            }

            public void AddBoid()
            {
                for (int i = 0; i < 100; i++)
                {
                    var pos = new Vector3(1, 1, 1);
                    var boid = Instantiate(BoidPrefab, pos, Quaternion.identity);
                    allBoids.Add(boid.GetComponent<BoidBehaviour>());
                }
            }
        }
    }
}
