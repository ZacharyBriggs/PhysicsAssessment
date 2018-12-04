using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZachPhysics.ZachCloth;

public class ClothTriangle
{
    public ClothParticle Particle1;
    public ClothParticle Particle2;
    public ClothParticle Particle3;
    public int Particle1Index;
    public int Particle2Index;
    public int Particle3Index;

    public ClothTriangle(ClothParticle p1,int p1index, ClothParticle p2,int p2index, ClothParticle p3,int p3index)
    {
        Particle1 = p1;
        Particle1Index = p1index;
        Particle2 = p2;
        Particle2Index = p2index;
        Particle3 = p3;
        Particle3Index = p3index;
    }
}
