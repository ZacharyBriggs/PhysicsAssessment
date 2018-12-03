using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZachPhysics.ZachCloth;
public class AerodynamicForce
{
    private Vector3 Density;
    private float DragCoefficient;
    private float Area;
    private Vector3 OppositeVector;
    private ClothParticle Particle1;
    private ClothParticle Particle2;
    private ClothParticle Particle3;

    public AerodynamicForce(ClothParticle p1, ClothParticle p2, ClothParticle p3, float drag)
    {
        Particle1 = p1;
        Particle2 = p2;
        Particle3 = p3;
        DragCoefficient = drag;
    }
    // Use this for initialization
    private void function()
    {
        var vSurface = (Particle1.Velocity + Particle2.Velocity + Particle3.Velocity) / 3;
        var vel = vSurface - Density;
        var diff2And1 = Particle2._particle.Position - Particle1._particle.Position;
        var diff3and1 = Particle3._particle.Position - Particle1._particle.Position;
        var cross = Vector3.Cross(diff2And1, diff3and1);
        var normal = cross / cross.magnitude;
        var areaO = cross.magnitude / 2;
        Area = areaO + (Vector3.Dot(vel, normal) / vel.magnitude);
        var normalPrime = Vector3.Cross(diff2And1,diff3and1);
        var thing = ((vel.magnitude * Vector3.Dot(vel, normal)) / (2*normalPrime.magnitude))*normalPrime;
    }
}
