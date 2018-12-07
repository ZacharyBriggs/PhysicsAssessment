using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if Unity_Editor
using UnityEditor;
#endif

[CreateAssetMenu]
public class CalculatorScriptable : ScriptableObject
{
    public static float CalcFlightTime(float vel, float angle)
    {
        var vx = vel * Mathf.Cos(angle);
        var vy = vel * Mathf.Sin(angle);
        return (2 * vy) / 9.81f;
    }

    public static float CalcDistanceTraveled(float vel, float angle)
    {
        var vx = vel * Mathf.Cos(angle);
        var vy = vel * Mathf.Sin(angle);
        return (2 * vx * vy) / 9.81f;
    }

    public static float CalcMaxHeight(float vel, float angle)
    {
        var vx = vel * Mathf.Cos(angle);
        var vy = vel * Mathf.Sin(angle);
        return (vy * vy) / (2.0f * 9.81f);
    }
#if Unity_Editor
    [CustomEditor(typeof(CalculatorScriptable))]
    public class CalculatorScriptableEditor : Editor
    {
        public float Velocity = 1;
        public float Angle = 1;
        public float InitialHeight;
        public float flightTime;
        public float disTraveled;
        public float maxHeight;
        private CalculatorScriptable calculator;
        public override void OnInspectorGUI()
        {
            calculator = (CalculatorScriptable)target;
            if (GUILayout.Button("Calculate"))
            {
                flightTime = CalcFlightTime(Velocity,Angle);
                disTraveled = CalcDistanceTraveled(Velocity, Angle);
                maxHeight = CalcMaxHeight(Velocity, Angle);
            }
            GUILayout.Box("Flight Time: " + flightTime.ToString());
            GUILayout.Box("Distance Traveled: " + disTraveled.ToString());
            GUILayout.Box("Max Height: " + maxHeight.ToString());
        }
    }
#endif
}
