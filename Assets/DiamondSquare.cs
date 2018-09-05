// Code adapted from original creator Ather Omar @
// https://www.youtube.com/watch?v=1HV8GbFnCik



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSquare : MonoBehaviour {

    // number of faces in one row
    public int nFaces;

    // terrain dimensions
    public float size;
    public float height;

    public float minHeight;
    public float maxHeight;

    public Transform water;

    public Renderer rend;

    // vertex array
    Vector3[] vertices;
    // vertex count
    int nVertices;



	// Use this for initialization
	void Start () {

        CreateTerrain();

        minHeight = MinHeight();
        maxHeight = MaxHeight();
        //this.renderer.material.SetFloat("minHeight", minHeight);
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Custom/TerrainShader");
        rend.material.SetFloat("_MIN", minHeight);
        rend.material.SetFloat("_MAX", maxHeight);
        rend.material.SetFloat("_Height1", Mathf.Lerp(minHeight, maxHeight, 0.8f));
	    water.transform.position = new Vector3(water.transform.position.x, Mathf.Lerp(minHeight, maxHeight, 0.5f), water.transform.position.z);
	    //Debug.Log(minHeight);
	    //Debug.Log(maxHeight);
	}

	//
	void CreateTerrain () {

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

        // iterate over vertices
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
        vertices[0].y = Random.Range(-height, height);
        // top right
        vertices[nFaces].y = Random.Range(-height, height);
        // bottom right
        vertices[vertices.Length - 1].y = Random.Range(-height, height);
        // bottom left
        vertices[vertices.Length - 1 - nFaces].y = Random.Range(-height, height);

        // number of iterations required
        int iterations = (int)Mathf.Log(nFaces, 2);
        // entire terrain taken initially; one square
        int numSquares = 1;
        // number of squares in each iteration
        int squareSize = nFaces;


        for (int i = 0; i < iterations; i++)
        {
            int row = 0;
            for (int j = 0; j < numSquares; j++) {

                int col = 0;

                for (int k = 0; k < numSquares; k++)
                {
                    DiamondSquareSteps(row, col, squareSize, height);
                    col += squareSize;
                }
                row += squareSize;
            }
            // terrain is divided in every step
            numSquares *= 2;
            squareSize /= 2;
            // depends how steep of a terrain you want
            height *= 0.5f;
        }

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = tris;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
	}

    void DiamondSquareSteps(int row, int col, int size, float offset)
    {
        int halfSize = (int)(size * 0.5f);

        // two starting corners
        int topLeft = row * (nFaces + 1) + col;
        int botLeft = (row + size) * (nFaces + 1) + col;

        // diamond step
        int mid = (int)(row + halfSize) * (nFaces + 1) + (int)(col + halfSize);
        vertices[mid].y = (vertices[topLeft].y + vertices[topLeft + size].y + vertices[botLeft].y + vertices[botLeft + size].y) * 0.25f + Random.Range(-offset, offset);

        // square step
        vertices[topLeft + halfSize].y = (vertices[topLeft].y + vertices[topLeft + size].y + vertices[mid].y) / 3 + Random.Range(-offset, offset);
        vertices[mid - halfSize].y = (vertices[topLeft].y + vertices[botLeft].y + vertices[mid].y) / 3 + Random.Range(-offset, offset);
        vertices[mid + halfSize].y = (vertices[topLeft + size].y + vertices[botLeft + size].y + vertices[mid].y) / 3 + Random.Range(-offset, offset);
        vertices[botLeft + halfSize].y = (vertices[botLeft].y + vertices[botLeft + size].y + vertices[mid].y) / 3 + Random.Range(-offset, offset);
    }

    // find the minHeight of the diamond square terrain
    public float MinHeight()
    {
        float minHeight = float.MaxValue;

        for (int i = 0; i < nVertices; i++)
        {
            if (vertices[i].y < minHeight) minHeight = vertices[i].y;
        }
        return minHeight;
    }

    public float MaxHeight()
    {
        float maxHeight = float.MinValue;

        for (int i = 0; i < nVertices; i++)
        {
            if (vertices[i].y > maxHeight) maxHeight = vertices[i].y;
        }

        return maxHeight;
    }
}
