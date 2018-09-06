// the following code is adapted from Learn Everything Fast on Youtube:
// https://www.youtube.com/watch?v=lYIRm4QEqro

using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public float increment = 0.2f;
    public float panSpeed = 5f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public float halfSize;

    public DiamondSquare terrain;

    // Update is called once per frame
    void Update () {
        
        // use to determine the limits of the terrain
        halfSize = terrain.size * 0.5f;

        // z axis forward
        if (Input.GetKey(KeyCode.W)) {
            if (transform.localPosition.z + increment < halfSize)
            {
                transform.localPosition += new Vector3(0.0f, 0.0f, increment);
            }
            //transform.Translate(new Vector3(increment * Time.deltaTime, 0, 0));
        }
        // z axis backwards
        if (Input.GetKey(KeyCode.S)) {
            if (transform.localPosition.z - increment > -halfSize)
            {
                transform.localPosition -= new Vector3(0.0f, 0.0f, increment);
            }
        }
        // x axis forwards
        if (Input.GetKey(KeyCode.D)) {
            if (transform.localPosition.x + increment < halfSize)
            {
                transform.localPosition += new Vector3(increment, 0.0f, 0.0f);
            }
        }
        // x axis backwards
        if (Input.GetKey(KeyCode.A)) {
            if (transform.localPosition.x - increment > -halfSize)
            {
                transform.localPosition -= new Vector3(increment, 0.0f, 0.0f);
            }
        }

        yaw += panSpeed * Input.GetAxis("Mouse X");
        pitch -= panSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

    }
}
