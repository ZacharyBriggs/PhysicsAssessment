using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZachPhysics;

public class ClothTriangle
{
    public Particle Particle1;
    public Particle Particle2;
    public Particle Particle3;
    public int Particle1Index;
    public int Particle2Index;
    public int Particle3Index;

    public ClothTriangle(Particle p1,int p1index, Particle p2,int p2index, Particle p3,int p3index)
    {
        Particle1 = p1;
        Particle1Index = p1index;
        Particle2 = p2;
        Particle2Index = p2index;
        Particle3 = p3;
        Particle3Index = p3index;
    }
}
