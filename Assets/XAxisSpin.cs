using UnityEngine;
using System.Collections;

public class XAxisSpin : MonoBehaviour {

    public float spinSpeed = 10;

	// Update is called once per frame
	void Update () {
		transform.localRotation *= Quaternion.AngleAxis(Time.deltaTime * spinSpeed, new Vector3(1.0f, 0.0f, 0.0f));
	}
}
