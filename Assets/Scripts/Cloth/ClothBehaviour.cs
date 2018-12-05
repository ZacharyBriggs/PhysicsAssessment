using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZachPhysics.ZachCloth;

public class ClothBehaviour : MonoBehaviour
{
    public int width;
    public int height;
    public float dist;
    public float SpringConstant;
    public float DampingFactor;
    public float GravityScale;
    public Vector3 AirDensity;
    public float Drag;
    ClothParticle heldParticle;
    private List<ClothParticle> allParticles = new List<ClothParticle>();
    private List<SpringDamper> allDampers = new List<SpringDamper>();
    private List<SpringDamper> bendingSprings = new List<SpringDamper>();
    List<AerodynamicForce> allTriangles = new List<AerodynamicForce>();

    void Start()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var particle = CreateParticle(x + dist, y, false);
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
        for (int i = 0; i < crossdampers + width - 1; i++)
        {
            if (i % width != width - 1)
            {
                var triangle = new AerodynamicForce(allParticles[i], i, allParticles[i + 1], i + 1, allParticles[i + width], i + width);
                var triangle2 = new AerodynamicForce(allParticles[i + 1], i + 1, allParticles[i + width + 1], i + width + 1, allParticles[i + width], i + width);
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

        for (int i = 0; i < allParticles.Count; i++)
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
        foreach(var spring in allDampers)
            spring.Update(SpringConstant, DampingFactor);

        foreach(var spring in bendingSprings)
            spring.Update(SpringConstant, DampingFactor);

            var mousePos = Input.mousePosition;
            var worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x,
                mousePos.y,
                -Camera.main.transform.position.z));
            

            if (Input.GetMouseButtonDown(0))
                foreach (var p in allParticles)
                {
                    var scaledPPositoin = new Vector3(p.Position.x * transform.localScale.x,
                        p.Position.y * transform.localScale.y,
                        p.Position.z * transform.localScale.z);
                    var checkPos = new Vector3(worldMousePos.x, worldMousePos.y, p.Position.z);
                    if (Vector3.Distance(checkPos, scaledPPositoin) <= 1f)
                        heldParticle = p;
                }

            if (Input.GetMouseButton(0) && heldParticle != null)
            {
                heldParticle.Position = worldMousePos;
                if (Input.GetKeyDown(KeyCode.R))
                {
                    heldParticle.Active = false;
                    for (var i = 0; i < allDampers.Count; i++)
                        if (allDampers[i].Particle1 == heldParticle || allDampers[i].Particle2 == heldParticle)
                            allDampers.RemoveAt(i);
                    for (var i = 0; i < allTriangles.Count; i++)
                        if (allTriangles[i].Triangle.Particle1 == heldParticle || allTriangles[i].Triangle.Particle2 == heldParticle || allTriangles[i].Triangle.Particle3 == heldParticle)
                            allTriangles.RemoveAt(i);
                }

                if (Input.GetKeyDown(KeyCode.A))
                    heldParticle.Anchored = !heldParticle.Anchored;
            }

            if (Input.GetMouseButtonUp(0))
                heldParticle = null;
    }

    private void LateUpdate()
    {
        var gravity = -9.81f;
        foreach (var p in allParticles)
            p.AddForce(new Vector3(0,gravity*GravityScale,0));

        foreach (var sd in allDampers)
            sd.Update(SpringConstant,DampingFactor);

        foreach (var tri in allTriangles)
        {
            tri.Density = AirDensity;
            tri.DragCoefficient = Drag;
            tri.AddAerodynamicForce(AirDensity);
        }

        foreach (var p in allParticles)
        {
            p.Update(Time.deltaTime);
        }

        for (int i = 0; i < allParticles.Count; i++)
        {
            //Spheres[i].transform.position = Particles[i].Position;
        }
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
