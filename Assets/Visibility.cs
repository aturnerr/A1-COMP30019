using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour {

    public new Transform gameObject;

    // Use this for initialization
    void Start () {
        this.GetComponent<Renderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        // sphere will be same height as water
        this.transform.localPosition = new Vector3 (this.transform.position.x, gameObject.position.y, this.transform.position.z);
    }
}
