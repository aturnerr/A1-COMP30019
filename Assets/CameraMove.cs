// some the following code is adapted from Learn Everything Fast on Youtube:
// https://www.youtube.com/channel/UCG5XadFg6icC2TcF0I5DIig

using UnityEngine;

public class CameraMove : MonoBehaviour {

    public float increment;
    public float panSpeed;
    private float yaw;
    private float pitch;

    public float halfSize;

    public DiamondSquare terrain;
    public Rigidbody rb;
    
    

    void Start()
    {
        // use to determine the limits of the terrain
        halfSize = terrain.size * 0.5f;
        // set the initial positions of the camera rigidbody
        rb = GetComponent<Rigidbody>();
        pitch = 20;
        yaw = 45;
        rb.transform.position = new Vector3(-halfSize, rb.position.y, -halfSize);
    }
    
    void Update()
    {
        // retrieve mouse input
        yaw += Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;
        // rotate camera based on mouse
        rb.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
    
    // Update is called once per frame
    void FixedUpdate () {
        // z axis forward
        if (Input.GetKey(KeyCode.W))
        {
            rb.position += transform.forward*increment;
        }
        // z axis backwards
        if (Input.GetKey(KeyCode.S))
        { 
            rb.position -= transform.forward*increment;
        }
        // x axis forwards
        if (Input.GetKey(KeyCode.D))
        {
            rb.position += transform.right*increment;
        }
        // x axis backwards
        if (Input.GetKey(KeyCode.A))
        {
            rb.position -= transform.right*increment;
        }

        // check if within bounds
        if (transform.localPosition.z > halfSize)
        {
            rb.position -= new Vector3(0.0f, 0.0f, increment);
        }
        if (transform.localPosition.z < -halfSize)
        {
            rb.position += new Vector3(0.0f, 0.0f, increment);
        }
        if (transform.localPosition.x > halfSize)
        {
            rb.position -= new Vector3(increment, 0.0f, 0.0f);
        }
        if (transform.localPosition.x < -halfSize)
        {
            rb.position += new Vector3(increment, 0.0f, 0.0f);
        }  
    }
}
