using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform Target, Camera;
    Vector3 zOffset, cameraFollow;
    
    void Start()
    {
        OnInit();
    }

    void LateUpdate()
    {
        FollowPlayer();
    }

    private void OnInit()
    {
        zOffset.z = Camera.position.z - Target.position.z;
    }

    public void FollowPlayer()
    {
        if (Target != null)
        {
            cameraFollow.x = Target.position.x;
            cameraFollow.y = Camera.position.y;
            cameraFollow.z = Target.position.z + zOffset.z;

            Camera.position = cameraFollow;
        }
    }
}
