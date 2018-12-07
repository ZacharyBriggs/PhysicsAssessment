using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControllerBehaviour : MonoBehaviour
{
    public ClothBehaviour cloth;
    public Slider AirSliderX;
    public Slider AirSliderY;
    public Slider AirSliderZ;
    public Slider Gravity;
    public Slider SpringConstant;
    public Slider DampeningFactor;


    // Update is called once per frame
    void Update ()
    {
        cloth.AirDensity.x = AirSliderX.value;
        cloth.AirDensity.y = AirSliderY.value;
        cloth.AirDensity.z = AirSliderZ.value;
        cloth.GravityScale = Gravity.value;
        cloth.SpringConstant = SpringConstant.value;
        cloth.DampingFactor = DampeningFactor.value;
    }
}
