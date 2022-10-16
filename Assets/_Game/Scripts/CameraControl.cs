using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform Target, Camera;
    [SerializeField]Vector3 cameraFollow, clampPos, smoothPos, minValue, maxValue;
    float clampX, clampY, clampZ, zOffset;
    
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
        zOffset = -10;
    }

    public void FollowPlayer()
    {
        CameraClamp();
        clampPos = new Vector3(clampX, clampY, clampZ);
        smoothPos = Vector3.Lerp(Target.position, clampPos, 1);
        Camera.position = smoothPos;
    }

    public void CameraClamp()
    {
        clampX = Mathf.Clamp(Target.position.x, minValue.x, maxValue.x);
        clampY = Mathf.Clamp(Target.position.y, minValue.y, maxValue.y);
        clampZ = Mathf.Clamp(Target.position.z + zOffset, minValue.z, maxValue.z);
    }
}
