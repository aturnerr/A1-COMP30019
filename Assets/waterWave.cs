using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class waterWave : MonoBehaviour {
	
	public float Scale = 0.03f;
	public float Speed = 0.5f;
	public float NoiseStrength = 1f;
	public float NoiseWalk = 1f;
	private Vector3[] _baseHeight;
	private Mesh _mesh;
	public MeshCollider MC;
	
	// Use this for initialization
	void Start ()
	{
		_mesh = GetComponent<MeshFilter>().mesh;
	}
	
	// Update is called once per frame
	void Update () {
		if (_baseHeight == null)
			_baseHeight = _mesh.vertices;
 
		Vector3[] vertices = new Vector3[_baseHeight.Length];
		for (int i = 0; i < vertices.Length; i++)
		{
			Vector3 vertex = _baseHeight[i];
			vertex.y += Mathf.Sin(Time.time * Speed + _baseHeight[i].x + _baseHeight[i].y + _baseHeight[i].z) * Scale;
			vertex.y += Mathf.PerlinNoise(_baseHeight[i].x + NoiseWalk, _baseHeight[i].y + Mathf.Sin(Time.time * 0.1f)) * NoiseStrength;
			vertices[i] = vertex;
			vertices[i] = vertex;
		}
		_mesh.vertices = vertices;
		_mesh.RecalculateNormals();
 
		MC.sharedMesh = _mesh;
	}
}
