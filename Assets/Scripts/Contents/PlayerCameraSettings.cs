using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraSettings : MonoBehaviour
{
    public Vector3 Distance;

    public Transform FollowTarget;
    public Transform LookTarget;

    private void Update()
    {
        transform.position = FollowTarget.position + Distance;
    }
}
