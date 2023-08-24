using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TankMovement : ITankMovement
{
    private readonly float _maxSpeed;
    private readonly float _moveSpeed;
    private readonly float _rotationSpeed;


    public TankMovement(float maxSpeed, float moveSpeed, float rotationSpeed)
    {
        _maxSpeed = maxSpeed;
        _moveSpeed = moveSpeed;
        _rotationSpeed = rotationSpeed;
    }

    public void Move(float verticalInput, Rigidbody rigi, Transform transform)
    {
        Vector3 movement = transform.forward * verticalInput * _moveSpeed;
        rigi.AddForce(movement, ForceMode.VelocityChange);
        rigi.velocity = Vector3.ClampMagnitude(rigi.velocity, _maxSpeed);
    }

    public void Rotate(float rotationInput, Rigidbody rigi)
    {
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotationInput * _rotationSpeed * Time.deltaTime);
        rigi.MoveRotation(rigi.rotation * deltaRotation);
    }
}
