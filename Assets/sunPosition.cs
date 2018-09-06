using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunPosition : MonoBehaviour {
    public Transform sunSphere;
    public Renderer rend;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Unlit/PhongShader");
        
    }

    // Update is called once per frame
    void Update () {
        rend.material.SetVector("_PointLightPosition", new Vector3(sunSphere.transform.position.x, sunSphere.transform.position.y, sunSphere.transform.position.z));

    }
}
