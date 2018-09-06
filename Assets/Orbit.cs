// The following code is adapted from Unity Tutorials > Scripting > Quaternions 
// https://unity3d.com/learn/tutorials/topics/scripting/quaternions

using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour
{
    // public Transform target;
    //public float speed = 10;
    /*
    void Update()
    {
        // vector between object and target, the direction to look in
        Vector3 relativePos = target.position - transform.position;
        // create rotation with given direction to face
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        // the rotation of the transform relative to the parent transform's rotation
        Quaternion current = transform.localRotation;

        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
        // actual movement
        transform.Translate(0, 0, speed * Time.deltaTime);

    }*/

}