using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioner : MonoBehaviour
{

    public static Camera mainCamera;
    public Transform cameraPositionTransform;

    float originalFOV;
    public float fov;
    public bool changeFOV;

    private void Start()
    {
        mainCamera = Camera.main;
        originalFOV = mainCamera.fieldOfView;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainCamera.transform.position = cameraPositionTransform.position;
            mainCamera.transform.rotation = cameraPositionTransform.rotation;

            mainCamera.fieldOfView = changeFOV ? (fov != 0 ? fov : originalFOV) : originalFOV;

        }
    }
}
