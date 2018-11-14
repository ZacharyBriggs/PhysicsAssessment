using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ZachPhysics
{
    namespace ZachAABB
    {

        public class CollisionController2D : MonoBehaviour
        {
            public List<CollisionVolume2D> AllVolumes;
            public List<CollisionVolume2D> xValues;
            public List<CollisionVolume2D> yValues;
            public List<CollisionVolume2D> OpenList;
            public List<CollisionVolume2D> ClosedList;

            public void Start()
            {
                AllVolumes = new List<CollisionVolume2D>(GetComponents<CollisionVolume2D>());
                xValues = new List<CollisionVolume2D>(GetComponents<CollisionVolume2D>());
            }
            public void CheckCollisionVolumes()
            {
                AllVolumes.OrderBy(v => v.minX);
                foreach(var volume in AllVolumes)
                {
                    OpenList.Add(volume);
                    if(OpenList.Count >= 2)
                    {
                        if(OpenList[0].maxX < OpenList[1].minX)
                        {
                            ClosedList.Add(OpenList[0]);
                            OpenList.Remove(OpenList[0]);
                            Debug.Log("no collision on x chief");
                        }
                        //else they are colliding on x

                        else if(OpenList[0].minY < OpenList[1].minY && OpenList[0].maxY > OpenList[1].minY)
                        {
                            Debug.Log("they be colliding");
                        }
                        else
                        {
                            Debug.Log("collision on x but not on y");
                        }
                    }
                }
            }
        }
    }
}
