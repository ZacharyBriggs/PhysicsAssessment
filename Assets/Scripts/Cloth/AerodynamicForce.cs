using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZachPhysics;
public class AerodynamicForce
{
    public Vector3 Density;
    public float DragCoefficient;
    private float Area;
    private Vector3 OppositeVector;
    public ClothTriangle Triangle;

    public AerodynamicForce(Particle p1, int p1index, Particle p2, int p2index, Particle p3, int p3index)
    {
        Triangle = new ClothTriangle(p1,p1index,p2,p2index,p3,p3index);
    }
    
    public void AddAerodynamicForce(Vector3 density)
    {
        Density = density;
        var vSurface = (Triangle.Particle1.Velocity + Triangle.Particle2.Velocity + Triangle.Particle3.Velocity) / 3;
        var vel = vSurface - Density;
        var diff2And1 = Triangle.Particle2.Position - Triangle.Particle1.Position;
        var diff3and1 = Triangle.Particle3.Position - Triangle.Particle1.Position;
        var cross = Vector3.Cross(diff2And1, diff3and1);
        var normal = cross / cross.magnitude;
        var areaO = 0.5f * cross.magnitude ;
        Area = areaO + (Vector3.Dot(vel, normal) / vel.magnitude);
        var normalPrime = cross;
        var totalForce = -.5f * ((vel.magnitude * Vector3.Dot(vel, normalPrime)) 
            / (2*normalPrime.magnitude))*normalPrime.normalized;
        Triangle.Particle1.AddForce(totalForce / 3);
        Triangle.Particle2.AddForce(totalForce / 3);
        Triangle.Particle3.AddForce(totalForce / 3);
    }
}
