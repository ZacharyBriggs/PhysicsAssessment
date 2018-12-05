using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMeshGenerator : MonoBehaviour
{
    public MeshFilter InstanceMeshFilter;
    public Mesh InstanceMesh;
    List<Vector3> Vertices = new List<Vector3>();
    List<int> TrianlgePoints = new List<int>();
    List<Vector3> SurfaceNormals = new List<Vector3>();
    List<Vector2> UVs = new List<Vector2>();
    public ClothBehaviour cb;

    // Use this for initialization
    private void Start()
    {
        cb = GetComponent<ClothBehaviour>();
    }
    void GenMesh()
    {
        InstanceMesh = new Mesh();
        InstanceMesh.name = "Example";

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                Vertices.Add(new Vector3(x, y, 0));
            }
        }
        InstanceMesh.vertices = Vertices.ToArray();

        for (int i = 0; i < Vertices.Count; i++)
        {
            //If we are not on the edge of the verts we will create  triangle
            if (i % 5 != 5 - 1 && i < Vertices.Count - 5)
            {
                //Bot Triangle
                TrianlgePoints.Add(i); //bot left
                TrianlgePoints.Add(i + 1); //bot right
                TrianlgePoints.Add(i + 5); //top Left

                //Top Trianlge
                TrianlgePoints.Add(i + 1); //bot right
                TrianlgePoints.Add(i + 5 + 1); //top right
                TrianlgePoints.Add(i + 5); //top left
            }
        }
        InstanceMesh.triangles = TrianlgePoints.ToArray();

        foreach (var vert in Vertices)
        {
            SurfaceNormals.Add(new Vector3(0, 0, 1));
        }
        InstanceMesh.normals = SurfaceNormals.ToArray();

        foreach (var vert in Vertices)
        {
            //We set the x and y of each UV to be the x and y values of the vert's posititon divided by the width of the verts - 1
            UVs.Add(new Vector2(vert.x / (5 - 1), vert.y / (5 - 1)));
        }
        InstanceMesh.uv = UVs.ToArray();

        InstanceMeshFilter.mesh = InstanceMesh;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
