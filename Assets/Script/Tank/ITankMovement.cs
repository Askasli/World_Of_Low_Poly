using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITankMovement
{
    void Move(float verticalInput, Rigidbody rigi, Transform transform);
    void Rotate(float horizonalInput, Rigidbody rigi);
}

