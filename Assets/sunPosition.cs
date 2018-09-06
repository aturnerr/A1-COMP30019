using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunPosition : MonoBehaviour {
    
    public Transform sunSphere;
    public Renderer rend;
    
	// Use this for initialization
	void Start () {
	    // set the phong shader script
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Unlit/PhongShader");
        
    }

    // Update is called once per frame
    void Update () {
        // update the property with the current location of the sun object
        rend.material.SetVector("_PointLightPosition", new Vector3(sunSphere.transform.position.x, sunSphere.transform.position.y, sunSphere.transform.position.z));
    }
}
