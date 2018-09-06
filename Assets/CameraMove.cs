// some the following code is adapted from Learn Everything Fast on Youtube:
// https://www.youtube.com/channel/UCG5XadFg6icC2TcF0I5DIig

using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public float increment;
    public float panSpeed;
    private float yaw;
    private float pitch;

    public float halfSize;

    public DiamondSquare terrain;

    public Camera myCamera;

    void Start()
    {
        //myCamera = Camera.main;
        //myCamera.enabled = true;
    }

    // Update is called once per frame
    void Update () {
        
        // use to determine the limits of the terrain
        halfSize = terrain.size * 0.5f;

        // z axis forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += myCamera.transform.forward*increment;
        }
        // z axis backwards
        if (Input.GetKey(KeyCode.S))
        { 
            transform.localPosition -= myCamera.transform.forward * increment;
        }
        // x axis forwards
        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += myCamera.transform.right * increment;
        }
        // x axis backwards
        if (Input.GetKey(KeyCode.A))
        {
             transform.localPosition -= myCamera.transform.right * increment;
        }

        // check if within bounds
        if (transform.localPosition.z > halfSize)
        {
            transform.localPosition -= new Vector3(0.0f, 0.0f, increment);
        }
        if (transform.localPosition.z < -halfSize)
        {
            transform.localPosition += new Vector3(0.0f, 0.0f, increment);
        }
        if (transform.localPosition.x > halfSize)
        {
            transform.localPosition -= new Vector3(increment, 0.0f, 0.0f);
        }
        if (transform.localPosition.x < -halfSize)
        {
            transform.localPosition += new Vector3(increment, 0.0f, 0.0f);
        }

        // retrieve mouse input
        yaw += Input.GetAxis("Mouse X") * panSpeed;
        pitch -= Input.GetAxis("Mouse Y") * panSpeed;
        // rotate camera based on mouse
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

    }
}
