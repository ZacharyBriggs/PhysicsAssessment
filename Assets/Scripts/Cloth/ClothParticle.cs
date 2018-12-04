﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZachPhysics;

namespace ZachPhysics
{
    namespace ZachCloth
    {
        public class ClothParticle  
        {
            public bool Anchored;
            private Particle _particle = new Particle();
            public Vector3 Position;
            public Vector3 Force;
            public Vector3 Velocity;
            public float Mass = 1;
            
            public ClothParticle(Vector3 pos)
            {
                Position = pos;
                _particle.Position = pos;
                _particle.Force = Force;
                _particle.Velocity = Velocity;
                _particle.Mass = Mass;
            }

            public void AddForce(Vector3 force)
            {
                _particle.AddForce(force);
            }

            // Update is called once per frame
            public void Update()
            {
                if (!Anchored)
                {
                    Force = _particle.Force;
                    Velocity = _particle.Velocity;
                    Position = _particle.Position;                    
                    _particle.AddForce(new Vector3(0, -9.81f, 0) * .25f);
                    _particle.Update(Time.deltaTime);                    
                }
            }
        }
    }
}
