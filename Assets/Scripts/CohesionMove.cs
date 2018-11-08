using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachPhysics
{
    public class CohesionMove : MovementObject
    {
        public override Vector3 Move(Vector3 pos, Vector3 force, float mass)
        {
            return new Vector3(0, 0, 0);   
        }
    }
}
