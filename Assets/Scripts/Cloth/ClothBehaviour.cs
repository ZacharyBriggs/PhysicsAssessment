using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZachPhysics.ZachCloth;

public class ClothBehaviour : MonoBehaviour
{
    public int width;
    public int height;
    public float dist;
    public float AirForce;
    private List<ClothParticle> allParticles = new List<ClothParticle>();
    private List<SpringDamper> allDampers = new List<SpringDamper>();
    private List<SpringDamper> bendingSprings = new List<SpringDamper>();
    private List<AerodynamicForce> allTriangles = new List<AerodynamicForce>();

    void Start()
    {

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var particle = CreateParticle(x + dist, y ,false);
                if (y == height - 1)
                    particle.Anchored = true;

                allParticles.Add(particle);
            }
        }
        for (int i = 0; i < allParticles.Count; i++)
        {
            if (i % width != width - 1)
            {
                var damper = CreateDamper(allParticles[i], allParticles[i + 1]);
                allDampers.Add(damper);
            }
            if (i < allParticles.Count - height)
            {
                var damper = CreateDamper(allParticles[i], allParticles[i + (int)width]);
                allDampers.Add(damper);
            }
        }

        var crossdampers = ((width - 1) * height) - (width - 1);
        for (int i = 0; i < crossdampers; i++)
        {
            if (i % width == width - 1)
            {
                var triangle = new AerodynamicForce(allParticles[i], i, allParticles[i + 1], i + 1, allParticles[i + width], i + width, AirForce);
                var triangle2 = new AerodynamicForce(allParticles[i + 1], i + 1, allParticles[i], i, allParticles[i + 1 + width], i + 1 + width, AirForce);
                allTriangles.Add(triangle);
                allTriangles.Add(triangle2);
            }
        }

        for (int i = 0; i < allParticles.Count; i++)
        {
            if (i < allParticles.Count - height)
            {
                if (i % width == 0)
                {
                    var damper = CreateDamper(allParticles[i], allParticles[i + width + 1]);
                    allDampers.Add(damper);
                }
                else if (i % width == width - 1)
                {
                    var damper = CreateDamper(allParticles[i], allParticles[i + width - 1]);
                    allDampers.Add(damper);
                }
                else
                {
                    var damper = CreateDamper(allParticles[i], allParticles[i + width + 1]);
                    allDampers.Add(damper);

                    var damper2 = CreateDamper(allParticles[i], allParticles[i + width - 1]);
                    allDampers.Add(damper2);
                }
            }
        }

        for(int i = 0; i<allParticles.Count;i++)
        {
            if (i == width - 1 || i == width - 2)
            {
                var damper = CreateDamper(allParticles[i], allParticles[i + 2]);
                bendingSprings.Add(damper);
            }
        }
	}
	
	void Update ()
    {
        foreach(var particle in allParticles)
            particle.Update();

        foreach(var spring in allDampers)
            spring.Update();

        foreach(var spring in bendingSprings)
            spring.Update();

        foreach(var force in allTriangles)
            force.AddAerodynamicForce();
    }
    
    ClothParticle CreateParticle(float x, float y, bool IsAnchored)
    {
        ClothParticle cp = new ClothParticle(new Vector3(x,y,0));
        cp.Anchored = IsAnchored;
        return cp;
    }
    
    SpringDamper CreateDamper(ClothParticle particle1, ClothParticle particle2)
    {
        SpringDamper sd = new SpringDamper(particle1,particle2);
        return sd;
    }

    private void OnDrawGizmos()
    {
        foreach(var particle in allParticles)
            Gizmos.DrawSphere(particle.Position,.25f);

        foreach(var damper in allDampers)
            Gizmos.DrawLine(damper.Particle1.Position, damper.Particle2.Position);
    }
}
