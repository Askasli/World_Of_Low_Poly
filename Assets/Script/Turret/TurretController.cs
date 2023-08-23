using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TurretController : ITurretControl
{
    private InputHandler inputHandler;
  
    private bool isGunActive = false;
    private ITargetPosition _targetPosition;

    private float rotationSpeed;

    private float minAngle = -17.569f;
    private float maxAngle = 7.607f;


    public TurretController(float turretRotationSpeed, ITargetPosition targetPosition)
    {
        rotationSpeed = turretRotationSpeed;
        _targetPosition = targetPosition;

        inputHandler = new InputHandler();

        inputHandler.OnQPressed += HandleQPressed;
        inputHandler.OnEPressed += HandleEPressed;
    }

    public void RotateTurretTowardsCamera(Transform turretTransform) //Вращение башни танка
    {
        Vector3 targetDirection = Camera.main.transform.position - turretTransform.position;

        targetDirection.y = 0f; 

        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(-targetDirection.normalized, Vector3.up);
            turretTransform.rotation = Quaternion.RotateTowards(turretTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void RotateGunTowardsCamera(Transform gunTransform) 
    {
        inputHandler.HandleInput();

        if (!isGunActive)
            return;

        Vector3 directionToCrosshair = _targetPosition.GetPosition() - gunTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToCrosshair, Vector3.up);
        gunTransform.rotation = Quaternion.RotateTowards(gunTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void HandleQPressed()
    {
        isGunActive = false;
    }

    private void HandleEPressed()
    {
        isGunActive = true;
    }

    private void OnDestroy()
    {
        inputHandler.OnQPressed -= HandleQPressed;
        inputHandler.OnEPressed -= HandleEPressed;
    }


}


