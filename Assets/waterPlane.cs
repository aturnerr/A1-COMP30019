using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterPlane : MonoBehaviour {
	
	public int nFaces;
	public float size;

	Vector3[] vertices;
	int nVertices;
	
	// Use this for initialization
	void Start ()
	{
		CreatePlane();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreatePlane()
	{
		// number of vertices in the terrain
		nVertices = (nFaces + 1) * (nFaces + 1);
		// array holding all the vertices
		vertices = new Vector3[nVertices];
		// U,V positions of the vertices
		Vector2[] uvs = new Vector2[nVertices];
		// triangles in the terrain
		int[] tris = new int[nFaces * nFaces * 6];
		
		float halfSize = size * 0.5f;
        float faceSize = size / nFaces;

        // create new mesh
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        int triOffset = 0;
		
		for (int i = 0; i <= nFaces; i++)
		{
			for (int j = 0; j <= nFaces; j++)
			{
				// initialise vertices and uvs row by row
				vertices[i * (nFaces + 1) + j] = new Vector3(-halfSize + j * faceSize, 0.0f, halfSize - i * faceSize);
				uvs[i * (nFaces + 1) + j] = new Vector2((float) i / nFaces, (float) j / nFaces);

				// check if finished
				if (i < nFaces && j < nFaces)
				{
					// access correct triangle
					int topLeft = i * (nFaces + 1) + j;
					int botLeft = (i + 1) * (nFaces + 1) + j;

					// first triangle
					tris[triOffset] = topLeft;
					tris[triOffset + 1] = topLeft + 1;
					tris[triOffset + 2] = botLeft + 1;

					// second triangle
					tris[triOffset + 3] = topLeft;
					tris[triOffset + 4] = botLeft + 1;
					tris[triOffset + 5] = botLeft;

					// goes to next face because 6 vertices per square
					triOffset += 6;
				}

			}
		}
		
		// get corners
		// top left
		vertices[0].y = transform.position.y;
		// top right
		vertices[nFaces].y = transform.position.y;
		// bottom right
		vertices[vertices.Length - 1].y = transform.position.y;
		// bottom left
		vertices[vertices.Length - 1 - nFaces].y = transform.position.y;

		// number of iterations required
		int iterations = (int)Mathf.Log(nFaces, 2);
		// entire terrain taken initially; one square
		int numSquares = 1;
		// number of squares in each iteration
		int squareSize = nFaces;

		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.triangles = tris;
		
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
	}
}
