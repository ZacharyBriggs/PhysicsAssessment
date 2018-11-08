using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachPhysics
{
    public class ParticleMoveBehaviour : MonoBehaviour
    {
        private Particle particle = new Particle();
        public MovementObject moveType;
        public Vector3 Force = new Vector3(0,0,0);
        public float Mass = 0;
        public float velocity;
        public float angle;
        public float initHeight;
        // Use this for initialization
        void Start()
        {
            particle.Position = this.transform.position;
            particle.Force = this.Force;
            particle.Mass = this.Mass;
        }
        
        // Update is called once per frame
        void Update()
        {
            this.transform.position = moveType.Move(particle.Position, particle.Force, particle.Mass);
            particle.Position = this.transform.position;
            particle.Force = this.Force;
            particle.Mass = this.Mass;
            if (Input.GetKeyDown(KeyCode.A))
            {
                CalculateAngle(velocity, angle, initHeight);
            }
        }

        

        public static void CalculateAngle(float vel, float angle, float initHeight)
        {
            var vx = vel * Mathf.Cos(angle);
            var vy = vel * Mathf.Sin(angle);
            var flightTime = (2 * vy) / -9.81;
            Debug.Log(flightTime);
            var range = (2 * vx * vy) / -9.81;
            Debug.Log(range);
            var maxHeight = (vy * vy) / (2 * -9.81);
            Debug.Log(maxHeight);
        }
    }
}