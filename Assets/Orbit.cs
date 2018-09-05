// The following code is adapted from Unity Tutorials > Scripting > Quaternions 
// https://unity3d.com/learn/tutorials/topics/scripting/quaternions

using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour
{
    public Transform target;


    void Update()
    {
        Vector3 relativePos = (target.position + new Vector3(0, 1.5f, 0)) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        Quaternion current = transform.localRotation;

        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
        transform.Translate(0, 0, 3 * Time.deltaTime);
    }
}