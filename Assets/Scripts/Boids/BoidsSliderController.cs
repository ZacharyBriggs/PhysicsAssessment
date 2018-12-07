using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZachPhysics.ZachBoids;
public class BoidsSliderController : MonoBehaviour
{
    public Slider Rule1;
    public Slider DistBetweenBoids;
    public Slider Rule3;
    public BoidMoveBehaviour boidController;
	
	void Update ()
    {
        boidController.ruleOneMagicNum = Rule1.value;
        boidController.MinDistBetweenBoids = DistBetweenBoids.value;
        boidController.vLim = Rule3.value;
    }
}
