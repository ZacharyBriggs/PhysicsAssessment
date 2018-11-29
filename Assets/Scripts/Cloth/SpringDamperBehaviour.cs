using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZachPhysics.ZachCloth;

public class SpringDamperBehaviour : MonoBehaviour
{
    public ClothParticle Particle1; //p1/f1
    public ClothParticle Particle2; //p1/f1
    public float SpringConstant; //ks
    public float DampingFactor; //kd
    public float RestLength; //lo
    private float Length; //l
    private Vector3 UnitLength; //e
    private float Velocity1Prime; //v1*
    private float Velocity2Prime; //v2*
    private Vector3 Force1;
    private Vector3 Force2;
    // Use this for initialization
    void Start ()
    {
        RestLength = Vector3.Distance(Particle1.transform.position, Particle2.transform.position);
	}
	
	// Update is called once per frame
	void Update ()
    {
        CalculateUnitLength();
        CalculateVelocityPrime();
        ConvertDimensions();
	}

    void CalculateUnitLength()
    {
        Vector3 ePrime = Particle2.transform.position - Particle1.transform.position;
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
