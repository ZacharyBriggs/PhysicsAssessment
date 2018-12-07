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
            private List<CollisionVolume2D> AllVolumes;
            private List<CollisionVolume2D> OpenList = new List<CollisionVolume2D>();
            private List<CollisionVolume2D> ClosedList = new List<CollisionVolume2D>();

            public void Start()
            {
                AllVolumes = new List<CollisionVolume2D>(GetComponents<CollisionVolume2D>());
                AllVolumes = FindObjectsOfType<CollisionVolume2D>().ToList();
            }

            public void Update()
            {
                CheckCollisionVolumes();
            }



            public void CheckCollisionVolumes()
            {
                AllVolumes.OrderBy(v => v.minX);
                foreach (var volume in AllVolumes)
                {
                    OpenList.Add(volume);
                    if (OpenList.Count >= 2)
                    {
                        if (OpenList[0].minX < OpenList[1].maxX && OpenList[0].maxX > OpenList[1].minX)
                        {
                            if (OpenList[0].minY < OpenList[1].maxY && OpenList[0].maxY > OpenList[1].minY)
                            {
                                Debug.Log("Collision");
                                OpenList[0].isColliding = true;
                                OpenList[1].isColliding = true;
                                ClosedList.Add(OpenList[0]);
                                OpenList.Remove(OpenList[0]);
                            }
                            else
                            {
                                Debug.Log("Y collision but no X collision");
                                OpenList[0].isColliding = false;
                                OpenList[1].isColliding = false;
                                ClosedList.Add(OpenList[0]);
                                OpenList.Remove(OpenList[0]);
                            }
                        }
                        else if (OpenList[0].minY < OpenList[1].maxY && OpenList[0].maxY > OpenList[1].minY)
                        {
                            Debug.Log("X collision but no Y collision");
                            OpenList[0].isColliding = false;
                            OpenList[1].isColliding = false;
                            ClosedList.Add(OpenList[0]);
                            OpenList.Remove(OpenList[0]);
                        }
                        else
                        {
                            OpenList[0].isColliding = false;
                            OpenList[1].isColliding = false;
                            Debug.Log("No collision");
                        }
                    }
                }
                ClosedList = new List<CollisionVolume2D>();
            }
        }
    }
}
