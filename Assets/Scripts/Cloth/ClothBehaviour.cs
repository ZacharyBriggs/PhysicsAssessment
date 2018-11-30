using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZachPhysics.ZachCloth;

namespace trash
{
    //var total = width * height;
    //var totalDampers = (width - 1) * height + (height - 1) * width;
    //for (int p = 0; p < total;p++)
    //    allParticles.Add(new ClothParticle());
    //for(int d = 0; d < totalDampers; d++)
    //    allDampers.Add(new SpringDamperBehaviour());

    //var xPos = -1;
    //var yPos = 0;
    //for (int i = 0;i<total;i++)
    //{
    //    if (i > 0)
    //        if (i % width == 0)
    //        {

    //            xPos = -1;
    //            yPos++;
    //        }
    //    if(i < width-1)
    //    {
    //        xPos++;
    //        var component = new ClothParticle();
    //        if (i == 0)
    //        {
    //            var cParticle = CreateParticle(xPos, yPos,true);
    //            component = cParticle.GetComponent<ClothParticle>();
    //            allParticles[i] = component;
    //        }
    //        else
    //        {
    //            component = allParticles[i].GetComponent<ClothParticle>();
    //        }
    //        if (!(i == width - 1))
    //        {
    //            var cParticle2 = CreateParticle(xPos + 1, yPos,true);
    //            var component2 = cParticle2.GetComponent<ClothParticle>();
    //            allParticles[i + 1] = component2;

    //            var sd = CreateDamper(component, component2);
    //            var sdc = sd.GetComponent<SpringDamperBehaviour>();
    //            allDampers[i] = sdc;
    //        }

    //        var cParticle3 = CreateParticle(xPos,yPos+1,false);
    //        var component3 = cParticle3.GetComponent<ClothParticle>();
    //        allParticles[i + width] = component3;

    //        var sd2 = CreateDamper(component,component3);
    //        var sdc2 = sd2.GetComponent<SpringDamperBehaviour>();
    //        allDampers[i+width-1] = sdc2;
    //    }

    //    else if(yPos == height)
    //    {
    //        var component = allParticles[i].GetComponent<ClothParticle>();
    //        var component2 = allParticles[i+1].GetComponent<ClothParticle>();
    //    }

    //    else
    //    {
    //        xPos++;
    //        var component = allParticles[i].GetComponent<ClothParticle>();
    //        if (i == width - 1)
    //        {
    //            var component2 = allParticles[i + 1].GetComponent<ClothParticle>();

    //            var sd = CreateDamper(component, component2);
    //            var sdc = sd.GetComponent<SpringDamperBehaviour>();
    //            allDampers[i] = sdc;
    //        }
    //        if (yPos < height-1)
    //        {
    //            var cParticle = CreateParticle(xPos, yPos + 1,false);
    //            var component3 = cParticle.GetComponent<ClothParticle>();
    //            allParticles[i + width] = component;


    //            var sd2 = CreateDamper(component, component3);
    //            var sdc2 = sd2.GetComponent<SpringDamperBehaviour>();
    //            allDampers[i + width-1] = sdc2;
    //        }
    //    }
    //    for(int o = total; o<totalDampers;o++)
    //    {

    //    }
    //}
}
public class ClothBehaviour : MonoBehaviour
{
    public int width;
    public int height;
    public float dist;
    private List<ClothParticle> allParticles = new List<ClothParticle>();
    private List<SpringDamperBehaviour> allDampers = new List<SpringDamperBehaviour>();
    private List<SpringDamperBehaviour> crossDampers = new List<SpringDamperBehaviour>();
	// Use this for initialization
	void Awake ()
    {

		for(int y = 0;y < height;y++)
        {
            for(int x = 0;x < width;x++)
            {
                var particle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                particle.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                particle.transform.position = new Vector3(x+dist, y, 0);
                particle.AddComponent<ClothParticle>();
                var clothParticle = particle.GetComponent<ClothParticle>();
                if (y == height-1)
                {
                    clothParticle.Anchored = true;
                }
                allParticles.Add(clothParticle);
            }
        }
        for(int i = 0; i<allParticles.Count;i++)
        {
            if(i%width != width-1)
            {
                var damper = CreateDamper(allParticles[i],allParticles[i+1]);
                var component = damper.GetComponent<SpringDamperBehaviour>();
                allDampers.Add(component);
            }
            if(i<allParticles.Count - height)
            {
                var damper = CreateDamper(allParticles[i], allParticles[i + (int)width]);
                var component = damper.GetComponent<SpringDamperBehaviour>();
                allDampers.Add(component);
            }
        }

        var thing = 1;
        for (int i = 0; i < allParticles.Count; i++)
        {
            if (i % width == 0)
            {
                var damper = CreateDamper(allParticles[i], allParticles[i + width + 1]);
                var component = damper.GetComponent<SpringDamperBehaviour>();
                allDampers.Add(component);
            }
            else if (i % width == width-1)
            {
                var damper = CreateDamper(allParticles[i], allParticles[i + width - 1]);
                var component = damper.GetComponent<SpringDamperBehaviour>();
                allDampers.Add(component);
            }
            else
            {
                var damper = CreateDamper(allParticles[i], allParticles[i + width + 1]);
                var component = damper.GetComponent<SpringDamperBehaviour>();
                allDampers.Add(component);

                var damper2 = CreateDamper(allParticles[i], allParticles[i + width - 1]);
                var component2 = damper2.GetComponent<SpringDamperBehaviour>();
                allDampers.Add(component2);
            }
        }
        //var totaldampers = width - 1 * height + height - 1 * width;
        //for(int d = 0;d < totaldampers; d++)
        //{
        //    var damper = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //    damper.AddComponent<SpringDamperBehaviour>();
        //    var sd = damper.GetComponent<SpringDamperBehaviour>();
        //    sd.Particle1 = allParticles[d];
        //    sd.Particle2 = allParticles[d+1];
        //}
	}
	
	// Update is called once per frame
	void Update ()
    {
        int z = 0;
    }
    
    GameObject CreateParticle(float x, float y, bool IsAnchored)
    {
        var particle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        particle.transform.position = new Vector3(x, y, 0);
        particle.AddComponent<ClothParticle>();
        var clothParticle = particle.GetComponent<ClothParticle>();
        clothParticle.Anchored = IsAnchored;
        return particle;
    }
    
    GameObject CreateDamper(ClothParticle particle1, ClothParticle particle2)
    {
        var damper = new GameObject();
        damper.AddComponent<SpringDamperBehaviour>();
        var sd = damper.GetComponent<SpringDamperBehaviour>();
        sd.Particle1 = particle1;
        sd.Particle2 = particle2;
        return damper;
    }

    private void OnDrawGizmos()
    {
        foreach(var damper in allDampers)
        {
            Gizmos.DrawLine(damper.Particle1._particle.Position, damper.Particle2._particle.Position);
        }
    }
}
