using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraControl : MonoBehaviour
{
    private ICameraRotation cameraRotation;

    private Vector3 rotationAmount;
    [SerializeField] private Vector3 targetOffset;

    [Inject]
    private void Construct(ICameraRotation cameraRotation)
    {
        this.cameraRotation = cameraRotation;
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

  
    private void LateUpdate()
    {
        RotateCamera();
    }

    public void RotateCamera()
    {
        cameraRotation.RotateCamera(targetOffset);
    }
}
