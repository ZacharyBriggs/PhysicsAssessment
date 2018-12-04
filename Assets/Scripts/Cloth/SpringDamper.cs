using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZachPhysics.ZachCloth;

public class SpringDamper
{
    public ClothParticle Particle1; //p1/f1
    public ClothParticle Particle2; //p1/f1
    public Vector3 p1pos;
    public Vector3 p2pos;
    public float SpringConstant = 50; //ks
    public float DampingFactor = 2; //kd
    public float RestLength; //lo
    private float Length; //l
    private Vector3 UnitLength; //e
    private float Velocity1Prime; //v1*
    private float Velocity2Prime; //v2*
    private Vector3 Force1;
    private Vector3 Force2;
    // Use this for initialization
    public SpringDamper(ClothParticle p1, ClothParticle p2)
    {
        Particle1 = p1;
        Particle2 = p2;
        RestLength = Vector3.Distance(Particle1.Position, Particle2.Position);
	}
	
	// Update is called once per frame
	public void Update ()
    {
        p1pos = Particle1.Position;
        p2pos = Particle2.Position;

        CalculateUnitLength();
        CalculateVelocityPrime();
        ConvertDimensions();
	}

    void CalculateUnitLength()
    {
        Vector3 ePrime = Particle2.Position - Particle1.Position;
        Length = ePrime.magnitude;
        UnitLength = (ePrime.normalized / Length);
    }

    void CalculateVelocityPrime()
    {
        var vel1 = Particle1.Velocity;
        var vel2 = Particle2.Velocity;
        Velocity1Prime = Vector3.Dot(UnitLength, vel1);
        Velocity2Prime = Vector3.Dot(UnitLength,vel2);
    }

    void ConvertDimensions()
    {
        var force = -SpringConstant * (RestLength - Length) - DampingFactor * (Velocity1Prime - Velocity2Prime);
        Force1 = force * UnitLength;
        Force2 = -Force1;
        Particle1.AddForce(Force1);
        Particle2.AddForce(Force2);
    }
}
