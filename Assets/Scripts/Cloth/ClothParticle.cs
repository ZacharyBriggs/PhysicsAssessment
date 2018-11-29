using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZachPhysics;

namespace ZachPhysics
{
    namespace ZachCloth
    {
        public class ClothParticle : MonoBehaviour
        {
            public bool Anchored;
            private Particle _particle = new Particle();
            public Vector3 Force;
            public Vector3 Velocity;
            public float Mass = 1;
            // Use this for initialization
            void Awake()
            {
                _particle.Position = transform.position;
                _particle.Force = Force;
                _particle.Velocity = Velocity;
                _particle.Mass = Mass;
            }

            public void AddForce(Vector3 force)
            {
                _particle.AddForce(force);
            }

            // Update is called once per frame
            void Update()
            {
                if (!Anchored)
                {
                    Force = _particle.Force;
                    Velocity = _particle.Velocity;
                    this.transform.position = _particle.Position;                    
                    _particle.AddForce(new Vector3(0, -9.81f, 0) * .25f);
                    _particle.Update(Time.deltaTime);                    
                }
            }
        }
    }
}
