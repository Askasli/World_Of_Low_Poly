using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraControl : MonoBehaviour
{
    private ICameraRotation cameraRotation;
    private IMouseInput mouseInput;

    private Vector3 rotationAmount;
    [SerializeField] private Vector3 targetOffset;

    [Inject]
    private void Construct(ICameraRotation cameraRotation, IMouseInput mouseInput)
    {
        this.cameraRotation = cameraRotation;
        this.mouseInput = mouseInput;
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
        cameraRotation.RotateCamera(targetOffset, mouseInput.GetMouseX(), mouseInput.GetMouseY());
    }


}
