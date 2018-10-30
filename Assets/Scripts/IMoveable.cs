using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachPhysics
{
    public interface IMoveable
    {
        Vector3 Move(Vector3 pos, Vector3 force, float mass);
    }
    public abstract class MovementObject : ScriptableObject ,IMoveable
    {
        public abstract Vector3 Move(Vector3 pos, Vector3 force, float mass);
    }
}
