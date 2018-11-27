using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZachPhysics;

public class NewBehaviourScript : MonoBehaviour
{
    public Particle Particle1;
    public Particle Particle2;
    public SpringDamper Spring;
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    Vector3 CalculateUnitVector()
    {
        Vector3 ePrime = Particle2.Position - Particle1.Position;
        float Length = ePrime.magnitude;
        return ePrime / Length;
    }

    void CalculateVelocity(Vector3 unitLength)
    {
        var vel1 = Particle1.Velocity;
        var vel2 = Particle2.Velocity;
        var vel1Prime = Vector3.Dot(unitLength, vel1);
        var vel2Prime = Vector3.Dot(unitLength,vel2);
    }
}
