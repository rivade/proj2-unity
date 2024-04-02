using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // LateUpdate is called once per frame after all Update functions
    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
